using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NServiceBus.SLR
{
    public class RetryAttemptSettings
    {
        private Type _activeType;

        /// <summary>
        /// Defines the number of Retries attempted before the error process is triggered
        /// </summary>
        public RetryAttemptSettings As(int numberOfRetries)
        {
            RetryManager.SetPolicy(_activeType, numberOfRetries);
            return this;
        }

        /// <summary>
        /// Defines the TimeSpan by which each successive retry attempt is incremented by
        /// </summary>
        public RetryAttemptSettings IncrementingEachBy(TimeSpan span)
        {
            RetryManager.SetPolicy(_activeType, span);
            return this;
        }

        /// <summary>
        /// Disables the retries for this type
        /// </summary>
        public void Disable()
        {
            RetryManager.SetPolicy(_activeType, 0);
        }

        /// <summary>
        /// Sets the type which these settings apply to
        /// </summary>
        internal void SetType(Type type)
        {
            _activeType = type;
        }
    }

}
