using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SecretSanta;

namespace SecretSantaConsole
{
    class Program
    {
        private const char DICT_FILE_SEPERATOR = ',';
        private const string OUTPUT_DICT_SEPERATOR = " -> "; 

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting...");
                CreateSanatasList(args);
                Console.WriteLine("Complete!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured: {0}", ex.ToString());
            }
            finally
            {
                Console.WriteLine("Press enter to quit.");
                Console.ReadLine();
            }
        }

        private static void CreateSanatasList(string[] args)
        {
            IList<string> participants = ReadFile(args[0]);

            IDictionary<string, string> bannedPairs = new Dictionary<string, string>();
            string outputFile = string.Empty;

            switch (args.Length)
            {
                case 2:
                    outputFile = args[1];
                    break;

                case 3:
                    bannedPairs = ReadDictFile(args[1]);
                    outputFile = args[2];
                    break;

                default:
                    throw new ArgumentException("Invalid number of arguments passed");
            }

            var santasList = SecretSantaGenerator.Generate(participants, bannedPairs);

            WriteDictFile(outputFile, santasList);
        }

        private static List<string> ReadFile(string filePath)
        {
            return File.ReadLines(filePath).Select(record => record.Trim()).ToList();
        }

        private static IDictionary<string, string> ReadDictFile(string filePath)
        {
            var dict = new Dictionary<string, string>();

            foreach (var record in ReadFile(filePath))
            {
                var splitRecord = record.Split(DICT_FILE_SEPERATOR);
                dict.Add(splitRecord[0].Trim(), splitRecord[1].Trim());
            }

            return dict;
        }

        private static void WriteDictFile(string filePath, IDictionary<string, string> recordPairs)
        {
            var records = recordPairs.Select(pair => string.Concat(pair.Key.ToString(), OUTPUT_DICT_SEPERATOR, pair.Value.ToString()));

            File.WriteAllLines(filePath, records);
        }
    }
}
