create or replace package pck_opc is
  -- Author  : Poritski
  -- Created : 22.11.2011
  PROCEDURE StorePLC_Value(sGroupInp IN VARCHAR2
                     ,sAliasInp IN VARCHAR2
                     ,sPLC_AdrInp IN VARCHAR2
                     ,iTypeCodeInp   IN number
                     ,sDataValueInp IN VARCHAR2
                     ,dtDataValueInp IN date
                     ,iDataValueInp  IN number
                     ,iReadCntInp   IN number
                     ,iReadCntMaxInp  IN number
                     ,dtMaxInp IN date
                     );

end;
/
CREATE OR REPLACE PACKAGE BODY pck_opc IS

   intSQLCode     NUMBER;
   strSQLErrMess  VARCHAR2(260);
   intJobNo       NUMBER;

  PROCEDURE StorePLC_Value(sGroupInp IN VARCHAR2
                     ,sAliasInp IN VARCHAR2
                     ,sPLC_AdrInp IN VARCHAR2
                     ,iTypeCodeInp   IN number
                     ,sDataValueInp IN VARCHAR2
                     ,dtDataValueInp IN date
                     ,iDataValueInp  IN number
                     ,iReadCntInp   IN number
                     ,iReadCntMaxInp  IN number
                     ,dtMaxInp IN date
                     ) IS
iCnt number;                       
CURSOR curRead IS 
   SELECT o.sgroup
   FROM OPC_PLC_Points o;
                     
begin
--   TRACE.TRACE_MES(sAlias||' '||sDataValue||' '||to_char(dtDataValue,'dd.mm.yy hh24:mi:ss')||' '||to_char(iDataValue), 'StorePLC_Value',0);
   select count(*) into iCnt from OPC_PLC_Points o
    where o.salias = sAliasInp;
   if iCnt=0 then
      insert into OPC_PLC_Points (sGroup,sAlias,sPlc_Adr,iTypeCode)
      values (sGroupInp,sAliasInp,sPLC_AdrInp,iTypeCodeInp);
   end if;
   update OPC_PLC_Points o set iReadCnt=iReadCntInp,iReadCntMax=iReadCntMaxInp,dtMax=dtMaxInp
     where o.salias = sAliasInp;
   if iTypeCodeInp =0 then
      update OPC_PLC_Points o set sDataValue=sDataValueInp
        where o.salias = sAliasInp;
   elsif iTypeCodeInp =1 then
      update OPC_PLC_Points o set dtDataValue=dtDataValueInp
        where o.salias = sAliasInp;
   else
      update OPC_PLC_Points o set iDataValue=iDataValueInp
        where o.salias = sAliasInp;
   end if;
   commit;

   Exception
      When others then
      intSQLCode:=SQLCODE;
       strSQLErrMess:=SUBSTR(SQLERRM,1,150);
      TRACE.TRACE_MES(DBMS_UTILITY.FORMAT_ERROR_STACK, 'StorePLC_Value',1);
--      TRACE_LOG.LOGGER('StorePLC_Value: sAlias '||sAlias||' Exception: '||strSQLErrMess, 'TRIGGERS.LOG');
end;
                     
END;
/
