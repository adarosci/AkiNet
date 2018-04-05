using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AkiNet.Entities
{
    public class CGuess : BaseParameters
    {
        public class AkiElement
        {
            public class Character
            {
                [JsonProperty("id")]
                public ulong Id { get; set; }
                [JsonProperty("name")]
                public string Name { get; set; }
                [JsonProperty("id_base")]
                private ulong IdBase { get; set; }
                [JsonProperty("proba")]
                public float Probabilty { get; set; }
                [JsonProperty("description")]
                public string Description { get; set; }
                [JsonProperty("ranking")]
                public int Ranking { get; set; }
                [JsonProperty("absolute_picture_path")]
                public Uri PhotoPath { get; set; }
            }
            public Character element;
        }

        [JsonProperty("elements")]
        private IReadOnlyList<AkiElement> AkiElements;
        public IReadOnlyCollection<AkiElement.Character> Characters
            => AkiElements.Select(a => a.element).ToList();

        [JsonProperty("NbObjetsPertinents")]
        private int ObjectCount { get; set; }
    }
}
