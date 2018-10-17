using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using com.tool.action;

namespace com.tool.search
{
    public class SearchWorker : ISearchWorker
    {
        public event SearchFinishDel SearchEnd;
        
        private IAction _action;
        private List<string> _listFiles;
        private CancellationTokenSource _cts;
        

        public void StartSearch(IAction action, string rootDirectory)
        {
            _action = action;
            _cts = new CancellationTokenSource();
            _listFiles = new List<string>();

            if (!Directory.Exists(rootDirectory))
            {
                throw new ArgumentException(String.Format("{0} Is invalid", rootDirectory), "rootDirectory");
            }
            
            SearchAsync(rootDirectory);
            
        }

        public void StopSearch()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        private async Task SearchAsync(string root)
        {
            await Task.Run(() => SearchNodeAsync(root), _cts.Token);

            _action.GetProcessResult(root, _listFiles);

            if (SearchEnd != null)
            {
                var status = _cts.IsCancellationRequested
                    ? SearchResult.SearchStatus.Abort
                    : SearchResult.SearchStatus.Success;
                
                SearchEnd(new SearchResult(
                        root,
                        _listFiles,
                        status
                    )
                );
            }
        }

        private async Task SearchNodeAsync(string node)
        {
            try
            {
                if (_cts.IsCancellationRequested)
                {
                    return;
                }
                _listFiles.AddRange(Directory.GetFiles(node, _action.SearchPattern));

                foreach (var dir in Directory.GetDirectories(node))
                {
                    await SearchNodeAsync(dir);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}