using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PortfolioTwo.Utility
{
    public static class LinkBuilder
    {




        public static object CreatePageLink(Func<string, object, string> method, string route, int page, int pagesize)
        {
            return new
            {
                next = method.Invoke(route, new { page = page+1, pagesize }),
                prev = page == 0 ? null : method.Invoke(route, new { page = page-1, pagesize }),
            };

        }

        //NOTE: Apparently this also works on pages wheres an id is need, ex GetCommentsByPostId, because Url.Link passed the request url.
        // less work for us i guess.
        public static string CreateIdentityLink(Func<string, object, string> method, string route, int id)
        {
            return method.Invoke(route, new { id });
        }



    }
}
