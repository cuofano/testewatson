using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteWatson.Models
{
    public class AlunoModels

    {
        public int escola_id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "O campo matrícula permite no máximo 30 caracteres!")]
        public string matricula{ get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("data de nascimento")]
        public DateTime dtnascimento { get; set; }
        
    }
}