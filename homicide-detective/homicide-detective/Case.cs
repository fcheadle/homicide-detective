using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{
    class Case
    {
        int caseNumber;
        Person victim;
        Person murderer;
        Scene murderScene;
        Scene whereTheyFoundTheBody;
        Scene[] placesOfInterest;
        Person[] personsOfInterest;
        Person[] witnesses;
        Item murderWeapon;
        Item[] evidence;
    }
}
