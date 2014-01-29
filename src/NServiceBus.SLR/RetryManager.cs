using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.SecondLevelRetries.Helpers;

namespace NServiceBus.SLR
{
    internal static class RetryManager
    {
        private const string HEADER_NAME = "NServiceBus.ExceptionInfo.ExceptionType";
        private static readonly Dictionary<Type, SecondLevelRetryPolicy> Policies;

        internal static Type DefaultType
        {
            get { return typeof(System.Exception); }
        }

        static RetryManager()
        {
            Policies = new Dictionary<Type, SecondLevelRetryPolicy> { { RetryManager.DefaultType, new SecondLevelRetryPolicy() { Retries = 0, Span = TimeSpan.MinValue } } };
        }

        internal static TimeSpan RetryPolicy(TransportMessage tm)
        {
            // Find the exception - if does not exist get default
            var policy = RetrievePolicy(tm);
            var retriesPerformed = TransportMessageHelpers.GetNumberOfRetries(tm);

            return retriesPerformed >= policy.Retries ? TimeSpan.MinValue  :
                                                        TimeSpan.FromTicks(policy.Span.Ticks * retriesPerformed);
        }

        internal static void SetPolicy(Type activeType, TimeSpan span)
        {
            if (!Policies.ContainsKey(activeType))
            { Policies.Add(activeType, new SecondLevelRetryPolicy() { Span = span }); }
            else
            { Policies[activeType].Span = span; }
        }

        internal static void SetPolicy(Type activeType, int numberOfRetries)
        {
            if (!Policies.ContainsKey(activeType))
            { Policies.Add(activeType, new SecondLevelRetryPolicy() { Retries = numberOfRetries }); }
            else
            { Policies[activeType].Retries = numberOfRetries; }
        }

        private static SecondLevelRetryPolicy RetrievePolicy(TransportMessage message)
        {
            var type = Policies.Where(p => p.Key.FullName == message.Headers[HEADER_NAME]).ToList();

            return type.Any() ? type.First().Value : Policies[DefaultType];
        }

        #region Container for Retry Configuration

        private class SecondLevelRetryPolicy
        {
            public SecondLevelRetryPolicy()
            {
                Span = TimeSpan.FromSeconds(5);
                Retries = 3;
            }

            internal TimeSpan Span { get; set; }
            internal int Retries { get; set; }
        }

        #endregion

    }
}
