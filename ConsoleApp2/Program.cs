using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static string path = @"D:\text.txt";
        static void Main(string[] args)
        {
            Console.OutputEncoding = new UnicodeEncoding();
            Dictionary<string, string> dict = LoadDictionary();
            ReadString(dict);

        }

        private static void ReadString(Dictionary<string, string> dict)
        {
            while (true)
            {
                Console.Write(">");
                string line = Console.ReadLine();
                char startChar = line[0];
                if (startChar == '+')
                {
                    if (line.Length > 1)
                    {
                        AddArticle(line.Substring(1),dict);
                       
                    }
                    else
                    {
                        Console.WriteLine("Please enter article in form name=article");
                        AddArticle(Console.ReadLine(),dict) ;
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
            if (dict.ContainsKey(str)) Console.WriteLine($"{dict[str]}");
            else Console.WriteLine("Dictionary doesn`t  contain article on this name");
           
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

        private static void AddArticle(string str, Dictionary<string, string> dict)
        {
            
            string[] nameValue = str.Split('=');
            var name = nameValue[0];
            if (dict.ContainsKey(name))
            {
                Console.WriteLine("Article with that name alredy exist.Do you want to rewrite it?");
                var ans = Console.ReadLine();
                if(ans=="y"){
                    dict.Remove(name);
                }
                else if(ans=="n")
                {
                    ReadString(dict);
                }
                else
                {
                    Console.WriteLine("Please answer yes or no");
                }
            }
            dict.Add(name, nameValue[1]); 
            
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