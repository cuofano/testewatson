using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class RetornoApiTraducaoModel
    {
        public IEnumerable<Traduzir> translations { get; set; }
        public int word_count { get; set; }
        public int character_count { get; set; }
    }

    public class Traduzir {
        public string translation { get; set; }

    }
}