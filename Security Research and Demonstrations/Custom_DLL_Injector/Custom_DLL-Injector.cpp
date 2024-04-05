#include <iostream>
#include <Windows.h>
#include <tlhelp32.h>
#include <tchar.h>
#include <memoryapi.h>
using namespace std;

static DWORD getProcess(const char* processName)
{
    PROCESSENTRY32 procEntry;
    HANDLE snapshot;
   
    snapshot  = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (snapshot != INVALID_HANDLE_VALUE) 
    {
        cout << "Snapshot created successfully\n";
        cout << "Looking for process: " << processName << endl;

        if (Process32First(snapshot, &procEntry)) {
            do {
                if (!_stricmp(procEntry.szExeFile, processName)) {
                    return procEntry.th32ProcessID;
                }
            } while (Process32Next(snapshot, &procEntry));
        } else cout << "Failed to get process list\n";
    } else cout << "Failed to create snapshot\n";

     return -1;
}

static void openTargetProcess(DWORD targetProcessID, const char* data)
{
    HANDLE processHandle;
    LPVOID baseAddress;

    processHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, targetProcessID);
    if (processHandle != NULL) {
		cout << "Process opened successfully\n";

         baseAddress = VirtualAllocEx(processHandle, 0, sizeof(data), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
        if (baseAddress != NULL) {
            cout << "Memory allocated successfully\n";

            BOOL result = WriteProcessMemory(processHandle, baseAddress, data, sizeof(data), 0);
            if (result != 0) {
                cout << "Memory written successfully\n";

            } else cout << "Failed to write memory\n";
        } else cout << "Failed to allocate memory\n";
	} else cout << "Failed to open process\n";

    CloseHandle(processHandle);
}



int main()
{
    const char* processName = "notepad++.exe";
    const char* data = "ENTER DLL";
    DWORD targetProcessID;

    do {
		targetProcessID = getProcess(processName);
		if (targetProcessID == -1) {
			cout << "Process not found\n";
			Sleep(10000);
		}
	} while (targetProcessID == -1);

    cout << "Found process ID: " << targetProcessID << endl;
    openTargetProcess(targetProcessID, data);

    cout << "Error code: " << GetLastError() << endl;
    return 0;
}

