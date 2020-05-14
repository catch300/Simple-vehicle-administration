using System;
using System.Collections.Generic;
using System.Text;

namespace Project_service.PagingFIlteringSorting
{
    public class Sorting
    {
        public string SortOrder { get; set; }

        public Sorting( )
        {

        }
        public Sorting(string sortOrder )
        {
            SortOrder = sortOrder;
        }
    
    }
}
