using Project_DisKWeb.DAL;
using Project_DisKWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_DisKWeb.Controllers
{
    [RoutePrefix("Api/Usuario")]
    public class UsuarioController : ApiController
    {
        //api/Usuario/Usuarios
        [Route("Usuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioDAO.ListaUsuario();
        }

        //api/Usuario/EnderecoUsuario

        [Route("EnderecoUsuario")]
        public List<Endereco> GetEnderecos()
        {
            return UsuarioDAO.ListaEndereco();

        }

        [Route("ListarProdutos")]
        public List<Produto> GetProduto()
        {
            return ProdutoDAO.ListProduto();
        }

        [Route("ProdutoPorId/{produtoId}")]
        public dynamic GetProdutoPorId(int produtoId)
        {
            Produto produto = ProdutoDAO.SearchProdutoByID(produtoId);

            if (produto != null)
            {
                dynamic produtoDinamico = new
                {
                    Nome = produto.Nome,
                    Autor = produto.Autor,
                    Categoria = produto.Categoria,
                    Descricao = produto.Descricao,
                    Ano_Lancamento = produto.Ano_Lancamento,
                    QTDE_Estoque_aluguel = produto.QTDE_Estoque_aluguel,
                    Preco_Aluguel = produto.Preco_Aluguel.ToString("C2"),
                    
                    DataEnvio = DateTime.Now
                };
                return new { Produto = produtoDinamico };
            }
            return NotFound();
        }

        [Route("ListaCompras")]
        public List<Compra> GetCompras()
        {
            return ProdutoDAO.ListCompras();
        }

        [Route("ComprasFinalizadas")]
        public List<FinalCompra> GetFinalCompra()
        {
            return ProdutoDAO.ListFinalCompras();

        }

        

    }
}
