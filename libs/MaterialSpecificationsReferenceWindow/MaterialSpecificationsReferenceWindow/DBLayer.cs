using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace MaterialSpecificationsReferenceWindow
{
    class DBLayer
    {
        OracleConnection m_Connection;


        public DBLayer()
        {
            m_Connection = new OracleConnection("DATA SOURCE=localhost;PERSIST SECURITY INFO=True;USER ID=SMK;PASSWORD=SMK");
            m_Connection.Open();
        }
        public void Close()
        {
            m_Connection.Close();
        }
        private string OracleDate(DateTime Date)
        {
            return string.Format("to_date('{0}','dd.mm.yyyy HH24:MI:SS')", Date.ToString());
        }

        private string CheckNubmerForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "0";
        }

        private string CheckDateForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "01.01.01";
        }

        private OracleDataReader Execute(string sql)
        {
            if (m_Connection.State != System.Data.ConnectionState.Open)
                m_Connection.Open();
            OracleCommand dbcmd = m_Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteReader();
        }

        public List<MaterialReference> GetMaterialReference()
        {
            List<MaterialReference> materialReference = new List<MaterialReference>();
            string sql = "SELECT mat_id,name_english, name_other FROM MATERIAL_SPEC";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                materialReference.Add(new MaterialReference(){ID = int.Parse(CheckNubmerForNull(reader[0].ToString())),NameEnglish = reader[1].ToString(),NameOther=reader[2].ToString() } );
            }
            reader.Close();
            return materialReference;
        }

        public List<MaterialAnalysesReference> GetMaterialAnalysesReference()
        {
            List<MaterialAnalysesReference> materialAnalysesReference = new List<MaterialAnalysesReference>();
            string sql = "SELECT MAT_ID, EL_NAME, EL_VALUE FROM material_ana";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                materialAnalysesReference.Add(new MaterialAnalysesReference() { ID = int.Parse(CheckNubmerForNull(reader[0].ToString())), ElementName = reader[1].ToString(), ElementValue = double.Parse(CheckNubmerForNull(reader[2].ToString())) });
            }
            reader.Close();
            return materialAnalysesReference;
        }

        public Dictionary<string, double> GetChemestryAttributes(string MaterialName)
        {
            string sql = "SELECT ma.EL_NAME, ma.EL_VALUE FROM material_ana ma, material_spec m WHERE m.MAT_ID=ma.MAT_ID AND m.NAME_ENGLISH='" + MaterialName+"'";
            OracleDataReader reader = Execute(sql);
            Dictionary<string, double> chemestryAttributes = new Dictionary<string, double>();
            while (reader.Read())
            {
                chemestryAttributes.Add(reader[0].ToString(), double.Parse(CheckNubmerForNull(reader[1].ToString())));
            }
            reader.Close();
            return chemestryAttributes;
        }

        public Dictionary<string, double> GetChemestryAttributes(int ID)
        {
            string sql = "SELECT EL_NAME, EL_VALUE FROM material_ana WHERE MAT_ID=" + ID.ToString();
            OracleDataReader reader = Execute(sql);
            Dictionary<string, double> chemestryAttributes = new Dictionary<string, double>();
            while (reader.Read())
            {
                chemestryAttributes.Add(reader[0].ToString(), double.Parse(CheckNubmerForNull(reader[1].ToString())));
            }
            reader.Close();
            return chemestryAttributes;
        }

        

       



 
    }
}
