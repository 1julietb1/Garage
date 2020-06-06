using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class Motorcycle : Vehicle
    {
        public Motorcycle(Dictionary<string, string> i_MotorcycleProperties)

            : base(i_MotorcycleProperties)
        {
        }

        public static List<string> GetMotorcycleProperties()
        {
            List<string> motorcyclePropertiesList = new List<string>();
            motorcyclePropertiesList.Add("License type");
            motorcyclePropertiesList.Add("Engine capacity");
            return motorcyclePropertiesList;
        }
    }
}
