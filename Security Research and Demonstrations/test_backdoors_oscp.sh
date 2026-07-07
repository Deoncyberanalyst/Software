#!/bin/bash
# =============================================
# OSCP-style Backdoor Tester with netcat
# Supports IP:PORT format + Aggregate Summary
# =============================================

TARGET_PORTS_FILE="$1"
TIMEOUT=8
OUTPUT_FILE="backdoor_test_$(date +%F_%H%M).log"

# Counters for summary
TOTAL=0
POSSIBLE_BACKDOORS=0
TIMEOUTS=0
NORMAL=0

declare -a BACKDOOR_LIST=()

if [ ! -f "$TARGET_PORTS_FILE" ]; then
    echo "[-] Target file $TARGET_PORTS_FILE not found!"
    echo "    Format: IP:PORT (one per line)"
    exit 1
fi

echo "[+] Starting backdoor test"
echo "[+] Input file : $TARGET_PORTS_FILE"
echo "[+] Log file   : $OUTPUT_FILE"
echo "==================================================" | tee -a "$OUTPUT_FILE"

while IFS= read -r line || [ -n "$line" ]; do
    [[ -z "$line" || "$line" =~ ^# ]] && continue
    
    entry=$(echo "$line" | tr -d '[:space:]')
    TARGET=$(echo "$entry" | cut -d':' -f1)
    PORT=$(echo "$entry" | cut -d':' -f2-)
    
    if [[ -z "$TARGET" || -z "$PORT" || ! "$PORT" =~ ^[0-9]+$ ]]; then
        echo "[-] Invalid entry: $line" | tee -a "$OUTPUT_FILE"
        continue
    fi

    ((TOTAL++))
    echo "[*] Testing $TARGET:$PORT" | tee -a "$OUTPUT_FILE"
    
    result=$(timeout "$TIMEOUT" bash -c "
        {
            echo 'whoami'
            sleep 1
            echo 'id'
            sleep 1
            echo 'uname -a'
            sleep 1
            echo 'exit'
        } | nc -w $TIMEOUT -v $TARGET $PORT 2>&1
    ")
    
    if [ $? -eq 124 ]; then
        echo "   [-] Timeout on $TARGET:$PORT" | tee -a "$OUTPUT_FILE"
        ((TIMEOUTS++))
    elif echo "$result" | grep -qE "whoami|uid=|Linux|root|shell|bash"; then
        echo "   [+] POSSIBLE BACKDOOR on $TARGET:$PORT" | tee -a "$OUTPUT_FILE"
        echo "   --------------------------------------------------" | tee -a "$OUTPUT_FILE"
        echo "$result" | tee -a "$OUTPUT_FILE"
        echo "   --------------------------------------------------" | tee -a "$OUTPUT_FILE"
        
        ((POSSIBLE_BACKDOORS++))
        BACKDOOR_LIST+=("$TARGET:$PORT")
    else
        echo "   [-] No obvious shell on $TARGET:$PORT" | tee -a "$OUTPUT_FILE"
        echo "$result" | head -n 12 | tee -a "$OUTPUT_FILE"
        ((NORMAL++))
    fi
    
    echo "--------------------------------------------------" | tee -a "$OUTPUT_FILE"
done < "$TARGET_PORTS_FILE"

# ====================== AGGREGATE RESULTS ======================
echo
echo "==================================================" | tee -a "$OUTPUT_FILE"
echo "                 AGGREGATE RESULTS                 " | tee -a "$OUTPUT_FILE"
echo "==================================================" | tee -a "$OUTPUT_FILE"
echo "Total ports tested     : $TOTAL" | tee -a "$OUTPUT_FILE"
echo "Possible Backdoors     : $POSSIBLE_BACKDOORS" | tee -a "$OUTPUT_FILE"
echo "Timeouts               : $TIMEOUTS" | tee -a "$OUTPUT_FILE"
echo "Normal services        : $NORMAL" | tee -a "$OUTPUT_FILE"
echo "==================================================" | tee -a "$OUTPUT_FILE"

if [ $POSSIBLE_BACKDOORS -gt 0 ]; then
    echo "Possible Backdoors found:" | tee -a "$OUTPUT_FILE"
    for bd in "${BACKDOOR_LIST[@]}"; do
        echo "   [+] $bd" | tee -a "$OUTPUT_FILE"
    done
else
    echo "No obvious backdoors were found." | tee -a "$OUTPUT_FILE"
fi

echo "==================================================" | tee -a "$OUTPUT_FILE"
echo "[+] Full log saved to: $OUTPUT_FILE"
echo

# Optional: Show full log at the end
# less "$OUTPUT_FILE"
