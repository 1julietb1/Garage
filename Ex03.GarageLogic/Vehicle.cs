using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    public abstract class Vehicle
    {
        protected string m_VehicleModel;
        protected string m_LicenseNumber;
        internal List<Wheel> m_SetOfWheels;
        internal Power m_Power;
        internal Dictionary<string, string> m_VehiclesDictionary;
        private float percentPowerLeft;

        public Vehicle(Dictionary<string, string> i_VehicleProperties)
        {
            m_VehiclesDictionary = i_VehicleProperties;
            m_VehicleModel = i_VehicleProperties["Model name"];
            m_LicenseNumber = i_VehicleProperties["License number"];
            if (i_VehicleProperties.ContainsKey("Fuel type") == true)
            {
                m_Power = new Fuel(i_VehicleProperties);
            }
            else
            {
                m_Power = new Electrical(i_VehicleProperties);
            }

            m_SetOfWheels = CreateListOfWheels(i_VehicleProperties);
            m_VehiclesDictionary.Add("Percent of power left", PercentPowerLeft.ToString());
        }

        internal static bool ValidationBoolType(string io_InputStringToBool)
        {
            io_InputStringToBool = io_InputStringToBool.ToUpper();
            bool returnValue = false;
            if (!io_InputStringToBool.Equals("FALSE") && !io_InputStringToBool.Equals("TRUE"))
            {
                throw new FormatException("Does your truck carry dangerous materials? Please enter True/False");
            }
            else
            {
                if (io_InputStringToBool.Equals("True"))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        internal static float ValidationFloatType(string io_InputStringToFloat)
        {
            float returnValue = 0;
            bool isValidFloat = float.TryParse(io_InputStringToFloat, out returnValue);
            if (!isValidFloat)
            {
                throw new FormatException("Input must contain only digits");
            }

            return returnValue;
        }

        internal static int ValidationIntType(string io_InputStringToInt)
        {
            int returnValue = 0;
            bool isValidInt = int.TryParse(io_InputStringToInt, out returnValue);

            if (!isValidInt)
            {
                throw new FormatException("Input must contain only digits");
            }

            return returnValue;
        }

        public static float CalculatePercentOfPowerLeft(Dictionary<string, string> i_VehicleDictionary)
        {
            float currentPower = 0;
            float maxPowerPossible = 0;

            if (i_VehicleDictionary.ContainsKey("Fuel type") == true)
            {
                currentPower = ValidationFloatType(i_VehicleDictionary["Current amount of fuel"]);
                maxPowerPossible = ValidationFloatType(i_VehicleDictionary["Maximum fuel possible"]);
            }
            else
            {
                currentPower = ValidationFloatType(i_VehicleDictionary["Current level of baterry"]);
                maxPowerPossible = ValidationFloatType(i_VehicleDictionary["Maximum battery possible"]);
            }

            return (currentPower / maxPowerPossible) * 100;
        }

        public Dictionary<string, string> VehiclesDictionary
        {
            get
            {
                return m_VehiclesDictionary;
            }
        }

        public string VehicleModel
        {
            get
            {
                return m_VehicleModel;
            }

            set
            {
                m_VehicleModel = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        internal List<Wheel> SetOfWheels
        {
            get
            {
                return m_SetOfWheels;
            }
        }

        public Power PowerUnit
        {
            get
            {
                return m_Power;
            }
        }

        public float PercentPowerLeft { get => percentPowerLeft; set => percentPowerLeft = value; }

        internal List<Wheel> CreateListOfWheels(Dictionary<string, string> i_VehicleProperties)
        {
            int numberOfWheels = int.Parse(i_VehicleProperties["Number of wheels"]);
            List<Wheel> setOfWheels = new List<Wheel>(numberOfWheels);

            for (int i = 0; i < numberOfWheels; i++)
            {
                setOfWheels.Add(new Wheel(i_VehicleProperties));
            }

            return setOfWheels;
        }

        internal string WheelsCharacteristic()
        {
            StringBuilder currentAirPressureOfAllWheelsStr = new StringBuilder();
            List<float> currentAirPressureOfAllWheels = new List<float>();

            for (int i = 0; i < m_SetOfWheels.Count; i++)
            {
                currentAirPressureOfAllWheels[i] = m_SetOfWheels[i].ExistingAirPressure;
                currentAirPressureOfAllWheelsStr.Append(string.Format(
                    "Current air pressure of the #{0} wheel is: {1} ",
                    i,
                    currentAirPressureOfAllWheels[i]));
            }

            return currentAirPressureOfAllWheelsStr.ToString();
        }

        internal virtual string VehicleCharacteristic()
        {
            return null;
        }

        public static List<string> GetVehicleProperties()
        {
            List<string> vehiclePropertiesList = new List<string>();
            vehiclePropertiesList.Add("Model name");
            vehiclePropertiesList.Add("License number");

            return vehiclePropertiesList;
        }
    }
}
