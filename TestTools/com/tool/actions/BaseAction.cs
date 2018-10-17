using System.Collections.Generic;

namespace com.tool.action
{
    public class BaseAction : IAction
    {
        private readonly string _name;

        protected BaseAction(string name)
        {
            _name = name;
        }

        public virtual IList<string> GetProcessResult(string root, IList<string> listFiles)
        {
            for (int i = 0; i < listFiles.Count; i++)
            {
                listFiles[i] = GetProcessLine(root, listFiles[i]);
            }
            
            return listFiles;
        }

        public virtual string GetProcessLine(string root, string file)
        {
            return GetPathWithoutRoot(root, file);
        }

        protected string GetPathWithoutRoot(string root, string str)
        {
            return str.Substring(root.Length).Trim('/').Trim('\\');
        }

        public virtual string SearchPattern
        {
            get { return "*.*"; }
        }
        
        public string Name
        {
            get { return _name; }
        }
    }
}