using System.Windows;
using MugenMvvmToolkit;
using MugenMvvmToolkit.WPF.Infrastructure;

namespace Hostel
{
    public partial class App : Application
    {
        public App()
        {
            new Bootstrapper<HostelPortable.App>(this, new AutofacContainer());
        }
    }
}