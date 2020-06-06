using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private Garage m_garage = new Garage();

        internal void StartUI()
        {
            bool exitFlag = false;
            int choice;

            while (!exitFlag)
            {
                showMenu();
                choice = getValidChoiceFromUser();
                commitChoice(choice, ref exitFlag);
            }
        }

        private int getValidChoiceFromUser()
        {
            return getValidIntChoiceFromUser(0, 9);
        }

        private int getValidIntChoiceFromUser(int i_LowerBound, int i_UpperBound)
        {
            int userCoice = 0;
            string stringFromUser = Console.ReadLine();

            bool coiceIsValid = int.TryParse(stringFromUser, out userCoice);
            coiceIsValid = coiceIsValid && userCoice > i_LowerBound && userCoice < i_UpperBound;

            while (!coiceIsValid)
            {
                Console.WriteLine(
                    "Invalid Argument, please type a number between {0} and {1}",
                    i_LowerBound,
                    i_UpperBound);
                stringFromUser = Console.ReadLine();
                coiceIsValid = int.TryParse(stringFromUser, out userCoice);
                coiceIsValid = coiceIsValid && userCoice > i_LowerBound && userCoice < i_UpperBound;
            }

            return userCoice;
        }

        private void commitChoice(int i_ChoiceFromUser, ref bool io_ExitFlag)
        {
            switch (i_ChoiceFromUser)
            {
                case 1:
                    putNewVehicleInGarage();
                    break;
                case 2:
                    showAllLicenceNemberWithFilter();
                    break;
                case 3:
                    changeStatusOfVehicle();
                    break;
                case 4:
                    pumpAirToMaxPressure();
                    break;
                case 5:
                    fuelAVehicle();
                    break;
                case 6:
                    chargeAVehicle();
                    break;
                case 7:
                    showFullStats();
                    break;
                case 8:
                    io_ExitFlag = true;
                    break;
            }
        }

        private void showMenu()
        {
            Console.WriteLine(
@"
Please choose the number of your desired option:
    1) Put a new vehicale in the garage.
    2) Show all License Numbers of vehicles in the garage (filter option inside).
    3) Change the state of a vehicle in the garage.
    4) Pump air to max pressuare to a vehicles weels.
    5) Fuel a fuel powered vehicle
    6) Charge an electricity powered vehicle
    7) Show vehicle's full detailes
    8) Exit");
        }

        private void showFullStats()
        {
            Console.WriteLine("Please insert license number of the vehicle you want to see:");
            string licenseNumFromUser = Console.ReadLine();

            try
            {
                Console.WriteLine(m_garage.DataOfVehicle(licenseNumFromUser));
            }
            catch (VehicleNotInGarageException notFoundException)
            {
                notFoundException.PrintError();
            }
        }

        private void chargeAVehicle()
        {
            Console.WriteLine("Please insert license number of the vehicle you want to Charge:");
            string licenseNumFromUser = Console.ReadLine();
            Console.WriteLine("Please insert number of minutes to charge:");

            string numberOfMinutesStr = Console.ReadLine();
            float numberOfminutes;
            bool isFloat = float.TryParse(numberOfMinutesStr, out numberOfminutes);

            if (!isFloat)
            {
                Console.WriteLine("Invalid argument for number of minutes");
            }
            else
            {
                try
                {
                    m_garage.ChargeBatteryOfVehicleInTheGarage(licenseNumFromUser, numberOfminutes / 60);
                }
                catch (VehicleNotInGarageException vehicleNotFound)
                {
                    vehicleNotFound.PrintError();
                }
                catch (WrongTypeOfVehicalException)
                {
                    Console.WriteLine("Fuel Vehicle cannot be Charged");
                }
                catch (ValueOutOfRangeException rangeOutOfBounds)
                {
                    Console.WriteLine(rangeOutOfBounds.Message);
                }
            }
        }

        private void fuelAVehicle()
        {
            Console.WriteLine("Please insert license number of the vehicle you want to fuel:");
            string licenseNumFromUser = Console.ReadLine();
            Console.WriteLine("Please insert Fuel type (Soler, Octan95, Octan96, Octan98):");
            string fuelType = Console.ReadLine();
            Console.WriteLine("Please insert amout of fuel to add:");
            string amountOfFuelStr = Console.ReadLine();
            float amountOfFuelFloat;

            bool isFloat = float.TryParse(amountOfFuelStr, out amountOfFuelFloat);

            if (!isFloat)
            {
                Console.WriteLine("Invalid argument for amount of fuel to add");
            }
            else
            {
                try
                {
                    m_garage.RefulVehicleInTheGarage(licenseNumFromUser, fuelType, amountOfFuelFloat);
                }
                catch (VehicleNotInGarageException vehicleNotFound)
                {
                    vehicleNotFound.PrintError();
                }
                catch (WrongTypeOfVehicalException)
                {
                    Console.WriteLine("Electric Vehicle cannot be fueled");
                }
                catch (ValueOutOfRangeException rangeOutOfBounds)
                {
                    Console.WriteLine(rangeOutOfBounds.Message);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid type of fuel");
                }
            }
        }

        private void pumpAirToMaxPressure()
        {
            Console.WriteLine("Please insert license number of the vehicle you want to fill air pressuare to Max:");
            string licenseNumFromUser = Console.ReadLine();

            try
            {
                m_garage.AddAirPressureForAllWheels(licenseNumFromUser);
            }
            catch (VehicleNotInGarageException vehicleNotFound)
            {
                vehicleNotFound.PrintError();
            }
        }

        private void changeStatusOfVehicle()
        {
            Console.WriteLine("Please insert license number of the vehicle you want to change it's status:");
            string licenseNumFromUser = Console.ReadLine();
            Console.WriteLine("Please insert the status to change to (InRepair/Fixed/Paid)");
            string statusFromUser = Console.ReadLine();

            try
            {
                m_garage.ChangeStatusOfVehicle(licenseNumFromUser, statusFromUser);
            }
            catch (VehicleNotInGarageException notInGarage)
            {
                notInGarage.PrintError();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid status to change to.");
            }
        }

        private void showAllLicenceNemberWithFilter()
        {
            Console.WriteLine("Please insert filter for license number display (InRepair/Fixed/Paid) or press Enter to skip:");
            string filterFromUser = Console.ReadLine();
            if (string.IsNullOrEmpty(filterFromUser))
            {
                filterFromUser = "None";
            }

            try
            {
                List<string> listOfNumbersToPrint = m_garage.ListOfLicenseNumbers(filterFromUser);

                if (listOfNumbersToPrint.Count == 0)
                {
                    Console.WriteLine("No vehicles match the filter");
                }
                else
                {
                    Console.WriteLine("Vehicles relevent to your search:");
                    foreach (string strToPrint in listOfNumbersToPrint)
                    {
                        Console.WriteLine(strToPrint);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid filter inserted");
            }
        }

        private void putNewVehicleInGarage()
        {
            Console.WriteLine("Please write the license number of the vehicle to add:");
            string licenseNumFromUser = Console.ReadLine();

            while (string.IsNullOrEmpty(licenseNumFromUser))
            {
                Console.WriteLine("License number cant be empty, try again");
                licenseNumFromUser = Console.ReadLine();
            }

            if (m_garage.isVehicleInGarage(licenseNumFromUser))
            {
                Console.WriteLine("Vehicle already in the garage, statuse changed to InRepair");
                m_garage.ChangeStatusOfVehicle(licenseNumFromUser, "InRepair");
            }
            else
            {
                string ownerNameFromUser = getValidOwnerNameFromUser();
                string ownerPhoneNumber = getValidOwnerPhoneNumberFromUser();
                string modelName = getValidNameFromUser("Model Name");
                int vehicaltypeToCreate = getValidVehicleType();

                Vehicle vehicleToCreat = VehiclesFactory.SelectConstructor(vehicaltypeToCreate);
                vehicleToCreat.VehicleModel = modelName;
                vehicleToCreat.LicenseNumber = licenseNumFromUser;
                string weelManufactor = getValidNameFromUser("Weel manufactor");
                float weelCurrentPressuare = getValidCurrentPressuare(getMaxpressuare(vehicaltypeToCreate));

                vehicleToCreat.VehiclesDictionary["Model name"] = modelName;
                vehicleToCreat.VehiclesDictionary["License number"] = licenseNumFromUser;
                vehicleToCreat.VehiclesDictionary["Wheels manufacturer name"] = weelManufactor;
                vehicleToCreat.VehiclesDictionary["Wheels existing air pressure"] = weelCurrentPressuare.ToString();

                if (vehicaltypeToCreate == 1 || vehicaltypeToCreate == 3 || vehicaltypeToCreate == 5)
                {
                    float currentFuel = getValidCurrentFuel(getMaxFuel(vehicaltypeToCreate));
                    vehicleToCreat.VehiclesDictionary["Current amount of fuel"] = currentFuel.ToString();
                    vehicleToCreat.PowerUnit.PowerLeft = currentFuel;
                }
                else
                {
                    float currentElectricityLeft = getValidFloatInRange(
                        "Enter current Electricity in charger:",
                        getMaxElectricity(vehicaltypeToCreate));
                    vehicleToCreat.VehiclesDictionary["Current level of baterry"] = currentElectricityLeft.ToString();
                    vehicleToCreat.PowerUnit.PowerLeft = currentElectricityLeft;
                }

                if (vehicaltypeToCreate == 1 || vehicaltypeToCreate == 2)
                {
                    addInfoFromUserMotorcycle(vehicleToCreat);
                }
                else if (vehicaltypeToCreate == 3 || vehicaltypeToCreate == 4)
                {
                    addInfoFromUserCar(vehicleToCreat);
                }
                else
                {
                    addInfoFromUserTruck(vehicleToCreat);
                }

                vehicleToCreat.VehiclesDictionary["Vehicle's status"] = "In A Repair";
                vehicleToCreat.PercentPowerLeft = Vehicle.CalculatePercentOfPowerLeft(vehicleToCreat.VehiclesDictionary);
                vehicleToCreat.VehiclesDictionary["Percent of power left"] = string.Format("{0:F3}%", vehicleToCreat.PercentPowerLeft);
                m_garage.AddNewCarToTheGarage(vehicleToCreat, ownerNameFromUser, ownerPhoneNumber);
            }
        }

        private void addInfoFromUserCar(Vehicle i_VehicleToChange)
        {
            string[] colorOptions = { "Red", "Blue", "Black", "Gray" };
            string strFromUser = getValidStringFromUser("Enter color of the Car: (Red/Blue/Black/Gray)", colorOptions);
            i_VehicleToChange.VehiclesDictionary["Car color"] = strFromUser;

            string[] doorOptions = { "2", "3", "4", "5" };
            strFromUser = getValidStringFromUser("Enter color of the Car: (2/3/4/5)", doorOptions);
            i_VehicleToChange.VehiclesDictionary["Number of doors"] = strFromUser;
        }

        private string getValidStringFromUser(string i_FirstMessege, string[] i_Options)
        {
            Console.WriteLine(i_FirstMessege);
            string strFromUser = Console.ReadLine();
            bool isEqualtoOneOption = false;

            foreach (string strToCompareTo in i_Options)
            {
                if (strToCompareTo == strFromUser)
                {
                    isEqualtoOneOption = true;
                    break;
                }
            }

            while (!isEqualtoOneOption)
            {
                Console.WriteLine("Invalid input try again");
                strFromUser = Console.ReadLine();
                foreach (string strToCompareTo in i_Options)
                {
                    if (strToCompareTo == strFromUser)
                    {
                        isEqualtoOneOption = true;
                        break;
                    }
                }
            }

            return strFromUser;
        }

        private void addInfoFromUserMotorcycle(Vehicle i_VehicleToChange)
        {
            string[] options = { "A", "A1", "A2", "B" };
            string strFromUser = getValidStringFromUser("Enter License type: (A/A1/A2/B)", options);
            i_VehicleToChange.VehiclesDictionary["License type"] = strFromUser;

            Console.WriteLine("Enter Engine Capacity (only numbers are allowed, can't be empty)");
            strFromUser = Console.ReadLine();
            int capacity = -1;
            bool isValidCapacity = int.TryParse(strFromUser, out capacity);
            while (!isValidCapacity && capacity <= 0)
            {
                Console.WriteLine("Invalid input try again");
                strFromUser = Console.ReadLine();
                isValidCapacity = int.TryParse(strFromUser, out capacity);
            }

            i_VehicleToChange.VehiclesDictionary["Engine capacity"] = capacity.ToString();
        }

        private void addInfoFromUserTruck(Vehicle i_VehicleToChange)
        {
            string[] options = { "Yes", "No" };
            string strFromUser = getValidStringFromUser("Does your truck Carry hazardous materials? (Yes/No)", options);
            if (strFromUser == "Yes")
            {
                ((Truck)i_VehicleToChange).IsCarringHazardousMaterials = true;
            }
            else
            {
                ((Truck)i_VehicleToChange).IsCarringHazardousMaterials = false;
            }

            i_VehicleToChange.VehiclesDictionary["Carries hazardous materials"] = strFromUser;
            float cargoVolume = getValidFloatInRange("Enter Cargo volume", float.MaxValue);
            ((Truck)i_VehicleToChange).VolumeOfCargo = cargoVolume;
            i_VehicleToChange.VehiclesDictionary["Volume of cargo"] = cargoVolume.ToString();
        }

        private float getMaxElectricity(int i_VehicaltypeToCreate)
        {
            float result = 0;
            switch (i_VehicaltypeToCreate)
            {
                case 2:
                    result = 1.4F;
                    break;
                case 4:
                    result = 1.8F;
                    break;
            }

            return result;
        }

        private float getMaxFuel(int i_VehicaltypeToCreate)
        {
            float result = 0;
            switch (i_VehicaltypeToCreate)
            {
                case 1:
                    result = 8;
                    break;
                case 3:
                    result = 55;
                    break;
                case 5:
                    result = 110;
                    break;
            }

            return result;
        }

        private float getValidCurrentFuel(float i_MaxFuel)
        {
            return getValidFloatInRange("Enter current Fuel in tank:", i_MaxFuel);
        }

        private float getMaxpressuare(int i_VehicaltypeToCreate)
        {
            float result = 0;
            switch (i_VehicaltypeToCreate)
            {
                case 1:
                    result = 33;
                    break;
                case 2:
                    result = 33;
                    break;
                case 3:
                    result = 31;
                    break;
                case 4:
                    result = 31;
                    break;
                case 5:
                    result = 26;
                    break;
            }

            return result;
        }

        private float getValidCurrentPressuare(float i_MaxPressuare)
        {
            return getValidFloatInRange("Enter current air pressure in the weels:", i_MaxPressuare);
        }

        private float getValidFloatInRange(string i_FirstMessege, float i_MaxRange)
        {
            float result = 0;
            Console.WriteLine("{0} (positive below {1})", i_FirstMessege, i_MaxRange);
            string pressuareFromUser = Console.ReadLine();

            bool isValid = float.TryParse(pressuareFromUser, out result) && result <= i_MaxRange && result >= 0;
            while (!isValid)
            {
                Console.WriteLine("Invalid input try again");
                pressuareFromUser = Console.ReadLine();
                isValid = float.TryParse(pressuareFromUser, out result) && result <= i_MaxRange && result >= 0;
            }

            return result;
        }

        private string getValidNameFromUser(string i_NameOfPropertyToGet)
        {
            Console.WriteLine("Enter non-Empty {0}:", i_NameOfPropertyToGet);
            string modelName = Console.ReadLine();
            while (string.IsNullOrEmpty(modelName))
            {
                Console.WriteLine("{0} cannot be empty, try again", i_NameOfPropertyToGet);
                modelName = Console.ReadLine();
            }

            return modelName;
        }

        private int getValidVehicleType()
        {
            Console.WriteLine(
@"Choose the type of the vehicle to put in the garage:
1) Fuel Motorcycle
2) Electrical Motocycle
3) Fuel car
4) Electrical Car
5) Truck");

            return getValidIntChoiceFromUser(0, 6);
        }

        private string getValidOwnerPhoneNumberFromUser()
        {
            Console.WriteLine("Please insert the phone number of the owner of the vehicle: (only numbers are allowed, can't be empty)");
            string OwnerNumberFromUser = Console.ReadLine();

            while (!isAllDigitsAndNotEmpty(OwnerNumberFromUser))
            {
                Console.WriteLine("Invalid owner phone number try again (only numbers are allowed, can't be empty)");
                OwnerNumberFromUser = Console.ReadLine();
            }

            return OwnerNumberFromUser;
        }

        private string getValidOwnerNameFromUser()
        {
            Console.WriteLine("Please insert the name of the owner of the vehicle: (only letters and space are allowed, can't be empty)");
            string OwnerNameFromUser = Console.ReadLine();

            while (!isAllLettersAndNotEmpty(OwnerNameFromUser))
            {
                Console.WriteLine("Invalid owner name try again (Only letters and space are allowed, can't be empty)");
                OwnerNameFromUser = Console.ReadLine();
            }

            return OwnerNameFromUser;
        }

        private bool isAllLettersAndNotEmpty(string i_StringToCheck)
        {
            bool result = true;
            i_StringToCheck = i_StringToCheck.Trim();
            if (!string.IsNullOrEmpty(i_StringToCheck))
            {
                foreach (char charToCheck in i_StringToCheck)
                {
                    if (!char.IsLetter(charToCheck) && charToCheck != ' ')
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool isAllDigitsAndNotEmpty(string i_StringToCheck)
        {
            bool result = true;
            i_StringToCheck = i_StringToCheck.Trim();
            if (!string.IsNullOrEmpty(i_StringToCheck))
            {
                foreach (char charToCheck in i_StringToCheck)
                {
                    if (!char.IsDigit(charToCheck))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
