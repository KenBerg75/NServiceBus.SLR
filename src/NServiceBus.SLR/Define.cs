using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.SLR
{
    public static class Define
    {
        private static RetrySettings retries;

        public static RetrySettings Retries
        {
            get
            {
                return Define.retries ?? (Define.retries = new RetrySettings());
            }
        }
    }
}
