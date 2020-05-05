//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
	public class Vehicle
	{
        public string Make { get; set; } 
        public string Model { get; set; }
        public int Year { get; set; }
        public double Weight { get; set; }
        public bool NeedsMaintenance { get; protected set; } = false;
        public int TripsSinceMaintenance { get; set; } = 0; /* not protected for tests */

        public Vehicle(){
        }

        public Vehicle(string make, string model, int year, double weight){
            Make = make;
            Model = model;
            Year = year;
            Weight = weight;
        }
	}
	
    public class Cars : Vehicle 
    {
        private bool isDriving;

        public Cars(string make, string model, int year, double weight) : base(make, model, year, weight){

        }

        public void Drive(){
            isDriving = true;
        }

        public void Stop(){
            isDriving = false;
            this.TripsSinceMaintenance++;
            if (TripsSinceMaintenance>=100)
                NeedsMaintenance = true;
        }

        public void Repair(){
            TripsSinceMaintenance = 0;
            NeedsMaintenance = false;
        }
        
        public void WriteProperties(){
            Console.WriteLine("---------------------{0}-BEGIN---------------------", Model);
            Console.WriteLine("Make: {0}. Year: {1:d}. Weight: {2:f2}",Make, Year, Weight);
            Console.WriteLine("NeedsMaintenance: {0:b}. TripsSinceMaintenance: {1:d}",NeedsMaintenance, TripsSinceMaintenance);
            Console.WriteLine("----------------------{0}-END----------------------\n", Model);
        }
    }

    public class Planes : Vehicle 
    {
        private bool isDriving;

        public Planes(string make, string model, int year, double weight) : base(make, model, year, weight){

        }

        public void Drive(){
            if (!NeedsMaintenance)
                isDriving = true;
            else
                Console.WriteLine("Can't fly until it's repaired");
        }

        public void Stop(){
            isDriving = false;
            this.TripsSinceMaintenance++;
            if (TripsSinceMaintenance>=100)
                NeedsMaintenance = true;
        }

        public void Repair(){
            TripsSinceMaintenance = 0;
            NeedsMaintenance = false;
        }
        
        public void WriteProperties(){
            Console.WriteLine("---------------------{0}-BEGIN---------------------", Model);
            Console.WriteLine("Make: {0}. Year: {1:d}. Weight: {2:f2}",Make, Year, Weight);
            Console.WriteLine("NeedsMaintenance: {0:b}. TripsSinceMaintenance: {1:d}",NeedsMaintenance, TripsSinceMaintenance);
            Console.WriteLine("----------------------{0}-END----------------------\n", Model);
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            //Vars
            // Vehicle Car = new Vehicle("Toyota", "Camry", 3030, 789);
            Cars Mirai = new Cars("Toyota", "Mirai", 3030, 4075.11);
            Cars Malibu = new Cars("Chevrolet", "Malibu", 3031, 3223.22);
            Cars Mirage = new Cars("Mitsubishi", "Mirage", 3032, 2128.33);
            Mirai.WriteProperties();
            Malibu.WriteProperties();
            Mirage.WriteProperties();

            Mirai.TripsSinceMaintenance = 30;
            Malibu.TripsSinceMaintenance = 99;
            Mirage.TripsSinceMaintenance = 10;

            Mirai.Drive();
            Malibu.Drive();
            Mirage.Drive();

            Mirai.Stop();
            Malibu.Stop();
            Mirage.Stop();

            Mirai.WriteProperties();
            Malibu.WriteProperties();
            Mirage.WriteProperties();

            Malibu.Repair();
            Malibu.WriteProperties();
            
            Planes B737 = new Planes("Boeing", "B737", 3034, 40000.44);
            B737.WriteProperties();

            B737.TripsSinceMaintenance = 98;

            B737.Drive(); 
            B737.Stop(); //99
            B737.Drive(); 
            B737.Stop(); //100 Maintenance

            B737.Drive(); //Can't fly until it's repaired.
        }
    }
}
