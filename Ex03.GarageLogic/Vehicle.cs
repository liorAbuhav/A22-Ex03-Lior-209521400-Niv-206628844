using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        #region Data Members
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_RemainingPrecentageOfEnergy;
        private List<Wheel> m_Wheels;
        private PowerUnit m_PowerUnit;
        #endregion

        #region Properties
        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                this.m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return this.m_LicenseNumber;
            }
            set
            {
                this.m_LicenseNumber = value;
            }
        }

        public float RemainingPrecentageOfEnergy
        {
            get
            {
                return this.m_RemainingPrecentageOfEnergy;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return this.m_Wheels;
            }
            set
            {
                if (value.Count != this.m_Wheels.Count)
                {
                    throw new ArgumentException();
                }

                this.m_Wheels = value;
            }
        }

        public PowerUnit PowerUnit
        {
            get
            {
                return this.m_PowerUnit;
            }
            set
            {
                this.m_PowerUnit = value;
            }
        }
        #endregion

        #region Overrides
        public override int GetHashCode()
        {
            return this.m_LicenseNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            Vehicle toCompateTo = obj as Vehicle;

            if (toCompateTo != null)
            {
                equals = this.LicenseNumber == toCompateTo.LicenseNumber;
            }

            return equals;
        }

        public static bool operator ==(Vehicle i_LeftVehicleToOperand, Vehicle i_RightVehicleToOperand)
        {
            bool areVehiclesEqual;

            if (i_LeftVehicleToOperand is null && i_RightVehicleToOperand is null)
            {
                areVehiclesEqual = true;
            }
            else
            {
                areVehiclesEqual = i_LeftVehicleToOperand.Equals(i_RightVehicleToOperand);
            }

            return areVehiclesEqual;
        }

        public static bool operator !=(Vehicle i_LeftVehicleToOperand, Vehicle i_RightVehicleToOperand) => !(i_LeftVehicleToOperand == i_RightVehicleToOperand);
        #endregion

        #region Constructor 
        public Vehicle(float i_WheelMaxAirPressureSetByTheManufacturer, string i_WheelManufacturerName = null, eWheelsCount i_WheelsCount = default(eWheelsCount), string i_ModelName = null, string i_LicenseNumber = null)
        {
            this.m_ModelName = i_ModelName ?? string.Empty;
            this.m_LicenseNumber = i_LicenseNumber ?? string.Empty;
            this.m_RemainingPrecentageOfEnergy = 0;
            this.m_Wheels = this.getWheelsList(i_WheelMaxAirPressureSetByTheManufacturer, i_WheelsCount, i_WheelManufacturerName);
        }
        #endregion

        #region Public Methods
        public float GetWheelMaxAirPressureSetByTheManufacturer()
        {
            Wheel vehicleWheel = this.Wheels.First();
            return vehicleWheel.MaxAirPressureSetByTheManufacturer;
        }

        public void InflateVehicleWheelsToMaximum()
        {
            foreach (Wheel wheelToInflate in this.m_Wheels)
            {
                wheelToInflate.InflateWheelToMaximum();
            }
        }

        public void UpdateRemainingPrecentageOfEnergy()
        {
            this.m_RemainingPrecentageOfEnergy = this.PowerUnit.GetEnergyRatio();
        }
        #endregion

        #region Private Methods
        private List<Wheel> getWheelsList(float i_WheelMaxAirPressureSetByTheManufacturer, eWheelsCount i_WheelsCount, string i_WheelManufacturerName = null)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < (int)i_WheelsCount; i++)
            {
                wheels.Add(new Wheel(i_WheelMaxAirPressureSetByTheManufacturer, i_WheelManufacturerName));
            }

            return wheels;
        }
        #endregion
    }
}