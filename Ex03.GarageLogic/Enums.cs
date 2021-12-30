using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }

    public enum eColor
    {
        Red,
        White,
        Black,
        Blue,
    }

    public enum eDoorsNumber
    {
        Two = 2,
        Three,
        Four,
        Five,
    }

    public enum ePowerUnitType
    {
        Electric,
        Fuel
    }

    public enum eVehicleRepairStatus
    {
        InRepair,
        Fixed,
        Paid
    }

    public enum eLicenseType
    {
        A,
        A2,
        AA,
        B
    }

    public enum eWheelsCount
    {
        One = 1,
        Two,
        Four = 4,
        Sixteen = 16
    }

    public enum eVehicleType
    {
        FuelMotorcycle,
        ElectricMotorcycle,
        FuelCar,
        ElectricCar,
        Truck
    }
}