using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key]

        public int CompraId { get; set; }


        public Produto Produto { get; set; }

        public int Qtde { get; set; }

        public DateTime Data { get; set; }

        public DateTime DataDevolucao { get; set; }

        public double Multa { get; set; }

        public double Valor { get; set; }

        public string CarTId { get; set; }

    }
}