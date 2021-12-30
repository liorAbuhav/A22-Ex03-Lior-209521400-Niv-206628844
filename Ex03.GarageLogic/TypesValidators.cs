using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class TypesValidators
    {
        #region Public Static Methods
        public static float ValidateFloat(string i_StringToConvert)
        {
            float convertedFloat = 0;

            try
            {
                convertedFloat = float.Parse(i_StringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return convertedFloat;
        }

        public static T ValidateEnum<T>(string i_StringToConvert)
        {
            int convertedEnumValue = 0;
            try
            {
                if (!Enum.IsDefined(typeof(T), i_StringToConvert))
                {
                    throw new ArgumentException();
                }
                convertedEnumValue = (int)Enum.Parse(typeof(T), i_StringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return (T)(Object)convertedEnumValue;
        }

        public static int ValidateInt(string i_StringToConvert)
        {
            int convertedInt = 0;

            try
            {
                convertedInt = int.Parse(i_StringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return convertedInt;
        }

        public static bool ValidateBool(string i_StringToConvert)
        {
            bool convertedBool = false;

            try
            {
                convertedBool = bool.Parse(i_StringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return convertedBool;
        }
        #endregion
    }
}
