using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class TypesValidators
    {
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

        //public static eFuelType ValidateEFuelType(string i_StringToConvert)
        //{
        //    int convertedEnumValue = 0;
        //    try
        //    {
        //        if (!Enum.IsDefined(typeof(eFuelType), i_StringToConvert))
        //        {
        //            throw new ArgumentException();
        //        }
        //        convertedEnumValue = (int)Enum.Parse(typeof(eFuelType), i_StringToConvert);
        //    }
        //    catch (FormatException FEx)
        //    {
        //        Console.WriteLine(FEx.Message);
        //    }

        //    return (eFuelType)(convertedEnumValue);
        //}

        //public static eLicenseType ValidateELiecenceType(string i_StringToConvert)
        //{
        //    int convertedEnumValue = 0;
        //    try
        //    {
        //        if (!Enum.IsDefined(typeof(eLicenseType), i_StringToConvert))
        //        {
        //            throw new ArgumentException();
        //        }
        //        convertedEnumValue = (int)Enum.Parse(typeof(eLicenseType), i_StringToConvert);
        //    }
        //    catch (FormatException FEx)
        //    {
        //        Console.WriteLine(FEx.Message);
        //    }

        //    return (eLicenseType)(convertedEnumValue);
        //}

        //public static eColor ValidateEColor(string i_StringToConvert)
        //{
        //    int convertedEnumValue = 0;
        //    try
        //    {
        //        if (!Enum.IsDefined(typeof(eColor), i_StringToConvert))
        //        {
        //            throw new ArgumentException();
        //        }
        //        convertedEnumValue = (int)Enum.Parse(typeof(eColor), i_StringToConvert);
        //    }
        //    catch (FormatException FEx)
        //    {
        //        Console.WriteLine(FEx.Message);
        //    }

        //    return (eColor)(convertedEnumValue);
        //}

        //public static eDoorsNumber ValidateEDoorsNumber(string i_StringToConvert)
        //{
        //    int convertedEnumValue = 0;
        //    try
        //    {
        //        if (!Enum.IsDefined(typeof(eDoorsNumber), i_StringToConvert))
        //        {
        //            throw new ArgumentException();
        //        }
        //        convertedEnumValue = (int)Enum.Parse(typeof(eDoorsNumber), i_StringToConvert);
        //    }
        //    catch (FormatException FEx)
        //    {
        //        Console.WriteLine(FEx.Message);
        //    }

        //    return (eDoorsNumber)(convertedEnumValue);
        //}

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

    }
}
