using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.SLR
{
    internal static class Version
    {
        public const string Major = "1";
        public const string Minor = "1";
        public const string Revision = "11";

        /// <summary>
        /// Gets the full version number
        /// </summary>
        public const string Number = Major + "." + Minor + "." + Revision;
    }
}
