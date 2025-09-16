using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.GraphQlEntities.Results
{
    public class EntrantStandingResult
    {
        public Tournament? tournament { get; set; }

        [JsonProperty("event")]
        public Event? eventData { get; set; }
    }
}
