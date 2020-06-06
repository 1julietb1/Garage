using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class Wheel
    {
        private string m_manufacturerName;
        private float m_existingAirPressure;
        private float m_maxAirPressure;

        public Wheel(Dictionary<string, string> i_WheelProperties)
        {
            m_manufacturerName = i_WheelProperties["Wheels manufacturer name"];
            m_maxAirPressure = Vehicle.ValidationFloatType(i_WheelProperties["Wheels maximum air pressure"]);
        }

        public string ManufacturerName
        {
            get
            {
                return m_manufacturerName;
            }
        }

        public float ExistingAirPressure
        {
            get
            {
                return m_existingAirPressure;
            }

            set
            {
                m_existingAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_maxAirPressure;
            }
        }

        internal void AddAirPressure(float i_AirPressureToAdd)
        {
            if (m_existingAirPressure + i_AirPressureToAdd > m_maxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_maxAirPressure - m_existingAirPressure);
            }
            else
            {
                m_existingAirPressure = m_existingAirPressure + i_AirPressureToAdd;
            }
        }

        public static List<string> GetWheelProperties()
        {
            List<string> wheelPropertiesList = new List<string>();
            wheelPropertiesList.Add("Wheels manufacturer name");
            wheelPropertiesList.Add("Wheels existing air pressure");

            return wheelPropertiesList;
        }
    }
}
