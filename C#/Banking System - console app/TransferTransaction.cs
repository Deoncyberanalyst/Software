using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class TransferTransaction : Transaction
{
    //Instance variables
    private Account _fromAccount;
    private Account _toAccount;
    private decimal _amount;

    private DepositTransaction _deposit;
    private withdrawTransaction _withdraw;


    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;

        this._deposit = new DepositTransaction(toAccount, amount);
        this._withdraw = new withdrawTransaction(fromAccount, amount);
    }

    
    public bool Success
    {
        get
        {
            if (this._deposit.Success == true && this._withdraw.Success == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }


    public override void Print()
    {
        if (Executed == true)
        {
            Console.WriteLine($"\nTransferred ${base._amount} from {this._fromAccount.Name} to {this._toAccount.Name} at {base.DateStamp}");
            this._fromAccount.print();
            this._toAccount.print();
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Error at TransferTranscation");
        }
    }

    public override void Execute()
    {
        if (base.Success == false)
        {
            base.Execute();
            this._withdraw.Execute();
            this._deposit.Execute();
            this.Print();
        }

        else
        {
            Console.WriteLine("Transaction already attempted");
            throw new InvalidOperationException();
        }
    }

    public override void Rollback()
    {
        if (base.Executed == true)
        {
            base.Rollback();
            this._deposit.Rollback();
            this._withdraw.Rollback();
            this.Print();
        }

    }

}