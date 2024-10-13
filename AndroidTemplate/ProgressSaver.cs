using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class ProgressSaver
    {
        public string filePath;

        /// <summary>
        /// For reading and saving progress
        /// </summary>
        public ProgressSaver() 
        {
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// Writes progress information to the file
        /// </summary>
        /// <param name="fileName">Name of the file you want to write progress into</param>
        /// <param name="progress">Content you want to write</param>
        public void WriteProgress(string fileName, string progress)
        {
            StreamWriter writer = new StreamWriter(Path.Combine(filePath, fileName));
            writer.Write(progress);
            writer.Close();
        }

        /// <summary>
        /// Reads progress information from the file
        /// </summary>
        /// <param name="fileName">Name of the file you want to read</param>
        /// <returns>Content of the file</returns>
        public string ReadProgress(string fileName)
        {
            StreamReader reader = new StreamReader(Path.Combine(filePath,fileName));
            string readText = reader.ReadToEnd();
            reader.Close();
            return readText;
        }
    }
}
