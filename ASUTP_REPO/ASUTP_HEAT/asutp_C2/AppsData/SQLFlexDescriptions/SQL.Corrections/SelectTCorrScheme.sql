SELECT 
   SCHEMA, ITEM, CMIN, 
   CMAX, OXYGEN, HEATING
FROM TCORRECTION
WHERE SCHEMA = <Schema>
ORDER BY ITEM