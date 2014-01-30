using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.SLR;

namespace Tests.Spys
{
    internal class RetrySettingsSpy : RetrySettings
    {
        public RetrySettingsSpy()
        {
            CallCount = 0;
        }

        public Int32 CallCount { get; set; }

        public Type CallType { get; set; }

        protected override RetryAttemptSettings SetType(Type type)
        {
            CallCount = CallCount + 1;
            CallType = type;

            return base.SetType(type);
        }
    }
}
