using System.ComponentModel;

namespace OPCEmu
{
	public class INPC : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged( string propertyName )
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if ( handler != null )
			{
				var e = new PropertyChangedEventArgs( propertyName );
				handler( this, e );
			}
		}
	}
}
