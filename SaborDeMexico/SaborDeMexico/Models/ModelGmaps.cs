using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }

    }
    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }

    }
    public class Elements
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }

    }
    public class Rows
    {
        public IList<Elements> elements { get; set; }

    }
    public class ModelGMaps
    {
        public IList<string> destination_addresses { get; set; }
        public IList<string> origin_addresses { get; set; }
        public IList<Rows> rows { get; set; }
        public string status { get; set; }

    }
}
