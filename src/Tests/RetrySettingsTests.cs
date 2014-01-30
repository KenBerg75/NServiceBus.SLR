using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Spys;
using Xunit;

namespace Tests
{
    public class RetrySettingsTests
    {
        [Fact(DisplayName = "RetrySettings: For<T>() should call SetType()")]
        public void RetrySettings_For_Should_Call_SetType()
        {
            var spy = new RetrySettingsSpy();

            spy.For<Exception>();

            Assert.Equal(1, spy.CallCount);
        }

        [Fact(DisplayName = "RetrySettings: Default should call SetType()")]
        public void RetrySettings_Default_Should_Call_SetType()
        {
            var spy = new RetrySettingsSpy();

            var s = spy.Default;

            Assert.Equal(1, spy.CallCount);
        }

        [Fact(DisplayName = "RetrySettings: For<T>() should call SetType() with the appropriate type")]
        public void RetrySettings_For_Should_Call_SetType_With_The_Appropriate_Type()
        {
            var spy = new RetrySettingsSpy();

            spy.For<DivideByZeroException>();

            Assert.Equal(typeof(DivideByZeroException), spy.CallType);
        }

        [Fact(DisplayName = "RetrySettings: Default should call SetType() with the appropriate type")]
        public void RetrySettings_Default_Should_Call_SetType_With_The_Appropriate_Type()
        {
            var spy = new RetrySettingsSpy();

            var s = spy.Default;

            Assert.Equal(typeof(Exception), spy.CallType);
        }
    }
}
