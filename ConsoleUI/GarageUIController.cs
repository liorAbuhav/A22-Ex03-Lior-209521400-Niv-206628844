using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ex03.ConsoleUI
{
    public class GarageUIController
    {
        private GarageController m_GarageController;
        private static readonly string[] sr_GarageOperations = {
            "inserting new vehicle to the garage",
            "showing licensce numbers of garage's vheicles (with filtering option)",
            "changing Vehicle garage's status",
            "inflating the wheels to the maximum",
            "refueling a vheicle (for a powered-by fuel vehicle)",
            "charging a vehicle (for a powered-by electricity vehicle)",
            "showing all vehicle's data by license number"
        };

        public GarageUIController()
        {
            this.m_GarageController = new GarageController();

            this.initGarageUI();
        }

        private void initGarageUI()
        {
            int userOperationIDSelection = 0;

            Console.WriteLine("Welcome to the Garage! please choose what do you want to do today?\n");
            userOperationIDSelection = printGarageOperationsAndGetSelectionFromUser();
            this.navigateToOperationByUserSelectionId(userOperationIDSelection);

            Console.ReadLine();
        }

        private static int printGarageOperationsAndGetSelectionFromUser()
        {
            for (int i = 0; i < sr_GarageOperations.Length; i++)
            {
                Console.WriteLine(String.Format("For {0}, please enter {1}", sr_GarageOperations[i], i + 1));
            }

            return getValidIntegerValueFromUser(1, 7);
        }

        private static int getValidIntegerValueFromUser(int i_LowestValue, int i_HighestValue)
        {
            bool shouldGetValidOperationIdFromUser = true;
            int selectedOperationId = 0;

            while (shouldGetValidOperationIdFromUser)
            {
                try
                {
                    selectedOperationId = int.Parse(Console.ReadLine());
                    if (selectedOperationId < i_LowestValue || selectedOperationId > i_HighestValue)
                    {
                        throw new ArgumentException("number is not inside operations range, select number between 1 to 7");
                    }
                    shouldGetValidOperationIdFromUser = false;
                }
                catch (Exception argEx)
                {
                    Console.WriteLine(String.Format("Invalid input, {0}", argEx.Message));
                }
            }

            return selectedOperationId;
        }

        private static char getValidYesNoAnswerFromUser()
        {
            bool shouldGetValidYesNoAnswerFromUser = true;
            char usersYesNoAnswer = '0';

            while (shouldGetValidYesNoAnswerFromUser)
            {
                try
                {
                    usersYesNoAnswer = char.Parse(Console.ReadLine());
                    if (!(usersYesNoAnswer == 'y' || usersYesNoAnswer == 'n'))
                    {
                        throw new ArgumentException("input should be y / n");
                    }
                    else
                    {
                        shouldGetValidYesNoAnswerFromUser = false;
                    }
                }
                catch (Exception FormatEx)
                {
                    Console.WriteLine(String.Format("Invalid input, {0}", FormatEx.Message));
                }
            }

            return usersYesNoAnswer;
        }

        private static float getValidFloatFromUser()
        {
            bool shouldGetValidFloatFromUser = true;
            float usersFloat = 0;

            while (shouldGetValidFloatFromUser)
            {
                try
                {
                    usersFloat = float.Parse(Console.ReadLine());
                }
                catch (FormatException FormatEx)
                {
                    Console.WriteLine(String.Format("Invalid input, {0}", FormatEx.Message));
                }
            }

            return usersFloat;
        }

        private static float getFloatFromUserWithMsg(string i_MessageToShow)
        {
            Console.WriteLine(i_MessageToShow);
            return getValidFloatFromUser();
        }

        private void navigateToOperationByUserSelectionId(int i_UserSelectionId)
        {
            switch (i_UserSelectionId)
            {
                case 1:
                    this.insertVehicleToGarage();
                    break;
                case 2:
                    this.showGaragesVehiclesLicenseNumbers();
                    break;
                case 3:
                    this.changeVehicleStatus();
                    break;
                case 4:
                    this.InflateVehicleWheelsToMaximum();
                    break;
                case 5:
                    this.RefualFuelVehicle();
                    break;
                case 6:
                    this.chargeElectricVehicle();
                    break;
                case 7:
                    this.printFullVehicleData();
                    break;
            }
        }
        // ex1
        private string changeParamNameToReadableFormat(string i_ParamName)
        {
            string paramName = i_ParamName.Substring(2);
            string proccessedParamName = "";
            foreach (char paramChar in paramName)
            {
                if (char.IsUpper(paramChar))
                {
                    proccessedParamName += ' ';
                }
                proccessedParamName += paramChar;
            }
            proccessedParamName = proccessedParamName.Substring(1);
            return proccessedParamName;
        }
        private void insertVehicleToGarage()
        {
            int userVhicleIDSelection = 0;
            eVehicleType userVhicleNameBySelection;
            string[] paramsForCreationMethodBeforeParsing;

            Console.WriteLine("Please select vehicle type:");
            for (int i = 0; i < this.m_GarageController.VehicleCreationMethods.Keys.Count; i++)
            { 
                Console.WriteLine(String.Format("For {0} - press {1}", this.m_GarageController.VehicleCreationMethods.Keys.ElementAt(i).ToString(), i + 1 ));
            }

            userVhicleIDSelection = getValidIntegerValueFromUser(1, this.m_GarageController.VehicleCreationMethods.Keys.Count);
            userVhicleNameBySelection = this.m_GarageController.VehicleCreationMethods.Keys.ElementAt(userVhicleIDSelection - 1);

            paramsForCreationMethodBeforeParsing = this.createParametersDynamiclyForCreationMethod(this.m_GarageController.VehicleCreationMethods[userVhicleNameBySelection].GetParameters());
            try
            {
                this.m_GarageController.getVehicleParamsFromUserAndParseByVehicleType(userVhicleNameBySelection, paramsForCreationMethodBeforeParsing);
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(string.Format("Invalid input, cannot parse" + formatEx.Message));
            }
        }

        private string[] createParametersDynamiclyForCreationMethod(ParameterInfo[] i_CreationMethodParametersInfo)
        {
            string[] paramsForCreationMethodBeforeParsing = new string[i_CreationMethodParametersInfo.Length];
            Type currentParameterType;
            for (int i = 0; i < i_CreationMethodParametersInfo.Length; i++)
            {     
                Console.WriteLine(string.Format("please enter value for {0}, should be {1}",
                    this.changeParamNameToReadableFormat(i_CreationMethodParametersInfo[i].Name), i_CreationMethodParametersInfo[i].ParameterType));
                currentParameterType = i_CreationMethodParametersInfo[i].ParameterType;
                if (currentParameterType.IsEnum)
                {
                    Console.WriteLine("this is an enum, your options are: (copy and paste selected option)");
                    foreach (string enumOptionName in Enum.GetNames(currentParameterType))
                    {
                        Console.Write(string.Format("{0} ", enumOptionName));
                        Console.WriteLine();
                    }
                }
                paramsForCreationMethodBeforeParsing[i] = Console.ReadLine();
            }

            return paramsForCreationMethodBeforeParsing;
        }
        // ex02
        private void showGaragesVehiclesLicenseNumbers()
        {
            char userFilterSelection;
            int userRepairStatusSelection;
            string[] garagesVehiclesLicenseNumbers = { };

            Console.WriteLine("Do you want to filter vehicle's license numbers by vehicle status? press y/n");
            userFilterSelection = getValidYesNoAnswerFromUser();
            try
            {
                switch (userFilterSelection)
                {
                    case 'y':
                        userRepairStatusSelection = this.getSelectedRepairStatus();
                        garagesVehiclesLicenseNumbers = this.m_GarageController.GetVehiclesLicenseNumbersByRepairStatus((eVehicleRepairStatus)userRepairStatusSelection);
                        break;
                    case 'n':
                        garagesVehiclesLicenseNumbers = this.m_GarageController.GetAllVehiclesLicenseNumbers();
                        break;
                }

                Console.WriteLine("Vehicles license numbers are:");
                foreach (string licenseNumber in garagesVehiclesLicenseNumbers)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }
        }

        private int getSelectedRepairStatus()
        {
            string[] repairStatusOptions = this.m_GarageController.GetAllRepairStatusOptions();
            Console.WriteLine("select one of the folowing options:");
            for (int i = 0; i < repairStatusOptions.Length; i++)
            {
                Console.WriteLine("for {0}, press {1}", repairStatusOptions[i], i + 1);
            }
            Console.WriteLine();
            return getValidIntegerValueFromUser(1, repairStatusOptions.Length) - 1;
        }
        // ex03
        private string getLicenseNumberFromUser()
        {
            string userLicenseNumber;

            Console.WriteLine("Enter vehicle license number\n");
            userLicenseNumber = Console.ReadLine();
            Console.WriteLine();

            return userLicenseNumber;

        }
        private void changeVehicleStatus()
        {
            string userLicenseNumber;
            int userRepairStatusSelection;

            userLicenseNumber = this.getLicenseNumberFromUser();
            userRepairStatusSelection = this.getSelectedRepairStatus();
            try
            {
                this.m_GarageController.ChangeVehicleRepairStatus(userLicenseNumber, (eVehicleRepairStatus)userRepairStatusSelection);
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }
        }
        //ex04
        private void InflateVehicleWheelsToMaximum()
        {
            string userLicenseNumber = this.getLicenseNumberFromUser();
            try
            {
                this.m_GarageController.InflateVehicleWheelsToMaximum(userLicenseNumber);
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }
        }
        //ex05
        private int getSelectedFuelType()
        {
            string[] repairStatusOptions = this.m_GarageController.GetAllFuelTypeOptions();
            Console.WriteLine("select one of the folowing options:");
            for (int i = 0; i < repairStatusOptions.Length; i++)
            {
                Console.WriteLine("for {0}, press {1}", repairStatusOptions[i], i + 1);
            }
            Console.WriteLine();
            return getValidIntegerValueFromUser(1, repairStatusOptions.Length) - 1;
        }

        private void RefualFuelVehicle()
        {
            string userLicenseNumber;
            int userSelectedFuelType;
            float userFuelAmountToFill;
            
            userLicenseNumber = this.getLicenseNumberFromUser();
            userSelectedFuelType = this.getSelectedFuelType();
            userFuelAmountToFill = getFloatFromUserWithMsg("please select how many liters of fuel to fill \n");

            try
            {
                this.m_GarageController.RefualFuelVehicle(userLicenseNumber, (eFuelType)(userSelectedFuelType), userFuelAmountToFill);
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }

        }
        //ex06
        private void chargeElectricVehicle()
        {
            string userLicenseNumber;
            float userMinutesAmountToCharge;

            userLicenseNumber = this.getLicenseNumberFromUser();
            userMinutesAmountToCharge = getFloatFromUserWithMsg("please select amount of charging minutes \n");

            try
            {
                this.m_GarageController.ChargeElectricVehicle(userLicenseNumber, userMinutesAmountToCharge);
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }
        }
        //ex07
        private void printFullVehicleData()
        {
            string userLicenseNumber;

            userLicenseNumber = this.getLicenseNumberFromUser();

            try
            {
                // this.m_GarageController.GetRepairVehicleFullData(userLicenseNumber, userMinutesAmountToCharge);
            }
            catch (NullReferenceException NREx)
            {
                Console.WriteLine(NREx.Message);
            }
        }

    }
}
