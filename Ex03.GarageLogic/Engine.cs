using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Engine : PowerUnit
    {
        #region Data Members
        private eFuelType m_FuelType;
        #endregion

        #region Properties
        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }
        #endregion

        #region Constructor
        public Engine(float i_MaxEnergyRate, eFuelType i_FuelType) : base(i_MaxEnergyRate)
        {
            this.m_FuelType = i_FuelType;
        }
        #endregion

        #region Methods
        public void Refuel(eFuelType i_EngineFuelType, float i_FuelAmountToAddInLiters)
        {
            if (this.m_FuelType != i_EngineFuelType)
            {
                throw new ArgumentException("can't refuel engine, fuel type doen't match");
            }
            base.loadEnergy(i_FuelAmountToAddInLiters);
        }
        #endregion
    }
}