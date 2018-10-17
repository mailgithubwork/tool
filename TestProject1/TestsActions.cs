using NUnit.Framework;
using System.IO;
using com.tool.action;
using Unity;

namespace TestProject1
{
    [TestFixture]
    public class TestsActions
    {
        private IUnityContainer _unityContainer;
        private IAction _commonAction;
        private bool _isInit = false;
        
        public void Init()
        {
            if (_isInit)
            {
                return;
            }
            
            _unityContainer = new UnityContainer();
            new ActionWorker().Init(_unityContainer);
        }
        
        
        [Test]
        public void TestActionAll()
        {
            Init();
            
            _commonAction = _unityContainer.Resolve<IAction>("all");
            Assert.AreEqual(_commonAction.SearchPattern, "*.*");

            
            string root = "c:" + Path.DirectorySeparatorChar + "some-dir";
            string s_exp = root + Path.DirectorySeparatorChar + "some-file.cs";
            string s_was = "some-file.cs";
            Assert.AreEqual(_commonAction.GetProcessLine(root, s_exp), s_was);
        }

        
        [Test]
        public void TestActionCS()
        {
            Init();

            _commonAction = _unityContainer.Resolve<IAction>("cs");
            Assert.AreEqual(_commonAction.SearchPattern, "*.cs");

            
            string root = "c:" + Path.DirectorySeparatorChar + "some-dir";
            string s_exp = root + Path.DirectorySeparatorChar + "some-file.cs";
            string s_was = "some-file.cs" + " /";
            Assert.AreEqual(_commonAction.GetProcessLine(root, s_exp), s_was);
        }
        
        
        [Test]
        public void TestActionReversed1()
        {
            Init();

            _commonAction = _unityContainer.Resolve<IAction>("reversed1");
            
            
            Assert.AreEqual(_commonAction.SearchPattern, "*.*");

            
            string s_exp = "f\\bla\\ra\\t.dat";
            string s_was = "t.dat\\ra\\bla\\f";
            Assert.AreEqual(_commonAction.GetProcessLine("", s_exp), s_was);
        }
        
        
        [Test]
        public void TestActionReversed2()
        {
            Init();
            
            _commonAction = _unityContainer.Resolve<IAction>("reversed2");
            
            Assert.AreEqual(_commonAction.SearchPattern, "*.*");

            
            string s_exp = "f\\bla\\ra\\t.dat";
            string s_was = "tad.t\\ar\\alb\\f";
            Assert.AreEqual(_commonAction.GetProcessLine("", s_exp), s_was);
        }
    }
}