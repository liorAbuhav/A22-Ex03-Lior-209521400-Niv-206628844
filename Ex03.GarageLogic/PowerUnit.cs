using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class PowerUnit
    {
        #region Data Members
        private float m_CurrentEnergyRate;
        private float m_MaxEnergyRate;
        #endregion
        
        #region Properties
        protected float CurrentEnergyRate
        {
            get
            {
                return this.m_CurrentEnergyRate;
            }
            set
            {
                this.m_CurrentEnergyRate = value;
            }
        }

        protected float MaxEnergyRate
        {
            get
            {
                return this.m_MaxEnergyRate;
            }
            set
            {
                this.m_MaxEnergyRate = value;
            }
        }
        #endregion

        #region Constructor
        public PowerUnit(float i_MaxEnergyRate)
        {
            this.m_CurrentEnergyRate = 0;
            this.m_MaxEnergyRate = i_MaxEnergyRate;
        }
        #endregion

        #region Overrides
        public override int GetHashCode()
        {
            return this.m_MaxEnergyRate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            PowerUnit toCompateTo = obj as PowerUnit;

            if (toCompateTo != null)
            {
                equals = this.m_MaxEnergyRate == toCompateTo.MaxEnergyRate;
            }

            return equals;
        }

        public static bool operator ==(PowerUnit i_LeftPowerUnitToOperand, PowerUnit i_RightPowerUnitToOperand)
        {
            bool areVehiclesEqual;

            if (i_LeftPowerUnitToOperand is null && i_RightPowerUnitToOperand is null)
            {
                areVehiclesEqual = true;
            }
            else
            {
                areVehiclesEqual = i_LeftPowerUnitToOperand.Equals(i_RightPowerUnitToOperand);
            }

            return areVehiclesEqual;
        }

        public static bool operator !=(PowerUnit i_LeftPowerUnitToOperand, PowerUnit i_RightPowerUnitToOperand)
            => !(i_LeftPowerUnitToOperand == i_RightPowerUnitToOperand);
        #endregion

        #region Methods
        public float GetEnergyRatio()
        {
            return this.m_CurrentEnergyRate / m_MaxEnergyRate;
        }

        protected void loadEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd < 0 || this.m_CurrentEnergyRate + i_EnergyToAdd > this.m_MaxEnergyRate)
            {
                throw ValueOutOfRangeException.CreateExeption(0, this.m_MaxEnergyRate);
            }

            this.m_CurrentEnergyRate += i_EnergyToAdd;
        }
        #endregion
    }
}