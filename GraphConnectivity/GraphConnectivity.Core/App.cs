using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphConnectivity.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace GraphConnectivity.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            var appStart = new MvxAppStart<MainViewModel>();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);
        }
    }
}
