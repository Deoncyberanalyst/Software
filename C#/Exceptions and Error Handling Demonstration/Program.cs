class Program
{

    public static void Factorial()
    {
        Factorial();
    }

    static void Demonstrate_ArgumentNullException(string message)
    {
        Console.WriteLine(message.ToUpper());
    }

    static void Demonstrate_ArgumentOutOfRange(int value)
    {
        if (value < 0 || value > 23)
        {
            throw new ArgumentOutOfRangeException("Hour must be between 0-23!");
        }
    }

    static void Demonstrate_FormatException(string message)
    {
        Console.WriteLine(Convert.ToInt32(message));
    }

    static void Demonstrate_ArgumentException(string value)
    {

        int newvalue = 0;
        if (!int.TryParse(value.ToString(), out newvalue))
        {
            throw new ArgumentException("You need to enter a numeral value");
        }

        Console.WriteLine(newvalue);
    }

    static void Demonstrate_SystemException(string message)
    {
        try
        {
            int value = Convert.ToInt32(message);
        }
        catch
        {
            throw new SystemException("Error occured!");
        }
    }

    public static void Main()
    {

        try
        {

            //nullexception
            string text = null;
            Console.WriteLine(text.ToLower());

            // indexoutofrangeexception
            int[] mylist = new int[] { 0, 1, 2, 3, 4, 5 };
            int length = mylist.Length + 1;

            for (int i = 0; i <= length; i++)
            {
                Console.WriteLine(mylist[i] + ", ");
            }

            //StackOverflowException
            Factorial();

            //OutOfMemoryException
            List<int[]> myList = new List<int[]>();

            while (true)
            {
                int[] myArray = new int[100000000];
                myList.Add(myArray);
            }

            //DividebyZeroException
            int number = 10;
            Console.WriteLine(number / 0);

            ////ArgumentNullException
            string message = null;
            Demonstrate_ArgumentNullException(message);

            //ArgumentOutofRange
            Demonstrate_ArgumentOutOfRange(25);

            //FormatException
            Demonstrate_FormatException("Hello5");

            //ArugmentException
            Demonstrate_ArgumentException("Hi");

            //SystemException
            Demonstrate_SystemException("TEST");


            //ArgumentNullException
            string message1 = null;
            Demonstrate_ArgumentNullException(message1);
        }

        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Null reference error: {ex.Message}");
        }

        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Index you've attempted to access is out of bounds. Error msg: {ex.Message}. Error type: {ex.GetType().ToString()}");
        }

        catch (StackOverflowException ex)
        {
            Console.WriteLine($"Error! Stack overflow error. Error msg: {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (OutOfMemoryException ex)
        {
            Console.WriteLine($"Out of RAM. Error: {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"You cant divide by zero.  {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Argument passed to method is null.  {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Argument you provided is out of range to the method/constructor.  {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (FormatException ex)
        {
            Console.WriteLine($"Can't convert object to different type. {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (ArgumentException ex)
        {
            Console.WriteLine($"Argument you provided to method is not valid. {ex.Message}. Type: {ex.GetType().ToString()}");
        }

        catch (SystemException ex)
        {
            Console.WriteLine($"SystemException raised.  {ex.Message}. Type: {ex.GetType().ToString()}");
        }
    }
}

