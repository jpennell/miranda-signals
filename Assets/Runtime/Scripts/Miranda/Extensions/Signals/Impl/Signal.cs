using Jamespennell.Injection.Extensions.Signals.Api;
using Mestevens.Injection.Core;
using System;
using System.Collections.Generic;
using Mestevens.Injection.Core.Api;

namespace Jamespennell.Injection.Extensions.Signals.Impl
{
	public abstract class BaseSignal
	{
		#region Members

		protected IDictionary<Type, IList<Type>> SignalsToCommands 
		{ 
			get 
			{
				return this.Binder.Get<IDictionary<Type, IList<Type>>>(Constants.SIGNAL_TO_COMMANDS) as IDictionary<Type, IList<Type>>;
			}
		}

		protected IDictionary<Type, IList<Type>> SignalsToAsyncCommands 
		{ 
			get 
			{
				return this.Binder.Get<IDictionary<Type, IList<Type>>>(Constants.SIGNAL_TO_ASYNC_COMMANDS) as IDictionary<Type, IList<Type>>;
			}
		}

		[Inject]
		public IBinder Binder { get; set; }
		
		#endregion

		#region Methods

		protected IList<Type> GetCommands(Type signalType)
		{
			return this.GetCommands(signalType, this.SignalsToCommands);
		}

		protected IList<Type> GetAsyncCommands(Type signalType)
		{
			return this.GetCommands(signalType, this.SignalsToAsyncCommands);
		}

		protected IList<Type> GetCommands(Type signalType, IDictionary<Type, IList<Type>> dict)
		{
			if(dict.ContainsKey(signalType)) 
			{
				return dict[signalType];
			}
			return new List<Type>();
		}

		#endregion
	}

	public abstract class Signal : BaseSignal
	{
		public void Dispatch()
		{
			foreach(Type commandType in this.GetAsyncCommands(this.GetType()))
			{
				(this.Binder.Get(commandType) as ICommandAsync).Execute();
			}

			foreach(Type commandType in this.GetCommands(this.GetType()))
			{
				(this.Binder.Get(commandType) as ICommand).Execute();
			}
		}
	}

	public abstract class Signal<P> : BaseSignal
	{
		#region Methods

		public void Dispatch(P param)
		{
			foreach(Type commandType in this.GetAsyncCommands(this.GetType()))
			{
				(this.Binder.Get(commandType) as ICommandAsync<P>).Execute(param);
			}

			foreach(Type commandType in this.GetCommands(this.GetType()))
			{
				(this.Binder.Get(commandType) as ICommand<P>).Execute(param);
			}
		}
		
		#endregion
	}
}