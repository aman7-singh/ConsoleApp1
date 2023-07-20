using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flicker.Model
{
    public class FlickrPhotosSearchResponse
    {
        [JsonProperty("photos")]
        public FlickrPhotos Photos { get; set; }
    }
}
