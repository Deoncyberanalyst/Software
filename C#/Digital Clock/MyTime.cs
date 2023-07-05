
using System.Security.Cryptography.X509Certificates;

class MyTime
{
    //Instance variables
    private int hour;
    private int minute;
    private int second;

    public MyTime()
    {
        this.hour = 0;
        this.minute = 0;
        this.second = 0;
    }

    public MyTime(int hour, int minute, int second)
    {

        if (hour < 0 || hour > 23)
        {
            throw new ArgumentException("Hour must be between 0-23!");
        }

        else if (minute < 0 || minute > 59)
        {
            throw new ArgumentException("Minutes must be between 0-59");
        }

        else if (second < 0 || second > 59)
        {
            throw new ArgumentException("Seconds must be between 0-59");
        }

        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }

    public void SetTime(int hour, int minute, int second)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentException("Hour must be between 0-23!");
        }

        else if (minute < 0 || minute > 59)
        {
            throw new ArgumentException("Minutes must be between 0-59");
        }

        else if (second < 0 || second > 59)
        {
            throw new ArgumentException("Seconds must be between 0-59");
        }

        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }


    public void SetHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentException("Hour must be between 0-23!");
        }
        this.hour = hour;
    }

    public void SetMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentException("Minutes must be between 0-59");
        }
        this.minute = minute;
    }

    public void SetSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentException("Minutes must be between 0-59");
        }
        this.second = second;
    }

    public int GetHour()
    {
        return this.hour;
    }

    public int GetMinute()
    {
        return this.minute;
    }

    public int GetSecond()
    {
        return this.second;
    }

    public override string ToString()
    {
        string time;
        time = this.hour.ToString("D2");
        time += ":";
        time += this.minute.ToString("D2");
        time += ":";
        time += this.second.ToString("D2");
        return time;
    }

    public MyTime NextSecond()
    {
        this.second++;
        //59+1=60

        if (this.second > 59) //true
        {
            this.second = 0; 
            this.minute++;//59+1 =60

            if (this.minute > 59) //==true
            {
                this.minute = 0;
                this.hour++;

                if (this.hour > 23)
                {
                    this.hour = 0;
                }
            }
        }
        return this;
    }


    public MyTime NextMinute()
    {
        this.minute++;

        if ( this.minute > 59)
        {
            this.minute = 0;
            this.hour++;

            if (this.hour > 23)
            {
                this.hour = 0;
            }
        }
        return this;
    }

    public MyTime NextHour()
    {
        this.hour++;

        if ( this.hour > 23)
        {
            this.hour= 0;
        }
        return this;
    }


    public MyTime PreviousSecond()
    {
        this.second--;

        if (this.second < 0)
        {
            this.second = 59;
            this.minute--;

            if (this.minute < 0)
            {
                minute = 59;
                this.hour--;

                if (this.hour < 0)
                {
                    hour = 23;
                }
            }
        }
        return this;
    }

    public MyTime PreviousMinute()
    {
        this.minute--;

        if ( this.minute < 0)
        {
            this.minute= 59;
            this.hour--;

            if(this.hour < 0)
            {
                hour = 23;
            }
        }
        return this;
    }

    public MyTime PreviousHour()
    {
        this.hour--;
        
        if (this.hour < 0)
        {
            hour = 23;
        }
        return this;
    }
}

