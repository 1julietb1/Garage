using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class Car : Vehicle
    {
        internal Car(Dictionary<string, string> i_CarProperties)

            : base(i_CarProperties)
        {
        }

        private static void convertingNumberToEnum(Dictionary<string, string> io_CarProperties)
        {
            switch (io_CarProperties["Number of doors"])
            {
                case "2":
                    io_CarProperties["Number of doors"] = "Two";
                    break;
                case "3":
                    io_CarProperties["Number of doors"] = "Three";
                    break;
                case "4":
                    io_CarProperties["Number of doors"] = "Four";
                    break;
                case "5":
                    io_CarProperties["Number of doors"] = "Five";
                    break;
                default:
                    throw new FormatException("Invalid number of doors");
            }
        }

        public static List<string> GetCarProperties()
        {
            List<string> carPropertiesList = new List<string>();
            carPropertiesList.Add("Car color");
            carPropertiesList.Add("Number of doors");
            return carPropertiesList;
        }
    }
}