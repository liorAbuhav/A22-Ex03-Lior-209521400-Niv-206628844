using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        #region Data Members
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;
        #endregion

        #region Properties
        public eLicenseType LicenseType
        {
            get
            {
                return this.m_LicenseType;
            }
            set
            {
                this.m_LicenseType = value;
            }
        }
        #endregion

        #region Private Constructor
        private Motorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, int i_EngineCapacity, eLicenseType i_MotorcycleLicenseType, PowerUnit i_MotorcycleEngine)
            : base(i_WheelMaxAirPressureSetByTheManufacturer, i_MotorcycleEngine, i_WheelManufacturerName, eWheelsCount.Two, i_ModelName, i_LicenseNumber)
        {
            this.m_EngineCapacity = i_EngineCapacity;
            this.m_LicenseType = i_MotorcycleLicenseType;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            Engine isEngine = this.PowerUnit as Engine;
            string powerUnitString;

            if (isEngine != null)
            {
                powerUnitString = isEngine.ToString();
            }
            else
            {
                powerUnitString = (this.PowerUnit as Battery).ToString();
            }

            return String.Format("Motorcycle: liecence type-{0}, engine capcacity-{1}, \npowerUnit-{2}\n", this.m_LicenseType, this.m_EngineCapacity, powerUnitString) + base.ToString();
        }
        #endregion

        #region Static Methods
        public static Motorcycle CreateFuelMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName = null, string i_LicenseNumber = null, string i_WheelManufacturerName = null,
            int i_EngineCapacity = 0, eLicenseType i_MotorcycleLicenseType = default(eLicenseType))
        {
            Engine engine = new Engine(i_EngineFuelAmountToAddInLiters, i_EngineFuelType);

            return new Motorcycle(i_WheelMaxAirPressureSetByTheManufacturer, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_EngineCapacity, i_MotorcycleLicenseType, engine);
        }

        public static Motorcycle CreateElectricMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
            string i_ModelName = null, string i_LicenseNumber = null, string i_WheelManufacturerName = null, int i_EngineCapacity = 0,
            eLicenseType i_MotorcycleLicenseType = default(eLicenseType))
        {
            Battery battery = new Battery(i_MaxBatteryTimeInHours);

            return new Motorcycle(i_WheelMaxAirPressureSetByTheManufacturer, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_EngineCapacity, i_MotorcycleLicenseType, battery);
        }
        #endregion
    }
}