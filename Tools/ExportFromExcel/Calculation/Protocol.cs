using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator
{
    public class Protocol
    {
        #region Данные получаемые один раз за плавку

        /// <summary>
        /// Продолжительность i-го шага вычисления
        /// </summary>
        public int dt { get; set; }
        /// <summary>
        /// Масса завалки с пальцев, кг
        /// </summary>
        public double mCh1 { get; set; }
        /// <summary>
        /// Масса залитого жидкого чугуна, кг
        /// </summary>
        public double mHI { get; set; }
        /// <summary>
        /// Интенсивность заливки шлака, содержащегося в чугуновозном ковше, кг/с
        /// </summary>
        public double mHISl { get; set; }
        /// <summary>
        /// Масса завалки на следующую плавку, кг
        /// </summary>
        public double mCh1SN { get; set; }
        /// <summary>
        /// Температура чугуна в ковше, К
        /// </summary>
        public double THI { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором открытие пальцев
        /// </summary>
        public int Ch1 { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором началась заливка жидкого чугуна
        /// </summary>
        public int HIB { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором окончилась заливка жидкого чугуна
        /// </summary>
        public int HIE { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором произвели завалку на следующую плавку в шахту
        /// </summary>
        public int Ch1SN { get; set; }
        /// <summary>
        /// Шибер сталевыпускного отверстия открыт
        /// </summary>
        public int StTapB { get; set; }
        /// <summary>
        /// Шибер сталевыпускного отверстия закрыты
        ///  </summary>
        public int StTapE { get; set; }
        /// <summary>
        /// Масса металла болота, кг
        /// </summary>
        public double mStH { get; set; }
        /// <summary>
        /// Масса шлака болота, кг
        /// </summary>
        public double mSlH { get; set; }
        /// <summary>
        /// Температура болота, К
        /// </summary>
        public double TH { get; set; }
        /// <summary>
        /// Физическое тепло лома в шахте с предыдущей плавки, МДж
        /// протокол предыдущей плавки
        /// </summary>
        public double WCh1SP { get; set; }

        #endregion

        #region Не периодические данные.
        
        /// <summary>
        /// Масса подвалки, кг
        /// </summary>
        public double mCh2S { get; set; }
        /// <summary>
        /// Масса разовой порции УСМ в загрузочном бункере, кг
        /// </summary>
        public double mCkAdd { get; set; }
        /// <summary>
        /// Масса извести в загрузочном бункере, кг
        /// </summary>
        public double mLmAdd { get; set; }
        /// <summary>
        /// Масса доломита в загрузочном бункере, кг
        /// </summary>
        public double mDlmtAdd  { get; set; }
        /// <summary>
        ///  Шаг вычисления, на котором произвели подвалку в шахту
        /// </summary>
        public int Ch2S { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором произвели открытие загрузочного бункера
        /// </summary>
        public int SlAddB { get; set; }
        /// <summary>
        /// Шаг вычисления, на котором произвели закрытие загрузочного бункера
        /// </summary>
        public int SlAddE { get; set; }
        

        #endregion

        #region Периодические данные

        /// <summary>
        /// Шаг вычисления
        /// </summary>
        public int i { get; set; }
        /// <summary>
        /// Время с начала плавки, c
        /// </summary>
        public int t { get; set; }
        /// <summary>
        /// Активная мощность трансформатора), МВт
        /// </summary>
        public double We { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания CH4 через горелку, м3/ч
        /// </summary>
        public double UCH4B { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания O2 через горелку, м3/ч
        /// </summary>
        public double UO2B { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания CH4 через горелку RCB в режиме горелки, м3/ч
        /// </summary>
        public double UCH4RB { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания O2 через горелку RCB в режиме горелки, UO2RB 
        /// </summary>
        public double UO2RB { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания O2 через горелку RCB в режиме фурмы, м3/ч
        /// </summary>
        public double UO2RL { get; set; }
        /// <summary>
        /// Объемная интенсивность вдувания O2 через сводовую фурму, м3/ч
        /// </summary>
        public double UO2L { get; set; }
        /// <summary>
        /// Интенсивность вдувания порошкового УСМ, кг/с
        /// </summary>
        public double mCP { get; set; }
        /// <summary>
        /// Скорость выпуска металла, кг/с
        /// </summary>
        public double mStTap { get; set; }
        /// <summary>
        /// Скорость слива шлака во время выпуска металла, кг/с
        /// </summary>
        public double mSlTap { get; set; }
        /// <summary>
        /// Скорость слива шлака, кг/с
        /// </summary>
        public double mSlTip { get; set; }
        /// <summary>
        /// Температура воздуха в цехе, Tenv
        /// </summary>
        public double Tenv { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на входе в охлаждающий контур стен, К
        /// </summary>
        public double TWout { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на выходе из охлаждающего контура стен, К
        /// </summary>
        public double TWin { get; set; }
        /// <summary>
        /// Объемный расход охлаждающей воды в контуре охлаждения стен, м3/ч
        /// </summary>
        public double UCWW { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на входе в охлаждающий контур пальцев, К
        /// </summary>
        public double TFout { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на выходе из охлаждающего контура пальцев, К
        /// </summary>
        public double TFin { get; set; }
        /// <summary>
        /// Объемный расход охлаждающей воды в контуре охлаждения пальцев, м3/ч
        /// </summary>
        public double UCWF { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на входе в охлаждающий контур пальцев, К
        /// </summary>
        public double TRout { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на выходе из охлаждающего контура пальцев, К
        /// </summary>
        public double TRin { get; set; }
        /// <summary>
        /// Объемный расход охлаждающей воды в контуре охлаждения пальцев, м3/ч
        /// </summary>
        public double UCWR { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на входе в охлаждающий контур шахты, К
        /// </summary>
        public double TSout { get; set; }
        /// <summary>
        /// Температура охлаждающей воды на выходе из охлаждающего контура шахты, К
        /// </summary>
        public double TSin { get; set; }
        /// <summary>
        /// Объемный расход охлаждающей воды в контуре охлаждения шахты, м3/ч
        /// </summary>
        public double UCWS { get; set; }
        /// <summary>
        /// Усредненная (по нескольким точкам) температура поверхности нижней части корпуса печи, К
        /// </summary>
        public double TS { get; set; }
        /// <summary>
        /// Интенсивность подсоса воздуха в печь, кг/с
        /// </summary>
        public double mAir { get; set; }
        /// <summary>
        /// Коэффициент перекрытия рабочего окна дверцей
        /// 0 - полностью закрыто;
        /// 1 - полностью открыто
        /// </summary>
        public double OCSD { get; set; }

        #endregion
    }
}
