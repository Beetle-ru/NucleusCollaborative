using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine {
    /// <summary>
    /// Класс описывает состояние весов.
    /// Пустые -   WeigherLoadFree = True,  WeigherUnLoadFree = False, WeigherEmpty = True;
    /// Заняты -   WeigherLoadFree = True,  WeigherUnLoadFree = True,  WeigherEmpty = False;
    /// Загрузка - WeigherLoadFree = False, WeigherUnLoadFree = False, WeigherEmpty = False;
    /// Выгрузка - WeigherLoadFree = False, WeigherUnLoadFree = False, WeigherEmpty = True;
    /// </summary>
    internal class WeigherState {
        /// <summary>
        /// Свободны для загрузки - True
        /// </summary>
        public bool WeigherLoadFree { set; get; }

        /// <summary>
        /// Свободны для выгрузки - True
        /// </summary>
        public bool WeigherUnLoadFree { set; get; }

        /// <summary>
        /// Пустые - True
        /// </summary>
        public bool WeigherEmpty { set; get; }

        /// <summary>
        /// Признак того что класс заполнен реальными данными
        /// </summary>
        public bool Actual { set; get; }

        public enum State {
            NoActual,
            Empty,
            Load,
            Unload,
            Full
        };

        public State GetState() {
            State s = State.NoActual;
            if (Actual && WeigherLoadFree && (!WeigherUnLoadFree) && WeigherEmpty)
                s = State.Empty;
            if (Actual && (!WeigherLoadFree) && (!WeigherUnLoadFree) && (!WeigherEmpty))
                s = State.Load;
            if (Actual && WeigherLoadFree && WeigherUnLoadFree && (!WeigherEmpty))
                s = State.Full;
            if (Actual && (!WeigherLoadFree) && (!WeigherUnLoadFree) && WeigherEmpty)
                s = State.Unload;
            return s;
        }

        public WeigherState() {
            WeigherLoadFree = false;
            WeigherUnLoadFree = false;
            WeigherEmpty = false;
            Actual = false;
        }

        public override string ToString() {
            string str = base.ToString() + "<";
            str += WeigherLoadFree.ToString() + ";";
            str += WeigherUnLoadFree.ToString() + ";";
            str += WeigherEmpty.ToString() + ";";
            str += Actual.ToString() + ";";
            return str + ">";
        }
    }
}