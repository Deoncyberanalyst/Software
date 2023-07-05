abstract class Transaction
{
    protected decimal _amount;
    protected bool _success;
    private bool _executed;
    private bool _reversed;
    private DateTime _dateStamp;


    public bool Success
    { get { return this._success; } }

    public bool Executed
    { get {return this._executed; } }

    public bool Reversed
    { get { return this._reversed; } }

    public DateTime DateStamp
    {
        get {return this._dateStamp; }
    }

    public Transaction(decimal amount)
    {
        Console.WriteLine("");
        this._amount = amount;

    }

    public abstract void Print();
    

    public virtual void Execute()
    {
        this._executed = true;
        this._dateStamp = DateTime.Now;
        this._success = true;
    }

    public virtual void Rollback()
    {
        if (this.Success == true && this.Reversed == false)
        {
            this._success = false;
            this._reversed = true;
            this._dateStamp = DateTime.Now;
        }
    }
}
