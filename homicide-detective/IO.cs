using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace homicide_detective
{
    //these classes are so unit tests can have an overrideable way to read console input
    public class IO
    {
        static string saveFolder = Directory.GetCurrentDirectory() + @"\save\";
        static string objectsFolder = Directory.GetCurrentDirectory() + @"\objects\";
        static readonly string extension = ".json";

        #region console
        //read the console input
        public string Read(bool debug = false)
        {
            if (debug) return ReadDebug();
            else return Console.ReadLine();
        }

        //write to console
        public void Write(string output, bool debug = false)
        {
            if (debug) WriteDebug(output);
            else Console.Write(output);
        }

        //write to console
        public void WriteLine(string output, bool debug = false)
        {
            if (debug) WriteLineDebug(output);
            else Console.WriteLine(output);
        }

        //write to console
        public void WriteLine(string output, string detective, bool debug = false)
        {
            if (debug) WriteLineDebug(output, detective);
            else Console.WriteLine(output, detective);
        }

        //write to console
        public void WriteLine(string output, string aAn, string name, bool debug = false)
        {
            if (debug) WriteLineDebug(output, aAn, name);
            else Console.WriteLine(output, aAn, name);
        }
        #endregion

        #region debug
        //get from a file you pass in
        public string ReadFromFile(string file)
        {
            string path = saveFolder + file;
            return File.ReadAllText(path);
        }

        //get from a file you pass in
        public void WriteToFile(string file, string output)
        {
            string path = saveFolder + file;
            string text = File.ReadAllText(path);
            text += output;
        }

        private string ReadDebug()
        {
            string path = saveFolder + "test";
            string input = File.ReadAllText(path);
            return input;
        }

        internal void WriteDebug(string output, bool objects = false)
        {
            string path = saveFolder + "test";
            File.WriteAllText(path, output);
        }

        internal void WriteLineDebug(string output)
        {
            string path = saveFolder + "test";
            File.WriteAllText(path, output);
        }

        internal void LogError(string error)
        {
            string path = Directory.GetCurrentDirectory() + @"\error_log";
            string allErrors = File.ReadAllText(path);
            allErrors += "\n" + error;
            File.WriteAllText(path, allErrors);
        }

        internal void WriteLineDebug(string output, string name, bool objects = false)
        {
            string folder = objects ? objectsFolder : saveFolder;
            string path = folder + "test";
            output = string.Format(output, name);
            File.WriteAllText(path, output);
        }

        internal void WriteLineDebug(string input, string aAn, string name, bool objects = false)
        {
            string folder = objects ? objectsFolder : saveFolder;
            string path = folder + "test";
            string output = string.Format(input, aAn, name);
            File.WriteAllText(path, output);
        }
        #endregion

        #region get templates from json
        //gets input from the user about what item they mean to manipulate
        public Item GetItemOrScene(string command)
        {
            throw new NotImplementedException();
        }

        //returns all person templates from the json
        public List<Template> GetTemplates(SubstantiveType type)
        {
            string[] files = Directory.GetFiles(objectsFolder);
            List<Template> templates = new List<Template>();
            string prefix = "asdf";
            switch(type)
            {
                case SubstantiveType.item: prefix = "item"; break;
                case SubstantiveType.scene: prefix = "scene"; break;
                case SubstantiveType.person: prefix = "person"; break;
            }

            foreach (string file in files)
            {
                if (file.Contains(prefix))
                {
                    string contents = File.ReadAllText(file);
                    Template template = JsonConvert.DeserializeObject<Template>(contents);
                    templates.Add(template);
                }
            }
            return templates;
        }

        //gets a random template from those in the json
        public Template GetRandomTemplate(SubstantiveType type, int seed)
        {
            List<Template> templates = GetTemplates(type);
            Random random = new Random(seed);
            Template template = templates[random.Next(0, templates.Count)];
            return template;
        }

        //gets a random template from those in the json that has a specific class
        public Template GetRandomTemplate(SubstantiveType type, string _class, int seed)
        {
            List<Template> templates = GetTemplates(type);
            templates = templates.Where(l => l.classes.Contains(_class)).ToList();
            Random random = new Random(seed);
            Template template = templates[random.Next(0, templates.Count)];
            return template;
        }

        #endregion


        #region utilities
        //Check to see if a file exists
        internal bool CheckThatFileExists(string name, bool objects = false)
        {
            string folder = objects ? objectsFolder : saveFolder;
            string path = folder + name.ToLower();

            // Get current directory of binary and create a save directory if it doesn't exist.
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return File.Exists(path);
        }

        //saves the game to a file
        public void Save(object thing)
        {
            string path = saveFolder + thing.ToString() + extension;
            File.WriteAllText(path, JsonConvert.SerializeObject(thing));
        }
        
        //loads the game from a file
        public Game Load(string name)
        {
            string path = saveFolder + name + extension;
            string saveFileContents = File.ReadAllText(path);
            Game game = JsonConvert.DeserializeObject<Game>(saveFileContents);
            return game;
        }
        #endregion
    }
}
