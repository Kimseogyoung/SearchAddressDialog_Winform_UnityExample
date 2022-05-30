using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Dialog
{
    public class Locale
    {
        public string name { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public Locale(string name, float lat,float lng)
        {
            this.name = name;
            this.lat = lat;
            this.lng = lng;
        }
    }
}
