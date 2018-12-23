using System;
using System.IO;
using System.Reflection;

namespace FileSwitcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {            
            string productionFileName = "RendererDX11"; // no .ini
            Console.WriteLine("Save (s) or Replace (r)");
            string action = Console.ReadLine().ToLower();
            switch(action)
            {
                case "s":
                    
                    Console.WriteLine("Enter File Name (no .ini):");
                    string fileName = Console.ReadLine();
                    if (ReadAndReplace(productionFileName, fileName))
                    {
                        Console.WriteLine("Success");
                        Console.WriteLine();
                        Console.WriteLine("Press any Button to Exit");
                        Console.ReadKey();
                    }                   

                    break;

                case "r":
                    string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
                    FileInfo[] files = directoryInfo.GetFiles("*.ini");
                    Console.WriteLine("Current .ini Files:");
                    foreach (FileInfo item in files)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Enter Name of File to Replace Production Settings: (no .ini)");
                    string replacementFileName = Console.ReadLine();
                    if (ReadAndReplace(replacementFileName, productionFileName))
                    {
                        Console.WriteLine("Success");
                        Console.WriteLine();
                        Console.WriteLine("Press any Button to Exit");
                        Console.ReadKey();
                    }
                    break;
            }
        }

        static bool ReadAndReplace(string sourceName, string destinationName)
        {
            string currentFile = string.Empty;
            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            FileStream fileStream = new FileStream(basePath + "\\" + sourceName + ".ini", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                currentFile = reader.ReadToEnd();
            }

            using (StreamWriter file = new StreamWriter(basePath + "\\" + destinationName + ".ini"))
            {
                file.Write(currentFile);
            }

            return true;
        }
    }
}
