using NUnit.Framework;
using Jamespennell.Injection.Extensions.Signals.Api;
using Mestevens.Injection.Core;
using System;

namespace Jamespennell.Injection.Extensions.Signals.Impl
{
	[TestFixture]
	public class SignalsTest
	{
		#region Methods

		[Test]
		public void TestSignalBinding()
		{
			Context context = Miranda.Init(new TestSignalContext());
			TestSignal testSignal = context.Get<TestSignal>();
			TestCommand testCommand = context.Get<TestCommand>();

			Assert.NotNull(testSignal);
			Assert.NotNull(testCommand);

			Assert.True(testSignal is TestSignal);
			Assert.True(testCommand is TestCommand);

			testSignal.Dispatch();
		}

		[Test]
		public void TestParamSignalBinding()
		{
			Context context = Miranda.Init(new TestSignalContext());
			TestParamSignal testSignal = context.Get<TestParamSignal>();
			TestParamCommand testCommand = context.Get<TestParamCommand>();
			
			Assert.NotNull(testSignal);
			Assert.NotNull(testCommand);
			
			Assert.True(testSignal is TestParamSignal);
			Assert.True(testCommand is TestParamCommand);
			
			testSignal.Dispatch("Hello, Miranda");
		}

		#endregion

		#region Classes

		public class TestSignalContext : SignalContext
		{
			#region Methods

			public override void MapBindings()
			{
				this.Bind<TestSignal>().To<TestCommand>();
				this.Bind<TestSignal>().To<TestAsyncCommand>();
				this.Bind<TestSignal>().To<TestCommand2>();
				this.Bind<TestParamSignal>().To<TestParamCommand>();
			}

			#endregion
		}

		public class TestSignal : Signal
		{

		}

		public class TestCommand : ICommand
		{
			public void Execute()
			{
				Console.WriteLine("TestCommand.Execute()");
			}
		}

		public class TestCommand2 : ICommand
		{
			public void Execute()
			{
				Console.WriteLine("TestCommand2.Execute()");
			}
		}

		public class TestAsyncCommand : ICommandAsync
		{
			public void Execute()
			{
				Console.WriteLine("TestAsyncCommand.Execute()");
			}
		}

		public class TestParamSignal : Signal<String>
		{
			
		}

		public class TestParamCommand : ICommand<String>
		{
			public void Execute(String message)
			{
				Console.WriteLine("TestCommand.Execute({0})", message);
			}
		}

		#endregion
	}
}