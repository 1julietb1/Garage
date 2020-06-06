using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    internal class Electrical : Power
    {
        public Electrical(Dictionary<string, string> i_ElectricalProperties)
            : base(i_ElectricalProperties)
        {
        }

        internal void ChargeBattery(float i_AddTime)
        {
            if (i_AddTime + m_PowerLeft > m_MaximumPowerPossible)
            {
                throw new ValueOutOfRangeException(0, m_MaximumPowerPossible - m_PowerLeft);
            }
            else
            {
                m_PowerLeft = m_PowerLeft + i_AddTime;
            }
        }

        public static List<string> GetElectricalProperties()
        {
            List<string> electricalPropertiesList = new List<string>();
            electricalPropertiesList.Add("Current level of baterry");

            return electricalPropertiesList;
        }
    }
}