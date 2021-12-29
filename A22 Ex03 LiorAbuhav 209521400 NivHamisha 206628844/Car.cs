using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A22_Ex03_LiorAbuhav_209521400_NivHamisha_206628844
{
    public enum Color
    {
        Red,
        White,
        Black, 
        Blue
    }

    public class Car: Vehicle
    {
        private Color m_Color;
        private int m_DoorsNumber; //should be an enum?
    }
}
