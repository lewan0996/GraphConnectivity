using System.Windows.Threading;
using GraphConnectivity.Core.Services;
using GraphConnectivity.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Wpf.Platform;
using MvvmCross.Wpf.Views;

namespace GraphConnectivity
{
    class Setup : MvxWpfSetup
    {
        public Setup(Dispatcher uiThreadDispatcher, IMvxWpfViewPresenter presenter) : base(uiThreadDispatcher, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterType<IGraphVizualiser,GraphVizGraphVizualiser>();
            return new Core.App();
        }
    }
}
