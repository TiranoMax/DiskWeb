using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.Models
{
    [Table("FinalCompra")]
    public class FinalCompra
    {
        [Key]
        public int FinalCompraId { get; set; }

        public Usuario Usuario { get; set; }

        public Endereco Endereco { get; set; }

        public List<Compra> Compras { get; set; }
    }
}