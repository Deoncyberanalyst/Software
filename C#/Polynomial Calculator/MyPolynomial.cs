using System;
using System.Globalization;
using System.Linq;
class MyPolynomial
{

    
    //Instance variable
    private double[] _coeffs;

    public MyPolynomial(double[] coeffs)
    {
       this._coeffs = new double[coeffs.Length];
       for (int i = 0; i < coeffs.Length; i++)
        {
            this._coeffs[i] = coeffs[i];
        }
    }

    public int GetDegree()
    {
        int length = (this._coeffs.Length - 1);
        return length;
    }

    public double Evaluate(double x)
    {
        double final = 0;
        double power = 1;

        foreach (double coefficient in this._coeffs)
        {
            final += coefficient * power;
            power *= x;
        }
        return final;
    }


    public override string ToString()
    {
        //intialise the string
        string result = "";
        //work backwards
        for (int i = this.GetDegree(); i >= 0; i--)
        {
            if (this._coeffs[i] == 0.0)
            {
                continue;
            }

            //both must equals true
            if (this._coeffs[i] > 0 && result != "") //dont want to add a + to the start
            {
                result += " + ";
            }

           
            //if coefficient is miun, print a negative sign
            if (this._coeffs[i] < 0)
            {
                result += " - ";
            }

            //OR == one condition must be true
            //runs on the first iteration
            //no point putting 1, to adhere to normal polynormial form
            if (i == 0 || Math.Abs(this._coeffs[i]) != 1)
            {
                result += Math.Abs(this._coeffs[i]).ToString();
            }
            
            //if x is greater (0 and 1) then, add the x and exponent symbol.
            if (i > 0)
            {
                result += "x";
            }
            if (i > 1)
            {
                result += "^" + i.ToString();
            }
        }
        return result;
    }


    public MyPolynomial Add (MyPolynomial another)
    {
        int maximumLength = Math.Max(this._coeffs.Length, another._coeffs.Length);
        int minimumLength = Math.Min(this._coeffs.Length, another._coeffs.Length);

        double[] newCoeffs = new double[maximumLength];

        for (int i = 0; i < minimumLength; i++)
        {
            newCoeffs[i] = another._coeffs[i] + this._coeffs[i];
        }

  
        if (this._coeffs.Length > another._coeffs.Length)
        {
            for (int i = another._coeffs.Length; i < this._coeffs.Length; i++)
            {
                newCoeffs[i] = this._coeffs[i];
            }
        }

        if (another._coeffs.Length > this._coeffs.Length)
        {
            for (int i = this._coeffs.Length; i < another._coeffs.Length; i++)
            {
                newCoeffs[i] = another._coeffs[i];
            }
            }

        return new MyPolynomial(newCoeffs);
    }

    public MyPolynomial Multiply(MyPolynomial another)
    {
        int maximumLength = Math.Max(this._coeffs.Length, another._coeffs.Length);
        int minimumLength = Math.Min(this._coeffs.Length, another._coeffs.Length);

        double[] newCoeffs = new double[maximumLength];

        for (int i = 0; i < minimumLength; i++)
        {
            newCoeffs[i] = another._coeffs[i] * this._coeffs[i];
        }

        return new MyPolynomial(newCoeffs);
    }
}