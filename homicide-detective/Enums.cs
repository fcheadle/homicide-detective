using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homicide_detective
{

    public enum SubstantiveType
    {
        person,
        item,
        scene
    }

    //all relationships between persons, items, and scenes are kept in this class
    public enum RelationshipType
    {
        //interpersonal
        acquainted,     // personIs has heard ofPerson. use this
        distantFamily,  // personIs cousins/uncles/in-laws ofPerson
        friend,         // personIs friends ofPerson
        sibling,        // personIs siblings ofPerson
        child,          // personIs child ofPerson
        parent,         // personIs parent ofPerson
        roommate,       // personIs roommate ofPerson
        coworker,       // personIs coworker ofPerson
        customer,       // personIs customer ofPerson
        superior,       // personIs superior ofPerson
        subordinate,    // personIs subordinate ofPerson
        enemy,          // personIs enemy ofPerson
        spouse,         // personIs spouse ofPerson
        partner,        // personIs non-married romantic partner ofPerson
        cheatingWith,   // personIs mistress/affair with ofPerson    

        //scene-scene relationships
        connectedTo,    // sceneIs connected to ofScene

        //item-item relationships
        itemContains,   // itemIs contains some ofItem
        on,             // itemIs on ofItem
        behind,         // itemIs behind ofItem
        matches,        // itemIs part of a set with ofItem

        //person-item relationships
        owns,           // personIs owns ofItem
        covets,         // personIs does not own ofItem, but wants to
        borrowing,      // personIs borrowing ofItem
        lending,        // personIs lending ofItem
        missing,        // personIs missing ofItem

        //person-scene relationships
        livingAt,       // personIs living at ofScene
        crashingAt,     // personIs crashing at ofScene
        welcomeAt,      // personIs welcome at ofScene
        forbidden,      // personIs forbidden from ofScene

        //scene-item relationships
        contains,       // sceneIs contains some ofItem
    }

    public enum Gender
    {
        male,
        female
        //that's all for now.
    }
}
