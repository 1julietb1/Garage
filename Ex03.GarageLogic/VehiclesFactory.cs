using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    public static class VehiclesFactory
    {
        public enum eTypeOfVehicle
        {
            FuelMotor = 1,
            ElectricalMotor = 2,
            FuelCar = 3,
            ElectricalCar = 4,
            FuelTruck = 5,
        }

        private static void createVehicleDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> wheelKeys = Wheel.GetWheelProperties();
            List<string> vehicleKeys = Vehicle.GetVehicleProperties();

            addListToDictionaryWithValueNull(wheelKeys, io_DictionaryToAdd);
            addListToDictionaryWithValueNull(vehicleKeys, io_DictionaryToAdd);
        }

        private static void addFuelPropToDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> fuelKeys = Fuel.GetFuelProperties();
            addListToDictionaryWithValueNull(fuelKeys, io_DictionaryToAdd);
        }

        private static void addElectricPropToDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> electricalKeys = Electrical.GetElectricalProperties();
            addListToDictionaryWithValueNull(electricalKeys, io_DictionaryToAdd);
        }

        private static void addCarPropToDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> carKeys = Car.GetCarProperties();
            addListToDictionaryWithValueNull(carKeys, io_DictionaryToAdd);
        }

        private static void addMotorcyclePropToDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> motorcycleKeys = Motorcycle.GetMotorcycleProperties();
            addListToDictionaryWithValueNull(motorcycleKeys, io_DictionaryToAdd);
        }

        private static void addTruckPropToDictionary(Dictionary<string, string> io_DictionaryToAdd)
        {
            List<string> truckKeys = Truck.GetTruckProperties();
            addListToDictionaryWithValueNull(truckKeys, io_DictionaryToAdd);
        }

        private static void addListToDictionaryWithValueNull(
            List<string> i_ListToAdd,
            Dictionary<string, string> io_VehicleDictionary)
        {
            foreach (string stringToAdd in i_ListToAdd)
            {
                io_VehicleDictionary.Add(stringToAdd, null);
            }
        }

        private static Car createCar(Dictionary<string, string> io_VehicleDictionary)
        {
            addDefinitionsToDictionary(
                io_VehicleDictionary,
                "55",
                "1.8",
                "31",
                "4",
                true);

            return new Car(io_VehicleDictionary);
        }

        private static Motorcycle createMotorcycle(Dictionary<string, string> io_VehicleDictionary)
        {
            addDefinitionsToDictionary(
                io_VehicleDictionary,
                "8",
                "1.4",
                "33",
                "2",
                false);

            return new Motorcycle(io_VehicleDictionary);
        }

        private static void addDefinitionsToDictionary(
                                Dictionary<string, string> io_VehicleDictionary,
                                string MaxFuel,
                                string MaxBaterry,
                                string MaxAirPressuare,
                                string NumberOfWeels,
                                bool AddRepairFlag)
        {
            if (io_VehicleDictionary.ContainsKey("Fuel type") == true)
            {
                io_VehicleDictionary.Add("Maximum fuel possible", MaxFuel);
            }
            else
            {
                io_VehicleDictionary.Add("Maximum battery possible", MaxBaterry);
            }

            io_VehicleDictionary.Add("Wheels maximum air pressure", MaxAirPressuare);
            io_VehicleDictionary.Add("Number of wheels", NumberOfWeels);

            if (AddRepairFlag)
            {
                io_VehicleDictionary.Add("Vehicle's status", "In Repair");
            }
        }

        private static Truck createTruck(Dictionary<string, string> io_VehicleDictionary)
        {
            io_VehicleDictionary.Add("Maximum fuel possible", "110");
            io_VehicleDictionary.Add("Wheels maximum air pressure", "26");
            io_VehicleDictionary.Add("Number of wheels", "12");

            return new Truck(io_VehicleDictionary);
        }

        public static Vehicle SelectConstructor(int i_TypeOfVehicle)
        {
            Vehicle newVehicle = null;
            Dictionary<string, string> vehicleDictionary = new Dictionary<string, string>();

            createVehicleDictionary(vehicleDictionary);
            switch ((eTypeOfVehicle)i_TypeOfVehicle)
            {
                case eTypeOfVehicle.FuelCar:
                    addFuelPropToDictionary(vehicleDictionary);
                    addCarPropToDictionary(vehicleDictionary);
                    vehicleDictionary["Fuel type"] = "Octan96";
                    newVehicle = createCar(vehicleDictionary);
                    break;
                case eTypeOfVehicle.ElectricalCar:
                    addElectricPropToDictionary(vehicleDictionary);
                    addCarPropToDictionary(vehicleDictionary);
                    newVehicle = createCar(vehicleDictionary);
                    break;
                case eTypeOfVehicle.FuelMotor:
                    addFuelPropToDictionary(vehicleDictionary);
                    addMotorcyclePropToDictionary(vehicleDictionary);
                    vehicleDictionary["Fuel type"] = "Octan95";
                    newVehicle = createMotorcycle(vehicleDictionary);
                    break;

                case eTypeOfVehicle.ElectricalMotor:
                    addElectricPropToDictionary(vehicleDictionary);
                    addMotorcyclePropToDictionary(vehicleDictionary);
                    newVehicle = createMotorcycle(vehicleDictionary);
                    break;
                case eTypeOfVehicle.FuelTruck:
                    addFuelPropToDictionary(vehicleDictionary);
                    addTruckPropToDictionary(vehicleDictionary);
                    vehicleDictionary["Fuel type"] = "Soler";
                    newVehicle = createTruck(vehicleDictionary);
                    break;
            }

            return newVehicle;
        }
    }
}
