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

        #region Methods
        public void Charge(float i_HoursToAdd)
        {
            base.loadEnergy(i_HoursToAdd);
        }
        #endregion
    }
}