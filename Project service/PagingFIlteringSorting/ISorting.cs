using System;
using System.Collections.Generic;
using System.Text;

namespace Project_service.PagingFIlteringSorting
{
    public abstract class ISorting
    {
        
        public string SortOrder {get; set;}

        protected ISorting( ) { }
        

    }
}
