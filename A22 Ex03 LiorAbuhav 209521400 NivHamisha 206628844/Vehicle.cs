using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A22_Ex03_LiorAbuhav_209521400_NivHamisha_206628844
{
    public enum VehicleCondition
    {
        InRepair,
        Fixed,
        Paid
    }
    public class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_remainingPrecentageOfEnergy;
        private List<Wheel> m_Wheels;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private VehicleCondition m_VehicleCondition;
        private Engine m_Engine;
    }
}
