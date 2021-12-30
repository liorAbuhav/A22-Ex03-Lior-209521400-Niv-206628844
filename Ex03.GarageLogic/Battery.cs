using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Battery : PowerUnit
    {
        #region Constructor
        public Battery(float i_MaxBatteryTimeInHours) : base(i_MaxBatteryTimeInHours)
        {
        }
        #endregion

        #region Overrides
        public override string ToString() 
        {
            return String.Format("Battery: Max battery time in hours-{0}, current battery time-{1}", this.MaxEnergyRate, this.CurrentEnergyRate);
        }
        #endregion

        #region Methods
        public void Charge(float i_HoursToAdd)
        {
            base.loadEnergy(i_HoursToAdd);
        }
        #endregion
    }
}