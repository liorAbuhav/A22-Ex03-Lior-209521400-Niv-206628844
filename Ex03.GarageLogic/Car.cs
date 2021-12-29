using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        #region Data Members
        private eColor m_CarColor;
        private eDoorsNumber m_DoorsNumber;
        #endregion

        #region Properties
        public eColor CarColor
        {
            get
            {
                return this.m_CarColor;
            }
            set
            {
                this.m_CarColor = value;
            }
        }

        public eDoorsNumber DoorsNumber
        {
            get
            {
                return this.m_DoorsNumber;
            }
            set
            {
                this.m_DoorsNumber = value;
            }
        }
        #endregion

        #region Private Constructor
        private Car(float i_WheelMaxAirPressureSetByTheManufacturer, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, eColor i_CarColor, eDoorsNumber i_DoorsNumber, PowerUnit i_carEngine)
            : base(i_WheelMaxAirPressureSetByTheManufacturer, i_WheelManufacturerName, eWheelsCount.Four, i_ModelName, i_LicenseNumber)
        {
            this.m_DoorsNumber = i_DoorsNumber;
            this.m_CarColor = i_CarColor;
            base.PowerUnit = i_carEngine;
        }
        #endregion

        #region Static Methods
        public static Car CreateFuelCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName = null, string i_LicenseNumber = null,
            string i_WheelManufacturerName = null, eColor i_CarColor = default(eColor),
            eDoorsNumber i_DoorsNumber = default(eDoorsNumber))
        {
            Engine engine = new Engine(i_EngineFuelAmountToAddInLiters, i_EngineFuelType);

            return new Car(i_WheelMaxAirPressureSetByTheManufacturer, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_CarColor, i_DoorsNumber, engine);
        }

        public static Car CreateElectricCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
            string i_ModelName = null, string i_LicenseNumber = null, string i_WheelManufacturerName = null,
            eColor i_CarColor = default(eColor), eDoorsNumber i_DoorsNumber = default(eDoorsNumber))
        {
            Battery battery = new Battery(i_MaxBatteryTimeInHours);

            return new Car(i_WheelMaxAirPressureSetByTheManufacturer, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_CarColor, i_DoorsNumber, battery);
        }
        #endregion
    }
}