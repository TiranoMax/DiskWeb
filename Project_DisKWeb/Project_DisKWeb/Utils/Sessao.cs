using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_DisKWeb.Utils
{
    public class Sessao
    {
        private static string NOME_SESSAO = "Cart";

        public static string ReturnCarT()
        {
            if (HttpContext.Current.Session[NOME_SESSAO] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session[NOME_SESSAO] = guid.ToString();
            }
            return HttpContext.Current.Session[NOME_SESSAO].ToString();
        }

        public static void NewSessao()
        {
            Guid guid = Guid.NewGuid();
            HttpContext.Current.Session[NOME_SESSAO] = guid.ToString();
        }

    }
}