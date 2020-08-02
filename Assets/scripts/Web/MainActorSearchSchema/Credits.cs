using System;
using System.Collections.Generic;

namespace WebJSONFormats
{
    [Serializable]
    public class Credits
    {
        public List<Cast> cast;
        public List<Crew> crew;
    }
}
