using System.Security.Cryptography.X509Certificates;

class withdrawTransaction : Transaction
{
    //Instance variables
    private Account _account;


    public withdrawTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }

    public override void Print()
    {
        if (base.Executed == true)
        {
            Console.WriteLine($"{this._account.Name} withdrew: ${base._amount} at {base.DateStamp}");
        }
    }

    public override void Execute()
    {
        bool status;

        if (base.Executed == false)
        {
            base.Execute();
            status = this._account.withdraw(decimal.ToDouble(base._amount));

            if (status == true)
            {
                this.Print();
            }

            else
            {
                Console.WriteLine($"Insufficient funds in {this._account.Name} or  attempting to withdraw $0 or lower");
                throw new InvalidOperationException();

            }
        }

        else
        {
            Console.WriteLine("Withdraw already attempted");
            throw new InvalidOperationException();
        }


    }

    public override void Rollback()
    {

        if (base.Reversed != true)
        {
            base.Rollback();
            this._account.deposit(decimal.ToDouble(base._amount));
        }
        else
        {
            Console.WriteLine("Withdrawal not executed or has already been reversed");
            throw new InvalidOperationException();
            
        }
    }

}