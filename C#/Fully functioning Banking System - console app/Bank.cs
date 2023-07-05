using System.Security.Cryptography;

class Bank
{
    private List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public Bank()
    {
    }

    public void AddAccount(Account account)
    {
        this._accounts.Add(account);
    }


    public Account GetAccount(string name)
    {

        foreach (Account account in this._accounts)
        {
            if (name == account.Name)
            {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        this._transactions.Add(transaction);
        transaction.Execute();
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
    }

    public void TransactionHistory()
    {
        Console.WriteLine("\nTransaction History below:");
        int i = 0;
        foreach (Transaction transaction in this._transactions)
        {
            Console.WriteLine($"{i}. Time: {transaction.DateStamp}. Status: {transaction.Success}");
            i++;
        }
    }

    public Transaction FindTransaction(int index)
    {
        return this._transactions[index];
    }
}