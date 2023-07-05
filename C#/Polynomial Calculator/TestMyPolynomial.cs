class TestMyPolynomial
{

    static void Main()
    {
        double[] myArray0 = new double[] {11,-4,3};
        MyPolynomial newPoly0 = new MyPolynomial(myArray0);

        Console.WriteLine(newPoly0.ToString());
        Console.WriteLine(newPoly0.GetDegree());
        Console.WriteLine(newPoly0.Evaluate(5));

        double[] myArray = new double[] { 11, -4, 3, 1 };
        double[] myArray2 = new double[] { 11, -4, 3, 1 };

        MyPolynomial newPoly = new MyPolynomial(myArray);
        MyPolynomial newPoly2 = new MyPolynomial(myArray2);

        MyPolynomial newPoly3 = newPoly.Add(newPoly);
        MyPolynomial newPoly4 = newPoly.Multiply(newPoly2);

        Console.WriteLine(newPoly3.ToString());
        Console.WriteLine(newPoly4.ToString());
    }
}
