using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
            int countOfAllLetters; //кількість літер у тексті
            double[,] array = new double[alphabet.Length, 2];
            string pathFileOne = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Садок вишневий.txt";
            string pathFileTwo = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Створення світу біблія.txt";
            string pathFileThree = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\DHCP.txt";

            string text = ReadFile(pathFileOne);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            double amountOfInformation = CountEntropyAmountOfInformation(array, pathFileOne, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            CompareWithSizeOfArchive(amountOfInformation, pathFileOne);
            ShowArray(array, alphabet);

            text = ReadFile(pathFileTwo);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathFileTwo, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            CompareWithSizeOfArchive(amountOfInformation, pathFileTwo);
            ShowArray(array, alphabet);

            text = ReadFile(pathFileThree);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathFileThree, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            CompareWithSizeOfArchive(amountOfInformation, pathFileThree);
            ShowArray(array, alphabet);

            //Часть задания со второго задания
            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            countOfAllLetters=0; //кількість літер у тексті
            array = new double[alphabet.Length, 2];
            pathFileOne = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Садок вишневийBase64.txt";
            pathFileTwo = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\DHCPBase64.txt";
            pathFileThree = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Створення світу бібліяBase64.txt";

            string pathArchiveOne = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Садок вишневий ArchiveBase64.txt";
            string pathArchiveTwo = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\DHCPArchiveBase64.txt";
            string pathArchiveThree = @"E:\Университет\3 курс\2 семестр\Комп'ютерні системи\Лабораторна робота №1\Створення світу бібліяArchiveBase64.txt";


            text = ReadFile(pathFileOne);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathFileOne, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathFileOne, amountOfInformation);

            text = ReadFile(pathFileTwo);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathFileTwo, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathFileTwo, amountOfInformation);

            text = ReadFile(pathFileThree);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathFileThree, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathFileThree, amountOfInformation);

            text = ReadFile(pathArchiveOne);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathArchiveOne, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathArchiveOne, amountOfInformation);

            text = ReadFile(pathArchiveTwo);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathArchiveTwo, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathArchiveTwo, amountOfInformation);

            text = ReadFile(pathArchiveThree);
            CountLetters(array, text, out countOfAllLetters, alphabet); //Масив з кількістю окремої літери та частотою її появи у тексті
            CountFrequency(array, countOfAllLetters); //Рахуємо частоту появи літер у тексті
            amountOfInformation = CountEntropyAmountOfInformation(array, pathArchiveThree, countOfAllLetters); //Рахуємо інтропію та кількість інфомації
            Console.WriteLine("{0}   {1:F4}", pathArchiveThree, amountOfInformation);

            Console.ReadLine();
        }

        //Порівняння кількості інформації та розміру архівів
        static void CompareWithSizeOfArchive(double amountOfInformation, string path)
        {
            string[] archive = new string[] { ".rar", ".zip", ".gz", ".bz2", ".xz" };
            foreach (string extention in archive)
            {
                FileInfo file = new FileInfo(path + extention);
                Console.WriteLine("Розмір архіву {0}: {1}", extention, file.Length);
                if (file.Length > amountOfInformation)
                    Console.WriteLine("Розмір архіву " + extention + " > кількість інформації\n");
                else
                {
                    if (file.Length == amountOfInformation)
                        Console.WriteLine("Розмір архіву " + extention + " = кількість інформації\n");
                    else
                        Console.WriteLine("Розмір архіву " + extention + " < кількість інформації\n");
                }
            }

        }

        //Рахує інтропію та кількість інфомації
        static double CountEntropyAmountOfInformation(double[,] array, string path, int countOfAllLetters)
        {
            double entropy = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, 0] != 0)
                    entropy += array[i, 1] * Math.Log(1 / array[i, 1], 2);
            }
            FileInfo file = new FileInfo(path);
            double amountOfInformation;
            Console.WriteLine("Середня ентропія (у бітах): {0:F4}", entropy);
            Console.WriteLine("Кількість інформації у тексті (у бітах): {0:F4}", entropy * countOfAllLetters); //* на не алфавита, а символов текста
            Console.WriteLine("Кількість інформації у тексті (у байтах): {0:F4}\n", amountOfInformation = entropy * countOfAllLetters / 8); //1байт=8бит
            Console.WriteLine("Розмір файлу: {0} байт", file.Length);
            if (file.Length > amountOfInformation)
                Console.WriteLine("Розмір файлу>кількість інформації\n");
            else
            {
                if (file.Length == amountOfInformation)
                    Console.WriteLine("Розмір файлу=кількість інформації\n");
                else
                    Console.WriteLine("Розмір файлу < кількість інформації\n");
            }
            return amountOfInformation;
        }

        //Виводить масив з кількістю літер та частотою їх появи у тексті
        static void ShowArray(double[,] array, string alphabet)
        {
            Console.WriteLine("Літера           Кількість  Відносна частота");
            for (int i = 0; i < array.GetLength(0); i++)
            {

                Console.Write("{0}         ", alphabet[i]);
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0,15:F4}", array[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Рахує частоту появи літер у тексті
        static void CountFrequency(double[,] array, int countOfAllLetters)
        {
            for (int i = 0; i < array.GetLength(0); i++) //array.GetLength(1) -кількість літер українського алфавіту
            {
                if (array[i, 0] != 0)
                {
                    array[i, 1] = array[i, 0] / countOfAllLetters;

                }
            }
        }

        //Рахує кількість літер у тексті
        static void CountLetters(double[,] array, string text, out int countOfAllLetters, string alphabet)
        {
            int counterLetter;
            countOfAllLetters = 0;
            text = text.ToLower();
            bool firstTime = true;
            for (int i = 0; i < alphabet.Length; i++) //мінус 34, тому що нижче +33, тобто враховує діапазон літер 34-66
            {
                counterLetter = 0;
                foreach (char letter in text)
                {
                    if (alphabet.Contains(letter))
                    {
                        if (firstTime)
                            countOfAllLetters++;
                        if (letter == alphabet[i])
                        {
                            counterLetter++;
                        }
                    }
                }
                firstTime = false;

                array[i, 0] = counterLetter;
            }
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("Всього літер у тексті: {0}", countOfAllLetters);
        }

        //Читаємо зміст файлу
        static string ReadFile(string pathFile)
        {
            string text = "";
            string line;
            if (File.Exists(pathFile))
            {
                using (StreamReader sw = new StreamReader(pathFile))
                {
                    while ((line = sw.ReadLine()) != null)
                    {
                        text += line + "\n";
                    }
                }
            }
            else
                throw new Exception("Файлу з таким шляхом не існує");
            Console.WriteLine(text);
            return text;
        }
    }
}

//АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ
