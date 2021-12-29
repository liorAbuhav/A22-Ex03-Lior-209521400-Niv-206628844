using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A22_Ex03_LiorAbuhav_209521400_NivHamisha_206628844
{
    public abstract class Engine
    {
        private float m_CurrentEnergyRate;
        private float m_MaxEnergyRate;
        public abstract void LoadEnergy();
    }
}
