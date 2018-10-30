using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PortfolioTwo.Utility
{
    public static class LinkBuilder
    {




        public static string CreatePageLink(Func<string, object, string> method, string route, int page, int pagesize)
        {
            return method.Invoke(route, new { page, pagesize });
        }


        public static string CreateIdentityLink(Func<string, object, string> method, string route, int id)
        {
            return method.Invoke(route, new { id });
        }



    }
}
