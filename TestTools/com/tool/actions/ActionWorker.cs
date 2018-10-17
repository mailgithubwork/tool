using Unity;

namespace com.tool.action
{
    public class ActionWorker : IActionWorker
    {
        
        public IActionWorker Init(IUnityContainer actionContainer)
        {
            AutoRegister(actionContainer);
            return this;
        }

        private void AutoRegister(IUnityContainer actionContainer)
        {
            foreach (var t in typeof(BaseAction).Assembly.GetExportedTypes())
            {
                // ignore two base type
                if (typeof(BaseAction) == t || typeof(IAction) == t)
                {
                    continue;
                }
                
                if (typeof(IAction).IsAssignableFrom(t))
                {
                    var nameRegister = t.Name.Substring(0, t.Name.Length - "Action".Length).ToLower();
                    actionContainer.RegisterType(typeof(IAction), t, nameRegister);
                }
            }
        }

    }
}