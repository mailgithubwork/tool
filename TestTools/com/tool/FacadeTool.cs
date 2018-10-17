using System;
using System.IO;
using com.tool.action;
using com.tool.search;
using Unity;

namespace com.tool
{
    public class FacadeTool
    {
        private readonly UnityContainer _unityContainer;
        private readonly Action<string> _debugLog;
        private string _outputFolder;


        public FacadeTool(Action<string> debugLog = null)
        {
            _debugLog = debugLog;
            _unityContainer = new UnityContainer();
            Init();
        }

        private void Init()
        {
            Print("initialize...");
            _unityContainer.RegisterType<IActionWorker, ActionWorker>(); // "ActionWorkerConcrete"
            _unityContainer.RegisterType<ISearchWorker, SearchWorker>(); // "SearchWorkerConcrete"
            
            //init concrete actions
            _unityContainer.Resolve<IActionWorker>().Init(_unityContainer);
        }
        
        public void Start(string directory, string actionName, string output = null)
        {
            try
            {
                var iAction = GetAction(actionName);
                var iSearch = _unityContainer.Resolve<ISearchWorker>();
                _unityContainer.RegisterInstance<ISearchWorker>(iSearch);
                _outputFolder = output;
                iSearch.SearchEnd += EndSearch;
                Print("start search...");
                iSearch.StartSearch(iAction, directory);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Stop()
        {
            _unityContainer.Resolve<ISearchWorker>().StopSearch();
            
        }
        
        private IAction GetAction(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException(String.Format("{0} Is Null Or Empty", actionName), "actionName");
            }

            actionName = actionName.ToLower();
            if (!_unityContainer.IsRegistered<IAction>(actionName))
            {
                throw new ArgumentException(String.Format("{0} Is invalid", actionName), "actionName");
            }

            return _unityContainer.Resolve<IAction>(actionName);
        }

        private void EndSearch(SearchResult result)
        {
            Print("Search end. Status: " + result.Status);
            Print("Files find: " + result.ListFiles.Count);
            var path = GetSaveUrl(result.Root);
            Print("start save...");
            File.WriteAllLines(path, result.ListFiles);
            Print("finish save. Path: " + path);
        }

        private string GetSaveUrl(string root)
        {
            if (string.IsNullOrEmpty(_outputFolder))
            {
                return root + Path.DirectorySeparatorChar + "results.txt";
            }

            if (String.Equals(Path.GetExtension(_outputFolder), "txt"))
            {
                return Path.GetFullPath(_outputFolder);
            }

            return Path.GetFullPath(root + Path.DirectorySeparatorChar + _outputFolder);
        }

        private void Print(string message)
        {
            if (_debugLog == null)
            {
                return;
            }

            _debugLog(message);
        }
    }
}