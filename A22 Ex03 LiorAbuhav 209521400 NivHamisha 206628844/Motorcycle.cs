using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A22_Ex03_LiorAbuhav_209521400_NivHamisha_206628844
{
    public enum LicenseType
    {
        A,
        A2,
        AA,
        B
    }
    public class Motorcycle: Vehicle
    {
        private LicenseType m_LicenseType;
        private int m_EngineCapacity;
    }
}
