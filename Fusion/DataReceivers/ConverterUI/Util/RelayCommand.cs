using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ConverterUI
{
	public class RelayCommand : ICommand
	{
		#region Fields

		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		#endregion // Fields

		#region Constructors

		public RelayCommand( Action<object> execute )
			: this( execute, null )
		{
		}

		public RelayCommand( Action<object> execute, Predicate<object> canExecute )
		{
			if ( execute == null )
				throw new ArgumentNullException( "execute" );

			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion // Constructors

		#region ICommand Members

		[DebuggerStepThrough]
		public bool CanExecute( object parameter )
		{
			return this._canExecute == null || this._canExecute( parameter );
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute( object parameter )
		{
			_execute( parameter );
		}

		#endregion
	}
}
