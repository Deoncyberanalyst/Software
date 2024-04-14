using System;
using System.Xml.Linq;

class Account
{
    //Instance variables
    private double _balance;
    private string _name;


    //A property has two main components: a getter method, which retrieves the value of the property, and a setter method,
    //properties provide a simpler and more intuitive way to encapsulate fields in C# than accessor and mutator methods
    public String Name
    {
        get { return _name; }
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
            //    Console.WriteLine($"You have deposited {amount.ToString("C")}");
            this._balance += amount;
            return true;
        }
        else
        {
            //    Console.WriteLine($"You cannot deposit {amount.ToString("C")}");
            return false;
        }
    }

    public bool withdraw(double amount)
    {
        if (amount <= 0)
        {
            //Console.WriteLine($"You cannot withdraw {amount.ToString("C")}");
            return false;
        }

        else if (amount > this._balance)
        {
            //Console.WriteLine($"You cannot withdraw {amount.ToString("C")}. Your account balance is {this._balance}");
            return false;
        }

        else
        {
            this._balance -= amount;
            //  Console.WriteLine($"You have withdrawn {amount}. Your new account balance is {this._balance}");
            return true;
        }
    }

    public void print()
    {
        Console.WriteLine("User: " + this._name + ". Balance: " + this._balance.ToString("C"));
    }
}