NServiceBus.SRL
===============

A plugin to NServiceBus that allows configuration of Second Level Retries based on the Exception Type.


Configuration Options
---------------------
We can specify these options for any exception type that may be thrown by our handler. We are commonly refering to these options as RetryAttemptSettings

Number of retries - Defines the number of Second Level Retry attempts made for messages throwing this particular exception.
					This is defined with the As(int) method

Incrementing	  - Defines the TimeSpan by which we increment subsequent retries for this message
					This is defines with the IncrementingEachBy(TimeSpan) method


Registration with NServiceBus
-----------------------------
The package is set up to be automatically pulled into NServiceBus and register itself.


How to Configure
----------------
Once we pull the package into our endpoint project, we can configure our SLR's within the endpoint configuration like so:

        public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	    {
	        public void Init()
	        {
	            Define.Retries.For<DivideByZeroException>().As(5).IncrementingEachBy(TimeSpan.FromSeconds(5));	            

                // Other NServiceBus Configuration

	        }
        }

This defines a SLR for a DivideByZeroException, defining 5 retries for this exception, incrementing each retry by 5 seconds.



Global SLR Configuration
------------------------
The global configuration will apply to any exception which is not specifically defined. This acts as the default.
We can change the default in a couple different ways:

By defining a rule for the System.Exception:

        public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	    {
	        public void Init()
	        {
	            Define.Retries.For<Exception>().As(3).IncrementingEachBy(TimeSpan.FromSeconds(1));	            

                // Other NServiceBus Configuration

	        }
        }

By using the Default:

        public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	    {
	        public void Init()
	        {
	            Define.Retries.Default.As(3).IncrementingEachBy(TimeSpan.FromMinutes(1));	            

                // Other NServiceBus Configuration

	        }
        }



Disabling SLR by exception:
---------------------------
You can also disable SLR for a particular exception. This is useful if you have a default set, but for some particular exception you do not want to retry.
This is done like so:

        public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	    {
	        public void Init()
	        {
	            Define.Retries.Default.As(3).IncrementingEachBy(TimeSpan.FromMinutes(1));	            
                Define.Retries.For<System.DivideByZeroException>().Disable();

                // Other NServiceBus Configuration

	        }
        }

This will enable a default SLR configuration, yet disable SLR for a System.DivideByZeroException