using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;

public class Tester
{

    public static void Main()
    {
        string[] namesList = new string[30] { "Ethan Smith", "Olivia Johnson", "Liam Williams", "Ava Brown", "Noah Jones", "Isabella Miller", "Lucas Davis", "Sophia Wilson", "Mason Taylor", "Mia Anderson", "Oliver Thomas", "Charlotte Martinez", "Elijah Moore", "Amelia Jackson", "Aiden Thompson", "Harper White", "James Clark", "Evelyn Hall", "Benjamin Hill", "Abigail Turner", "Lucas Phillips", "Emily Bennett", "Alexander Reed", "Elizabeth Murphy", "Samuel Rivera", "Sofia Cooper", "Henry Richardson", "Avery Cox", "Jackson Ward", "Ella Peterson" };
        double[] balanceList = new double[30] { 2183, 131, -7167, 9024, 7971, 6915, 6937, 9273, 9408, 5109, 4661, -1554, -2824, 8818, 6110, -4511, 7238, 22, 4745, 9264, 3518, 8334, -8229, 1349, 6664, -9925, 4816, 2878, -4258, 2608 };
        MyStack<Account> bank = new MyStack<Account>(30);
        for (int i = 0; i < 30;  i++) bank.Push(new Account(namesList[i], balanceList[i]));

        decimal search = 0;
        Func<Account, bool> account_balance = item => (item.Balance > search);
        Func<Account, bool> account_balance2 = item => ( (item.Balance < search) & (item.Balance > 0));
        Func<Account, bool> account_balance3 = item => ((item.Balance < 0));


        //Find: Account exists
        search = 6000;
        Account acc = bank.Find(account_balance);
        Console.WriteLine($"Account name: {acc.Name}. Balance: {acc.Balance.ToString("C")}");
        Console.WriteLine("");

        //Find: No account
        search = 90_000;
        Account acc2 = bank.Find(account_balance);
        if(acc2 == null) Console.WriteLine($"No Account with that {search.ToString("C")} exists");
        Console.WriteLine("");

        //Find: Account exists
        search = 100;
        Account acc3 = bank.Find(account_balance2);
        Console.WriteLine($"Account name: {acc3.Name}. Balance: {acc3.Balance.ToString("C")}");
        Console.WriteLine("");

        //FindAll: Account exists
        search = 100;
        Account[] acc4 = bank.FindAll(account_balance3);
        int x = 0;
        foreach (Account item in acc4)
        {
            Console.WriteLine($"Account name: {acc4[x].Name}. Balance: {acc4[x].Balance.ToString("C")}");
            x++;
        }
        Console.WriteLine("");


        //Find specific accounts
        Func<Account, bool> account_search = item => (item.Name[0] == 'E') & (item.Balance > 300);
        Account[] acc5 = bank.FindAll(account_search);
        x = 0;
        foreach (Account item in acc5)
        {
            Console.WriteLine($"Account name: {acc5[x].Name}. Balance: {acc5[x].Balance.ToString("C")}");
            x++;
        }
        Console.WriteLine("");


        //Find specific accounts
        Func<Account, bool> account_search2 = item => (item.Name[0] == 'A');
        Account[] acc6 = bank.FindAll(account_search2);
        x = 0;
        foreach (Account item in acc6)
        {
            Console.WriteLine($"Account name: {acc6[x].Name}. Balance: {acc6[x].Balance.ToString("C")}");
            x++;
        }
        Console.WriteLine("");


      //  Remove All
        Func<Account, bool> account_search3 = item => (item.Balance < 0);
        int acc7 = bank.RemoveAll(account_search3);
        Console.WriteLine($"Removed accounts: {acc7}");
        Console.WriteLine("");


        ////Find null
        Account acc8 = bank.Find(null);
        Console.WriteLine("");

        ////FindAll null
        Account[] acc9 = bank.FindAll(null);
        Console.WriteLine("");


        //RemoveAll null
        int acc10 = bank.RemoveAll(null);
        Console.WriteLine("");


        //Min
        Account acc12 = bank.Min();
        Console.WriteLine($"Account name: {acc12.Name}. Balance: {acc12.Balance.ToString("C")}");
        Console.WriteLine("");

        //Max
        Account acc11 = bank.Max();
        Console.WriteLine($"Account name: {acc11.Name}. Balance: {acc11.Balance.ToString("C")}");
        Console.WriteLine("");
    }
}


