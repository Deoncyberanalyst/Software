
using System.Security.Principal;
using System.Xml.Linq;

class BankSystem
{
    enum MenuOptions
    {
        AddNewAccount,
        Withdraw,
        Deposit,
        Transfer,
        Print,
        PrintTransactions,
        Quit
    }

    static MenuOptions ReadUserOption()
    {
        int option;
        do
        {
            Console.WriteLine("\n**MENU**");
            Console.WriteLine($"0. {MenuOptions.AddNewAccount}");
            Console.WriteLine($"1. {MenuOptions.Withdraw}");
            Console.WriteLine($"2. {MenuOptions.Deposit}");
            Console.WriteLine($"3. {MenuOptions.Transfer}");
            Console.WriteLine($"4. {MenuOptions.Print}");
            Console.WriteLine($"5. {MenuOptions.PrintTransactions}");
            Console.WriteLine($"6. {MenuOptions.Quit}");
            Console.Write("Enter Option:\t");
            option = Convert.ToInt32(Console.ReadLine());

        } while (!(option >= 0 && option <= 6));

        if (option == 0)
        {
            return MenuOptions.AddNewAccount;
            
        }

        else if (option == 1)
        {
            return MenuOptions.Withdraw;
            
        }

        else if (option == 2)
        {
            return MenuOptions.Deposit;
            
        }

        else if (option == 3)
        {
            return MenuOptions.Transfer;
        }


        else if (option == 4)
        {
            return MenuOptions.Print;
        }

        else if (option == 5)
        {
            return MenuOptions.PrintTransactions;
        }

        else 
        {
            return MenuOptions.Quit;
        }
    }


    static void DoDeposit(Bank bank)
    {
        decimal amount;
        string confirm;

        Account selectedUser = FindAccount(bank);

        if (selectedUser != null)
        {
            Console.WriteLine("How much would you like to deposit:");
            amount = Convert.ToDecimal(Console.ReadLine());

            DepositTransaction newTransaction = new DepositTransaction(selectedUser, amount);

            Console.Write($"Are you sure you wish to deposit ${amount}? (y/n)?");
            confirm = Console.ReadLine();

            if (confirm == "y")
            {
                bank.ExecuteTransaction(newTransaction);
            }
        }

        SelectMenu(bank);
    }


    static void DoWithdraw(Bank bank)
    {
        decimal amount;
        string confirm;

        Account selectedUser = FindAccount(bank);


        Console.WriteLine("How much would you like to withdraw:");
        amount = Convert.ToDecimal(Console.ReadLine());

        withdrawTransaction newWithdrawal = new withdrawTransaction(selectedUser, amount);

        Console.Write($"Are you sure you wish to withdraw ${amount}? (y/n)?");
        confirm = Console.ReadLine();

        if (confirm == "y")
        {
            bank.ExecuteTransaction(newWithdrawal);
        }

        SelectMenu(bank);
    }

    static void DoTransfer(Bank bank)
    {
        Account fromAccount;
        Account toAccount;
        string confirm = "";
        string rollBack = "";
        decimal amount = 0;


        do
        {
            Console.WriteLine("Select payer and Payee below.");
            Console.Write("\nSelect payer: ");
            fromAccount = FindAccount(bank);

            Console.Write("Select payee: ");
            toAccount = FindAccount(bank);

        } while (fromAccount == null || toAccount == null);
            

            Console.WriteLine("How much would you like to transfer: ");
            amount = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine(
                $"Transfer {amount.ToString("C")}, from {fromAccount.Name}, to {toAccount.Name} (y/n)?");
            confirm = Console.ReadLine();


            if (fromAccount == toAccount)
            {

                Console.WriteLine("\nInvalid transfer configurations! Try again.");
            }
            else
            {
                if (confirm == "y")
                {
                    TransferTransaction newTransfer = new TransferTransaction(fromAccount, toAccount, amount);
                    bank.ExecuteTransaction(newTransfer);
                }
            }

    SelectMenu(bank);
    }

    static void DoPrint(Bank bank)
    {
        Account selectedUser = FindAccount(bank);
        if (selectedUser != null)
        {
            selectedUser.print();
            SelectMenu(bank);
        }

        SelectMenu(bank);

    }

    static void DoPrintTransactions(Bank bank)
    {
        int index;
        string confirm;

        bank.TransactionHistory();

        Console.WriteLine("\nDo you wish to rollback any transactions (y/n)?");
        confirm = Console.ReadLine();

        if (confirm == "y")
        {
            Console.WriteLine("Enter index of transcation you wish to rollback.");
            index = Convert.ToInt32(Console.ReadLine());

            bank.RollbackTransaction(bank.FindTransaction(index));
        }

        SelectMenu(bank);
    }

    static void DoAddNewAccount(Bank bank)
    {
        string name;
        double balance;

        Console.WriteLine("Enter name of new account: ");
        name = Console.ReadLine();

        Console.WriteLine($"Enter balance of {name}: ");
        balance = Convert.ToDouble((Console.ReadLine()));
        Account newAccount = new Account(name, balance);
        bank.AddAccount(newAccount);

        SelectMenu(bank);
    }

    private static Account FindAccount(Bank bank)
    {
        string accountName;
        Account newAccount;

        Console.WriteLine("Search account: ");
        accountName = Console.ReadLine();

        newAccount = bank.GetAccount(accountName);

        if (newAccount == null)
        {
            Console.WriteLine("No account found.");
        }

        return newAccount;
    }


    static void Main()
    {
        Bank bank = new Bank();
        SelectMenu(bank);
    }


    static void SelectMenu(Bank bank) { 

    int selection;
        string confirm;
        do
        {
            selection = Convert.ToInt32(ReadUserOption());
            Console.Write($"Are you sure you wish to {Enum.GetName(typeof(MenuOptions), selection)}(y/n)?");
            confirm = Console.ReadLine();
        } while (confirm != "y");

        switch (selection)
        {
            case 0:
                DoAddNewAccount(bank);
                break;

            case 1:
                DoWithdraw(bank);
                break;

            case 2:
                DoDeposit(bank);
                break;

            case 3:
                DoTransfer(bank);
                break;

            case 4:
                DoPrint(bank);
                break;

            case 5:
                DoPrintTransactions(bank);
                break;

            case 6:
                break;
        }
    }
    
}