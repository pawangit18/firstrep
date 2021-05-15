using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Class
{
    public class ServiceRequest
    {
        List<ServiceHeader> headerList = new List<ServiceHeader>();
        public string Endpoint { get; set; }
        public string Method { get; set; }
        public string MediaType { get; set; }
        public string RequestData { get; set; }       
        public List<ServiceHeader> ServiceHeaders
        {
            get
            {
                return headerList;
            }
            set
            {
                headerList = value;
            }
        }       
    }
    public class ServiceHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public enum ServiceMethod
    {
        POST,
        GET,
        PUT,
        DELETE
    }
}
