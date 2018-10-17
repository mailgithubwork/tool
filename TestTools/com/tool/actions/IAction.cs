using System.Collections.Generic;

namespace com.tool.action
{
    public interface IAction
    {
        IList<string> GetProcessResult(string root, IList<string> listFiles);
        string GetProcessLine(string root, string files);
        string SearchPattern { get; }
        string Name { get; }
    }
}