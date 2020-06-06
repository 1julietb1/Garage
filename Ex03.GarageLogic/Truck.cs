using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    public class Truck : Vehicle
    {
        private bool m_isCarringHazardousMaterials;
        private float m_volumeOfCargo;

        public Truck(Dictionary<string, string> i_TruckProperties)

            : base(i_TruckProperties)
        {
        }

        public bool IsCarringHazardousMaterials
        {
            get
            {
                return m_isCarringHazardousMaterials;
            }

            set
            {
                m_isCarringHazardousMaterials = value;
            }
        }

        public float VolumeOfCargo
        {
            get
            {
                return m_volumeOfCargo;
            }

            set
            {
                m_volumeOfCargo = value;
            }
        }

        public static List<string> GetTruckProperties()
        {
            List<string> truckPropertiesList = new List<string>();
            truckPropertiesList.Add("Carries hazardous materials");
            truckPropertiesList.Add("Volume of cargo");
            return truckPropertiesList;
        }

        internal override string VehicleCharacteristic()
        {
            return string.Format(
                "Is carrying hazardous materials? {0}, The maximal weight allowed: {1}",
                m_isCarringHazardousMaterials,
                m_volumeOfCargo);
        }
    }
}
