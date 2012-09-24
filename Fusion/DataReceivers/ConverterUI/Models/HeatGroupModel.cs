using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterUI.Models
{
	/// <summary>
	/// Группа шаблонов плавки с соответствующим списком
	/// </summary>
	public sealed class HeatGroupModel : INPC
	{

		int _id;
		public int Id
		{
			get { return this._id; }
			set { this._id = value; OnPropertyChanged( "Id" ); }
		}

		string _name;
		public string Name
		{
			get { return this._name; }
			set { this._name = value; OnPropertyChanged( "Name" ); }
		}

		List<string> _templates;
		public List<string> Templates
		{
			get { return this._templates; }
			set { this._templates = value; OnPropertyChanged( "Templates" ); }
		}

		public HeatGroupModel()
		{
			Templates = new List<string>();
		}
	}
}
