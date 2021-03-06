﻿using Project_DisKWeb.Models;
using Project_DisKWeb.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.DAL
{
    public class ProdutoDAO
    {
        private static Context ctx = Singleton.GetInstance();

        #region Lista Produtos
        public static List<Produto> ListProduto()
        {
            return ctx.Produtos.ToList();
        }
        #endregion

        #region  Cadastra Produto
        public static void CadProduto(Produto produto)
        {
            ctx.Produtos.Add(produto);
            ctx.SaveChanges();
        }
        #endregion

        #region Busca Produto pelo ID 
        public static Produto SearchProdutoByID(int Id)
        {
            return ctx.Produtos.Find(Id);
        }
        #endregion

        #region Alterar Produto
        public static bool AlterProduto(Produto produto)
        {
            if (ctx.Produtos.FirstOrDefault(x => x.ProdutoId.Equals(produto.ProdutoId)) != null)
            {
                ctx.Entry(produto).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Excluir Produto
        public static void DeleteProduto(int Id)
        {
            ctx.Produtos.Remove(ProdutoDAO.SearchProdutoByID(Id));
            ctx.SaveChanges();
        }
        #endregion


        #region AddToCarT
        public static void AddToCart(Compra compra)
        {
            string Cart = Sessao.ReturnCarT();

            Compra item = ctx.Compras.Include("Produto").FirstOrDefault(x => x.Produto.ProdutoId == compra.Produto.ProdutoId && x.CarTId.Equals(Cart));

            if (item == null)
            {
                ctx.Compras.Add(compra);
            }
            ctx.SaveChanges();


        }
        #endregion

        #region Retorna Produtos Cart
        public static List<Compra> SearchProdutosByCarTId()
        {
            string CarTId = Sessao.ReturnCarT();
            return ctx.Compras.Include("Produto").Where(x => x.CarTId.Equals(CarTId)).ToList();
        }
        #endregion

        #region Remover Do Carrinho
        public static void RemoveToCart(int id)
        {
            Compra item = ctx.Compras.Find(id);
  
            ctx.Compras.Remove(item);
            ctx.SaveChanges();
        }
        #endregion

        #region Aumenta data entrega
        public static void AumentarDataEntregaProdutoCart(int id)
        {
            Compra item = ctx.Compras.Find(id);
            item.DataDevolucao = item.DataDevolucao.AddDays(1);
            ctx.SaveChanges();

            DateTime dataInicial = item.Data; DateTime datafinal = item.DataDevolucao;
            TimeSpan date = Convert.ToDateTime(datafinal) - Convert.ToDateTime(dataInicial);
            int totalDias = date.Days + 1;
            double subTotal = item.Produto.Preco_Aluguel * totalDias;
            item.Valor = subTotal;
            ctx.SaveChanges();
        }
        #endregion

        #region Diminui data entrega
        public static void DiminuirDataEntregaProdutoCart(int id)
        {
            Compra item = ctx.Compras.Find(id);
            if (item.DataDevolucao >= DateTime.Now)
            {
                item.DataDevolucao = item.DataDevolucao.AddDays(-1);
                ctx.SaveChanges();

                DateTime dataInicial = item.Data; DateTime datafinal = item.DataDevolucao;
                TimeSpan date = Convert.ToDateTime(datafinal) - Convert.ToDateTime(dataInicial);
                int totalDias = date.Days + 1;
                double subTotal = item.Produto.Preco_Aluguel * totalDias;

                item.Valor = subTotal;
                ctx.SaveChanges();
            }
        }
        #endregion

        public static Compra SearchCompraByID(int id)
        {
            return ctx.Compras.Find(id);
        }

        public static void FinishCompra(FinalCompra finish)
        {
            ctx.FinalCompras.Add(finish);
            ctx.SaveChanges();
        }

        public static List<Compra> ListCompras()
        {
            return ctx.Compras.Include("Produto").ToList();
        }

        public static List<FinalCompra> ListFinalCompras()
        {
            return ctx.FinalCompras.Include("Usuario").Include("Endereco").Include("Compras").ToList();
        }
        public static double SomaTotalCart() {

            
                return SearchProdutosByCarTId().Sum(x => x.Qtde * x.Valor);
            
        }

       
    }
}

