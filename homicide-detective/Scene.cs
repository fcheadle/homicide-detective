using System;
using System.Collections.Generic;

namespace homicide_detective
{
    //the json files will be read into a scene template instead of a scene itself
    public class SceneTemplate
    {
        public string name;
        public string description;
        public Range lengthRange;
        public Range widthRange;
        public List<Shape> shapes = new List<Shape>();
        public List<string> classes = new List<string>();
        public List<string> connectionTypes = new List<string>();
        public List<string> containsTypes = new List<string>();
        public List<string> mustContainTypes = new List<string>();
    }

    public class Scene : SceneTemplate
    {
        public int id;
        public int caseNumber;
        public int length;                                  //in centimeters
        public int width;                                   //in centimeters
        public int area;                                    //in centimeters
        private Random random;
        public Shape shape = new Shape();
        public List<int> connections = new List<int>();    //ids of scenes that are connected to this scene
        public List<int> contains = new List<int>();       //names of items contained within this scene
        public List<int> owners = new List<int>();         //people associated with this scene

        public Scene()
        {

        }

        public Scene(int caseNumber, int id)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            IO io = new IO();
            Generate(io.GetRandomSceneTemplate(random.Next()));
        }

        public Scene(int caseNumber, int id, SceneTemplate template)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            Generate(template);
        }

        private void Generate(SceneTemplate template)
        {
            name = template.name;
            classes = template.classes;
            lengthRange = template.lengthRange;
            widthRange = template.widthRange;
            length = lengthRange.GenerateFromRange(random.Next());
            width = widthRange.GenerateFromRange(random.Next());
        }

        public override string ToString()
        {
            return name;
        }
    }
}
