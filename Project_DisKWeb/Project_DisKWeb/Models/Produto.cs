using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DisKWeb.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [MaxLength(50, ErrorMessage = "O campo deve conter no máximo 50 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [MaxLength(50, ErrorMessage = "O campo deve conter no máximo 50 caracteres")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Ano de Lançamento")]
        public int Ano_Lancamento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [MaxLength(50, ErrorMessage = "O campo deve conter no máximo 50 caracteres")]
        [Display(Name = "Autor / Artista")]
        public string Autor { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Display(Name = "Quantidade em estoque para compra")]
        public int QTDE_Estoque { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Preço de venda")]
        public double Preco_Venda { get; set; }

        [Display(Name = "Quantidade em estoque para aluguel")]
        public int QTDE_Estoque_aluguel { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Preço de Aluguel")]
        public double Preco_Aluguel { get; set; }

        public string Img { get; set; }
    }
}