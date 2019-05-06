namespace homicide_detective
{
    //all relationships between persons, items, and scenes are kept in this class
    public enum InterPersonalRelationshipType
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
        superior,       // personIs superior ofPerson. can indicate social power
        subordinate,    // personIs subordinate ofPerson
        enemy,          // personIs enemy ofPerson. strong indicator of guilt
        spouse,         // personIs spouse ofPerson
        partner,        // personIs non-married romantic partner ofPerson. May also indicate a mistress 
    }

    public enum InterSceneRelationshipType
    {
        //scene-scene relationships
        connectedTo,    // sceneIs connected to ofScene
    }

    public enum InterItemRelationshipType
    {
        //item-item relationships
        itemContains,   // itemIs contains some ofItem
        on,             // itemIs on ofItem
        behind,         // itemIs behind ofItem
        matches         // itemIs part of a set with ofItem
    }

    public enum PersonItemRelationshipType
    {
        //person-item relationships
        owns,           // personIs owns ofItem
        covets,         // personIs does not own ofItem, but wants to
        borrowing,      // personIs borrowing ofItem
        lending,        // personIs lending ofItem
        missing,        // personIs missing ofItem
    }

    public enum PersonSceneRelationshipType
    {
        //person-scene relationships
        livingAt,       // personIs living at ofScene
        crashingAt,     // personIs crashing at ofScene
        welcomeAt,      // personIs welcome at ofScene. use this for 
        forbidden,      // personIs forbidden from ofScene
    }

    public enum SceneItemRelationshipType
    {
        //scene-item relationships
        contains,  // sceneIs contains some ofItem
    }

    public class InterPersonalRelationship 
    {
        public int _is;
        public int _of;
        public InterPersonalRelationshipType type;
        public InterPersonalRelationship(int x, int y)
        {
            _is = x;
            _of = y;
        }
    }

    public class InterSceneRelationship 
    {
        public int _is;
        public int _of;
        public InterSceneRelationshipType type;
        public InterSceneRelationship(int x, int y)
        {
            _is = x;
            _of = y;
        }
    }

    public class InterItemRelationship
    {
        public int _is;
        public int _of;
        public InterItemRelationshipType type;
        public InterItemRelationship(int x, int y)
        {
            _is = x;
            _of = y;
        }
    }

    public class PersonItemRelationship
    {
        public int person;
        public int item;
        public PersonItemRelationshipType type;
        public PersonItemRelationship(int x, int y)
        {
            person = x;
            item = y;
        }
    }

    public class PersonSceneRelationship 
    {
        public int person;
        public int scene;
        public PersonSceneRelationshipType type;
        public PersonSceneRelationship(int x, int y)
        {
            person = x;
            scene = y;
        }
    }

    public class SceneItemRelationship
    {
        public int scene;
        public int item;
        public SceneItemRelationshipType type;
        public SceneItemRelationship(int x, int y)
        {
            scene = x;
            item = y;
        }
    }
}
