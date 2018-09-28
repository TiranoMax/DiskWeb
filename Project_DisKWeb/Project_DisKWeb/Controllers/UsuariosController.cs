using System;
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
                Session["UsuarioId"] = usuario.UsuarioId;
                
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

        public ActionResult EditUser(int id)
        {
            return View(UsuarioDAO.BuscarUsuario(id));
        }

        
        [HttpPost]
        public ActionResult EditUser(Usuario UsuarioAlterado)
        {

            Usuario usuarioOri = UsuarioDAO.BuscarUsuario(UsuarioAlterado.UsuarioId);

            usuarioOri.Nome = UsuarioAlterado.Nome;
            usuarioOri.Email = UsuarioAlterado.Email;
            usuarioOri.Cpf = UsuarioAlterado.Cpf;
            usuarioOri.Telefone = UsuarioAlterado.Telefone;
            usuarioOri.ConfirmeSenha = UsuarioAlterado.ConfirmeSenha;
            usuarioOri.Senha = UsuarioAlterado.Senha;
            usuarioOri.Nascimento = UsuarioAlterado.Nascimento;


            if (ModelState.IsValid)
            {
                UsuarioDAO.EditUser(usuarioOri);
                return RedirectToAction("Home", "Produto");

            }

            return View(UsuarioAlterado);
        }

        public ActionResult EditEndereco(int id)
        {
            Usuario usuario = UsuarioDAO.BuscarUsuario(id);
            Endereco endereco = UsuarioDAO.BuscaEndereco(usuario);
            return View(endereco);
        }


        [HttpPost]
       public ActionResult EditEndereco(Endereco EnderecoAlterado)
        {

           Endereco enderecoOri = UsuarioDAO.BuscaEndereco(EnderecoAlterado.Usuario);

            enderecoOri.Bairro = EnderecoAlterado.Bairro;
            enderecoOri.CEP = EnderecoAlterado.CEP;
            enderecoOri.Cidade = EnderecoAlterado.Cidade;
            enderecoOri.Complemento = EnderecoAlterado.Complemento;
            enderecoOri.Logradouro = EnderecoAlterado.Logradouro;
            enderecoOri.Estado = EnderecoAlterado.Estado;
            enderecoOri.Numero = EnderecoAlterado.Numero;


            if (enderecoOri.Bairro != null || enderecoOri.CEP != null || enderecoOri.Cidade != null
            || enderecoOri.Complemento != null || enderecoOri.Logradouro != null || enderecoOri.Estado != null)
            {
                UsuarioDAO.EditEndereco(enderecoOri);
                return RedirectToAction("Home", "Produto");

            }

            return View(EnderecoAlterado);
        }

        public ActionResult ExcluirUsuario(int id)
        {
            Usuario usuario = UsuarioDAO.BuscarUsuario(id);
            Endereco endereco = UsuarioDAO.BuscaEndereco(usuario);

            UsuarioDAO.RemoveEndereco(endereco);
            UsuarioDAO.RemoveUsuario(usuario);
            

            return Logout();
        }


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