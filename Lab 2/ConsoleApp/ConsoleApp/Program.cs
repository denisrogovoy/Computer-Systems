using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int m, r;
            Console.Write("Input а: ");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Input b: ");
            r = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine(m * r);

            char[] array = BoothAlgorithm(m, r);
            foreach (char t in array)
            {
                Console.Write(t);
            }
            Console.ReadLine();
        }
        //Конвертируем обычное число в двоичное
        static char[] ConvertToBit(int a, ref int CountOfBit)
        {
            CountOfBit = 1;
            int b = a;
            if (a > 0)
                while ((b = b / 2) >= 1 & b > 0)
                {
                    CountOfBit++;
                }
            else
                while ((b = b / 2) <= -1 & b < 0)
                {
                    CountOfBit++;
                }
            if (a < 0)
                CountOfBit++;

            char[] boolArrayOfBits = new char[32];
            string boolArray = Convert.ToString(a, 2);
            //Заполняем не нужные биты нулями
            int i;
            for (i = 0; i < 32 - CountOfBit; i++)
            {
                boolArrayOfBits[i] = '0';
            }
            //Дописываем переведенное число
            //foreach (char c in boolArray)
            //{
            //    boolArrayOfBits[i] = c;
            //    i++;
            //    //CountOfBit++;
            //}4
            int g;
            if (a < 0)
                for (g = boolArray.Length - CountOfBit; g < boolArray.Length; g++)
                {
                    boolArrayOfBits[g] = boolArray[g];
                }
            else
                for (g = 0; g < boolArray.Length; g++)
                {
                    boolArrayOfBits[boolArrayOfBits.Length - 1 - g] = boolArray[g];
                }
            return boolArrayOfBits;
        }

        //Находим число в дополнительном коде
        static char[] Invert(char[] a)
        {
            //Инвертируем массив
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] == '0')
                    a[i] = '1';
                else
                    a[i] = '0';
            }
            //добавляем "1" в конец
            for (int i = a.Length - 1; i >= 0; i--)
            {
                if (a[i] != '0')
                {
                    a[i] = '0';

                }
                else
                {
                    a[i] = '1';
                    break;
                }
            }
            return a;
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
        //Алгоритм Бута
        static char[] BoothAlgorithm(int m, int r)
        {
            int x = 0;
            int y = 0;
            char[] mInBits = ConvertToBit(m, ref x);
            char[] rInBits = ConvertToBit(r, ref y);

            char[] a = new char[x + y + 1], s = new char[x + y + 1], p = new char[x + y + 1];
            //filling A
            int g = 0;
            int i;
            for (i = 0; i < x; i++)
            {
                a[i] = mInBits[31 - g];
                g++;
            }
            for (; i < 1 + y + x; i++) //31-последний индекс, -1 из алгоритма Бута
            {
                //BitConverter.GetBytes()
                a[i] = '0';
            }
            //filling S
            g = x;
            mInBits = Invert(mInBits);
            for (i = 0; i < x; i++) //31-последний индекс, -1 из алгоритма Бута
            {
                //BitConverter.GetBytes()

                s[i] = mInBits[32 - g];
                g--;
            }
            for (; i < 1 + y + x; i++)
            {
                //BitConverter.GetBytes()
                s[i] = '0';
            }

            //filling P
            g = y;
            for (i = 0; i < x; i++)
            {
                p[i] = '0';

            }
            for (; i < x + y + 1; i++)
            {
                if (i != x + y)
                {
                    p[i] = rInBits[32 - g];
                    g--;
                }
                else
                    p[i] = '0';
            }

            int counter = 0;
            while (counter != y)
            {
                if ((p[x + y] == '0' & p[x + y - 1] == '0') | (p[x + y] == '1' & p[x + y - 1] == '1'))
                {

                    for (i = x + y; i > 0; i--)
                    {
                        p[i] = p[i - 1];
                    }
                    p[0] = '0';
                    counter++;
                    continue;
                }
                else
                {
                    //A+P

                    if (p[x + y] == '1' & p[x + y - 1] == '0')
                    {
                        p = Addition(p, a);
                        for (i = x + y; i > 0; i--)
                        {
                            p[i] = p[i - 1];
                        }
                        p[0] = '1';
                        counter++;
                        continue;
                    }
                    else
                    {
                        //S+P
                        if ((p[x + y] == '0' & p[x + y - 1] == '1'))
                        {
                            p = Addition(p, s);
                            for (i = x + y; i > 0; i--)
                            {
                                p[i] = p[i - 1];
                            }
                            p[0] = '1';
                            counter++;
                            continue;
                        }
                    }
                }
            }

            char[] withDeletedLastBit = new char[x + y];

            for (i = x + y - 1; i > 0; i--)
            {
                withDeletedLastBit[i] = p[i];
            }

            withDeletedLastBit[0] = '1';
            return withDeletedLastBit;
        }
    }
}
