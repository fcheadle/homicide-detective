using Newtonsoft.Json;

namespace homicide_detective
{
    class Save
    {
        //non-static
        public string detective;
        public int seed;
        public Case activeCase;
        public Case[] solvedCases;
        public Case[] coldCases;

        public Save(string detectiveName)
        {
            if (detectiveName != null)
            {
                detective = detectiveName;
                seed = Base36.Decode(Game.SanitizeDetective(detective));
            }

        }
    }
}
