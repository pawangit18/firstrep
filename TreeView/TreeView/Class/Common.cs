using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Class
{
    /// <summary>
    /// Token class for api token json 
    /// </summary>
    public class Token
    {
        public string access_token = string.Empty;
        //public string refresh_token = string.Empty;
    }
    /// <summary>
    /// 
    /// </summary>
    public enum WebRequestMethod
    {
        GET, POST, DELETE, PUT
    }
    /// <summary>
    /// 
    /// </summary>
    public enum TokenType
    {
        None, Bearer
    }

    public enum LogType
    {
        REQUEST_PAYLOAD, RESPONSE_PAYLOAD, INVALID_RESPONSE
    }


    public class ServiceMessage
    {
        public const string INHEALTH_INVALID_RESPONSE = "[INHEALTH INVALID RESPONSE]-Invalid response received from inhealth service.";
        public const string INHEALTH_SERVICE_ERROR = "[INHEALTH SERVICE ERROR]-Error while communicating with inhealth service.";
        public const string ESB_SERVICE_ERROR = "[ESB SERVICE ERROR]-Message could not be processed due to service error.";
    }
}
