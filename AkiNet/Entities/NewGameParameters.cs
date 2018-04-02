using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AkiNet.Entities
{
    class NewGameParameters : BaseParameters
    {
        internal class CIdentification
        {
            [JsonProperty("channel")]
            public int Channel { get; set; }
            [JsonProperty("session")]
            public string Session { get; set; }
            [JsonProperty("signature")]
            public string Signature { get; set; }
        }
        [JsonProperty("identification")]
        internal CIdentification Identification { get; set; }

        [JsonProperty("step_information")]
        public CQuestion StepInformation { get; set; }
    }
}
