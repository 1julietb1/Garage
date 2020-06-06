using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_minValue;
        private float m_maxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(
          string.Format(
              "Invalid value range: minimum value possible is {0}, and maximum value possible is {1}",
              i_MinValue,
              i_MaxValue))
        {
            m_minValue = i_MinValue;
            m_maxValue = i_MaxValue;
        }

        public float MinValue
        {
            get
            {
                return m_minValue;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_maxValue;
            }
        }
    }
}