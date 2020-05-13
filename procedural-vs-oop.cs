/*
POP: Uses variables and functions through a sequence of instructions.
OOP: Uses classes and objects to create models based on the real world environment.
      _________________________________
      |  Security  | Easy to Maintain |
---------------------------------------
| POP |     No     |        No        |
---------------------------------------
| OOP |    Yes     |       Yes        |
---------------------------------------
*/

//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace POPvsOOP
{
    public class Program
    {
        public static void Main(string[] args)
        {
          Console.WriteLine("POP-Begin");
          POPLionfunction();
          //To create Another Animal, Copy the Whole Function with Vars and it is not easy to create new ones or maintain the LOC.
          Console.WriteLine("POP-End");

          Console.WriteLine("OOP-Begin");
          Felidae Lion = new Felidae("Lion", "Felis Leo", 4, 3.9F, 303);
          Lion.Says = "Grrr";
          Console.WriteLine(Lion.Name+" says "+Lion.Says);
          Felidae Cat = new Felidae("Cat", "Hapy Kat", 4, 33.99F, 3003);
          Cat.Says = "Meao";
          Console.WriteLine(Cat.Name+" says "+Cat.Says);
          Animal Dog = new Animal("Dog", "Treavesus Perrito", 4, 99.33F, 6006);
          Dog.Says = "Woof";
          Console.WriteLine(Dog.Name+" says "+Dog.Says);
          Console.WriteLine("OOP-End");
        }

        public static void POPLionfunction()
        {
            //Vars
            string animal = "Lion";
            byte legs = 4;
            float heigthFeet = 3.9F;
            int weightPound = 303;           
            char firstLetter = 'L';
            bool felidae = true;
            string says = "Grrrr";
            string scientificName = "Felis Leo";
            string[] colors = new string[2]{ "orange", "yellow" };
            string[] habitat = new string[2]{ "Africa", "Asia" };
            
            //Output
            Console.WriteLine("animal: {0:s}\nlegs: {1:d}", animal, legs);
            Console.WriteLine("heigthFeet: {0:f2}\nweightPound: {1:d}", heigthFeet, weightPound);
            Console.WriteLine("firstLetter: {0:c}\nfelidae: {1:b}", firstLetter, felidae);
            Console.WriteLine("says: {0:s}\nscientificName: {1:s}", says, scientificName);
            Console.WriteLine("colors:");
            foreach(var item in colors){
                  Console.WriteLine(" * {0:s}", item);
            }
            Console.WriteLine("habitat:");
            foreach(var item in habitat){
                  Console.WriteLine(" * {0:s}", item);
            }
            // Console.ReadLine();
        }
      }
        public class Animal
        {
              
            public Animal(string name, string scientificName, byte legs, float heigthFeet, int weightPound){
              this.Name = name;
              this.FirstLetter = name[0];
              this.ScientificName = scientificName;
              this.Legs = legs;
              this.HeigthFeet = heigthFeet;
              this.WeightPound = weightPound;
            }
            //Vars
            public string Name { get; private set; }
            public byte Legs { get; private set; }
            public float HeigthFeet { get; private set; }
            public int WeightPound { get; private set; }           
            public char FirstLetter { get; private set; }
            public string Says { get; set; }// = "Grrrr";
            public string ScientificName { get; private set; }//= "Felis Leo";
            public string[] Colors { get; protected set; }//= new string[2]{ "orange", "yellow" };
            public string[] Habitat { get; set; } //= new string[2]{ "Africa", "Asia" };
        }

        public class Felidae : Animal
        {
              
            public Felidae(string name, string scientificName, byte legs, float heigthFeet, int weightPound): base(name, scientificName, legs, heigthFeet, weightPound){
                  this.Colors = new string[5]{ "orange", "yellow", "white", "black", "purple" };
            }
            
        }
}
