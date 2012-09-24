using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulator
{
    public static class Constants
    {

        #region Типовой состав металла

        /// <summary>
        /// >Концентрация С в металле болота, % масс.
        /// </summary>
        public const double CCH = 0.07;

        /// <summary>
        /// Концентрация Fe в металле болота, % масс.
        /// </summary>
        public const double CFeH = 99.86;
        /// <summary>
        /// Концентрация Mn в металле болота, % масс.
        /// </summary>
        public const double CMnH = 0.05;
        /// <summary>
        /// Концентрация Si в металле болота, % масс.
        /// </summary>
        public const double CSiH = 0.01; 
        /// <summary>
        /// Концентрация O2 в металле болота, % масс.
        /// </summary>
        public const double COH = 0.01; 
        
        #endregion

        #region Типовой состав шлака

        /// <summary>
        /// Концентрация FeO в шлаке болота, % масс.
        /// </summary>
        public const double CFeOH = 32.8;
        /// <summary>
        /// Концентрация MnO в шлаке болота, % масс.
        /// </summary>
        public const double CMnOH = 3.9;
        /// <summary>
        /// >Концентрация SiO2 в шлаке болота, % масс.
        /// </summary>
        public const double CSiO2H = 20.7;
        /// <summary>
        /// Концентрация CaO в шлаке болота, % масс.
        /// </summary>
        public const double CCaOH = 42.6;   

        #endregion

        #region Типовой состав чугуна

        /// <summary>
        /// >Концентрация С в чугуне, % масс.
        /// </summary>
        public const double CCHi = 4.0;
        /// <summary>
        /// Концентрация Fe в чугуне, % масс.
        /// </summary>
        public const double CFeHi = 94.8;
        /// <summary>
        /// Концентрация Mn в чугуне, % масс.
        /// </summary>
        public const double CMnHi = 0.5;
        /// <summary>
        /// Концентрация Si в чугуне, % масс.
        /// </summary>
        public const double CSiHi = 0.7; 

        #endregion  

        #region Типовой состав лома

        /// <summary>
        /// >Концентрация С в ломе, % масс.
        /// </summary>
        public const double CCСн = 0.3;
        /// <summary>
        /// Концентрация Fe в ломе, % масс.
        /// </summary>
        public const double CFeСн = 98.8;
        /// <summary>
        /// Концентрация Mn в ломе, % масс.
        /// </summary>
        public const double CMnСн = 0.7;
        /// <summary>
        /// Концентрация Si в ломе, % масс.
        /// </summary>
        public const double CSiСн = 0.3; 

        #endregion 


        /// <summary>
        /// Мольная масса FeO, кг/моль
        /// </summary>
        public const double muFeO = 0.0718;
        /// <summary>
        /// Мольная масса MnO, кг/моль
        /// </summary>
        public const double muMnO = 0.0709;
        /// <summary>
        /// Мольная масса SiO2, кг/моль
        /// </summary>
        public const double muSiO2 = 0.0601;
        /// <summary>
        /// Мольная масса CaO, кг/моль
        /// </summary>
        public const double muCaO = 0.0561;
        /// <summary>
        /// Мольная масса C, кг/моль
        /// </summary>
        public const double muC = 0.0120;
        /// <summary>
        /// Мольная масса Fe, кг/моль
        /// </summary>
        public const double muFe = 0.0558;
        /// <summary>
        /// Мольная масса Mn, кг/моль
        /// </summary>
        public const double muMn = 0.0549;
        /// <summary>
        /// Мольная масса Si, кг/моль
        /// </summary>
        public const double muSi = 0.0281;
        /// <summary>
        /// Мольная масса O, кг/моль
        /// </summary>
        public const double muO = 0.0160;
        /// <summary>
        /// Мольная масса CH4, кг/моль
        /// </summary>
        public const double muCH4 = 0.0160;
        /// <summary>
        /// Мольная масса CO, кг/моль
        /// </summary>
        public const double muCO = 0.0280;
        /// <summary>
        /// Мольная масса CO2, кг/моль
        /// </summary>
        public const double muCO2 = 0.0440;
        /// <summary>
        /// Мольная масса H2O, кг/моль
        /// </summary>
        public const double muH2O = 0.0180;
        /// <summary>
        /// Универсальная газовая постоянная, Дж/(моль*К)
        /// </summary>
        public const double R = 8.31;
        /// <summary>
        /// Изменение энтальпии при восстановлении FeO из шлака, Дж/моль
        /// </summary>
        public const int HRedFeO = 127000;
        /// <summary>
        /// Изменение энтальпии при восстановлении MnO из шлака, Дж/моль
        /// </summary>
        public const int HRedMnO = 295900;
		/// <summary>
		/// Изменение энтальпии при восстановлении SiO2 из шлака, Дж/моль
		/// </summary>
		public const int HRedSiO2 =	579200;
        /// <summary>
        /// Изменение энтальпии при растворении C в металле, Дж/моль
        /// </summary>
        public const int HRedC = 22600;
        /// <summary>
        /// Изменение энтальпии при окислении Fe, Дж/моль
        /// </summary>
        public const int HOxFeO = 245000;
        /// <summary>
        /// Изменение энтальпии при окислении C, Дж/моль
        /// </summary>
        public const int HOxCO = 140600;
        /// <summary>
        /// Изменение энтальпии при окислении Mn, Дж/моль
        /// </summary>
        public const int HOxMnO = 413900;
		/// <summary>
		/// Изменение энтальпии при окислении Si, Дж/моль
		/// </summary>		
		public const int HOxSiO2 = 815200;
        /// <summary>
        /// Изменение энтальпии при растворении O, Дж/моль
        /// </summary>
        public const int HOxO = 117000;
        /// <summary>
        /// Параметр взаимодействия C по Mn
        /// </summary>
        public const double eCMn = -0.012;
        /// <summary>
        /// Параметр взаимодействия Mn по С
        /// </summary>
        public const double eMnC = -0.0700;
        /// <summary>
        /// Параметр взаимодействия Mn по Mn
        /// </summary>
        public const int eMnMn = 0;
        /// <summary>
        /// Параметр взаимодействия Mn по Si
        /// </summary>
        public const int eMnSi = 0;
        /// <summary>
        /// Параметр взаимодействия Si по Mn
        /// </summary>
        public const double eSiMn = 0.0020;
        /// <summary>
        /// Параметр взаимодействия C по O
        /// </summary>
        public const double eCO = -0.3400;
        /// <summary>
        /// Параметр взаимодействия Mn по O
        /// </summary>
        public const double eMnO = -0.0830;
        /// <summary>
        /// Параметр взаимодействия Si по O
        /// </summary>
        public const double eSiO = -0.2300;
        /// <summary>
        /// Параметр взаимодействия O по C
        /// </summary>
        public const double eOC = -0.4500;
        /// <summary>
        /// Параметр взаимодействия O по Mn
        /// </summary>
        public const double eOMn = -0.0210;
        /// <summary>
        /// Параметр взаимодействия O по Si
        /// </summary>
        public const double eOSi = -0.1310;
        /// <summary>
        /// Молярный объем газа при н.у., м3/моль
        /// </summary>
        public const double Vmol = 0.0224;
        /// <summary>
        /// Удельная теплота сгорания природного газа, МДж/кг
        /// </summary>
        public const double rCH4 = 50.25;
        /// <summary>
        /// Удельная теплоемкость природного газа (20 ºС), МДж/(кг*К)
        /// </summary>
        public const double cNG = 0.00220;
        /// <summary>
        /// Удельная теплоемкость кислорода (20 ºС), МДж/(кг*К)
        /// </summary>
        public const double cOxg = 0.00092;
        /// <summary>
        /// Удельная теплоемкость жидкого чугуна, МДж/(кг*К)
        /// </summary>
        public const double cHI = 0.00088;
        /// <summary>
        /// Удельная теплоемкость шлака в чугуновозном ковше, МДж/(кг*К)
        /// </summary>
        public const double cHISl = 0.00125;
        /// <summary>
        /// Удельная теплоемкость УСМ, МДж/(кг*К)
        /// </summary>
        public const double cCk = 0.00085;
        /// <summary>
        /// Удельная теплоемкость извести, МДж/(кг*К)
        /// </summary>
        public const double cLm = 0.00090;
        /// <summary>
        /// Удельная теплоемкость доломита, МДж/(кг*К)
        /// </summary>
        public const double cDlmt = 0.00093;
        /// <summary>
        /// Удельная теплоемкость порошка УСМ, МДж/(кг*К)
        /// </summary>
        public const double cCP = 0.00121;
        /// <summary>
        /// Плотность охлаждающей воды (от 35 до 55 °С), кг/м3
        /// </summary>
        public const double roCW = 0.98990;
        /// <summary>
        /// Удельная теплоемкость охлаждающей воды (от 35 до 55 °С), МДж/(кг*К)
        /// </summary>
        public const double cCW = 4.1810;
        /// <summary>
        /// Коэффициент конвективной теплоотдачи, МВт/(м2*К)
        /// Необходим расчет (зависит от температур воздуха и теплоотдающей поверхности). Ориентировочно - 5,4*10-6.
        /// </summary>
        public const double alfSI = 5.4E-6;
        /// <summary>
        /// Константа Стефана-Больцмана, МВт/(м2*К4)
        /// </summary>
        public const double sgm = 5.7E-14;
        /// <summary>
        /// Степень черноты поверхности корпуса  печи
        /// </summary>
        public const double epsS = 0.9;
        /// <summary>
        /// Удельная теплоемкость CO2 (1600 ºС), МДж/(кг*К)
        /// </summary>
        public const double cCO2 = 0.00121;
        /// <summary>
        /// Удельная теплоемкость CO (1600 ºС), МДж/(кг*К)
        /// </summary>
        public const double cCO = 0.00118;
        /// <summary>
        /// Удельная теплоемкость H2O (пара) (1600 ºС), МДж/(кг*К)
        /// </summary>
        public const double cH2O = 0.00232;
        /// <summary>
        /// Удельная теплоемкость сухого воздуха (1600 ºС), МДж/(кг*К)
        /// </summary>
        public const double cAirH = 0.00114;
        /// <summary>
        /// Площадь внешней поверхности нижней (неводоохлаждаемой) части корпуса печи, м2
        /// Необходим расчет
        /// </summary>
        public const double AShell = 1;
        /// <summary>
        /// Степень черноты рабочего пространства печи
        /// </summary>
        public const double epsSD = 0.6;
        /// <summary>
        /// Площадь рабочего окна, м2
        /// Необходим расчет
        /// </summary>
        public const double ASD = 1;
        /// <summary>
        /// Удельная теплоемкость жидкого шлака, МДж/(кг*К)
        /// </summary>
        public const double cSl = 0.00125;
        /// <summary>
        /// Удельная теплоемкость жидкой стали, МДж/(кг*К)
        /// </summary>
        public const double cStL = 0.000837;
        /// <summary>
        /// Удельная теплоемкость твердой стали, МДж/(кг*К)
        /// </summary>
        public const double cStS = 0.0007;
        /// <summary>
        /// Удельная теплоемкость сухого воздуха (до 50 °С), МДж/(кг*К)
        /// </summary>
        public const double cAirC = 0.00101;
        /// <summary>
        /// Скрытая теплота плавления стали, МДж/кг
        /// </summary>
        public const double lmbdSt = 0.27216;
        /// <summary>
        /// Температура ликвидус стали, К
        /// </summary>
        public const double TStL = 1803.15;
        /// <summary>
        /// Теплосодержание стали при температуре ликвидус, МДж/кг
        /// </summary>
        public const double EStL = 1.5344;
        /// <summary>
        /// Мольная масса H, кг/моль
        /// </summary>
        public const double muH = 0.0010;
        /// <summary>
        /// Теплосодержание чугуна при температуре ликвидус, МДж/кг
        /// </summary>
        public const double EIL = 1.3152;
        /// <summary>
        /// Температура ликвидус чугуна, К
        /// </summary>
        public const double TIL = 1473.15;
        /// <summary>
        /// Мольная масса воздуха, кг/моль
        /// </summary>
        public const double muAir = 0.02898;
        /// <summary>
        /// Парциальное давление CO в рабочем пространстве печи, атм.	
        /// </summary>
        public const int PCO = 1;
        /// <summary>
        /// Парциальное давление O2 в рабочем пространстве печи, атм.	
        /// </summary>
        public const int PO2 = 1;
        /// <summary>
        /// Доля анионов кислорода в шлаке
        /// </summary>
        public const int xO = 1;
        /// <summary>
        /// Тепловой КПД шахты
        /// Необходим расчет
        /// </summary>
        public const int COES = 1;
        /// <summary>
        /// Момент окончания предыдущей плавки
        /// пока принимаем равным моменту начала текущей плавки
        /// </summary>
        public const int PHE = 0;
    }
}
