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
			//Vars
			string animal = "Lion";
			byte feet = 4;
			float heigthFeet = 3.9F;
			int weightPound = 303;           
			char firstLetter = 'L';
			bool felidae = true;
			string says = "Grrrr";
			
			//Output
			Console.WriteLine("animal: {0:s}\nfeet: {1:d}", animal, feet);
			Console.WriteLine("heigthFeet: {0:f2}\nweightPound: {1:d}", heigthFeet, weightPound);
			Console.WriteLine("firstLetter: {0:c}\nfelidae: {1:b}", firstLetter, felidae);
			Console.WriteLine("says: {0:s}", says);
       		
			// Console.ReadLine();
        }
    }
}
