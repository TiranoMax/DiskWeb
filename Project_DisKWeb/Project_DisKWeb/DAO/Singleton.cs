using Project_DisKWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.DAL
{
    public class Singleton
    {
        private static Context ctx;

        private Singleton() { }

        public static Context GetInstance()
        {
            if (ctx == null)
            {
                ctx = new Context();
            }
            return ctx;
        }

    }
}