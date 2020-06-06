using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class VehicleAtTheGarage
    {
        private readonly Vehicle m_vehicle;
        private string m_ownerName;
        private string m_phoneNumber;
        private eCarStatus m_carStatus;

        internal enum eCarStatus
        {
            InRepair,
            Fixed,
            Paid,
        }

        public VehicleAtTheGarage(string i_OwnerName, string i_PhoneNumber, Vehicle i_Vehicle)
        {
            m_ownerName = i_OwnerName;
            m_phoneNumber = i_PhoneNumber;
            m_carStatus = eCarStatus.InRepair;
            m_vehicle = i_Vehicle;
        }

        public string OwnerName
        {
            get
            {
                return m_ownerName;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_phoneNumber;
            }
        }

        public eCarStatus CarStatus
        {
            get
            {
                return m_carStatus;
            }

            set
            {
                m_carStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_vehicle;
            }
        }
    }
}
