using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    public abstract class Power
    {
        protected float m_PowerLeft;
        protected float m_MaximumPowerPossible;

        public Power(Dictionary<string, string> i_PowerProperties)
        {
            if (i_PowerProperties.ContainsKey("Fuel type") == true)
            {
                m_MaximumPowerPossible = Vehicle.ValidationFloatType(i_PowerProperties["Maximum fuel possible"]);
            }
            else
            {
                m_MaximumPowerPossible = Vehicle.ValidationFloatType(i_PowerProperties["Maximum battery possible"]);
            }
        }

        public float MaximumPowerPossible
        {
            get
            {
                return m_MaximumPowerPossible;
            }

            set
            {
                m_MaximumPowerPossible = value;
            }
        }

        public float PowerLeft
        {
            get
            {
                return m_PowerLeft;
            }

            set
            {
                m_PowerLeft = value;
            }
        }
    }
}
