//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    /**
      * Structure is a collection of variables of different data types under a single unit.
      * Hold values.
      * No Constructor.
      * Can not inherit from another struct or class.
      */
	public struct Car{
		
		public string Brand; 
    	public string Model; 
    	public string Color; 
    	public int Year;
	}
	
	/**
	  * Class is a user-defined unit with atributes and methods from which objects are instanced.
	  * References to values.
	  * Has a Constructor.
	  * Can inherit from another class.
 	  */
	public class Driver{
		public string fullname;
		public bool license;
		
		public Driver(string fullname, bool license){
			this.fullname = fullname;
			this.license = license;
		}
		
		public void PrintFullName(){
			Console.WriteLine("Full Name: {0}", this.fullname);
		}
	}
	
    public class Program
    {
        public static void Main(string[] args)
        {

            /**
              * Testing structure
              */
            //Vars
            Car car1;
            car1.Brand = "Toyota";
            car1.Model = "Baby Camry";
            car1.Color = "Blue";
            car1.Year = 3030;
            
            //Output
            Console.WriteLine("Brand: {0}", car1.Brand);
            Console.WriteLine("----------------------------");
        	
            /**
              * Testing class
              */
            //Vars
            Driver ernesto = new Driver("Ernesto", true); //Class
            ernesto.PrintFullName();
			// Console.ReadLine();
        }
    }
}
