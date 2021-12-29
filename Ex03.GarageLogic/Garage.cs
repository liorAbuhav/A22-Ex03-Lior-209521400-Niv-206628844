using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        #region Data Members
        List<RepairedVehicle> m_CurrentGarageVehicles;
        #endregion

        #region Constructor
        public Garage()
        {
        }
        #endregion

        #region Public Methods
        public RepairedVehicle GetRepairedVehicleByLiecenceNumber(string i_LicenceNumber)
        {
            RepairedVehicle repairedVehicle = null;

            foreach (RepairedVehicle repairedVehicleToCheckWith in this.m_CurrentGarageVehicles)
            {
                if (repairedVehicleToCheckWith.Vehicle.LicenseNumber == i_LicenceNumber)
                {
                    repairedVehicle = repairedVehicleToCheckWith;
                    break;
                }
            }

            return repairedVehicle;
        }

        public bool UpdateRepairedVehicleStatusIfExists(RepairedVehicle i_RepairedVehicleToUpdateRepairStatus, eVehicleRepairStatus i_StatusToUpdateTo)
        {
            bool isUpdated = false;
            int repairVehicleIndex = this.m_CurrentGarageVehicles.IndexOf(i_RepairedVehicleToUpdateRepairStatus);

            if (repairVehicleIndex >= 0)
            {
                isUpdated = true;
                this.m_CurrentGarageVehicles[repairVehicleIndex].VehicleStatus = eVehicleRepairStatus.InRepair;
            }

            return isUpdated;
        }

        public bool EnsureVehicleRepairInGarage(RepairedVehicle i_VehicleToEnsureRepairing)
        {
            bool isVehicleExistsInGarage = this.UpdateRepairedVehicleStatusIfExists(i_VehicleToEnsureRepairing, eVehicleRepairStatus.InRepair);

            if (!isVehicleExistsInGarage)
            {
                this.m_CurrentGarageVehicles.Add(i_VehicleToEnsureRepairing);
            }

            return isVehicleExistsInGarage;
        }

        public void InflateVehicleWheelsToMaximum(RepairedVehicle i_VehicleToInflate)
        {
            int repairVehicleToInflate = this.m_CurrentGarageVehicles.IndexOf(i_VehicleToInflate);

            if (repairVehicleToInflate >= 0)
            {
                this.m_CurrentGarageVehicles[repairVehicleToInflate].Vehicle.InflateVehicleWheelsToMaximum();
            }
        }

        public string[] GetAllVehiclesLieceneNumbers()
        {
            RepairedVehicle[] garageRepairedVehicles = this.m_CurrentGarageVehicles.ToArray();
            string[] vehiclesLieceneNumbers = new string[garageRepairedVehicles.Length];

            for (int i = 0; i < garageRepairedVehicles.Length; i++)
            {
                vehiclesLieceneNumbers[i] = garageRepairedVehicles[i].Vehicle.LicenseNumber;
            }

            return vehiclesLieceneNumbers;
        }

        public string[] GetVehiclesLicenseNumbersByRepairStatus(eVehicleRepairStatus i_RepairStatusToFilterBy)
        {
            RepairedVehicle[] repairedVehiclesAfterFilter = getVehiclesfilteredByRepairStatus(i_RepairStatusToFilterBy).ToArray();
            string[] vehiclesLieceneNumbers = new string[repairedVehiclesAfterFilter.Length];

            for (int i = 0; i < repairedVehiclesAfterFilter.Length; i++)
            {
                vehiclesLieceneNumbers[i] = repairedVehiclesAfterFilter[i].Vehicle.LicenseNumber;
            }

            return vehiclesLieceneNumbers;
        }

        public void RefuelVehicle(RepairedVehicle repairedVehicleToRefuel, eFuelType i_FuelTypeToFill, float i_FuelAmountToFill)
        {
            Engine repairVehicleEngineToRefuel = repairedVehicleToRefuel.Vehicle.PowerUnit as Engine;

            if (repairVehicleEngineToRefuel == null)
            {
                throw new ArgumentException("can't refuel vehicle, power unit not engine type");
            }

            repairVehicleEngineToRefuel.Refuel(i_FuelTypeToFill, i_FuelAmountToFill);
            repairedVehicleToRefuel.Vehicle.UpdateRemainingPrecentageOfEnergy();
        }

        public void ChargeVehicle(RepairedVehicle repairedVehicleToCharge, float i_BatteryTimeToAddInMinutes)
        {
            Battery repairVehicleBatteryToCharge = repairedVehicleToCharge.Vehicle.PowerUnit as Battery;

            if (repairVehicleBatteryToCharge == null)
            {
                throw new ArgumentException("can't charge vehicle, power unit not battery type");
            }

            repairVehicleBatteryToCharge.Charge(i_BatteryTimeToAddInMinutes / 60);
            repairedVehicleToCharge.Vehicle.UpdateRemainingPrecentageOfEnergy();
        }

        public static bool IsVehicleEqualType(Vehicle i_VehicleToCheckWith, Vehicle i_VehicleToCompareTo)
        {
            bool doesVehiclesHaveEqualTypes = false;
            bool vehiclesHasEqualEngins = isVehiclesHasEqualPowerUnits(i_VehicleToCheckWith, i_VehicleToCompareTo);
            bool vehiclesHasEqualWheelMaxAirPressure = i_VehicleToCheckWith.GetWheelMaxAirPressureSetByTheManufacturer() == i_VehicleToCompareTo.GetWheelMaxAirPressureSetByTheManufacturer();

            if (vehiclesHasEqualWheelMaxAirPressure && vehiclesHasEqualEngins)
            {
                doesVehiclesHaveEqualTypes = true;
            }

            return doesVehiclesHaveEqualTypes;
        }
        #endregion

        #region Private Methods
        private static bool isVehiclesHasEqualPowerUnits(Vehicle i_VehicleToCheckWith, Vehicle i_VehicleToCompareTo)
        {
            bool vehiclesHasEqualEngins = false;

            if (i_VehicleToCheckWith.GetType() == i_VehicleToCompareTo.GetType())
            {
                vehiclesHasEqualEngins = i_VehicleToCheckWith.PowerUnit == i_VehicleToCompareTo.PowerUnit;
            }

            return vehiclesHasEqualEngins;
        }

        private List<RepairedVehicle> getVehiclesfilteredByRepairStatus(eVehicleRepairStatus i_RepairStatusToFilterBy)
        {
            List<RepairedVehicle> repairedVehiclesWithStatusFilter = new List<RepairedVehicle>();

            foreach (RepairedVehicle repairedVehicleToCheckWith in this.m_CurrentGarageVehicles)
            {
                if (repairedVehicleToCheckWith.VehicleStatus == i_RepairStatusToFilterBy)
                {
                    repairedVehiclesWithStatusFilter.Add(repairedVehicleToCheckWith);
                }
            }

            return repairedVehiclesWithStatusFilter;
        }
        #endregion
    }
}