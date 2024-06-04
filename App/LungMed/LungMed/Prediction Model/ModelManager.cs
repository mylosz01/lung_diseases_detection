using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace ModelPredict
{
    internal class ModelManager
    {
        public string pathToAudioFile;
        //do ustawienia we własnym zakresie zależnie od maszyny
        private const string interpreterPath = @"C:\\Users\\Ravelien\\.conda\\envs\\model_application_integration\\python.exe";
        private const string relation = @"..\..\..";
        private string scriptPath;

        public ModelManager(string filename)
        {
            string toSavePath = CreateFolderForAudioModification();
            //Tworzenie ścieżki do pliku
            this.pathToAudioFile = Path.Combine(toSavePath, filename);
            //Tworzenie ścieżki dla skryptu uruchamiającego
            string scriptPath = Path.Combine(Environment.CurrentDirectory, relation, $"Prediction Model\\Python Scripts\\main.py");
        }

        public string GetModelResultsFromPythonScripts() 
        {
            string arguments = $"\"{scriptPath}\" {string.Join(" ",pathToAudioFile)}";

            //Configure python process
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = interpreterPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                string result;
                using(Process process = Process.Start(psi)) 
                {
                    using(System.IO.StreamReader reader = process.StandardOutput)
                    {
                        //reader czyta wynik modelu z stdout skryptu python. 
                        //Skrypt pythona wywołuje print, aby przekazać wynik do C#
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
                return result; //zwraca wynik modelu z pliku python w tym miejscu
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't run programme");
                return "Program Execution failed";
            }
        }

        public string CreateFolderForAudioModification()
        {
            string toSavePath = Path.Combine(Environment.CurrentDirectory, $"Prediction Model\\Python Scripts\\Audio");
            Directory.CreateDirectory(toSavePath);
            return toSavePath;
        }

    }
}
