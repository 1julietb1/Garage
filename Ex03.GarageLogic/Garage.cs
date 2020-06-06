using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03
{
    public class Garage
    {
        private Dictionary<string, VehicleAtTheGarage> m_DictionaryOfVehicles;

        public Garage()
        {
            m_DictionaryOfVehicles = new Dictionary<string, VehicleAtTheGarage>();
        }

        public bool isVehicleInGarage(string i_LicenseNumber)
        {
            return m_DictionaryOfVehicles.ContainsKey(i_LicenseNumber);
        }

        public void AddNewCarToTheGarage(Vehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber)
        {
            VehicleAtTheGarage newVehicleInTheGarage = new VehicleAtTheGarage(i_OwnerName, i_PhoneNumber, i_Vehicle);
            m_DictionaryOfVehicles[i_Vehicle.LicenseNumber] = newVehicleInTheGarage;
        }

        public List<string> ListOfLicenseNumbers(string i_Filter)
        {
            List<string> listOfLicenses = new List<string>();

            if (i_Filter.Equals("None"))
            {
                foreach (string stringToAdd in m_DictionaryOfVehicles.Keys)
                {
                    listOfLicenses.Add(stringToAdd);
                }
            }
            else
            {
                VehicleAtTheGarage.eCarStatus filterEnumType = (VehicleAtTheGarage.eCarStatus)Enum.Parse(
                                                                                    typeof(VehicleAtTheGarage.eCarStatus),
                                                                                    i_Filter);
                foreach (VehicleAtTheGarage vehicleToCheck in m_DictionaryOfVehicles.Values)
                {
                    if (vehicleToCheck.CarStatus.Equals(filterEnumType))
                    {
                        listOfLicenses.Add(vehicleToCheck.Vehicle.LicenseNumber);
                    }
                }
            }

            return listOfLicenses;
        }

        public bool ChangeStatusOfVehicle(string i_LicenseNumber, string i_NewStatus)
        {
            VehicleAtTheGarage possibleVehicle = null;
            bool vehicleExistsInTheGarage = m_DictionaryOfVehicles.TryGetValue(i_LicenseNumber, out possibleVehicle);

            if (vehicleExistsInTheGarage)
            {
                switch (i_NewStatus)
                {
                    case "InRepair":
                        possibleVehicle.CarStatus = VehicleAtTheGarage.eCarStatus.InRepair;
                        possibleVehicle.Vehicle.m_VehiclesDictionary["Vehicle's status"] = "In A Repair";
                        break;

                    case "Fixed":
                        possibleVehicle.CarStatus = VehicleAtTheGarage.eCarStatus.Fixed;
                        possibleVehicle.Vehicle.m_VehiclesDictionary["Vehicle's status"] = "Fixed";
                        break;

                    case "Paid":
                        possibleVehicle.CarStatus = VehicleAtTheGarage.eCarStatus.Paid;
                        possibleVehicle.Vehicle.m_VehiclesDictionary["Vehicle's status"] = "Paid";
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                throw new VehicleNotInGarageException(i_LicenseNumber);
            }

            return vehicleExistsInTheGarage;
        }

        public bool AddAirPressureForAllWheels(string i_LicenseNumber)
        {
            VehicleAtTheGarage possibleVehicle = null;
            bool vehicleExistsInTheGarage = m_DictionaryOfVehicles.TryGetValue(i_LicenseNumber, out possibleVehicle);

            if (vehicleExistsInTheGarage)
            {
                foreach (Wheel weelToChange in possibleVehicle.Vehicle.SetOfWheels)
                {
                    weelToChange.ExistingAirPressure = weelToChange.MaxAirPressure;
                    possibleVehicle.Vehicle.m_VehiclesDictionary["Wheels current air pressure"] = weelToChange.ExistingAirPressure.ToString();
                }
            }
            else
            {
                throw new VehicleNotInGarageException(i_LicenseNumber);
            }

            return vehicleExistsInTheGarage;
        }

        public bool RefulVehicleInTheGarage(string i_LicenseNumber, string i_FuelType, float i_AmountOfFuelToAdd)
        {
            Fuel fuelPower;

            VehicleAtTheGarage possibleVehicle = null;
            bool vehicleExistsInTheGarage = m_DictionaryOfVehicles.TryGetValue(i_LicenseNumber, out possibleVehicle);

            if (vehicleExistsInTheGarage)
            {
                fuelPower = possibleVehicle.Vehicle.PowerUnit as Fuel;
                if (fuelPower != null)
                {
                    switch (i_FuelType)
                    {
                        case "Soler":
                            fuelPower.Refuel(i_AmountOfFuelToAdd, Fuel.eFuelType.Soler);
                            break;

                        case "Octan95":
                            fuelPower.Refuel(i_AmountOfFuelToAdd, Fuel.eFuelType.Octan95);
                            break;

                        case "Octan96":
                            fuelPower.Refuel(i_AmountOfFuelToAdd, Fuel.eFuelType.Octan96);
                            break;

                        case "Octan98":
                            fuelPower.Refuel(i_AmountOfFuelToAdd, Fuel.eFuelType.Octan98);
                            break;
                        default:
                            throw new ArgumentException();
                    }

                    possibleVehicle.Vehicle.m_VehiclesDictionary["Current amount of fuel"] = possibleVehicle.Vehicle.m_Power.PowerLeft.ToString();
                    possibleVehicle.Vehicle.m_VehiclesDictionary["Percent of power left"] = Vehicle.CalculatePercentOfPowerLeft(possibleVehicle.Vehicle.m_VehiclesDictionary).ToString();
                }
                else
                {
                    throw new WrongTypeOfVehicalException();
                }
            }
            else
            {
                throw new VehicleNotInGarageException(i_LicenseNumber);
            }

            return vehicleExistsInTheGarage;
        }

        public bool ChargeBatteryOfVehicleInTheGarage(string i_LicenseNumber, float i_AddTime)
        {
            Electrical electricalPower;
            VehicleAtTheGarage possibleVehicle = null;
            bool vehicleExistsInTheGarage = m_DictionaryOfVehicles.TryGetValue(i_LicenseNumber, out possibleVehicle);

            if (vehicleExistsInTheGarage)
            {
                electricalPower = possibleVehicle.Vehicle.PowerUnit as Electrical;
                if (electricalPower != null)
                {
                    electricalPower.ChargeBattery(i_AddTime);
                    possibleVehicle.Vehicle.m_VehiclesDictionary["Current level of baterry"] = possibleVehicle.Vehicle.m_Power.PowerLeft.ToString();
                    possibleVehicle.Vehicle.m_VehiclesDictionary["Percent of power left"] = Vehicle.CalculatePercentOfPowerLeft(possibleVehicle.Vehicle.m_VehiclesDictionary).ToString();
                }
                else
                {
                    throw new WrongTypeOfVehicalException();
                }
            }
            else
            {
                throw new VehicleNotInGarageException(i_LicenseNumber);
            }

            return vehicleExistsInTheGarage;
        }

        public string DataOfVehicle(string i_LicenseNumber)
        {
            VehicleAtTheGarage possibleVehicle = null;
            bool vehicleExistsInTheGarage = m_DictionaryOfVehicles.TryGetValue(i_LicenseNumber, out possibleVehicle);
            StringBuilder vehiclesData = new StringBuilder();

            if (vehicleExistsInTheGarage)
            {
                vehiclesData.Append(string.Format(
                                                "{0}: {1}{2}",
                                                "Owner Name",
                                                possibleVehicle.OwnerName,
                                                Environment.NewLine));
                vehiclesData.Append(string.Format(
                                                "{0}: {1}{2}",
                                                "Owner Phone Number",
                                                possibleVehicle.PhoneNumber,
                                                Environment.NewLine));

                foreach (string key in possibleVehicle.Vehicle.m_VehiclesDictionary.Keys)
                {
                    vehiclesData.Append(string.Format(
                                                "{0}: {1}{2}",
                                                key,
                                                possibleVehicle.Vehicle.m_VehiclesDictionary[key],
                                                Environment.NewLine));
                }
            }
            else
            {
                throw new VehicleNotInGarageException(i_LicenseNumber);
            }

            return vehiclesData.ToString();
        }
    }
}