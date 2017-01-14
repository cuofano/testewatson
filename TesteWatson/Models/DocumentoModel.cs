using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class DocumentoModel
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public byte[] DocContent { get; set; }
    }
    public class DadosArquivos
    {
        public string Dado { get; set; }
    }
}