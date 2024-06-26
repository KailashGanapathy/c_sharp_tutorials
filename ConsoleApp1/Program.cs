using System;
using System.IO;
using System.Collections.Generic;

namespace v1
{
    class Program
    {
        static bool Identity_char(char element, int[] countArray, int row, int col, int refFrequencyFactor, int refCounter, bool printRefpart)
        {
            bool insert_flag = false;
            switch (element)
            {
                case '1':
                    countArray[0]++;
                    insert_flag = true;
                    break;

                case '0':
                    countArray[1]++;
                    break;

                case '-':
                    countArray[2]++;
                    break;

                case 'R':
                    countArray[3]++;
                    if (countArray[3] % refFrequencyFactor == 0 && printRefpart == true)
                    {
                        Console.WriteLine($"Reference Part @ ({row}, {col}) detected");
                    }
                    break;
            }
            return insert_flag;
        }

        static void Main(string[] args)
        {
            string parentPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = null;

            int[] countArray = { 0, 0, 0, 0 }; // Array zur Berechnung der 4 verschiedene Parts (Good, Void, Inked, Reference)
            int i = 0, colcount = 0;

            List<char[]> rows = new List<char[]>();

            Console.WriteLine("File available: \n 1. small.MAP \n 2. 3H60.MAP \n Enter 1/2: ");
            string option = Console.ReadLine();
            int skipper = 0;
            int x = 0;
            Int32.TryParse(option, out x);
            if (x == 1)
            {
                filePath = Path.Combine(parentPath, "small.MAP");
                skipper = 1;
            }
            else if (x == 2)
            {
                filePath = Path.Combine(parentPath, "3H60.MAP");
                skipper = 36;
            }
            else
            {
                Console.WriteLine("Enter valid option");
                return;
            }

            Console.WriteLine("Enter the frequency for Reference Part detection (0-100): ");
            int refFrequency = int.Parse(Console.ReadLine());
            int refCounter = 0;
            bool printRefpart = true; // printflag

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                // Konvertierung der MAP-Datei in ein 2D-Array:
                while ((line = sr.ReadLine()) != null)
                {
                    // Erste Zeile überspringen
                    if (skipper != 0)
                    {
                        skipper--;
                        continue;
                    }
                    char[] numbers = new char[line.Length];
                    for (i = 0; i < line.Length; i++)
                        numbers[i] = line[i];
                    rows.Add(numbers);
                    colcount = line.Length;
                }

                for (int col = 0; col < colcount; col++)
                    for (int row = 0; row < rows.Count; row++)
                        if (rows[row][col] == 'R')
                            refCounter++;

                int refFrequencyFactor = (int)Math.Round(refCounter * (refFrequency / 100.0));
                if (refFrequencyFactor == 0)
                {
                    Console.WriteLine($"No Reference Part");
                    printRefpart = false;
                }
                //mäanderformig die Datei zu lesen (zig-zag pattern) :
                for (int col = 0; col < colcount; col++)
                {
                    if (col % 2 == 0)
                    {
                        for (int row = 0; row < rows.Count; row++)
                        {
                            bool ret = Identity_char(rows[row][col], countArray, row, col, refFrequencyFactor, refCounter, printRefpart);
                            if (ret == true)
                                Console.WriteLine("(" + row + "," + col + ")");
                        }
                    }
                    else
                    {
                        for (int row = rows.Count - 1; row >= 0; row--)
                        {
                            bool ret = Identity_char(rows[row][col], countArray, row, col, refFrequencyFactor, refCounter, printRefpart);
                            if (ret == true)
                                Console.WriteLine("(" + row + "," + col + ")");
                        }
                    }
                }

                //Ausgabe :  
                Console.WriteLine($"Good Parts: {countArray[0]}");
                Console.WriteLine($"Inked Parts: {countArray[1]}");
                Console.WriteLine($"Void Parts: {countArray[2]}");
                Console.WriteLine($"Reference Parts: {countArray[3]}");
            }
        }
    }
}