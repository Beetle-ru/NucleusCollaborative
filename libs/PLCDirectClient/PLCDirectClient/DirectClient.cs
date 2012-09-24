using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nordsteel.Data.PLC
{
    public class DirectClient
    {
        private bool _Connected;

        private int _PortNumber;
        private string _IPAdress;
        private int _Rack;
        private int _Slot;

        private libnodave.daveOSserialType _OSSerialType;
        private libnodave.daveInterface _Interface;
        private libnodave.daveConnection _Connection;

        public DirectClient(int portNumber, string IPAdress, int rack, int slot)
        {
            _PortNumber = portNumber;
            _IPAdress = IPAdress;
            _Rack = rack;
            _Slot = slot;
        }

        public bool Connect()
        {
            try
            {
                _OSSerialType.rfd = libnodave.openSocket(_PortNumber, _IPAdress);
                _OSSerialType.wfd = _OSSerialType.rfd;

                if (_OSSerialType.rfd > 0)
                {
                    _Interface = new libnodave.daveInterface(_OSSerialType, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                    _Interface.setTimeout(1000000);

                    _Connection = new libnodave.daveConnection(_Interface, 0, _Rack, _Slot);
                    _Connected = _Connection.connectPLC() == 0 ? true : false;
                }
            }
            catch
            {
                _Connected = false;
            }
            return _Connected;
        }

        public byte[] ReadBytes(int DBNumber,int offSet, int lenght)
        {
            byte[] buffer = new byte[lenght];
            _Connection.readBytes(libnodave.daveDB, DBNumber, offSet, lenght, buffer);
            float d =_Connection.getFloat();
            return buffer;
        }

        public int WriteBytes(byte[] buffer, int DBNumber, int offSet)
        {
            return _Connection.writeBytes(libnodave.daveDB, DBNumber, offSet, buffer.Length, buffer);
        }

        public void Disconnect()
        {
            _Connection.disconnectPLC();
            libnodave.closeSocket(_OSSerialType.rfd);
        }
    }
}
