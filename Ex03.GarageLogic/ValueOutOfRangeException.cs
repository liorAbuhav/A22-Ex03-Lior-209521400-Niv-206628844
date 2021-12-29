using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        #region Data Members
        private float m_MaxValue;
        private float m_MinValue;
        #endregion

        #region Private Constructor
        private ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_ExeptionMessage) : base(i_ExeptionMessage)
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }
        #endregion

        #region Static Method
        public static ValueOutOfRangeException CreateExeption(float i_MaxValue, float i_MinValue)
        {
            return new ValueOutOfRangeException(i_MaxValue, i_MinValue, string.Format("invalid value, expected between:{0} {1}", i_MinValue, i_MaxValue));
        }
        #endregion
    }
}