using com.tool.action;

namespace com.tool.search
{
    public delegate void SearchFinishDel(SearchResult result);
    
    public interface ISearchWorker
    {
        event SearchFinishDel SearchEnd;
        
        void StartSearch(IAction action, string rootDirectory);
        void StopSearch();
    }
}