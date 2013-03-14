<Event>;;;
Operation;OPC.SlagBlowingEvent;;по расходу азота на раздувку шлака
Flags;1;;
<Arguments>;;;
Converter1;SlagBlowingFlag;S7:[PLC11]DB2,W28;N раздувка шлака нач(1)/кон(0) 
Converter1;NVol;S7:[PLC11]DB2,W16;N расход азота на раздувку шлака
;;;
Converter2;SlagBlowingFlag;S7:[PLC21]DB2,W28;N раздувка шлака нач(1)/кон(0) 
Converter2;NVol;S7:[PLC21]DB2,W16;N расход азота на раздувку шлака
;;;
Converter3;SlagBlowingFlag;S7:[PLC31]DB2,W28;N раздувка шлака нач(1)/кон(0) 
Converter3;NVol;S7:[PLC31]DB2,W16;N расход азота на раздувку шлака
