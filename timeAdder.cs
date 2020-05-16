using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimeAdder
{
    public static class Program
    {
        
        public static string timeAdder(int value1, string label1, int value2, string label2) {
            if ((isValid(value1, label1)) && (isValid(value2, label2))) {
                if ((getScale(label1)) != (getScale(label2)))
                {
                    if (label1Bigger(label1, label2)) {
                        value1 = convert2MinimumScale(value1, label1, label2);
                    } else {
                        value2 = convert2MinimumScale(value2, label2, label1);
                    }
                }
                // Console.WriteLine("value1: {0}", value1);
                // Console.WriteLine("value2: {0}", value2);
                int value3 = value1 + value2;
                string label3 = getPlural(label1Bigger(label1, label2) ? label2 : label1);
                return round2Scale(value3, label3);
                // return "0";
            } else {
                return "Incorrect format"; //incorrect format
            }
        }

        public static string getPlural(string label) {
            string plural = label;
            switch (label) {
                case "second":
                case "minute":
                case "hour":
                case "day":
                plural += 's';
                break;
            }
            return plural;
        }

        public static bool isValid(int value, string label) {
            switch (label) {
                case "seconds":
                case "minutes":
                case "hours":
                case "days":
                return (value > 1) ? true : false;
                // break;
                case "second":
                case "minute":
                case "hour":
                case "day":
                return (value == 1) ? true : false;
                // break;
                default:
                return false;
            }
        }

        public static bool label1Bigger(string label1, string label2) {
            return (getScale(label1) > getScale(label2)) ? true : false;
        }

        public static int getScale(string label) {
            int scale = 0;
            switch (getPlural(label)) {
                case "seconds":
                scale = 1;
                break;
                case "minutes":
                scale = 2;
                break;
                case "hours":
                scale = 3;
                break;
                case "days":
                scale = 4;
                break;
            }
            return scale;
        }

        public static string getScaleName(int key, int value) {
            string name = "";
            switch (key) {
                case 1:
                name = "second";
                break;
                case 2:
                name = "minute";
                break;
                case 3:
                name = "hour";
                break;
                case 4:
                name = "day";
                break;
            }
            return (value > 1) ? getPlural(name) : name;
        }

        public static int getScaleValue(int key) {
            int scale = 0;
            switch (key) { //label
                case 1: //"seconds":
                scale = 60;
                break;
                case 2: //"minutes":
                scale = 60;
                break;
                case 3: //"hours":
                scale = 24;
                break;
                case 4: //"days":
                scale = 1;
                break;
            }
            return scale;
        }

        public static int convert2MinimumScale(int value, string labelBig, string labelSmall) {
            int small = getScale(labelSmall);
            int big = getScale(labelBig);
            int mult = 1;
            // Console.WriteLine("small = " + small);
            // Console.WriteLine("big = " + big);
            for (int x = small; x < big; x++) {
                mult *= getScaleValue(x);
                //Console.WriteLine("__"+mult);
            }
            return value * mult;
        }

        public static bool getNextScale() {
            return false;
        }

        public static string round2Scale(int value3, string label3) {
            int small = getScale(label3);
            //int big = getScale(labelBig);
            float mult = 1;
            int x = small;
            // int res = [];
            // Console.WriteLine("label3="+label3 +". value3="+value3);
            float value2check = 0;
            float valueTemp = value3;
            while (x < 4) { // 4  is the bigger scale
                mult *= getScaleValue(x);
                value2check = valueTemp / mult;
                // Console.WriteLine("value2check: {0:f2} = {1}/{2}", value2check, valueTemp, mult);
                if (value2check % 1 == 0) { //Number.isInteger
                value3 = (int)value2check;
                label3 = getScaleName(x + 1, value3);
                }
                x++;
            }
            // return [value3, label3];
            return value3 + " " + label3;
        }

        public static void Main(string[] args)
        {
            //Vars
            string call1 = timeAdder(2, "minutes", 3, "minutes");
            string call2 = timeAdder(1, "hour", 2, "minutes");
            string call3 = timeAdder(-2, "hour", 3, "days");
            string call4 = timeAdder(1, "hour", 1, "hour");
            string call5 = timeAdder(4, "minutes", 60, "seconds");
            string call6 = timeAdder(2, "minutes", 1, "second");
            string call7 = timeAdder(23, "hours", 1, "hour");
            string call8 = timeAdder(6600, "minutes", 36000, "seconds");

            //Output
            Console.WriteLine("call1: " + call1);
            Console.WriteLine("call2: " + call2);
            Console.WriteLine("call3: " + call3);
            Console.WriteLine("call4: " + call4);
            Console.WriteLine("call5: " + call5);
            Console.WriteLine("call6: " + call6);
            Console.WriteLine("call7: " + call7);
            Console.WriteLine("call8: " + call8);
        }
    }
}