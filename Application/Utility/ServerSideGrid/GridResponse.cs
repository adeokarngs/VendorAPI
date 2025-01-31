using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility.ServerSideGrid
{
    public class GridResponse<T>
    {
        public T pageData {  get; set; }
        public int total { get; set; }


        public GridResponse(T data, int total) {
            this.pageData = data;
            this.total = total;
        }   
    }
}
