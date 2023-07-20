using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flicker.Model
{
    public class FlickrPhoto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("farm")]
        public int Farm { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
