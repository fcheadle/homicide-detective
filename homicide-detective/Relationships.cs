namespace homicide_detective
{
    //the R stands for Relationship
    public abstract class Relationship 
    {
        public int _is;
        public int _of;
    }

    public class RInterPerson : Relationship
    {
        RInterPersonType type;
        public RInterPerson(int x, int y, RInterPersonType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }

    public class RInterScene : Relationship
    {
        RInterSceneType type;
        public RInterScene(int x, int y, RInterSceneType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }

    public class RInterItem : Relationship
    {
        RInterItemType type;
        public RInterItem(int x, int y, RInterItemType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }

    public class RPersonScene : Relationship
    {
        RPersonSceneType type;
        public RPersonScene(int x, int y, RPersonSceneType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }

    public class RPersonItem : Relationship
    {
        RPersonItemType type;
        public RPersonItem(int x, int y, RPersonItemType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }

    public class RSceneItem : Relationship
    {
        RSceneItemType type;
        public RSceneItem(int x, int y, RSceneItemType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }
}
