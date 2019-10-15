using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFWorkFlow
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; }
        public Album()
        {
            Tracks = new List<Track>();
        }
    }
}