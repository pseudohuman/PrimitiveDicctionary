using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static string path = @"D:\text.txt";
        static void Main(string[] args)
        {
            Dictionary<string, string> dict = LoadDictionary();
            Console.Write(">");
            
            ReadString(dict);
            

        }

        private static void ReadString(Dictionary<string, string> dict)
        {
            while (true)
            {
                string line = Console.ReadLine();
                char startChar = line[0];
                if (startChar == '+')
                {
                    if (line.Length > 1)
                    {
                        string str = line.Substring(1);
                        dict.Add(str.Split('=')[0], str.Split('=')[1]);
                    }
                    else
                    {
                        Console.WriteLine("Please enter article in form name=article");
                        ReadArticle(Console.ReadLine(),dict) ;
                    }
                }
                else if(startChar == '-')
                {
                    if (line.Length > 1)
                    {
                        string str = line.Substring(1);
                        dict.Remove(str);
                    }
                    else
                    {
                        Console.WriteLine("Please enter article in form name=article");
                        DeleteArticle(Console.ReadLine(),dict);
                    }
                }
                else if (startChar == '#')
                {
                    SaveDictionary(dict);
                    break;
                }
                else
                {
                    ShowDictionary(dict,line);
                }
            }
        }

        private static void ShowDictionary(Dictionary<string, string> dict,string str)
        {
            
                Console.WriteLine($"{dict[str]}");
           
        }

        private static void SaveDictionary(Dictionary<string, string> dict)
        {
            using (StreamWriter sw = File.AppendText(path)) 
            {
                foreach (KeyValuePair<string, string> kvp in dict)
                {
                    sw.WriteLine($"{kvp.Key}\t{kvp.Value}");
                }
                    
            }
        }

        private static void DeleteArticle(string str, Dictionary<string, string> dict)
        {
            dict.Remove(str);
        }

        private static void ReadArticle(string str, Dictionary<string, string> dict)
        {
            dict.Add(str.Split('=')[0], str.Split('=')[1]);
        }

        private static Dictionary<string, string> LoadDictionary()
        {
            string[] lines=File.ReadAllLines(path);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                string[] splitedString = line.Split('\t');
                dict.Add(splitedString[0], splitedString[1]);
            }
            return dict;
        }
    }
}