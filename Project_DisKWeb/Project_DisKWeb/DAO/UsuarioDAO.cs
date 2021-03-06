﻿using Project_DisKWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static void EditUser(Usuario usuario)
        {
            
            if(BuscarUsuario(usuario.UsuarioId) != null) {
                ctx.Entry(usuario).State = EntityState.Modified;
                ctx.SaveChanges();
            }

        }

        public static Usuario BuscarUsuario(int? id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static Endereco BuscaEndereco(Usuario usuario)
        {
            return ctx.Enderecos.FirstOrDefault(x => x.Usuario.UsuarioId.Equals(usuario.UsuarioId));

        }


        //parte da api
        public static List<Usuario> ListaUsuario()
        {
            return ctx.Usuarios.ToList();
        }

        public static List<Endereco> ListaEndereco()
        {
            return ctx.Enderecos.Include("Usuario").ToList();
        }

        public static void EditEndereco(Endereco enderecoOri)
       {
            if (BuscaEndereco(enderecoOri.Usuario) != null)
           {
                ctx.Entry(enderecoOri).State = EntityState.Modified;
                ctx.SaveChanges();
           }
        }

        public static void RemoveEndereco(Endereco endereco)
        {
            ctx.Enderecos.Remove(endereco);
            ctx.SaveChanges();
        }

        public static void RemoveUsuario(Usuario usuario)
        {
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }
    }
}