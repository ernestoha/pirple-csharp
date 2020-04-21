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
        public static void Main(string[] args)
        {
        	/**
              * Syllogism 1:
              *  This cake is either vanilla or chocolate.
              *  This cake is not chocolate.
              *  Therefore, this cake is vanilla.
              */
            //Vars
            string[] cakes = new string[2]{ "chocolate", "vainilla" };
            bool chocolate = true;
            string flavor = cakes[Convert.ToByte(!chocolate)]; //"chocolate";
            
            //Output
            Console.WriteLine("This cake is either" + " " + cakes[0] + " " + "or" + " " + cakes[1] + ".");
            Console.WriteLine("This cake is not" + " " + cakes[Convert.ToByte(chocolate)] + ".");
            Console.WriteLine("Therefore, this cake is " + flavor + ".");

            Console.WriteLine("----------------------------");
        	
        	/**
              * Syllogism 2:
              *  All men are mortal
              *  Socrates is a man.
              *  Therefore, socrates is mortal.
              */
			//Vars
			string man = "Socrates";
			bool mortal = true;
			string xtraText = (mortal ? " " : " not ");
			
			//Output
			Console.WriteLine("All men are mortal.\n" + man + " " + "is" + xtraText + "a man.\nTherefore," + " " + man + " " + "is" + xtraText + "mortal.");
			
			// Console.ReadLine();
        }
    }
}
