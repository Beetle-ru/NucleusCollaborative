<Event>;;;
Operation;OPC.BlowingEvent;;Продувка
Flags;1;;
<Arguments>;;;
Converter1;BlowingFlag;S7:[PLC11]DB2,W20;главная продувка нач/кон 1=старт продувки, 0=конец продувки
Converter1;O2TotalVol;S7:[PLC11]DB2,W0;общий O2 расход
;;;
Converter2;BlowingFlag;S7:[PLC21]DB2,W20;главная продувка нач/кон 1=старт продувки, 0=конец продувки
Converter2;O2TotalVol;S7:[PLC21]DB2,W0;общий O2 расход
;;;
Converter3;BlowingFlag;S7:[PLC31]DB2,W20;главная продувка нач/кон 1=старт продувки, 0=конец продувки
Converter3;O2TotalVol;S7:[PLC31]DB2,W0;общий O2 расход
