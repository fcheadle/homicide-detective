using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace homicide_detective
{
    public class PersonTemplate
    {
        //define people physiologically and psychologically
        public string name;
        public string description;

        //following are added to percents during person generation
        public int jealousy; 
        public int anger;
        public int pride;
        public int laziness;
        public int ambition;
        public int classiness;
        public int creativity;
        public int attentionToDetail;
        public int intelligence;
        public int wealth;
        public int importanceOfFamily;

        public PersonTemplate()
        {

        }
    }
}
