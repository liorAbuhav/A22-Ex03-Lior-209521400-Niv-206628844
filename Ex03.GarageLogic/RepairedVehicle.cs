using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class RepairedVehicle
    {
        #region Data Members
        private Vehicle m_VehicleInRepair;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleRepairStatus m_VehicleStatus;
        #endregion

        #region Properties
        public eVehicleRepairStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return this.m_VehicleInRepair;
            }
        }
        #endregion

        #region Constructor
        public RepairedVehicle(Vehicle i_VehicleToRepair, string i_OwnerName, string i_OwnerPhone, eVehicleRepairStatus i_VehicleStatus = eVehicleRepairStatus.InRepair)
        {
            this.m_VehicleInRepair = i_VehicleToRepair;
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhone = i_OwnerPhone;
            this.m_VehicleStatus = i_VehicleStatus;
        }
        #endregion
    }
}