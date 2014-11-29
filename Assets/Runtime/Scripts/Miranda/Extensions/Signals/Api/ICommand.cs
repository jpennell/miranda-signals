namespace Jamespennell.Injection.Extensions.Signals.Api
{
	public interface IBaseCommand
	{

	}

	public interface ICommand : IBaseCommand
	{
		#region Methods
		
		void Execute();
		
		#endregion
	}

	public interface ICommand<P> : IBaseCommand
	{
		#region Methods
		
		void Execute(P param);
		
		#endregion
	}

	public interface IBaseAsyncCommand : IBaseCommand
	{
		
	}

	public interface ICommandAsync : IBaseAsyncCommand
	{
		#region Methods
		
		void Execute();
		
		#endregion
	}
	
	public interface ICommandAsync<P> : IBaseAsyncCommand
	{
		#region Methods
		
		void Execute(P param);
		
		#endregion
	}
}