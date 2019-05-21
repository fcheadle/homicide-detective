using System;
using System.Collections.Generic;

namespace homicide_detective
{
    public class Scene : Substantive
    {
        public int area;                                    //in centimeters

        public Shape shape = new Shape();
        public List<int> connections = new List<int>();    //ids of scenes that are connected to this scene
        public List<int> contains = new List<int>();       //names of items contained within this scene
        public List<int> owners = new List<int>();         //people associated with this scene

        public Scene() { }

        public Scene(int caseNumber, int id)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            IO io = new IO();
            Generate(io.GetRandomTemplate(SubstantiveType.scene, random.Next()), id);
        }

        public Scene(int caseNumber, int id, Template template)
        {
            this.caseNumber = caseNumber;
            this.id = id;
            random = new Random(caseNumber + id);
            Generate(template, id);
        }

        public override string Describe()
        {
            return "";
        }
    }
}
