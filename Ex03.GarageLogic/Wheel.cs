using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        #region Data Members
        private string m_ManufacturerName;
        private float m_CurrentAirPressure = 0;
        private float m_MaxAirPressureSetByTheManufacturer;
        #endregion

        #region Properties
        public string ManufacturerName
        {
            get
            {
                return this.m_ManufacturerName;
            }
            set
            {
                this.m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return this.m_CurrentAirPressure;
            }
            set
            {
                this.m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressureSetByTheManufacturer
        {
            get
            {
                return this.m_MaxAirPressureSetByTheManufacturer;
            }
            set
            {
                this.MaxAirPressureSetByTheManufacturer = value;
            }
        }
        #endregion

        #region Constructor
        public Wheel(float i_WheelMaxAirPressureSetByTheManufacturer, string i_WheelManufacturerName = null)
        {
            this.m_ManufacturerName = i_WheelManufacturerName ?? "default";
            this.m_ManufacturerName = i_WheelManufacturerName;
            this.m_MaxAirPressureSetByTheManufacturer = i_WheelMaxAirPressureSetByTheManufacturer;
        }
        #endregion

        #region Methods
        public void InflateWheelToMaximum()
        {
            this.m_CurrentAirPressure = this.m_MaxAirPressureSetByTheManufacturer;
        }
        #endregion
    }
}