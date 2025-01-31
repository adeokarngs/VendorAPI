using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility.ServerSideGrid
{
    public class GridRequest
    {
        public GridRequest()
        {
            
        }

        public int page { get; set; }
        public int pageSize { get; set; }
        public string? sortColumn { get; set; } = "createdDate";
        public string? sortOrder { get; set; } = "desc";
    }
}
