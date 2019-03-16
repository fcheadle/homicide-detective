using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    public class Scene
    {
        //different locations that are involved, such as
        //scene of the crime
        //where the body was found
        //victim's place of work
        //so on

        public string name;
        public string description;
        public List<string> sceneClasses;
        public List<string> sceneConnections;
        public List<string> sceneContains;
        public List<string> sceneMustContain;
        public PhysicalPropertyRange lengthRange;
        public PhysicalPropertyRange widthRange;

        public Scene()
        {

        }

        public Scene(int seed)
        {

        }
    }
}
