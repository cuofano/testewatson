//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesteWatson.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class arquivoanalise
    {
        public int id { get; set; }
        public int arquivobase_id { get; set; }
        public int analise_id { get; set; }
    
        public virtual analise analise { get; set; }
        public virtual arquivobase arquivobase { get; set; }
    }
}
