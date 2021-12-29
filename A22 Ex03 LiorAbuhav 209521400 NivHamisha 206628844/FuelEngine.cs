using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A22_Ex03_LiorAbuhav_209521400_NivHamisha_206628844
{
    public enum FuelType
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }
    public class FuelEngine : Engine
    {
        private FuelType m_FuelType;
        public override void LoadEnergy()
        {
            throw new NotImplementedException();
        }
    }
}
