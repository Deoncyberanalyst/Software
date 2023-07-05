using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestMyClass
{

    static void Main()
    {
        Console.WriteLine("Test MyTime()");
        MyTime todayTime = new MyTime();
        Console.WriteLine($"Hour: {todayTime.GetHour()}");
        Console.WriteLine($"Minute: {todayTime.GetMinute()}");
        Console.WriteLine($"Second: {todayTime.GetSecond()}");


        Console.WriteLine();
        Console.WriteLine("Testing MyTime Constructor, SetHour, SetMinute, SetSecond, GetHour, GetMinute, GetSecond");
        todayTime = new MyTime(23,59,59);
        todayTime.SetHour(20);
        todayTime.SetMinute(50);
        todayTime.SetSecond(51);

        Console.WriteLine($"Hour: {todayTime.GetHour()}");
        Console.WriteLine($"Minute: {todayTime.GetMinute()}");
        Console.WriteLine($"Second: {todayTime.GetSecond()}");


        Console.WriteLine();
        Console.WriteLine("Testing ToString()");
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing NextSecond");
        todayTime.NextSecond();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine( "Testing NextMinute");
        todayTime.NextMinute();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing NetHour");
        todayTime.NextHour();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing PreviousSecond");
        todayTime.PreviousSecond();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing PreviousHour");
         todayTime.PreviousMinute();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing PreviousHour");
        todayTime.PreviousHour();
        Console.WriteLine($"Time of day {todayTime.ToString()}");



        Console.WriteLine();
        Console.WriteLine("Testing NextSecond");
        todayTime.SetHour(23);
        todayTime.SetMinute(59);
        todayTime.SetSecond(59);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.NextSecond();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing NextMinute");
        todayTime.SetHour(23);
        todayTime.SetMinute(59);
        todayTime.SetSecond(59);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.NextMinute();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing NextHour");
        todayTime.SetHour(23);
        todayTime.SetMinute(59);
        todayTime.SetSecond(59);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.NextHour();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing PreviousSecond");
        todayTime.SetHour(0);
        todayTime.SetMinute(0);
        todayTime.SetSecond(0);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.PreviousSecond();
        Console.WriteLine($"Time of day {todayTime.ToString()}");

        Console.WriteLine();
        Console.WriteLine("Testing PreviousMinute");
        todayTime.SetHour(0);
        todayTime.SetMinute(0);
        todayTime.SetSecond(0);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.PreviousMinute();
        Console.WriteLine($"Time of day {todayTime.ToString()}");

        Console.WriteLine();
        Console.WriteLine("Testing PreviousHour");
        todayTime.SetHour(0);
        todayTime.SetMinute(0);
        todayTime.SetSecond(0);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.PreviousHour();
        Console.WriteLine($"Time of day {todayTime.ToString()}");


        Console.WriteLine();
        Console.WriteLine("Testing PreviousMinute");
        todayTime.SetHour(0);
        todayTime.SetMinute(30);
        todayTime.SetSecond(0);
        Console.WriteLine($"Time of day {todayTime.ToString()}");
        todayTime.PreviousMinute();
        Console.WriteLine($"Time of day {todayTime.ToString()}");
    }


}
