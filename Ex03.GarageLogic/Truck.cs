using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        #region Data Members
        private bool m_IsDrivingRefregiratedContents;
        private float m_CargoVolume;
        #endregion

        #region Properties
        public bool IsDrivingRefregiratedContents
        {
            get
            {
                return this.m_IsDrivingRefregiratedContents;
            }
            set
            {
                this.m_IsDrivingRefregiratedContents = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return this.m_CargoVolume;
            }
            set
            {
                this.m_CargoVolume = value;
            }
        }
        #endregion

        #region Constructor
        public Truck(float i_WheelMaxAirPressureSetByTheManufacturer, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, bool i_IsDrivingRefregiratedContents, float i_CargoVolume, PowerUnit i_truckEngine)
            : base(i_WheelMaxAirPressureSetByTheManufacturer, i_truckEngine, i_WheelManufacturerName, eWheelsCount.Sixteen, i_ModelName, i_LicenseNumber)
        {
            this.m_IsDrivingRefregiratedContents = i_IsDrivingRefregiratedContents;
            this.m_CargoVolume = i_CargoVolume;
            base.PowerUnit = i_truckEngine;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return String.Format("Truck: refrigirated contents-{0}, cargo volume-{1}, \npowerUnit-{2}\n", this.m_IsDrivingRefregiratedContents, this.m_CargoVolume, (this.PowerUnit as Engine).ToString()) + base.ToString();
        }
        #endregion

        #region Static Methods
        public static Truck CreateTruck(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName = null, string i_LicenseNumber = null,
            string i_WheelManufacturerName = null, bool i_IsDrivingRefregiratedContents = false, float i_CargoVolume = 0)
        {
            Engine fuelEngine = new Engine(i_EngineFuelAmountToAddInLiters, i_EngineFuelType);

            return new Truck(i_WheelMaxAirPressureSetByTheManufacturer, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_IsDrivingRefregiratedContents, i_CargoVolume, fuelEngine);
        }
        #endregion
    }
}