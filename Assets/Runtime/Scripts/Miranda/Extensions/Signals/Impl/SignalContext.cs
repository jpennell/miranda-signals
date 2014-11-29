using System;
using System.Collections.Generic;

using Mestevens.Injection.Core;
using Mestevens.Injection.Core.Api;
using Jamespennell.Injection.Extensions.Signals.Api;

namespace Jamespennell.Injection.Extensions.Signals.Impl
{
	public abstract class SignalContext : Context
	{
		#region Members

		private readonly IDictionary<Type, IList<Type>> signalsToCommands;

		private readonly IDictionary<Type, IList<Type>> signalsToAsyncCommands;

		private Type signal;

		#endregion

		#region Constructors

		public SignalContext() : base()
		{
			this.signalsToCommands = new Dictionary<Type, IList<Type>>();
			this.signalsToAsyncCommands = new Dictionary<Type, IList<Type>>();
		}

		#endregion

		#region Methods

		protected override sealed void MapPluginBindings()
		{
			this.injectionBinder.Bind<IDictionary<Type, IList<Type>>>().Named(Constants.SIGNAL_TO_COMMANDS).To(this.signalsToCommands).ToSingleton();
			this.injectionBinder.Bind<IDictionary<Type, IList<Type>>>().Named(Constants.SIGNAL_TO_ASYNC_COMMANDS).To(this.signalsToAsyncCommands).ToSingleton();
		}

		public override sealed Context Bind<T>()
		{
			Type type = typeof(T);
			
			if(typeof(BaseSignal).IsAssignableFrom(type))
			{
				this.BindSignal<T>();
			}
			else
			{
				this.injectionBinder.Bind<T>();
			}
			
			return this;
		}

		public override sealed Context To<T>()
		{
			Type type = typeof(T);
			
			if(typeof(IBaseCommand).IsAssignableFrom(type))
			{
				this.ToCommand<T>();
			}
			else
			{
				this.injectionBinder.To<T>();
			}

			return this;
		}

		private void BindSignal<T>()
		{
			Type type = typeof(T);
			this.signal = type;

			if(!this.signalsToCommands.ContainsKey(type))
			{
				this.injectionBinder.Bind<T>().To<T>().ToSingleton();
			}
		}

		private void ToCommand<T>()
		{
			Type type = typeof(T);

			if(typeof(IBaseAsyncCommand).IsAssignableFrom(type))
			{
				this.AddMapping(this.signal, type, this.signalsToAsyncCommands);
			}
			else
			{
				this.AddMapping(this.signal, type, this.signalsToCommands);
			}

			this.injectionBinder.Bind<T>().To<T>();
		}

		private void AddMapping(Type signal, Type command, IDictionary<Type, IList<Type>> dict)
		{
			if (dict.ContainsKey(signal))
			{
				dict[signal].Add(command);
			} 
			else
			{
				dict.Add(signal, new List<Type>() { command });
			}
		}

		#endregion
	}
}