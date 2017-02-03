using Chromium.Event;
using Neutronium.JavascriptFramework.Vue;
using Neutronium.WPF;

namespace DragableDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App 
    {

        protected override void OnStartUp(IHTMLEngineFactory factory)
        {
            factory.RegisterJavaScriptFramework(new VueSessionInjectorV2());
            base.OnStartUp(factory);
        }
    }
}
