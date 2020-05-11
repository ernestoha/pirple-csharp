using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project1
{

    public static class Helpers
    {
        public static string getFloorsName(int level, Level levels) 
        {
            /*switch(level)
            {
                case -1 : name = "Basement"; break;
                case 0 : name = "Lobby"; break;
                case 1 : name = "First"; break;
                case 2 : name = "Second"; break;
                case 3 : name = "Third"; break;
                case 4 : name = "Fourth"; break;
                case 5 : name = "Fifth"; break;
                case 6 : name = "Sixth"; break;
                case 7 : name = "Seventh"; break;
                case 8 : name = "Eigth"; break;
                case 9 : name = "Ninth"; break;
                case 10 : name = "Penthouse"; break;
            }*/
            string name = "";
            if (level==0){
                name = "Lobby";
            } else if(level<0){
                name = "Basement #" + (level*-1);
            } else if(level==(levels.Up-1)){
                name = "Penthouse";
            } else { 
                name = "Floor #" + level;//.ToString()
            }
            return name;
        }
    }

    public class Floor
    {
        public int Level {get; set;} /* -1 or 0, etc... */
        public string Name {get; set;} /* Basement */
        public bool ButtonPush {get; set;} //Available Button DownButtonNamedPushed
        // public bool ButtonUp {get; set;}
        // public bool ButtonDown {get; set;}

        public Floor(int level, string name)
        {
            this.Level = level;
            this.Name = name;
            // this.ButtonUp = (level==10) ? false : true;
            // this.ButtonDown = (level==-1) ? false : true;
        }
    }

    public class Elevator
    {        
        private int Gap = 10; /* how many floors each elevator travel */
        private int TotalWeight = 1500; /* Total Weigth Pounds*/
        private int TotalPassengers = 5; /* Total Passengers to Lift */

        public int PassengersLift = 0;
        public Passenger[] Passengers { get; set;} 
        public int ElevatorWeight = 0;/* Total Weigth Pounds */
        public double TravelTime = 0;

        //Available Floors for the elevator
        public Floor[] Floors { get; private set;} //= new Floor[10];

        public char Name { get; set;} // = A, B
        public int ActualFloor { get; set;}
        // public int ToFloor {get; set;}
        // public int FromFloor {get; set;}
        public bool IsTraveling {get; set;} //false then is Opened on the ActualFloor
        public bool IsGoingUp {get; set;}
        
        public Elevator(char name, Level levels) 
        {   
            this.Name = name;
            setFloors(this.getStartingPoint(), levels);
            this.ActualFloor = this.Floors[0].Level; //Starting from bottom.
            this.Passengers = new Passenger[this.TotalPassengers];
        }

        public void EventEmergencyButtonPushed()
        {
            if (IsTraveling){
                if (IsGoingUp){
                    Console.Write("Opening doors in next Up floor {0:d}", ActualFloor+1);
                } else {
                    Console.Write("Opening doors in next Down floor {0:d}", ActualFloor-1);
                }
            } else {
                Console.Write("Opening doors in same floor {0:d}", ActualFloor);
            }
        }

        public void EventResetButtonPushed()
        {
            this.ResetValues();
            Console.Write("Elevator {0} Working. Floor: {1:d}", Name, ActualFloor);
        }

        public void AddPassenger(Shaft shaft){
            Console.WriteLine("CLOSE DOORS.");
            //if (PassengersLift<TotalPassengers || ElevatorWeigth < TotalWeight)
            this.Passengers[this.PassengersLift] = new Passenger(shaft); //.getLevelsTotal()
            this.PassengersLift++;
        }
        
        /**
          * Main logic of Elevator Starting Point
          */
        private int getStartingPoint()
        {
            int start = 0; //Elevator start from Lobby
            switch(this.Name) 
            {
                case 'A':
                    start = -1; //-1; only 1 level //-1 starting from basement 2
                    // start = -2; //-2 starting from basement 2 (previous update levels.Down = 2)
                    break;
                case 'B':
                    start = 0; //-1 starting from basement 1
                    // start = -1; //-1 starting from basement 1 (previous update levels.Down = 2)
                    // Floors = new int[10]{-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
                    break;
                // default:
                    // int start = 0;
                    // break;
            }
            return start;
        }

        private void setFloors(int start, Level levels)
        {
            this.Floors = new Floor[levels.getLevelsTotal()-1];
            // Floors[0] = new Floor(1, "e");
            for (int n = 0; n < levels.getLevelsTotal()-1; n++)
            {
                this.Floors[n] = new Floor(n+start, Helpers.getFloorsName(n+start, levels)); //.getLevelsTotal()
            }
        }

        private bool isValidFloorTo(int floorTo){
            // Console.WriteLine("  * " + "checking----between: {0}:{1}. ActualFloor: {2} - FloorTo:{3}", this.Floors[0].Level, this.Floors[this.Floors.Length-1].Level, this.ActualFloor, floorTo);
            if (floorTo>=this.Floors[0].Level && floorTo<=this.Floors[this.Floors.Length-1].Level && floorTo!=this.ActualFloor)
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public bool setFloorByPassenger(int floorTo)
        {
            
            if (this.isValidFloorTo(floorTo))
            {
                char direction = (this.ActualFloor>floorTo) ? 'D' : 'U';
                this.Floors[floorTo].ButtonPush = true; // Passenger Inside ELevator Push Button
                this.Passengers[this.PassengersLift-1].FloorTo = floorTo; //Last Passenger is setting the floor, other combinations.... @TODO
                this.Start(floorTo, direction);// after setted the floor to go, start...
                return true;
            }
            else 
            {
                Console.WriteLine("  * ERROR!! " + "Invalid Floor to Go <{0}>, check on the 'Elevator {1}' Panel and try again, or go to a Level near.", floorTo, this.Name);
                return false;
            }
        }

        private void GoingUp(int floorTo)
        {
            Console.WriteLine("  * " + "From: {0}, To:{1}", this.ActualFloor, floorTo);
            for (int n = this.ActualFloor+1; n <= floorTo; n++) //levels.getLevelsTotal()-1
            {
                //
                this.TravelTime++;
                Console.WriteLine("  * " + " Up ... Floor = {0}. Time = {1} seconds", n, this.TravelTime);
            }
        }

        private void GoingDown(int floorTo)
        {
            Console.WriteLine("  * " + "...From: {0}, To:{1}", this.ActualFloor, floorTo);
            for (int n = this.ActualFloor-1; n >= floorTo; n--)
            {
                //
                this.TravelTime++;
                Console.WriteLine("  * " + " Down ... Floor = {0}. Time = {1} seconds", n, this.TravelTime);
            }
        }

        public void Start(int floorTo, char direction)
        {
            // @TODO read the list of this.Floors[floorTo].ButtonPush = true and go to each one
            if (direction=='U')
                this.GoingUp(floorTo);
            else //D
                this.GoingDown(floorTo);
            this.ActualFloor = floorTo;
            this.ResetValues();
        }
    
        public void Stop()
        {
            this.ResetValues();
        }
        
        public void ResetValues()
        {
            this.TravelTime = 0;
            // this.EleIndex = -1;
            // this.EleShaftPos = -1000;//@TODO
            this.PassengersLift = 0;
            for (int n = 0; n < this.Passengers.Length-1; n++)
            {
                this.Passengers[n] = null;//
            }
            for (int n = 0; n < this.Floors.Length-1; n++)
            {
                this.Floors[n].ButtonPush = false;
            }
            Console.WriteLine("************************************************************************");
        }
        
        
    }

    public class Shaft : Floor
    {
        public bool ButtonUp {get;} //Available Button Up
        public bool ButtonDown {get;} //Available Button Down
        public char ButtonNamedPushed {get; set;} /* UP or DOWN*/

        // public Shaft(int level, string name) : base(level, name)
        // public Shaft(Level levels, int level, string name) : base(level, name)
        public Shaft(Level levels, int level) : base(level, Helpers.getFloorsName(level, levels))
        {
            // this.ButtonUp = (level==10) ? false : true;
            // this.ButtonDown = (level==-1) ? false : true;
            this.ButtonUp = (level==levels.Up-1) ? false : true;
            this.ButtonDown = (level==levels.Down*-1) ? false : true;
        }

        public void setButtonNamedPushed(char letter){
            if ("UD".Contains(letter)){
                this.ButtonNamedPushed = letter;
                this.ButtonPush = true;
            }
        }

    }

    public class Level 
    {
        public int Up {get; set;}
        public int Down {get; set;}

        public Level(int up, int down)
        {
            this.Up = up;
            this.Down = down;
        }

        public int getLevelsTotal()
        {
            return this.Up + this.Down;
        }
    }

    public class Building
    {
        public Elevator ElevatorA {get;}// = new Elevator('A');
        public Elevator ElevatorB {get;}// = new Elevator('B');
        public Elevator[] ElevatorToUse { get; private set;}// = new Elevator('A'or'B');
        // public Level Levels = new Level(11, 1); //[-1,0...10] public int Levels = 12; 
        public Level Levels {get; private set;} //new Level(11, 1); //[-1,0...10] public int Levels = 12; 
        public int Stories {get; set;}
        public Shaft[] Shafts { get; private set;} //To Travel Up or Down
        private int EleIndex = -1; //Elevator A(1) or B(2)
        private int EleShaftPos = -1000; //Shaft that called Elevator
        
        /**
          * stories, up (building floors up), down (building floors down)
          */
        // public Building()
        public Building(int stories, int up, int down)
        {
            this.Stories = stories;
            this.Levels = new Level(up, down); //11, 1
            this.ElevatorA = new Elevator('A', this.Levels);
            this.ElevatorB = new Elevator('B', this.Levels);
            this.ElevatorToUse =  new Elevator[2];
            this.ElevatorToUse[0] = this.ElevatorA;
            this.ElevatorToUse[1] = this.ElevatorB;
            this.setShafts();
        }

        private void setShafts()
        {
            this.Shafts = new Shaft[Levels.getLevelsTotal()];
            // Shafts[0] = new Shafts(1, "e");
            for (int n = 0; n < Levels.getLevelsTotal(); n++)
            {
                this.Shafts[n] = new Shaft(this.Levels, n-1); //.getLevelsTotal()
            }
        }
/*
 2
 1
 0
-1
*/
        private int floorsTo(int ActualFloorX, int FloorToX)
        {
            // Console.WriteLine(" *  .. {0} . {1}", ActualFloorX, FloorToX);
            int n = ActualFloorX;
            int c = 0;
            if (ActualFloorX<FloorToX)
            {    
                while (n < FloorToX)
                {
                    c++;
                    n++;
                    // Console.Write(n+";c="+c+";");
                }
            } else {
                n = FloorToX;
                while (n < ActualFloorX)
                {
                    c++;
                    n++;
                    // Console.Write(n+";c="+c+";");
                }
            }
            return c;
            // return n;
        }
/*
 3
 2 Pushed From
 1 
 0  B
-1  A 
*/
        private void wichElevatorSendToPushedCall()
        {
            string moveMsg = "MOVE TO A DIFERENT FLOOR";
            Console.WriteLine("  * " + "Pushed [From --->'{0}' {1}]",this.Shafts[this.EleShaftPos].Name,this.Shafts[this.EleShaftPos].Level);
            int distanceA = this.floorsTo(ElevatorA.ActualFloor, this.Shafts[this.EleShaftPos].Level);
            int distanceB = this.floorsTo(ElevatorB.ActualFloor, this.Shafts[this.EleShaftPos].Level);
            Console.WriteLine("  * " + "distanceA {0}. Actual Floor:{1} * ",distanceA,ElevatorA.ActualFloor);
            Console.WriteLine("  * " + "distanceB {0}. Actual Floor:{1} * ",distanceB,ElevatorB.ActualFloor);
            /*if (this.Shafts[this.EleShaftPos].Level==Levels.Down*-1 || this.Shafts[this.EleShaftPos].Level==Levels.Up-1){
                if (this.Shafts[this.EleShaftPos].Level==Levels.Down*-1){//To bottom Basement 
                    Console.WriteLine("{0}. Using Elevator A. Time {1} seconds. From {2} To {3}. ", moveMsg, distanceA, this.ElevatorA.ActualFloor, this.Shafts[this.EleShaftPos].Level);
                    this.ElevatorA.TravelTime += distanceA; //Nearest Elevator Traveling
                    this.ElevatorA.ActualFloor = this.Shafts[this.EleShaftPos].Level;//Elevator in site
                    this.EleIndex = 0;
                } else { //To Penthouse
                    Console.WriteLine("{0}. Using Elevator B. Time {1} seconds. From {2} To {3}. ", moveMsg, distanceB, this.ElevatorB.ActualFloor, this.Shafts[this.EleShaftPos].Level);
                    this.ElevatorB.TravelTime += distanceB; //Elevator B Traveling
                    this.ElevatorB.ActualFloor = this.Shafts[this.EleShaftPos].Level;//Elevator in site
                    this.EleIndex = 1;
                }
            } else {*/
                if (distanceA>distanceB){
                    Console.WriteLine("{0}. Using Elevator B.. Time {1} seconds. From {2} To {3}. ", moveMsg, distanceB, this.ElevatorB.ActualFloor, this.Shafts[this.EleShaftPos].Level);
                    this.ElevatorB.TravelTime += distanceB;
                    this.ElevatorB.ActualFloor = this.Shafts[this.EleShaftPos].Level;//Elevator in site
                    this.EleIndex = 1;
                } else {
                    Console.WriteLine("{0}. Using Elevator "+this.ElevatorToUse[0].Name +".. Time {1} seconds. From {2} To {3}. ", moveMsg, distanceA, this.ElevatorA.ActualFloor, this.Shafts[this.EleShaftPos].Level);
                    this.ElevatorA.TravelTime += distanceA;
                    this.ElevatorA.ActualFloor = this.Shafts[this.EleShaftPos].Level;//Elevator in site
                    this.EleIndex = 0;
                }
            // }
            Console.WriteLine("OPEN DOORS.");
        }

        public void AddPassengerToElevatorSelectedAuto()
        {
            this.ElevatorToUse[this.EleIndex].AddPassenger(this.Shafts[this.EleShaftPos]);
            // Buiding1.ElevatorA.AddPassenger(Buiding1.Shafts[2]); //adding 1 passenger Elevator A from Shaft[1]
        }

        public void setFloorByPassengerESA(int floorTo) //Elevator Selected Automatic
        { 
            this.ElevatorToUse[this.EleIndex].setFloorByPassenger(floorTo);
            // Buiding1.ElevatorA.setFloorByPassenger(9); // Passenger Inside ELevator Push Button "Floor # 9". Start...
        }

        private int fromNum2ShaftPos(int value)
        {
            for (int n = 0; n < this.Shafts.Length; n++)  
            {
                if (this.Shafts[n].Level == value)
                {
                    return n;
                }
            }
            return -1000;//@TODO
        }

        private void EventButtonPushed(char direction, int value)
        {
            this.EleShaftPos = this.fromNum2ShaftPos(value);
            this.Shafts[this.EleShaftPos].setButtonNamedPushed('U');
            // Console.WriteLine(".......---...."+this.Shafts[shaftPos].Level + "-"+this.Shafts[shaftPos].Name);
            //Calling Elevator and Setting to Elevator Actual Floor
            this.wichElevatorSendToPushedCall();
        }

        public void EventUpButtonPushed(int value)
        {
            this.EventButtonPushed('U', value);
        }

        public void EventDownButtonPushed(int value)
        {
            this.EventButtonPushed('D', value);
        }
    }

    public class Passenger
    {
        public int Weight {get; set;}
        public int FloorFrom {get; set;} //Button pushed On Floor Shaft
        public int FloorTo {get; set;} //Button Pushed On Elevator Panel

        public Passenger(Shaft shaft)
        {
            this.FloorFrom = shaft.Level;
        }
    }

    public class Program
    {

        public static Building Buiding1 = new Building(10, 11, 1);

        public static void Main(string[] args)
        {
            Test001();
        }


        public static void Test001()
        {
                        // Buiding1.Shafts[1].ButtonNamedPushed = 'U';//Passengert in Lobby [1] press Up Button
            Buiding1.EventUpButtonPushed(2);//Passenger in Lobby [1] press Up Button
            // OLD Buiding1.ElevatorA.AddPassenger(Buiding1.Shafts[2]); //adding 1 passenger Elevator A from Shaft[1]
            // OLD Buiding1.ElevatorA.setFloorByPassenger(9); // Passenger Inside ELevator Push Button "Floor # 9". Start...
            Buiding1.AddPassengerToElevatorSelectedAuto();
            Buiding1.setFloorByPassengerESA(9); // Passenger Inside ELevator Push Button "Floor # 9". Start...

            Console.WriteLine("  * -->" + Buiding1.ElevatorB.ActualFloor);

            Buiding1.EventUpButtonPushed(2);//Passenger in Floor #2 press Down Button
            // Buiding1.ElevatorB.AddPassenger(Buiding1.Shafts[2]); //adding 1 passenger Elevator A from Shaft[1]
            // Buiding1.ElevatorB.setFloorByPassenger(2); // Passenger Inside ELevator Push Button "Lobby". Start...
            Buiding1.AddPassengerToElevatorSelectedAuto();
            Buiding1.setFloorByPassengerESA(9); // Passenger Inside ELevator Push Button "Floor # 9". Start...

            // Buiding1.EventUpButtonPushed(7);//Passenger in Lobby [1] press Up Button
            // Buiding1.ElevatorA.AddPassenger(Buiding1.Shafts[7]); //adding 1 passenger Elevator A from Shaft[1]
            // Buiding1.ElevatorA.setFloorByPassenger(0); // Passenger Inside ELevator Push Button "Floor # 9". Start...

            Buiding1.EventUpButtonPushed(8);//Passenger in Floor #8 press Down Button
            Buiding1.AddPassengerToElevatorSelectedAuto();
            Buiding1.setFloorByPassengerESA(5);

            Buiding1.EventUpButtonPushed(-1);//Passenger in Bassement #1 press Down Button
            Buiding1.AddPassengerToElevatorSelectedAuto();
            Buiding1.setFloorByPassengerESA(4);


            Buiding1.EventUpButtonPushed(8);//Passenger in Floor #8 press Down Button
            Buiding1.AddPassengerToElevatorSelectedAuto();
            Buiding1.setFloorByPassengerESA(0);

            Console.WriteLine("  * ----------------------------------------");
            Console.WriteLine("  * " + Buiding1.ElevatorA.Name);
            Console.WriteLine("  * " + Buiding1.ElevatorA.ActualFloor);
            Console.WriteLine("  * " + Buiding1.ElevatorA.Floors[0].Name);
            Console.WriteLine("  * " + Buiding1.ElevatorA.Floors[9].Name);
            Console.WriteLine("  * " + Buiding1.ElevatorA.Floors[10].Name);
            Console.WriteLine("  * " + "A-Passengers="+Buiding1.ElevatorA.PassengersLift);

            Console.WriteLine("  * " + Buiding1.ElevatorB.Name);
            Console.WriteLine("  * " + Buiding1.ElevatorB.ActualFloor);
            Console.WriteLine("  * " + Buiding1.ElevatorB.Floors[0].Name);
            Console.WriteLine("  * " + Buiding1.ElevatorB.Floors[9].Name);
            Console.WriteLine("  * " + Buiding1.ElevatorB.Floors[10].Name);

            Console.WriteLine("  * " + "-------");

            // Console.WriteLine("  * " + "Shaft 0 level={0}, up={1}, down={2}", Buiding1.Shafts[0].Level, Buiding1.Shafts[0].ButtonUp, Buiding1.Shafts[0].ButtonDown);
            // Console.WriteLine("  * " + "Shaft 1 level={0}, up={1}, down={2}", Buiding1.Shafts[1].Level, Buiding1.Shafts[1].ButtonUp, Buiding1.Shafts[1].ButtonDown);
            // Console.WriteLine("  * " + "Shaft 10 level={0}, up={1}, down={2}", Buiding1.Shafts[10].Level, Buiding1.Shafts[10].ButtonUp, Buiding1.Shafts[10].ButtonDown);
            // Console.WriteLine("  * " + "Shaft 10 level={0}, up={1}, down={2}", Buiding1.Shafts[11].Level, Buiding1.Shafts[11].ButtonUp, Buiding1.Shafts[11].ButtonDown);
        }
    }
}