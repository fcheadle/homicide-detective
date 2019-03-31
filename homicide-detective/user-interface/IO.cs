using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    //these classes are so unit tests can have an overrideable way to read console input
    public class IO
    {
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

        //read the console input
        public virtual string Get(bool debug = false)
        {
            if (debug) return GetDebug();
            else return Console.ReadLine();
        }

        //write to console
        public virtual void Send(string output, bool debug = false)
        {
            if (debug) SendDebug(output);
            else Console.Write(output);
        }

        //write to console
        public virtual void SendLine(string output, bool debug = false)
        {
            if (debug) SendLineDebug(output);
            else Console.WriteLine(output);
        }

        //write to console
        public virtual void SendLine(string output, string detective, bool debug = false)
        {
            if (debug) SendLineDebug(output, detective);
            else Console.WriteLine(output, detective);
        }

        //write to console
        public void SendLine(string output, string aAn, string name, bool debug = false)
        {
            if (debug) SendLineDebug(output, aAn, name);
            else Console.WriteLine(output, aAn, name);
        }
        
        //write to file
        private string GetDebug()
        {
            string path = saveFolder + "test";
            string input = File.ReadAllText(path);

            return input;
        }

        //write to file
        private void SendDebug(string output)
        {
            string path = saveFolder + "test";
            File.WriteAllText(path, output);
        }
        
        //write to file
        private void SendLineDebug(string output)
        {
            string path = saveFolder + "test";
            File.WriteAllText(path, output);
        }

        //write to file
        private void SendLineDebug(string output, string name)
        {
            string path = saveFolder + "test";
            output = string.Format(output, name);
            File.WriteAllText(path, output);
        }

        private void SendLineDebug(string input, string aAn, string name)
        {
            string path = saveFolder + "test";
            string output = string.Format(input, aAn, name);
            File.WriteAllText(path, output);
        }
    }
}
