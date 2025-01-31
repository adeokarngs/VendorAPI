using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility.File
{
    public class FileSettings
    {

        public FileSettings() { }

        public string uploadPath { get; set; }  
        public int  maxSize { get; set; }
    }
}
