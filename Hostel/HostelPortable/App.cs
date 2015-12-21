using System;
using HostelPortable.ViewModels;
using MugenMvvmToolkit;

namespace HostelPortable
{
    public class App : MvvmApplication
    {
        public override Type GetStartViewModelType()
        {
            return typeof(MainVm);
        }
    }
}
