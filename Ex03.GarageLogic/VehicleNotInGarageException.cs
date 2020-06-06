using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleNotInGarageException : Exception
    {
        private string m_licenceNotFound;

        public VehicleNotInGarageException(string i_LicenceNotFound)
        {
            this.m_licenceNotFound = i_LicenceNotFound;
        }

        public void PrintError()
        {
            Console.WriteLine("Vehicle: {0} was not found in the garage", m_licenceNotFound);
        }
    }
}
