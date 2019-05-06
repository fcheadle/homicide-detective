using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        public List<PersonTemplate> GetPersonTemplates()
        {
            string[] files = Directory.GetFiles(objectsFolder);
            List<PersonTemplate> templates = new List<PersonTemplate>();
            foreach (string file in files)
            {
                if (file.Contains("person_"))
                {
                    string contents = File.ReadAllText(file);
                    PersonTemplate template = JsonConvert.DeserializeObject<PersonTemplate>(contents);
                    templates.Add(template);
                }
            }
            return templates;
        }

        //gets a random person template from those in the json
        public PersonTemplate GetRandomPersonTemplate(int seed)
        {
            List<PersonTemplate> templates = GetPersonTemplates();
            Random random = new Random(seed);
            PersonTemplate template = templates[random.Next(0, templates.Count)];
            return template;
        }

        //Gets a random scene template from those defined in the json
        public SceneTemplate GetRandomSceneTemplate(int seed)
        {
            List<SceneTemplate> templates = GetSceneTemplates();
            Random random = new Random(seed);
            SceneTemplate template = templates[random.Next(0, templates.Count)];
            return template;
        }

        //get all scene templates from the json
        public List<SceneTemplate> GetSceneTemplates()
        {
            string[] files = Directory.GetFiles(objectsFolder);
            List<SceneTemplate> templates = new List<SceneTemplate>();
            foreach (string file in files)
            {
                if (file.Contains("scene_"))
                {
                    string contents = File.ReadAllText(file);
                    SceneTemplate template = JsonConvert.DeserializeObject<SceneTemplate>(contents);
                    templates.Add(template);
                }
            }
            return templates;
        }

        public List<ItemTemplate> GetItemTemplates()
        {
            string[] files = Directory.GetFiles(objectsFolder);
            List<ItemTemplate> templates = new List<ItemTemplate>();
            foreach (string file in files)
            {
                if (file.Contains("item_"))
                {
                    string contents = File.ReadAllText(file);
                    ItemTemplate template = JsonConvert.DeserializeObject<ItemTemplate>(contents);
                    templates.Add(template);
                }
            }
            return templates;
        }

        public ItemTemplate GetRandomItemTemplate(int seed)
        {
            List<ItemTemplate> templates = GetItemTemplates();
            Random random = new Random(seed);
            ItemTemplate template = templates[random.Next(0, templates.Count)];
            return template;
        }
        #endregion


        #region utilities
        //Check to see if a saved game exists for this string
        internal bool CheckThatFileExists(string name, bool objects = false)
        {
            string folder = objects ? objectsFolder : saveFolder;
            string path = folder + name.ToLower();
            if (path.Contains(".json"));

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
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
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
