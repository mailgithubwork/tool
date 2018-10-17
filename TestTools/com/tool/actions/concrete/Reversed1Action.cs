using System;
using System.IO;


namespace com.tool.action.concrete
{
    public class Reversed1Action : BaseAction
    {
        public Reversed1Action() : base("Reversed1")
        {
            
        }

        public override string GetProcessLine(string root, string file)
        {
            string pathWithoutRoot = GetPathWithoutRoot(root, file);
            string[] arrFolders = pathWithoutRoot.Split(Path.DirectorySeparatorChar);
            Array.Reverse(arrFolders);
            return string.Join(Path.DirectorySeparatorChar.ToString(), arrFolders);
        }

    }
}