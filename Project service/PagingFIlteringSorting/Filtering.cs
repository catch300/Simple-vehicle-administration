using System;
using System.Collections.Generic;
using System.Text;

namespace Project_service.PagingFIlteringSorting
{
    public class Filtering
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }

        public Filtering( )
        {

        }
        public Filtering(string searchString, string currentFilter )
        {
            SearchString = searchString;
            CurrentFilter = currentFilter;
        }
    }
}
