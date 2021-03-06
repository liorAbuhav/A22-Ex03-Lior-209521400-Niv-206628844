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
            s_VehicleCreationMethods.Add(eVehicleType.ElectricMotorcycle, classTypeForReflection.GetMethod("createElectricMotorcycle"));
            s_VehicleCreationMethods.Add(eVehicleType.FuelCar, classTypeForReflection.GetMethod("createFuelCar"));
            s_VehicleCreationMethods.Add(eVehicleType.ElectricCar, classTypeForReflection.GetMethod("createElectricCar"));
            s_VehicleCreationMethods.Add(eVehicleType.Truck, classTypeForReflection.GetMethod("createTruck"));
        }
        #endregion

        #region Public Methods
        public void GetVehicleParamsFromUserAndParseByVehicleType(eVehicleType i_SelectedVehicleType, string[] i_UserParamsByVehicleType, string i_VehicleOwnerName, string i_VehicleOwnerPhone)
        {
            switch (i_SelectedVehicleType)
            {
                case eVehicleType.FuelMotorcycle:
                    Motorcycle fuelMotorcycle = GarageController.ValidateFuelMotorcycle(i_UserParamsByVehicleType);
                    this.InsertVehicleToGarage(fuelMotorcycle, i_VehicleOwnerName, i_VehicleOwnerPhone);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    Motorcycle electricMotorcycle = GarageController.ValidateElectricMotorcycle(i_UserParamsByVehicleType);
                    this.InsertVehicleToGarage(electricMotorcycle, i_VehicleOwnerName, i_VehicleOwnerPhone);
                    break;
                case eVehicleType.FuelCar:
                    Car fuelCar = GarageController.ValidateFuelCar(i_UserParamsByVehicleType);
                    this.InsertVehicleToGarage(fuelCar, i_VehicleOwnerName, i_VehicleOwnerPhone);
                    break;
                case eVehicleType.ElectricCar:
                    Car electricCar = GarageController.ValidateElectricCar(i_UserParamsByVehicleType);
                    this.InsertVehicleToGarage(electricCar, i_VehicleOwnerName, i_VehicleOwnerPhone);
                    break;
                case eVehicleType.Truck:
                    Truck truck = GarageController.ValidateTruck(i_UserParamsByVehicleType);
                    this.InsertVehicleToGarage(truck, i_VehicleOwnerName, i_VehicleOwnerPhone);
                    break;
            }
        }

        public static Motorcycle CreateFuelMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName,
            int i_EngineCapacity, eLicenseType i_MotorcycleLicenseType)
        {
            Motorcycle newFuelMotorcycle = Motorcycle.CreateFuelMotorcycle(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountInLiters, i_EngineFuelType, i_ModelName,
                i_LicenseNumber, i_WheelManufacturerName, i_EngineCapacity, i_MotorcycleLicenseType);

            if (!GarageController.isAllowedVehicleType(newFuelMotorcycle))
            {
                throw (new ArgumentException("vehicle typw not supported in garage, see allowed vehicles for reference"));
            }

            return newFuelMotorcycle;
        }

        public static Motorcycle ValidateFuelMotorcycle(string[] i_UserParamsByVehicleType)
        {
            float convertedWheelMaxAirPressureSetByTheManufacturer;
            float convertedEngineFuelAmountInLiters;
            eFuelType convertedEngineFuelType;
            string convertedModelName;
            string convertedLicenseNumber;
            string convertedWheelManufacturerName;
            int convertedEngineCapacity;
            eLicenseType convertedMotorcycleLicenseType;

            convertedWheelMaxAirPressureSetByTheManufacturer = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[0]);
            convertedEngineFuelAmountInLiters = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[1]);
            convertedEngineFuelType = TypesValidators.ValidateEnum<eFuelType>(i_UserParamsByVehicleType[2]);
            convertedModelName = i_UserParamsByVehicleType[3];
            convertedLicenseNumber = i_UserParamsByVehicleType[4];
            convertedWheelManufacturerName = i_UserParamsByVehicleType[5];
            convertedEngineCapacity = TypesValidators.ValidateInt(i_UserParamsByVehicleType[6]);
            convertedMotorcycleLicenseType = TypesValidators.ValidateEnum<eLicenseType>(i_UserParamsByVehicleType[7]);

            return GarageController.CreateFuelMotorcycle(convertedWheelMaxAirPressureSetByTheManufacturer, convertedEngineFuelAmountInLiters, convertedEngineFuelType,
                convertedModelName, convertedLicenseNumber, convertedWheelManufacturerName, convertedEngineCapacity, convertedMotorcycleLicenseType);
        }

        public static Motorcycle CreateElectricMotorcycle(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
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

        public static Motorcycle ValidateElectricMotorcycle(string[] i_UserParamsByVehicleType)
        {
            float convertedWheelMaxAirPressureSetByTheManufacturer;
            float convertedMaxBatteryTimeInHours;
            string convertedModelName;
            string convertedLicenseNumber;
            string convertedWheelManufacturerName;
            int convertedEngineCapacity;
            eLicenseType convertedMotorcycleLicenseType;

            convertedWheelMaxAirPressureSetByTheManufacturer = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[0]);
            convertedMaxBatteryTimeInHours = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[1]);
            convertedModelName = i_UserParamsByVehicleType[2];
            convertedLicenseNumber = i_UserParamsByVehicleType[3];
            convertedWheelManufacturerName = i_UserParamsByVehicleType[4];
            convertedEngineCapacity = TypesValidators.ValidateInt(i_UserParamsByVehicleType[5]);
            convertedMotorcycleLicenseType = TypesValidators.ValidateEnum<eLicenseType>(i_UserParamsByVehicleType[6]);

            return GarageController.CreateElectricMotorcycle(convertedWheelMaxAirPressureSetByTheManufacturer, convertedMaxBatteryTimeInHours,
                convertedModelName, convertedLicenseNumber, convertedWheelManufacturerName, convertedEngineCapacity, convertedMotorcycleLicenseType);
        }

        public static Car CreateFuelCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, eColor i_CarColor, eDoorsNumber i_DoorsNumber)
        {
            Car newFuelCar = Car.CreateFuelCar(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountInLiters, i_EngineFuelType, i_ModelName, i_LicenseNumber,
                i_WheelManufacturerName, i_CarColor, i_DoorsNumber);

            if (!GarageController.isAllowedVehicleType(newFuelCar))
            {
                throw (new ArgumentException());
            }

            return newFuelCar;
        }

        public static Car ValidateFuelCar(string[] i_UserParamsByVehicleType)
        {
            float convertedWheelMaxAirPressureSetByTheManufacturer;
            float convertedEngineFuelAmountInLiters;
            eFuelType convertedEngineFuelType;
            string convertedModelName;
            string convertedLicenseNumber;
            string convertedWheelManufacturerName;
            eColor convertedCarColor;
            eDoorsNumber convertedDoorsNumber;

            convertedWheelMaxAirPressureSetByTheManufacturer = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[0]);
            convertedEngineFuelAmountInLiters = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[1]);
            convertedEngineFuelType = TypesValidators.ValidateEnum<eFuelType>(i_UserParamsByVehicleType[2]);
            convertedModelName = i_UserParamsByVehicleType[3];
            convertedLicenseNumber = i_UserParamsByVehicleType[4];
            convertedWheelManufacturerName = i_UserParamsByVehicleType[5];
            convertedCarColor = TypesValidators.ValidateEnum<eColor>(i_UserParamsByVehicleType[6]);
            convertedDoorsNumber = TypesValidators.ValidateEnum<eDoorsNumber>(i_UserParamsByVehicleType[7]);

            return GarageController.CreateFuelCar(convertedWheelMaxAirPressureSetByTheManufacturer, convertedEngineFuelAmountInLiters,
                convertedEngineFuelType, convertedModelName, convertedLicenseNumber, convertedWheelManufacturerName, convertedCarColor, convertedDoorsNumber);
        }

        public static Car CreateElectricCar(float i_WheelMaxAirPressureSetByTheManufacturer, float i_MaxBatteryTimeInHours,
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

        public static Car ValidateElectricCar(string[] i_UserParamsByVehicleType)
        {
            float convertedWheelMaxAirPressureSetByTheManufacturer;
            float convertedMaxBatteryTimeInHours;
            string convertedModelName;
            string convertedLicenseNumber;
            string convertedWheelManufacturerName;
            eColor convertedCarColor;
            eDoorsNumber convertedDoorsNumber;

            convertedWheelMaxAirPressureSetByTheManufacturer = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[0]);
            convertedMaxBatteryTimeInHours = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[1]);
            convertedModelName = i_UserParamsByVehicleType[2];
            convertedLicenseNumber = i_UserParamsByVehicleType[3];
            convertedWheelManufacturerName = i_UserParamsByVehicleType[4];
            convertedCarColor = TypesValidators.ValidateEnum<eColor>(i_UserParamsByVehicleType[5]);
            convertedDoorsNumber = TypesValidators.ValidateEnum<eDoorsNumber>(i_UserParamsByVehicleType[6]);

            return GarageController.CreateElectricCar(convertedWheelMaxAirPressureSetByTheManufacturer, convertedMaxBatteryTimeInHours,
                convertedModelName, convertedLicenseNumber, convertedWheelManufacturerName, convertedCarColor, convertedDoorsNumber);
        }

        public static Truck CreateTruck(float i_WheelMaxAirPressureSetByTheManufacturer, float i_EngineFuelAmountInLiters,
            eFuelType i_EngineFuelType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturerName, bool i_IsDrivingRefregiratedContents, float i_CargoVolume)
        {
            Truck newTruck = Truck.CreateTruck(i_WheelMaxAirPressureSetByTheManufacturer, i_EngineFuelAmountInLiters, i_EngineFuelType, i_ModelName, i_LicenseNumber,
                i_WheelManufacturerName, i_IsDrivingRefregiratedContents, i_CargoVolume);

            if (!GarageController.isAllowedVehicleType(newTruck))
            {
                throw (new ArgumentException());
            }

            return newTruck;
        }

        public static Truck ValidateTruck(string[] i_UserParamsByVehicleType)
        {
            float convertedWheelMaxAirPressureSetByTheManufacturer;
            float convertedEngineFuelAmountInLiters;
            eFuelType convertedEngineFuelType;
            string convertedModelName;
            string convertedLicenseNumber;
            string convertedWheelManufacturerName;
            bool convertedIsDrivingRefregiratedContents;
            float convertedCargoVolume;

            convertedWheelMaxAirPressureSetByTheManufacturer = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[0]);
            convertedEngineFuelAmountInLiters = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[1]);
            convertedEngineFuelType = TypesValidators.ValidateEnum<eFuelType>(i_UserParamsByVehicleType[2]);
            convertedModelName = i_UserParamsByVehicleType[3];
            convertedLicenseNumber = i_UserParamsByVehicleType[4];
            convertedWheelManufacturerName = i_UserParamsByVehicleType[5];
            convertedIsDrivingRefregiratedContents = TypesValidators.ValidateBool(i_UserParamsByVehicleType[6]);
            convertedCargoVolume = TypesValidators.ValidateFloat(i_UserParamsByVehicleType[7]);

            return GarageController.CreateTruck(convertedWheelMaxAirPressureSetByTheManufacturer, convertedEngineFuelAmountInLiters,
                convertedEngineFuelType, convertedModelName, convertedLicenseNumber, convertedWheelManufacturerName, convertedIsDrivingRefregiratedContents, convertedCargoVolume);
        }

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

        public string GetRepairVehicle(string i_LicenseNumber)
        {
            RepairedVehicle repairedVehicle = this.getGarageRepairedVehicleByLiecenceNumber(i_LicenseNumber);
            StringBuilder vehicleInGarageDetailes = new StringBuilder();

            vehicleInGarageDetailes.Append(repairedVehicle.ToString());
            switch (repairedVehicle.Vehicle)
            {
                case Car c:
                    vehicleInGarageDetailes.Append(c.ToString());
                    break;
                case Motorcycle m:
                    vehicleInGarageDetailes.Append(m.ToString());
                    break;
                case Truck t:
                    vehicleInGarageDetailes.Append(t.ToString());
                    break;
                default:
                    throw new NullReferenceException("can't convert vehicle from type unknown to string");
            }

            return vehicleInGarageDetailes.ToString();
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