using System.Collections.Generic;

namespace com.tool.search
{
    public struct SearchResult
    {
        public enum SearchStatus
        {
            Success,
            Abort    
        };
        
        private readonly IList<string> _listFiles;
        private readonly string _root;
        private readonly SearchStatus _status;

        public SearchResult(string root, IList<string> listFiles, SearchStatus status)
        {
            _listFiles = listFiles;
            _status = status;
            _root = root;
        }

        public IList<string> ListFiles
        {
            get { return _listFiles; }
        }

        public string Root
        {
            get { return _root; }
        }

        public SearchStatus Status
        {
            get { return _status; }
        }
    }
}