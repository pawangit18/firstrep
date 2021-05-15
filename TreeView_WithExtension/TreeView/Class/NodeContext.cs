using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeView.Class
{
    public class NodeContext
    {
        List<string> childNodeIdList = new List<string>();
        List<string> valueOptionList = new List<string>();
        public string TypeName { get; set; }
        public string ExtnTypeName { get; set; }
        public string MinOccur { get; set; }
        public string MaxOccur { get; set; }
        public string IdAttr { get; set; }
        public string ValueAttr { get; set; }
        public string NodeDescription { get; set; }
        public string NodeTypeDescription { get; set; }
        public bool IsLastNode { get; set; }
        public bool HasChildValue { get; set; }
        public string NodeId { get; set; }
        public List<string> ValueOptions
        {
            get
            {
                return valueOptionList;
            }
            set
            {
                valueOptionList = value;
            }
        }
        public List<string> ChildNodeIdList
        {
            get
            {
                return childNodeIdList;
            }
            set
            {
                childNodeIdList = value;
            }
        }
    }
}
