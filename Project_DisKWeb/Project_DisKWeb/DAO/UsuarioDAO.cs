using Project_DisKWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.DAL
{
    public class UsuarioDAO
    {
        private static Context ctx = Singleton.GetInstance();
        public static bool CadUser(Usuario usuario)
        {
            if (BuscarUsuarioPorEmail(usuario) == null)
            {
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Usuario BuscarUsuarioPorEmail(Usuario usuario)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.Email.Equals(usuario.Email));
        }

        public static Usuario BucarUsuarioPorEmailESenha(Usuario usuario)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.Email.Equals(usuario.Email) && x.Senha.Equals(usuario.Senha));
        }

        public static void CadEnderecoUser(Endereco endereco)
        {
            ctx.Enderecos.Add(endereco);
            ctx.SaveChanges();

        }

        public static Usuario BuscarUsuario(int? id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static Endereco BuscaEndereco(Usuario usuario)
        {
            return ctx.Enderecos.FirstOrDefault(x => x.Usuario.UsuarioId.Equals(usuario.UsuarioId));

        }
    }
}