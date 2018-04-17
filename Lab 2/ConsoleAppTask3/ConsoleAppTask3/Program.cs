using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input multiplicant:");
            float multiplicant = float.Parse(Console.ReadLine());
            Console.WriteLine("Input multiplier:");
            float multiplier = float.Parse(Console.ReadLine());
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            //Множимое
            long BitMultiplicand = GetBitsFromNumber(multiplicant); //множимое в бит
            int signOfMultiplicant = (int)((BitMultiplicand >> 31) & 1); //знак
            Console.WriteLine("Sign of multiplicant: {0}", signOfMultiplicant);

            int expoOfMultiplicant = (int)((BitMultiplicand >> 23) & 255); //порядок
            Console.WriteLine("Exponent of multiplicant:");
            Console.WriteLine(Normalize(Convert.ToString(expoOfMultiplicant, 2), 8));

            long mantOfMultiplicant = BitMultiplicand & ((int)Math.Pow(2, 23) - 1); //мантиса
            Console.WriteLine("Mantissa of multiplier:");
            Console.WriteLine(Normalize(Convert.ToString(mantOfMultiplicant, 2), 24));
            Console.WriteLine();
            //Множитель
            long BitMultiplier = GetBitsFromNumber(multiplier); //множитель в бит
            int signOfMultiplier = (int)((BitMultiplier >> 31) & 1); //знак
            Console.WriteLine("Sign of multiplier: {0}", signOfMultiplier);

            int expoOfMultiplier = (int)((BitMultiplier >> 23) & 255); //порядок
            Console.WriteLine("Exponent of multiplier:");
            Console.WriteLine(Normalize(Convert.ToString(expoOfMultiplier, 2), 8));

            long mantOfMultiplier = BitMultiplier & ((int)Math.Pow(2, 23) - 1); //мантиса
            Console.WriteLine("Mantissa of multiplicant:");
            Console.WriteLine(Normalize(Convert.ToString(mantOfMultiplier, 2), 24));

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            long mantOfMultiplication = ((1 << 23) | mantOfMultiplicant) * ((1 << 23) | mantOfMultiplier);
            Console.WriteLine("Multiplicated mantissa:");
            Console.WriteLine(Normalize(Convert.ToString(mantOfMultiplication, 2), 48));

            mantOfMultiplication >>= 23;
            int expoAddition = ((1 << 24) & mantOfMultiplication) > 0 ? 1 : 0;
            if (expoAddition > 0)
            {
                Console.WriteLine("Normalization:");
                mantOfMultiplication >>= 1;
                mantOfMultiplication &= ~(1 << 23);
            }
            else
            {
                Console.WriteLine("Normalization is not needed:");
                mantOfMultiplication &= ~(3 << 23);
            }
            Console.WriteLine(Normalize(Convert.ToString(mantOfMultiplication, 2), 23));

            int sign = 1 & (signOfMultiplicant + signOfMultiplier);
            Console.WriteLine("Sign of quotient: {0}", sign);
            int exp = expoOfMultiplicant + expoOfMultiplier - 127 + expoAddition;
            Console.WriteLine("Exponent of quotient: ");
            Console.WriteLine(Convert.ToString(exp, 2));

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            int result = (int)mantOfMultiplication;
            byte[] bt = new byte[4];
            result |= exp << 23;
            result |= sign << 31;
            bt[0] = (byte)(result & 255);
            bt[1] = (byte)((result >> 8) & 255);
            bt[2] = (byte)((result >> 16) & 255);
            bt[3] = (byte)((result >> 24) & 255);
            Console.WriteLine("Result:");
            Console.WriteLine(Normalize(Convert.ToString(result, 2), 32));

            Console.ReadLine();
        }
        //Переводим float в бит
        static int GetBitsFromNumber(float income)
        {
            byte[] bt = BitConverter.GetBytes(income);
            int v = 0;
            v |= bt[0];
            v |= bt[1] << 8;
            v |= bt[2] << 16;
            v |= bt[3] << 24;
            return v;
        }
        //Нормализируем вид
        static string Normalize(string number, int c)
        {
            string addition = "";
            for (int i = 0; i < (c - number.Length); ++i)
                addition += "0";
            return addition + number;
        }
    }
}
