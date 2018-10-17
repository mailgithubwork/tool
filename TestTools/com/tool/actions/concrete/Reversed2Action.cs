using System;

namespace com.tool.action.concrete
{
    public class Reversed2Action : BaseAction
    {
        public Reversed2Action() : base("Reversed2")
        {
            
        }

        public override string GetProcessLine(string root, string file)
        {
            string str = GetPathWithoutRoot(root, file);
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string( charArray );
        }
    }
}