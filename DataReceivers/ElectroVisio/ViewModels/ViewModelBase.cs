using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ElectroVisio.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
	{
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged( string propertyName )
		{
			this.VerifyPropertyName( propertyName );

			PropertyChangedEventHandler handler = PropertyChanged;
			if ( handler != null )
			{
				var e = new PropertyChangedEventArgs( propertyName );
				handler( this, e );
			}

		}

		#endregion

		[Conditional( "DEBUG" )]
		[DebuggerStepThrough]
		public void VerifyPropertyName( string propertyName )
		{
			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if ( TypeDescriptor.GetProperties( this )[ propertyName ] == null )
			{
				string msg = "Invalid property name: " + propertyName;

				if ( this.ThrowOnInvalidPropertyName )
					throw new Exception( msg );
				else
					Debug.Fail( msg );
			}
		}

		protected bool ThrowOnInvalidPropertyName
		{
			get; set; }

		public void Dispose() {}
	}
}
