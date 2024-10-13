using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class GameOutputConsole
    {
        //Output Console is for developer to print logs or warning and read them while testing
        public bool isConsoleVisible = true;

        public Dictionary<string, string> savedValues = new();
        private List<string> consoleOutput = new();

        /// <summary>
        /// Prints a text on output console
        /// </summary>
        /// <param name="textToBePrinted">Your text you want to print on the console</param>
        public void ConsolePrint(string textToBePrinted)
        {
            consoleOutput.Insert(0, string.Format("[{0}] {1}", DateTime.Now.TimeOfDay.ToString().Substring(0, 11),textToBePrinted));
            if(consoleOutput.Count > 64)
                consoleOutput.RemoveAt(64);
        }

        /// <summary>
        /// Returns console output
        /// </summary>
        /// <returns>Console output with maximum of 64 lines</returns>
        public string GetConsoleOutput()
        {
            return isConsoleVisible ? string.Join(Environment.NewLine, consoleOutput) : "bug";
        }

        /// <summary>
        /// Prints saved value to the console
        /// </summary>
        /// <param name="valueKey">Key of the value to be printed. (@N all, $Key$Value edit)</param>
        public void PrintSavedValue(string valueKey)
        {
            if(valueKey[0] == '@')
            {
                //Printing all values
                ConsolePrint("");
                string[] keys = savedValues.Keys.ToArray();
                int printingRange = int.Parse(valueKey.Substring(1)) * 32;
                for(int i = printingRange; i < keys.Length && i < printingRange + 32; i++)
                {
                    ConsolePrint(string.Format("{0}={1}", keys[i], savedValues[keys[i]]));
                }
                ConsolePrint(string.Format("Printed all values between {0}-{1}", Math.Min(keys.Length, printingRange), Math.Min(keys.Length, printingRange + 32)));
            }
            else if (valueKey[0] == '$')
            {
                //Editing a value
                string[] toEdited = valueKey.Split('$');
                string oldValue = savedValues.ContainsKey(toEdited[1]) ? savedValues[toEdited[1]] : "NoValue";
                savedValues[toEdited[1]] = toEdited[2];
                ConsolePrint(string.Format("The value of {0} is changed to {1} from {2}", toEdited[1], toEdited[2], oldValue));
            }
            else
            {
                //Printing a value
                if (savedValues.ContainsKey(valueKey))
                    ConsolePrint(string.Format("{0}={1}", valueKey, savedValues[valueKey]));
                else
                    ConsolePrint("Key is not found: " + valueKey);
            }
        }
    }
}
