using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class GarageController
    {
        #region Data Members
        private Garage m_ControlledGarage;
        private static List<Vehicle> s_AllowedVehiclesTypes;
        private static Dictionary<eVehicleType, MethodInfo> s_VehicleCreationMethods;


        public Dictionary<eVehicleType, MethodInfo> VehicleCreationMethods
        {
            get
            {
                return s_VehicleCreationMethods;
            }
        }
        #endregion

        #region Constructor
        public GarageController()
        {
            List<Vehicle> allowedVehiclesTypes = new List<Vehicle>();
            Car test = Car.CreateFuelCar(29f, 48f, eFuelType.Octan95);
            Type classTypeForReflection = typeof(GarageController);

            allowedVehiclesTypes.Add(Motorcycle.CreateFuelMotorcycle(30f, 5.8f, eFuelType.Octan98));
            allowedVehiclesTypes.Add(Motorcycle.CreateElectricMotorcycle(30f, 2.3f));
            allowedVehiclesTypes.Add(Car.CreateFuelCar(29f, 48f, eFuelType.Octan95));
            allowedVehiclesTypes.Add(Car.CreateElectricCar(29f, 2.6f));
            allowedVehiclesTypes.Add(Truck.CreateTruck(25f, 130f, eFuelType.Soler));
            s_AllowedVehiclesTypes = allowedVehiclesTypes;
            this.m_ControlledGarage = new Garage();
            s_VehicleCreationMethods = new Dictionary<eVehicleType, MethodInfo>();
            s_VehicleCreationMethods.Add(eVehicleType.FuelMotorcycle, classTypeForReflection.GetMethod("CreateFuelMotorcycle"));
            s_VehicleCreationMethods.Add(eVehicleType.ElectricMotorcycle, classTypeForReflection.GetMethod("CreateElectricMotorcycle"));
            s_VehicleCreationMethods.Add(eVehicleType.FuelCar, classTypeForReflection.GetMethod("CreateFuelCar"));
            s_VehicleCreationMethods.Add(eVehicleType.ElectricCar, classTypeForReflection.GetMethod("CreateElectricCar"));
            s_VehicleCreationMethods.Add(eVehicleType.Truck, classTypeForReflection.GetMethod("CreateTruck"));
        }
        #endregion

        public void getVehicleParamsFromUserAndParseByVehicleType(eVehicleType i_SelectedVehicleType, string[] i_UserParamsByVehicleType)
        {
            // todo: here needed to create a validation function (generic / for every type)
            switch (i_SelectedVehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    //this.validate() / this.validateElectricMotorcycle()
                    //this.CreateFuelMotorcycle()
                    break;
                case eVehicleType.ElectricMotorcycle:
                    //this.CreateElectricMotorcycle()
                    break;
                case eVehicleType.FuelCar:
                    //this.CreateFuelCar()
                    break;
                case eVehicleType.ElectricCar:
                    //this.CreateElectricCar()
                    break;
                case eVehicleType.Truck:
                    //this.CreateTruck()
                    break;
            }
        }

        public Motorcycle CreateFuelMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName,
            int i_EngineCapacity, eLicenseType i_MotorcycleLicenseType)
        {
            Motorcycle newFuelMotorcycle = Motorcycle.CreateFuelMotorcycle(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountToAddInLiters, i_EngineFuelType, i_ModelName,
                i_LicenseNumber, i_WheelManufacturerName, i_EngineCapacity, i_MotorcycleLicenseType);

            if (!GarageController.isAllowedVehicleType(newFuelMotorcycle))
            {
                throw (new ArgumentException());
            }
            return newFuelMotorcycle;

        }

        public Motorcycle CreateElectricMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
            string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName, int i_EngineCapacity,
            eLicenseType i_MotorcycleLicenseType)
        {
            Motorcycle newElectricMotorcycle = Motorcycle.CreateElectricMotorcycle(i_WheelMaxAirPressureSetByTheManufacturer, i_MaxBatteryTimeInHours, i_ModelName, i_LicenseNumber,
                i_WheelManufacturerName, i_EngineCapacity, i_MotorcycleLicenseType);

            if (!GarageController.isAllowedVehicleType(newElectricMotorcycle))
            {
                throw (new ArgumentException());
            }

            return newElectricMotorcycle;
        }
        public Car CreateFuelCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, eColor i_CarColor, eDoorsNumber i_DoorsNumber)
        {
            Car newFuelCar = Car.CreateFuelCar(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountToAddInLiters, i_EngineFuelType, i_ModelName, i_LicenseNumber,
                i_WheelManufacturerName, i_CarColor, i_DoorsNumber);

            if (!GarageController.isAllowedVehicleType(newFuelCar))
            {
                throw (new ArgumentException());
            }

            return newFuelCar;
        }

        public Car CreateElectricCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
            string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName,
            eColor i_CarColor, eDoorsNumber i_DoorsNumber)
        {
            Car newElectricCar = Car.CreateElectricCar(i_WheelMaxAirPressureSetByTheManufacturer, i_MaxBatteryTimeInHours, i_ModelName, i_LicenseNumber, i_WheelManufacturerName,
                i_CarColor, i_DoorsNumber);

            if (!GarageController.isAllowedVehicleType(newElectricCar))
            {
                throw (new ArgumentException());
            }

            return newElectricCar;
        }

        public Truck CreateTruck(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountToAddInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, bool i_IsDrivingRefregiratedContents, float i_CargoVolume)
        {
            Truck newTruck = Truck.CreateTruck(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountToAddInLiters, i_EngineFuelType, i_ModelName, i_LicenseNumber,
                i_WheelManufacturerName, i_IsDrivingRefregiratedContents, i_CargoVolume);

            if (!GarageController.isAllowedVehicleType(newTruck))
            {
                throw (new ArgumentException());
            }

            return newTruck;
        }

        #region Public Methods

        public bool InsertVehicleToGarage(Vehicle i_VehicleToRepair, string i_OwnerName, string i_OwnerPhone)
        {
            RepairedVehicle vehicleToEnsureInGarage = new RepairedVehicle(i_VehicleToRepair, i_OwnerName, i_OwnerPhone);
            bool isVehicleAlreadyExistedInGarage = this.m_ControlledGarage.EnsureVehicleRepairInGarage(vehicleToEnsureInGarage);

            return isVehicleAlreadyExistedInGarage;
        }

        public string[] GetAllVehiclesLicenseNumbers()
        {
            return this.m_ControlledGarage.GetAllVehiclesLieceneNumbers();
        }

        public string[] GetAllRepairStatusOptions()
        {
            return Enum.GetNames(typeof(eVehicleRepairStatus));
        }

        public string[] GetVehiclesLicenseNumbersByRepairStatus(eVehicleRepairStatus i_RepairStatusToFilterBy)
        {
            return this.m_ControlledGarage.GetVehiclesLicenseNumbersByRepairStatus(i_RepairStatusToFilterBy);
        }

        public void ChangeVehicleRepairStatus(string i_LicenseNumber, eVehicleRepairStatus i_NewRepairStatus)
        {
            RepairedVehicle vehicleToChangeRepairStatus = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            this.m_ControlledGarage.UpdateRepairedVehicleStatusIfExists(vehicleToChangeRepairStatus, i_NewRepairStatus);
        }

        public void InflateVehicleWheelsToMaximum(string i_LicenseNumber)
        {
            RepairedVehicle repairedVehicleToInflateInGarage = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            this.m_ControlledGarage.InflateVehicleWheelsToMaximum(repairedVehicleToInflateInGarage);
        }

        public string[] GetAllFuelTypeOptions()
        {
            return Enum.GetNames(typeof(eFuelType));
        }

        public void RefualFuelVehicle(string i_LicenseNumber, eFuelType i_FuelTypeToFill, float i_FuelAmountToFill)
        {
            RepairedVehicle repairedVehicleToRefuel = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            this.m_ControlledGarage.RefuelVehicle(repairedVehicleToRefuel, i_FuelTypeToFill, i_FuelAmountToFill);
        }

        public void ChargeElectricVehicle(string i_LicenseNumber, float i_MinutesAmountToCharge)
        {
            RepairedVehicle repairedVehicleToCharge = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            this.m_ControlledGarage.ChargeVehicle(repairedVehicleToCharge, i_MinutesAmountToCharge);
        }

        public RepairedVehicle GetRepairVehicle(string i_LicenseNumber)
        {
            RepairedVehicle repairedVehicle = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            return repairedVehicle;
        }
        #endregion

        #region Private Methods
        private static bool isAllowedVehicleType(Vehicle i_VehicleToCheck)
        {
            bool isAllowedVehicle = false;

            foreach (Vehicle allowedVehicle in s_AllowedVehiclesTypes)
            {
                if (Garage.IsVehicleEqualType(i_VehicleToCheck, allowedVehicle))
                {
                    isAllowedVehicle = true;
                    break;
                }
            }

            return isAllowedVehicle;
        }

        private RepairedVehicle getGarageRepairedVehicleByLiecenceNumber(string i_LicenseNumber)
        {
            RepairedVehicle repairedVehicle = this.m_ControlledGarage.GetRepairedVehicleByLiecenceNumber(i_LicenseNumber);

            if (repairedVehicle == null)
            {
                throw new NullReferenceException(String.Format("vehclie with liecence number {0} does not exists in garage", i_LicenseNumber));
            }

            return repairedVehicle;
        }
        #endregion
    }
}