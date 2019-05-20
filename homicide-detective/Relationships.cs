namespace homicide_detective
{
    public class Relationship 
    {
        public int _is;
        public int _of;
        public RelationshipType type;
        public Relationship(int x, int y, RelationshipType t)
        {
            _is = x;
            _of = y;
            type = t;
        }
    }
}
