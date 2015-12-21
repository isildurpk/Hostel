using MugenMvvmToolkit;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.Modules;

namespace HostelPortable
{
    public class PortableModule : ModuleBase
    {
        #region Constructors

        public PortableModule() : base(false, LoadMode.All)
        {
        }

        #endregion

        #region Overrides of ModuleBase

        protected override bool LoadInternal()
        {
            IocContainer.BindToConstant<IViewModelSettings>(new DefaultViewModelSettings
            {
                DefaultBusyMessage = UiResources.DefaultBusyMessage
            });

            return true;
        }

        protected override void UnloadInternal()
        {
        }

        #endregion
    }
}