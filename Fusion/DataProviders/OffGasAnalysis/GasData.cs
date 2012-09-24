using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffGasAnalysis
{
    public class GasData
    {
        private Dictionary<string, byte[]> _gasData = null;
        private string m_ID = null;
        private int _converterNumber = 0;
        public GasData(byte[] rawData)
        {
            m_ID = System.Text.Encoding.ASCII.GetString(rawData, 1, 4);
            _gasData = new Dictionary<string, byte[]>();

            string key = "";
            List<byte> value = new List<byte>();
            bool isInKey = true;
            for (int i = 6; i < rawData.Length; i++)
            {
                if (isInKey)
                {
                    if (rawData[i] == 0x1E) // окончание ключа, переключаем на заначение
                    {
                        isInKey = false;
                    }
                    else
                    {
                        key += System.Text.Encoding.GetEncoding("x-cp1251").GetString(rawData, i, 1);
                    }
                }
                else
                {
                    if (rawData[i] == 4) // конец пасылке
                    {
                        _gasData.Add(key, value.ToArray());
                        break;
                    }

                    if (rawData[i] == 0x1D) // новый ключь
                    {
                        isInKey = true;
                        _gasData.Add(key, value.ToArray());
                        key = "";
                        value.Clear();
                    }
                    else
                    {
                        value.Add(rawData[i]);
                    }
                }
            }
            // Определим номер конвертера по наличию параметра T?_CO2. Не красиво но работает.
            if (_gasData.ContainsKey("T1_CO2"))
            {
                _converterNumber = 1;
            }
            else if (_gasData.ContainsKey("T2_CO2"))
            {
                _converterNumber = 2;
            }
            else if (_gasData.ContainsKey("T3_CO2"))
            {
                _converterNumber = 3;
            }
        }

        public string ID
        {
            get { return m_ID; }
        }

        public string this[string key] 
        {
            get { return System.Text.Encoding.GetEncoding("x-cp1251").GetString(_gasData[key].ToArray()); }
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach(var pair in _gasData)
            {
                res.AppendLine(string.Format("{0}: {1};",pair.Key, this[pair.Key]));
            }
            return res.ToString();
        }

        #region Поля с данными по газу

        public double O2
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_O2", _converterNumber)]);
            }
        }

        public double CO
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_CO", _converterNumber)]);
            }
        }

        public double CO2
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_CO2", _converterNumber)]);
            }
        }

        public double H2
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_H2", _converterNumber)]);
            }
        }

        public double N2
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_N2", _converterNumber)]);
            }
        }

        public double AR
        {
            get
            {
                return double.Parse(this[string.Format("T{0}_AR", _converterNumber)]);
            }
        }

        #endregion
    }
}
