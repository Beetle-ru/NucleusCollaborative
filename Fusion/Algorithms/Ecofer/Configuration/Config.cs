using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configuration
{
    public static class Config
    {
        public static string DBConnectionStringLocal = "metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=\"data source=.;initial catalog=L2ModelConverter;integrated security=True;multipleactiveresultsets=True;App=EntityFramework\"";
        public static string DBConnectionStringNativeLocal = "data source=.;initial catalog=L2ModelConverter;integrated security=True;multipleactiveresultsets=True";
        public static string DBConnectionStringRemote = "metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=\"data source=devel.vyvojsw.cz;initial catalog=L2ModelConverter;uid=L2ModelConverter;pwd=L2ModelConverter;multipleactiveresultsets=True;App=EntityFramework\"";
        public static string DBConnectionStringNativeRemote = "data source=devel.vyvojsw.cz;initial catalog=L2ModelConverter;uid=L2ModelConverter;pwd=L2ModelConverter;multipleactiveresultsets=True";

#if DEBUG
        public static string DBConnectionString = "metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=\"data source=.;initial catalog=L2ModelConverter;integrated security=True;multipleactiveresultsets=True;App=EntityFramework\"";
        public static string DBConnectionStringNative = "data source=.;initial catalog=L2ModelConverter;integrated security=True;multipleactiveresultsets=True";
#else
        public static string DBConnectionString = "metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=\"data source=devel.vyvojsw.cz;initial catalog=L2ModelConverter;uid=L2ModelConverter;pwd=L2ModelConverter;multipleactiveresultsets=True;App=EntityFramework\"";
        public static string DBConnectionStringNative = "data source=devel.vyvojsw.cz;initial catalog=L2ModelConverter;uid=L2ModelConverter;pwd=L2ModelConverter;multipleactiveresultsets=True";
#endif
    }
}
