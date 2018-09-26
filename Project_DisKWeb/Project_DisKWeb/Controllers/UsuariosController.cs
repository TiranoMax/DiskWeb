﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project_DisKWeb.DAL;
using Project_DisKWeb.Models;

namespace Project_DisKWeb.Controllers
{
    public class UsuariosController : Controller
    {


        #region pagina de login
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region realiza verificacao de login
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Senha")] Usuario usuario)
        {
            usuario = UsuarioDAO.BucarUsuarioPorEmailESenha(usuario);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Email, false);
                Session["NivelAdmin"] = usuario.NivelAdmin;
                Session["Nome"] = usuario.Nome;
                return RedirectToAction("Home", "Produto");
            }
            ModelState.AddModelError("", "O e-mail ou senha não coincidem!");
            return View();
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Produto");
        }
        #endregion

        #region pagina Cadastro  user
        public ActionResult CadUser()
        {
            return View();
        }
        #endregion

        #region cadastro de user
        [HttpPost]
        public ActionResult CadUser(Usuario usuario)
        {
            usuario.NivelAdmin = "Usuario";

            if (ModelState.IsValid)
            {

                if (UsuarioDAO.CadUser(usuario))
                {
                    TempData["Test"] = usuario.UsuarioId;
                    return RedirectToAction("CadEndereco", "Usuarios");
                }
                ModelState.AddModelError("", "Não é possivel cadastra um usuario que contenha o mesmo e-mail!");
                return View(usuario);
            }
            else
            {
                ModelState.AddModelError("", "Por Favor Preencha todos os campos!");
                return View(usuario);
            }
        }
        #endregion

        #region pagina cadastro de endereco usuario
        public ActionResult CadEndereco()
        {
            if (TempData["Mensagem"] != null)
            {
                ModelState.AddModelError("", TempData["Mensagem"].ToString());
            }
            return View((Endereco)TempData["endereco"]);
        }
        #endregion

        #region Cadastro de endereco usuario
        [HttpPost]
        public ActionResult CadEndereco(Endereco endereco)
        {
            var testc = Convert.ToInt32(TempData["Test"]);
            endereco.Usuario = UsuarioDAO.BuscarUsuario(testc);

            UsuarioDAO.CadEnderecoUser(endereco);
            return RedirectToAction("Login", "usuarios");

        }
        #endregion

        #region Consulta de Cep

        [HttpPost]
        public ActionResult ConsultaCep(Endereco endereco)
        {
            try
            {
                //Download da string em jason
                string url = "https://api.postmon.com.br/v1/cep/" + endereco.CEP;
                WebClient client = new WebClient();
                string json = client.DownloadString(url);

                //converter a string para utf-8
                byte[] bytes = Encoding.Default.GetBytes(json);
                json = Encoding.UTF8.GetString(bytes);

                //converter o json para objeto
                endereco = JsonConvert.DeserializeObject<Endereco>(json);

                //passar informação para qualquer action do controller
                TempData["endereco"] = endereco;
            }
            catch (Exception)
            {
                TempData["Mensagem"] = "CEP inválido!";

            }

            return RedirectToAction("CadEndereco", "Usuarios");
        }



        #endregion


    }
}