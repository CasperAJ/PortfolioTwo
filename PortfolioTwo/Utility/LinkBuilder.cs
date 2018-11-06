using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PortfolioTwo.Utility
{
    public static class LinkBuilder
    {




        public static object CreatePageLink(Func<string, object, string> method, string route, int page, int pagesize, int numberOfRecords)
        {

            var numberOfPages = (int) Math.Ceiling((double) numberOfRecords / pagesize);

            return new
            {
                next = page >= numberOfPages - 1 ? null : method.Invoke(route, new { page = page+1, pagesize }),
                prev = page == 0 ? null : method.Invoke(route, new { page = page-1, pagesize }),
            };

        }

        public static string CreateIdentityLink(Func<string, object, string> method, string route, int id)
        {
            return method.Invoke(route, new { id });
        }



    }
}
