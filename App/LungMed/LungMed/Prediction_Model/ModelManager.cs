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
        private const string interpreterPath = @"D:\\Python\\lung_diseases_detection\\.venv\\Scripts\\python.exe";
        private string scriptPath;
        private string filePathPythonArgument;

        public ModelManager(string filename)
        {
            string toSavePath = CreateFolderForAudioModification();
            //Tworzenie ścieżki do pliku
            this.pathToAudioFile = Path.Combine(toSavePath, filename);
            //Tworzenie ścieżki dla skryptu uruchamiającego
            scriptPath = Path.Combine(Environment.CurrentDirectory, @"Prediction_Model\\Python_Scripts\\predict_model.py");
            filePathPythonArgument = Path.Combine($"Audio\\", filename);
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
                UseShellExecute = false
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
            string toSavePath = Path.Combine(Environment.CurrentDirectory, $"Prediction_Model\\Python_Scripts\\Audio");
            Directory.CreateDirectory(toSavePath);
            return toSavePath;
        }

    }
}
