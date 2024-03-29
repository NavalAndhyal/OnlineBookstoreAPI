﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.Filter
{
    /// <summary>  
    /// This class contains properites used for paging, sorting, grouping, filtering and will be used as a parameter model  
    ///   
    /// SortOrder   - enum of sorting orders  
    /// SortColumn  - Name of the column on which sorting has to be done,  
    ///               as for now sorting can be performed only on one column at a time.  
    ///FilterParams - Filtering can be done on multiple columns and for one column multiple values can be selected  
    ///               key :- will be column name, Value :- will be array list of multiple values  
    ///GroupingColumns - It will contain column names in a sequence on which grouping has been applied   
    ///PageNumber   - Page Number to be displayed in UI, default to 1  
    ///PageSize     - Number of items per page, default to 25  
    /// </summary>  
    public class FilterAndPaginationModel
    {
        public SortingUtility? SortingUtility { set; get; }
        public FilterUtility? FilterUtility { get; set; }
        //public IEnumerable<string>? GroupingColumns { get; set; } = null;
        int pageNumber = 1;
        public int PageNumber { get { return pageNumber; } set { if (value > 1) pageNumber = value; } }

        int pageSize = 25;
        public int PageSize { get { return pageSize; } set { if (value > 1) pageSize = value; } }
    }
}
