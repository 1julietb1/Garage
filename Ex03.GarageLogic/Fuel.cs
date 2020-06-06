using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class Fuel : Power
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler,
        }

        private eFuelType m_fuelType;

        public Fuel(Dictionary<string, string> i_FuelProperties)
            : base(i_FuelProperties)
        {
            m_fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), i_FuelProperties["Fuel type"]);
        }

        internal void Refuel(float i_AmountOfFuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType.Equals(m_fuelType))
            {
                if (i_AmountOfFuelToAdd + m_PowerLeft > m_MaximumPowerPossible)
                {
                    throw new ValueOutOfRangeException(0, m_MaximumPowerPossible - m_PowerLeft);
                }
                else
                {
                    m_PowerLeft = m_PowerLeft + i_AmountOfFuelToAdd;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        internal string FuelLevelAndType()
        {
            return string.Format(
                "Fuel level is: {0}, Fuel type is: {1}",
                m_PowerLeft,
                m_fuelType);
        }

        public eFuelType FuelType
        {
            get
            {
                return m_fuelType;
            }
        }

        public static List<string> GetFuelProperties()
        {
            List<string> fuelPropertiesList = new List<string>();
            fuelPropertiesList.Add("Fuel type");
            fuelPropertiesList.Add("Current amount of fuel");
            return fuelPropertiesList;
        }
    }
}
