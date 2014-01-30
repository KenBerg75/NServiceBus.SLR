using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.SLR;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests
{
    public class DefineTests
    {
        [Fact(DisplayName = "Define: The Retries property should not be null")]
        public void Define_Retries_Should_Not_Be_Null()
        {
            Assert.IsNotNull(Define.Retries);
        }

        [Fact(DisplayName = "Define: The Retries property should return a RetrySettings object")]
        public void Define_Retries_Should_Return_RetrySettings()
        {
            Assert.IsInstanceOfType(Define.Retries, typeof(RetrySettings));
        }
    }
}
