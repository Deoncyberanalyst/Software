using System.Security.Cryptography.X509Certificates;

class DepositTransaction : Transaction
{
    //Instance variables
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }

    public override void Print()
    {
        if (base.Executed == true)
        {
            Console.WriteLine($"{this._account.Name } deposited: ${base._amount} at {base.DateStamp}");
        }
    }

    public override void Execute()
    {
        
        bool status;

        if (base.Executed == false)
        {
            base.Execute();
            status = this._account.deposit(decimal.ToDouble(base._amount));
            base._success = true;    
            if (status == true)
            {
                this.Print();
            }

            else
            {
                Console.WriteLine($"Deposit rejected. {this._account.Name} Ensure you're depositing more than $0");
                throw new InvalidOperationException();

            }
        }

        else
        {
            Console.WriteLine("Transaction already attempted");
            throw new InvalidOperationException();
        }
    }

    public void Rollback()
    {

        if (base.Reversed != true)
        {
            base.Rollback();
            this._account.withdraw(decimal.ToDouble(base._amount));
        }
        else
        {
            Console.WriteLine("Deposit not executed or has already been reversed");
            throw new InvalidOperationException();

        }
    }

}