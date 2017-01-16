﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class TraducaoModels
    {
        public string strTextoOriginal { get; set; }
        public string strTextoTraduzido { get; set; }
        public string strLinguaOriginal { get; set; }
        public string strLinguaTraducao { get; set; }

    }

    public class GerarJsonConsulta
    {
        public string source { get; set; }
        public string target { get; set; }
        public string text { get; set; }
        public string model_id { get; set; }
        



    }

}