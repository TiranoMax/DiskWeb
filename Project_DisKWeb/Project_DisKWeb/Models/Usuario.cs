using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MaxLength(45, ErrorMessage = "O nome deve ter no máximo 45 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "O Campo CPF deve ter no minimo e no máximo 11 caracteres"), MinLength(11, ErrorMessage = "O Campo CPF deve ter no minimo e no máximo 11 caracteres")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo e-mail deve ter no máximo 100 caracteres")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo e-mail deve ter no máximo 100 caracteres")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MaxLength(30, ErrorMessage = "A senha deve ter no máximo 30 caracteres"), MinLength(5, ErrorMessage = "A senha deve ter no mínimo 5 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [Display(Name = "Confirmação da senha")]
        [NotMapped]
        public string ConfirmeSenha { get; set; }

        [Display(Name = "Nivel Administrador")]
        public string NivelAdmin { get; set; }



    }
}