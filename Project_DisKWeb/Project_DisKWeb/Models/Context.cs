﻿using System.Data.Entity;

namespace Project_DisKWeb.Models
{
    public class Context : DbContext
    {
        public Context() : base("DbDisKWeb") { }

        public DbSet<Produto> Produtos { get; set; }
    }
}