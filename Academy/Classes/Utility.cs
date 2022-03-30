using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;

namespace Academy.Classes
{
    public static class Utility
    {   
        public static int SkipNo(int pageId , int take)
        {
            return pageId!=0 ? (pageId - 1) * take : 0;
        }  
    }
}
