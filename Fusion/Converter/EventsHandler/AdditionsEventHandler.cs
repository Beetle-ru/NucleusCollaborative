using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(AdditionsEvent _event)
        {
            //switch (_event.MaterialName)
            //{
            //    case "ИЗВЕСТ":  break;
            //}

            //Console.WriteLine("Addition has come. MatName={0} ConverterNumber={1} ", _event.MaterialName, _event.iCnvNr);
            //_event.ChemestryAttributes = DBWorker.Instance.GetChemestryAttributes(_event.MaterialName);
            //foreach (var pair in _event.ChemestryAttributes)
            //{
            //    Console.WriteLine("Elements El_name {0} El_val={1}", pair.Key, pair.Value);
            //}
            try
            {
                this._Module._Heat.AdditionsHistory.Add(_event);
            }
            catch { }
        }

    }
}
