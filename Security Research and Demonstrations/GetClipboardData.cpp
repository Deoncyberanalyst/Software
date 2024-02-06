/*
This C code attempts to interact with the Windows Clipboard, initially printing a message indicating an attempt to open it. 
If successful, it retrieves text data from the Clipboard and prints it. The code then calls a function named sendHTTP with the retrieved data as an argument, 
although the HTTP sending functionality is not  implemented. 
Overall, the code is a demonstration of interacting with the Windows Clipboard in C and intending to send the retrieved data over HTTP.
*/

#include <stdio.h>
#include <windows.h>

const char EVNT[] = "[*]";
const char SUCC[] = "[+] ";
const char FAIL[] = "[-] ";

void sendHTTP(char *str)
{
    //Implement a function that sends clipboard data using HTTP
    char* data = static_cast<char*>(str);

    printf("%s Inside SendHTTP function\n%s\n", EVNT, data);
   
}

int main() {

    printf("%sAttempting to open Clipboard...\n", SUCC);

    if(OpenClipboard(NULL))
    {
        printf("%sSuccessfully opened Clipboard...\n",SUCC);
        //Retrieves data from the clipboard in a specified format. The clipboard must have been opened previously.
        HANDLE hData = GetClipboardData(CF_TEXT);
        printf("%sMemory address of data: %p\n",SUCC, hData);
        printf("%sType casting\n", SUCC);
        char *data = static_cast<char*>(hData);
   
        printf("%sClipboard data is: %s\n",EVNT, data);

        sendHTTP(data);
    } else std::cout << "[-]Failed to open Clipboard\n";

    
    getchar();

    return 0;
}
