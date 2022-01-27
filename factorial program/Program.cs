using System;

namespace factorial_program
{
    class Program
    {
        static void Main(string[] args)
        {
            int i=null, fact =1, number;
            Nullable<int> j = null, fact = 1, number;
            Console.WriteLine("enter any number: ");
            number = int.Parse(Console.ReadLine());

            for(i=1; i<=number;i++)
            {
                fact = fact*i;

            }
            Console.WriteLine("factorial of" + number+ "is: "+fact);
        }
    }
}

