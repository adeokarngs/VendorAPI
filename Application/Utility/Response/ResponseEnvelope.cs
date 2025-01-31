using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility.Response
{

    public class ResponseEnvelope<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        // Constructor for success response
        public ResponseEnvelope(int status,T data, string message = null)
        {
            this.status = status;
            this.message = message;  // Correct assignment
            this.data = data;        // Correct assignment
        }

        // Constructor for error response
        public ResponseEnvelope(int status,string message)
        {
            this.status = status;
            this.message = message;  // Correct assignment
            this.data = default;     // default is assigned automatically, but we can keep this explicit for clarity
        }
    }
}
