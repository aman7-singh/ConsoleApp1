using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flicker.Model
{
    public class FlickrPhotos
    {
        [JsonProperty("photo")]
        public List<FlickrPhoto> Photo { get; set; }
    }
}
