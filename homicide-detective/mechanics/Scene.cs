using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Scene
    {
        //this is an actual scene that is involved, somehow, in a case

        public string name;         //bedroom_001, living_room_002, etc
        public string description;  //generated from properties

        public int height;          //in centimeters
        public int width;           //in centimeters

        public List<string> connections;    //names of scenes that are connected to this scene
        public List<string> contains;       //names of items contained within this scene
        public List<string> owners;         //names of peoople associated with this scene
    }
}
