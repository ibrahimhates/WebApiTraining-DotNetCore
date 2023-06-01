using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageNumber = 50;
        // Auto-implement property
        public int PageNumber { get; set; } = 1;
        
        private int _pageSize = 50;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageNumber ? maxPageNumber : value; }
        }
        public String? OrderBy { get; set; }
        public String? Fields { get; set; }
    }
}
