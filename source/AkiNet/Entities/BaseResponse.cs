using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AkiNet.Entities
{
    class BaseResponse<ParametersType> where ParametersType : BaseParameters
    {
        [JsonProperty("completion")]
        public string Completion { get; set; }
        [JsonProperty("parameters")]
        public ParametersType Parameters { get; set; }
    }
}
