using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }


        [Display(Name = "CEP")]
        public string CEP { get; set; }


        [Display(Name = "Endereço")]
        public string Logradouro { get; set; }


        [Display(Name = "Bairro")]
        public string Bairro { get; set; }


        [Display(Name = "Cidade")]
        public string Cidade { get; set; }


        [Display(Name = "Estado")]
        public string Estado { get; set; }


        [Display(Name = "Número da residência")]
        public int Numero { get; set; }


        [Display(Name = "Complemento")]
        public string Complemento { get; set; }


        public virtual Usuario Usuario { get; set; }

    }
}