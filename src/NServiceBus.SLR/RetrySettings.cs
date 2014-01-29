using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NServiceBus.SLR
{
    public class RetrySettings
    {
        private RetryAttemptSettings settings;

        private static readonly Type defaultType = typeof(System.Exception);

        public RetryAttemptSettings For<T>() where T : System.Exception
        {
            return SetType(typeof(T));
        }

        public RetryAttemptSettings Default
        {
            get
            {
                return SetType(defaultType);
            }
        }


        protected virtual RetryAttemptSettings SetType(Type type)
        {
            if (settings == null) { settings = new RetryAttemptSettings(); }

            settings.SetType(type);

            return settings;
        }
    }
}
