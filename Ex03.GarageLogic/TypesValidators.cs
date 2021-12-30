using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class TypesValidators
    {
        public static float validateFloat(string i_stringToConvert)
        {
            float convertedFloat = 0;

            try
            {
                convertedFloat = float.Parse(i_stringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return convertedFloat;
        }

        public static eFuelType validateEFuelType(string i_stringToConvert)
        {
            int convertedEnumValue = 0;
            try
            {
                if (!Enum.IsDefined(typeof(eFuelType), i_stringToConvert))
                {
                    throw new ArgumentException();
                }
                convertedEnumValue = (int)Enum.Parse(typeof(eFuelType), i_stringToConvert);
            }
            catch (FormatException FEx)
            {
                Console.WriteLine(FEx.Message);
            }

            return (eFuelType)(convertedEnumValue);
        }
    }
}
