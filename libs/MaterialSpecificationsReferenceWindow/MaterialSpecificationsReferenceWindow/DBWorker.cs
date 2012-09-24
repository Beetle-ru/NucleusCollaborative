using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialSpecificationsReferenceWindow
{
    // singletone
    public class DBWorker
    {
        private List<MaterialReference> m_MaterialHash;
        private List<MaterialAnalysesReference> m_MaterialAnalysesHash;
        private DBLayer m_DB;

        public List<MaterialReference> MaterialHash { get { return m_MaterialHash; } }
        public List<MaterialAnalysesReference> MaterialAnalysesHash { get {return m_MaterialAnalysesHash; } }

        private void UpdateDbReference(out List<MaterialReference> reference, bool keepConnection)
        {
            reference = m_DB.GetMaterialReference();
            if (!keepConnection)
                m_DB.Close();
        }

        private void UpdateDbReference(out List<MaterialAnalysesReference> reference, bool keepConnection)
        {
            reference = m_DB.GetMaterialAnalysesReference();
            if (!keepConnection)
                m_DB.Close();
        }

        private DBWorker()
        {
            m_DB = new DBLayer();
            UpdateDbReference(out m_MaterialHash, true);
            UpdateDbReference(out m_MaterialAnalysesHash, false);
        }

        private static DBWorker _Instance = new DBWorker();

        public static DBWorker Instance
        {
            get { return _Instance; }
        }

        public string GetMaterialNameByID(int ID)
        {
            MaterialReference matRef = m_MaterialHash.Find(p => p.ID == ID);
            if (matRef != null)
                return matRef.NameEnglish;
            return string.Empty;
        }

        public int GetMaterialIdByName(string materialName)
        {
            MaterialReference matRef = m_MaterialHash.Find(p => p.NameEnglish == materialName);
            if (matRef != null)
                return matRef.ID;
            return -1;
        }
        
        public Dictionary<string, double> GetChemestryAttributes(int ID)
        {
            Dictionary<string, double> chemestryAttributes = new Dictionary<string, double>();
            if (m_MaterialAnalysesHash != null)
            {
                List<MaterialAnalysesReference> data = m_MaterialAnalysesHash.Where(p => p.ID == ID).ToList();
                foreach (MaterialAnalysesReference matAnalyses in data)
                {
                    chemestryAttributes.Add(matAnalyses.ElementName, matAnalyses.ElementValue);
                }
            }

            if (chemestryAttributes.Count == 0)
            {
                chemestryAttributes = m_DB.GetChemestryAttributes(ID);
                UpdateDbReference(out m_MaterialAnalysesHash, false);
            }

            return chemestryAttributes;
        }
    }

   

}
