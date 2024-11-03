#include <windows.h>
#include <iostream>

bool IsDebuggerPresent()
{
    BOOL isDebuggerPresent = FALSE;

    HANDLE hProcess = GetCurrentProcess();

    // Check if there is a debugger attached
    if (CheckRemoteDebuggerPresent(hProcess, &isDebuggerPresent))
    {
        return isDebuggerPresent;
    }
    else
    {
        // Handle the error (e.g., function call failed)
        std::cerr << "Failed to check debugger presence." << std::endl;
        return false;
    }
}

int main()
{
    if (IsDebuggerPresent())
    {
        std::cout << "Debugger detected!" << std::endl;
    }
    else
    {
        std::cout << "No debugger detected." << std::endl;
    }

    return 0;
}
