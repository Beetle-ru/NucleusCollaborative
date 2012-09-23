using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    partial class ConverterEventsHandler
    {
        public void Process(LanceEvent _event)
        {
            if (this._Module._Heat.Number == -1) return;

            if (this._Module._Heat.BlowingScheme != null)
            {
                float curpersent = (float)_event.O2TotalVol / 22000f * 100f;
                if (this._Module._Heat.CurrentBlowingScheme == -1 && curpersent > this._Module._Heat.BlowingScheme[0].O2VolStep1)
                {
                    this._Module.PushEvent(this._Module._Heat.BlowingScheme[0]);
                    this._Module._Heat.CurrentBlowingScheme = 0;
                    this._Module.PushEvent(new cntBlowingSchemaEvent());
                }
                if (this._Module._Heat.CurrentBlowingScheme == 0 && curpersent > this._Module._Heat.BlowingScheme[1].O2VolStep1)
                {
                    this._Module._Heat.CurrentBlowingScheme = 1;
                    this._Module.PushEvent(this._Module._Heat.BlowingScheme[1]);
                    this._Module.PushEvent(new cntBlowingSchemaEvent());
                }
                if (this._Module._Heat.CurrentBlowingScheme == 1 && curpersent > this._Module._Heat.BlowingScheme[2].O2VolStep1)
                {
                    this._Module._Heat.CurrentBlowingScheme = 2;
                    this._Module.PushEvent(this._Module._Heat.BlowingScheme[2]);
                    this._Module.PushEvent(new cntBlowingSchemaEvent());
                }
                if (this._Module._Heat.CurrentBlowingScheme == 2 && curpersent > this._Module._Heat.BlowingScheme[3].O2VolStep1)
                {
                    this._Module._Heat.CurrentBlowingScheme = 3;
                    this._Module.PushEvent(this._Module._Heat.BlowingScheme[3]);
                    this._Module.PushEvent(new cntBlowingSchemaEvent());
                }
            }
            //if (_event.O2TotalVol)
            try
            {

                this._Module._Heat.LanceHistory.Add( _event);
            }
            catch { }

        }
    }
}
