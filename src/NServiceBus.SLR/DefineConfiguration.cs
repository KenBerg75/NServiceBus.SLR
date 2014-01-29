using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.SLR
{
    /// <summary>
    /// Registers the NServiceBus.SLR configuration with NServiceBus
    /// </summary>
    public class DefineConfiguration : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Features.SecondLevelRetries(s => s.CustomRetryPolicy(RetryManager.RetryPolicy));
        }
    }
}
