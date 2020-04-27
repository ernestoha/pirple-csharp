//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    public class Program
    {
        
        //Function
        public static bool isPrime(int a){
         bool x = false;
         int i = 2;
         if (a==i)
           return true;
         while (i<a){
           if (a%i == 0){
            x = false;
            break;
           } else {
            x = true;
            i++;
           }
         }
         return x;
        }

        public static void Main(string[] args)
        {
            for (int n = 1; n <= 100; n++) 
            {
                if (isPrime(n))
                    Console.WriteLine(n+" Prime");
                else if (n % 3 == 0 && n % 5 == 0)
                    Console.WriteLine(n+" FizzBuzz");
                else if (n % 3 == 0)
                    Console.WriteLine(n+" Fizz");
                else if (n % 5 == 0)
                     Console.WriteLine(n+" Buzz");
                else
                     Console.WriteLine(n);
              //Console.WriteLine(i);
            }
        }
    }
}