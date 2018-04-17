using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTask2Solve
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please, input dividend: ");
            int dividend = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Please, input division: ");
            int division = int.Parse(Console.ReadLine());
            Console.WriteLine();

            int quotient = dividend / division;
            Console.WriteLine("Quotient = {0}", quotient);

            int remainderInt = dividend % division;
            Console.WriteLine("Remainder = {0}", remainderInt);

            char[] remainder = new char[64];

            //находим частное
            List<char> quotientInBit = GetQuotient(ConvertToBit(dividend), ConvertToBit(division), ref remainder);
            //находим остаток
            int k;
            char[] divisionInBit = ConvertToBit(division);
            divisionInBit = Shift(ConvertToBit(dividend), divisionInBit, out k);
            char[] remainderInBit = GetRemainder(ConvertToBit(dividend), divisionInBit, remainder, k);

            foreach (char t in quotientInBit)
            {
                Console.Write(t);
            }
            Console.WriteLine();
            foreach (char t in remainderInBit)
            {
                Console.Write(t);
            }
            Console.ReadLine();
        }
        //Сдвигаем делитель влево, чтобы позиция первой "1" совпадала с позицией "1" в делимом
        static char[] Shift(char[] dividend, char[] division, out int k)
        {
            char[] newDivision = new char[64];
            int i = 0;
            while (division[i] != '1')
            {
                i++;
            }
            int j = 0;
            while (dividend[j] != '1')
            {
                j++;
            }
            k = i - j;
            for(int g=0; g<j; g++)
            {
                newDivision[g] = '0';
            }
            for (; i < division.Length; i++)
            {
                newDivision[j] = division[i];
                j++;
            }
            for (; j < division.Length; j++)
            {
                newDivision[j] = '0';
            }


            return newDivision;
        }
        //Находим число в дополнительном коде
        static char[] Invert(char[] a)
        {
            char[] invertArray = new char[64];
            //Инвертируем массив
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] == '0')
                    invertArray[i] = '1';
                else
                    invertArray[i] = '0';
            }
            //добавляем "1" в конец
            for (int i = invertArray.Length - 1; i >= 0; i--)
            {
                if (invertArray[i] != '0')
                {
                    invertArray[i] = '0';
                }
                else
                {
                    invertArray[i] = '1';
                    break;
                }
            }
            return invertArray;
        }
        //Складываем два двоичных числа
        static char[] Addition(char[] a, char[] b)
        {

            bool flag = false;
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] == '1' & b[i] == '1')
                {
                    if (flag)
                        a[i] = '1';
                    else
                    {
                        a[i] = '0';
                        flag = true;
                    }
                }
                else
                {
                    if ((a[i] == '1' & b[i] == '0') | (a[i] == '0' & b[i] == '1'))
                    {
                        if (flag)
                            a[i] = '0';
                        else
                            a[i] = '1';
                    }
                    else
                    {
                        if (a[i] == '0' & b[i] == '0' & flag)
                        {
                            a[i] = '1';
                            flag = false;
                        }

                    }
                }
            }
            return a;
        }
        //Находим частное
        static List<char> GetQuotient(char[] dividend, char[] division, ref char[] bufferDividend)
        {
            List<char> quotient = new List<char>();
            int k;
            char[] divisionBackup = division;
            division = Shift(dividend, division, out k);
            char[] divisionInAdditionalCode = Invert(division);
            bufferDividend = Addition(dividend, divisionInAdditionalCode);

            quotient = AddToQuotient(bufferDividend, quotient);
            if (k > 0)
                for (int i = 0; i < k; i++)
                {
                    bufferDividend = LeftShift(bufferDividend);
                    if (bufferDividend[0] == '0')
                        bufferDividend = Addition(bufferDividend, divisionInAdditionalCode);
                    else
                        bufferDividend = Addition(bufferDividend, division);
                    quotient = AddToQuotient(bufferDividend, quotient);
                }

            return quotient;
        }
        //Находим частное
        static List<char> AddToQuotient(char[] bufferDividend, List<char> quotient)
        {
            if (bufferDividend[0] == '1')
            {
                quotient.Add('0');
            }
            else
            {
                quotient.Add('1');
            }
            return quotient;
        }
        //Сдвигаем двоичное число влево на 1 позицию
        static char[] LeftShift(char[] bufferDividend)
        {
            //сдвиг влево
            for (int i = 1; i < bufferDividend.Length; i++)
            {
                bufferDividend[i - 1] = bufferDividend[i];
            }
            bufferDividend[bufferDividend.Length - 1] = '0';

            return bufferDividend;
        }

        //Находим остаток
        static char[] GetRemainder(char[] dividend, char[] division, char[] remainder, int k)
        {
            if (remainder[0] == '1')
            {
                remainder = Addition(remainder, division);
            }
            remainder = ShiftRemainderRight(remainder, k);

            return remainder;
        }
        static char[] ShiftRemainderRight(char[] remainder, int k)
        {
            for (int i = 0; i < k; i++)
            {
                for (int j = remainder.Length - 1; j > 0; j--)
                {
                    remainder[j] = remainder[j - 1];
                }
                remainder[0] = '0';
            }
            return remainder;
        }

        static char[] ConvertToBit(int a)
        {
            //CountOfBit = 1;
            //int[] arrayEigthBit = new int[8];
            //int i = Convert.ToInt32(textBox1.Text);
            char[] boolArrayOfBits = new char[64];
            string boolArray = Convert.ToString(a, 2);
            //boolArrayOfEightBit = boolArray;
            int i;
            for (i = 0; i < 64 - boolArray.Length; i++)
            {
                boolArrayOfBits[i] = '0';
            }
            foreach (char b in boolArray)
            {
                boolArrayOfBits[i] = b;
                i++;
                //CountOfBit++;
            }

            return boolArrayOfBits;
        }
    }
}
