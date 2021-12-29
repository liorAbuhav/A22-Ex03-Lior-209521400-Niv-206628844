using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using System.Reflection;

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
                        throw new FormatException("input should be y / n");
                    }
                    else
                    {
                        shouldGetValidYesNoAnswerFromUser = false;
                    }
                }
                catch (Exception argEx)
                {
                    Console.WriteLine(String.Format("Invalid input, {0}", argEx.Message));
                }
            }

            return usersYesNoAnswer;
        }

        private void navigateToOperationByUserSelectionId(int i_userSelectionId)
        {
            switch (i_userSelectionId)
            {
                case 1:
                    this.insertVehicleToGarage();
                    break;
                case 2:
                    this.showGaragesVehiclesLicenseNumbers();
                    break;
                case 3:
                    Console.WriteLine('3');
                    break;
                case 4:
                    Console.WriteLine('4');
                    break;
                case 5:
                    Console.WriteLine('5');
                    break;
                case 6:
                    Console.WriteLine('6');
                    break;
                case 7:
                    Console.WriteLine('7');
                    break;
            }
        }

        // ex1
        private void insertVehicleToGarage()
        {
            int userVhicleIDSelection = 0;
            eVehicleType userVhicleNameBySelection;
            string[] paramsForCreationMethodBeforeParsing;

            Console.WriteLine("Please select vehicle type:");
            for (int i = 0; i < this.m_GarageController.VehicleCreationMethods.Keys.Count; i++)
            { 
                Console.WriteLine(String.Format("For {0} - press {1}",this.m_GarageController.VehicleCreationMethods.Keys.ElementAt(i).ToString(), i + 1 ));
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

        private string[] createParametersDynamiclyForCreationMethod(ParameterInfo[] i_creationMethodParametersInfo)
        {
            string[] paramsForCreationMethodBeforeParsing = new string[i_creationMethodParametersInfo.Length];
            Type currentParameterType;
            for (int i = 0; i < i_creationMethodParametersInfo.Length; i++)
            {     
                Console.WriteLine(string.Format("please enter value for {0}, should be {1}",
                    i_creationMethodParametersInfo[i].Name, i_creationMethodParametersInfo[i].ParameterType));
                currentParameterType = i_creationMethodParametersInfo[i].ParameterType;
                if (currentParameterType.IsEnum)
                {
                    Console.WriteLine("this is an enum, your options are:");
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
            switch (userFilterSelection)
            {
                case 'y':
                    userRepairStatusSelection = this.getSelectedRepairStatus();
                    try
                    {
                        garagesVehiclesLicenseNumbers = this.m_GarageController.GetVehiclesLicenseNumbersByRepairStatus((eVehicleRepairStatus)userRepairStatusSelection);
                    }
                    catch (NullReferenceException NREx)
                    {
                        Console.WriteLine(NREx.Message);
                    }
                    break;
                case 'n':
                    try
                    {
                        garagesVehiclesLicenseNumbers = this.m_GarageController.GetAllVehiclesLicenseNumbers();
                    }
                    catch (NullReferenceException NREx)
                    {
                        Console.WriteLine(NREx.Message);
                    }
                    break;
            }

            Console.WriteLine("Vehicles license numbers are:");
            foreach (string licenseNumber in garagesVehiclesLicenseNumbers)
            {
                Console.WriteLine(licenseNumber);
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
            return getValidIntegerValueFromUser(1, 7) - 1;
        }

    }
}
