using System;
using System.Collections.Generic;
using System.Text;

namespace Project_service.PagingFIlteringSorting
{
   public abstract class IFiltering
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        
        protected IFiltering( ) { }
       
    }
}
