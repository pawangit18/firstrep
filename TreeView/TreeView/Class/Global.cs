using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TreeView.Class
{
    public class Global
    {
        public string Version { get; set; }
        public XDocument MainSchemaDoc { get; set; }

        public string ProfileData { get; set; }
    } 
    
    public enum PrimitiveDataType
    {
            dateTime,
            date,
            boolean,
            integer
    }      
}
