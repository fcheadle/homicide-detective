using System;
using System.Collections.Generic;
using System.Linq;

namespace homicide_detective
{
    public class Scene
    {
        //this is an actual scene that is involved, somehow, in a case

        public string name;         //bedroom_001, living_room_002, etc
        public string description;  //generated from properties

        public int height;          //in centimeters
        public int width;           //in centimeters
        public int area;

        public List<string> connections;    //names of scenes that are connected to this scene
        public List<string> contains;       //names of items contained within this scene
        public List<string> owners;         //names of peoople associated with this scene
        private Random random;

        public Scene(Random random)
        {
            this.random = random;
        }

        public Scene GenerateScene(List<SceneTemplate> scenes)
        {
            int sceneType = random.Next(0, scenes.Count() - 1);

            Scene scene = new Scene(random);
            scene.name = scenes[sceneType].name;
            return scene;
        }

        private string AddVolumeDescriptor(PhysicalPropertyRange range)
        {
            //get a simplified standard deviation of 10%
            int tenPercent = range.maximum - range.minimum / 10;

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
