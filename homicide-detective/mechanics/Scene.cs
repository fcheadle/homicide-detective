using System;
using System.Collections.Generic;
using System.Linq;

namespace homicide_detective
{
    public class SceneTemplate
    {
        //different locations that are involved, such as
        //scene of the crime
        //where the body was found
        //victim's place of work
        //so on

        public string name;
        public string description;
        public List<string> classes;
        public List<string> connectionTypes;
        public List<string> containsTypes;
        public List<string> mustContain;
        public Range lengthRange;
        public Range widthRange;

        public SceneTemplate()
        {

        }
    }

    public class Scene : SceneTemplate
    {
        //this is an actual scene that is involved, somehow, in a case
        public int length;          //in centimeters
        public int width;           //in centimeters
        public int area;            //in meters
        public int id;              //unique identifier

        public List<string> connections;    //names of scenes that are connected to this scene
        public List<string> contains;       //names of items contained within this scene
        public List<string> owners;         //names of peoople associated with this scene
        private Random random;

        public Scene()
        {

        }
        public Scene(int seed)
        {
            random = new Random(seed);
            id = seed;
            List<SceneTemplate> templates = new Text().sceneTemplates;
            Scene scene = GenerateScene(random.Next(), templates);
            name = scene.name;
            classes = scene.classes;
            length = scene.length;
            width = scene.width;
        }

        public Scene GenerateScene(int seed, List<SceneTemplate> scenes)
        {
            Random random = new Random(seed);
            Scene scene = new Scene();
            int sceneType = random.Next(0, scenes.Count() - 1);
            SceneTemplate template = scenes[sceneType];
            scene.name = template.name;
            scene.classes = template.classes;
            scene.length = Range.GetIntFromRange(random.Next(), template.lengthRange);
            scene.width = Range.GetIntFromRange(random.Next(), template.widthRange);

            return scene;
        }

        private string AddVolumeDescriptor(Range range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;

            //todo: get these hardcoded strings from the json instead
            if (area < range.mode - tenPercent - tenPercent)
            {
                return " It is much smaller than average.";
            }
            else if (area < range.mode - tenPercent)
            {
                return " It is smaller than average.";
            }
            else if (area < range.mode)
            {
                return " It is slightly smaller than average.";
            }
            else if (area == range.mode)
            {
                return " It is exactly average.";
            }
            else if (area > range.mode + tenPercent + tenPercent)
            {
                return " It is much larger than average.";
            }
            else if (area > range.mode + tenPercent)
            {
                return " It is larger than average.";
            }
            else if (area > range.mode)
            {
                return " It is slightly larger than average.";
            }
            else
            {
                return "";
            }
        }
    }
}
