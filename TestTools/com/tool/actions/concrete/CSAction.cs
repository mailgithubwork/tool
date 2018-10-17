using System.Collections.Generic;

namespace com.tool.action.concrete
{
    public class CSAction : BaseAction
    {
        public CSAction() : base("cs")
        {
            
        }

        public override string GetProcessLine(string root, string file)
        {
            return GetPathWithoutRoot(root, file) + " /";
        }

        public override string SearchPattern
        {
            get { return "*.cs"; }
        }
    }
}