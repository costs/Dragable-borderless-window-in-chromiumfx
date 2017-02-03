using System.Linq;
using System.Windows;
using System;
using System.Windows;
using Neutronium.WebBrowserEngine.ChromiumFx;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace DragableDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
 
        Chromium.WebBrowser.ChromiumWebBrowser browser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new object();
            wcBrowser.OnDisplay += WcBrowser_OnDisplay;

        }

        private void WcBrowser_OnDisplay(object sender, Neutronium.Core.Navigation.DisplayEvent e)
        {
            var windowCfx = wcBrowser.WPFWebWindow as IWPFCfxWebWindow;
            browser = windowCfx.ChromiumWebBrowser;
            windowCfx.ChromiumWebBrowser.DragHandler.OnDragEnter += DragHandler_OnDragEnter;
            windowCfx.ChromiumWebBrowser.DragHandler.OnDraggableRegionsChanged += DragHandler_OnDraggableRegionsChanged;

            
        }
        private void DragHandler_OnDragEnter(object sender, Chromium.Event.CfxOnDragEnterEventArgs e)
        {
            e.SetReturnValue(true);  
        }
        private Region draggableRegion = null;
        private void DragHandler_OnDraggableRegionsChanged(object sender, Chromium.Event.CfxOnDraggableRegionsChangedEventArgs args)
        {


            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var regions = args.Regions;

                if (regions.Length > 0)
                {
                    foreach (var region in regions)
                    {
                        var rect = new System.Drawing.Rectangle(region.Bounds.X, region.Bounds.Y, region.Bounds.Width, region.Bounds.Height);

                        if (draggableRegion == null)
                        {
                            draggableRegion = new Region(rect);
                        }
                        else
                        {
                            if (region.Draggable)
                            {
                                draggableRegion.Union(rect);
                            }
                            else
                            {
                                draggableRegion.Exclude(rect);
                            }
                        }
                    }
                }
            });

        }

    }
}
