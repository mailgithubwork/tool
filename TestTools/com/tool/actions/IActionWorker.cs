using Unity;

namespace com.tool.action
{
    public interface IActionWorker
    {
        IActionWorker Init(IUnityContainer actionContainer);
    }
}