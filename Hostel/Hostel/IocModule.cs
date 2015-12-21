using System.Collections.Generic;
using System.Data.SqlClient;
using Hostel.Properties;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Interfaces;
using MugenMvvmToolkit.Models;
using MugenMvvmToolkit.Models.IoC;
using MugenMvvmToolkit.Modules;

namespace Hostel
{
    public class IocModule : ModuleBase
    {
        #region Constructors

        public IocModule() : base(false, LoadMode.Runtime)
        {
        }

        #endregion

        #region Overrides of ModuleBase

        protected override bool LoadInternal()
        {
            IocContainer.BindToMethod(CreateConnection, DependencyLifecycle.TransientInstance);
            return true;
        }

        protected override void UnloadInternal()
        {
        }

        #endregion

        #region Methods

        private static SqlConnection CreateConnection(IIocContainer iocContainer, IList<IIocParameter> iocParameters)
        {
            return new SqlConnection(Resources.ConnectionString);
        }

        #endregion
    }
}