using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;

namespace v1
{
    class Program
    {
        static bool Identity_char(char element, int[]countArray)
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
                    break;
            }
            return insert_flag;
        }
        static void Main(string[] args)
        {
            string filePath = @"D:\swathy_akka\task1\small.MAP";
            int[] countArray = { 0, 0, 0, 0 };
            int i = 0, colcount=0;

            //List<char[]> rows = new List<char[]>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine(); 
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    char[] numbers = new char[line.Length];
                    for(i=0; i<line.Length; i++)
                            numbers[i] = line[i];
                    rows.Add(numbers);
                    colcount = line.Length;
                }
                for (int col = 0; col < colcount; col++)
                {
                    if(col%2 == 0)
                    {
                        for (int row = 0; row < rows.Capacity; row++)
                        {
                            bool ret = Identity_char(rows[row][col], countArray);
                            if (ret == true)
                                Console.WriteLine("(" + row + "," + col + ")");
                        }
                    }
                    else
                    {
                        for (int row = rows.Capacity-1; row >= 0 ; row--)
                        {   
                            bool ret = Identity_char(rows[row][col], countArray);
                            if (ret == true)
                                Console.WriteLine("("+row+","+col+")");
                        }
                    }
                }
                Console.WriteLine($"Good Parts: {countArray[0]}");
                Console.WriteLine($"Inked Parts:{countArray[1]}");
                Console.WriteLine($"Void Parts: {countArray[2]}");
                Console.WriteLine($"Reference Parts: {countArray[3]}");
            }
        }
    }
}
