public class Account : IComparable<Account>
{

    private double _balance;
    private string _name;

    public decimal Balance
    { get { return Convert.ToDecimal(_balance); } }


    public String Name
    {
        get { return _name; }
    }

    public int CompareTo(Account acc)
    {
        if (acc == null ) return 1; 
    
        int retrunV = this.Balance.CompareTo(acc.Balance);

        if (retrunV == 0) return 0;

        return retrunV;
    }

    public Account(string name, double balance)
    {
        this._balance = balance;
        this._name = name;
    }

    public bool deposit(double amount)
    {
        if (amount > 0)
        {
            this._balance += amount;
            return true;
        }
        else

        {
            return false;
        }
    }

    public bool withdraw(double amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        else if (amount > this._balance)
        {
            return false;
        }

        else
        {
            this._balance -= amount;
            return true;
        }
    }

    public void print()
    {
        Console.WriteLine("User:" + this._name + "\nBalance: " +
        this._balance.ToString("C"));
    }

}

