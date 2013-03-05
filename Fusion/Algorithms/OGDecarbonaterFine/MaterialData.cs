using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OGDecarbonaterFine
{
    public class MaterialData
    {
        /// <summary>
        /// Название используемое в программе
        /// </summary>
        public string CodeName;

        /// <summary>
        /// Название используемое в системе (в контроллерах и т.д.)
        /// </summary>
        public string SystemName;

        /// <summary>
        /// Накопленный вес
        /// </summary>
        public double TotalWeight;
    }

    public class SupportMaterials
    {
        public readonly List<MaterialData> MaterialList;

        public SupportMaterials()
        {
            MaterialList = new List<MaterialData>();

            MaterialList.Add(new MaterialData() { CodeName = "DOLOMS", SystemName = "ДОЛОМС", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "DOLMIT", SystemName = "ДОЛМИТ", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "FOM",    SystemName = "ФОМ   ", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "COKE",   SystemName = "KOKS  ", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "LIME",   SystemName = "ИЗВЕСТ", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "ALCONZ", SystemName = "ALKонц", TotalWeight = 0 });
            MaterialList.Add(new MaterialData() { CodeName = "MAXG",   SystemName = "МАХГ  ", TotalWeight = 0 });
        }

        public void SetTotalWeight(string name, double totalWeigth, bool isCodeName = true)
        {
            for (int i = 0; i < MaterialList.Count; i++)
            {
                var mat = MaterialList[i];
                if ((isCodeName && (mat.CodeName == name)) || ((!isCodeName) && (mat.SystemName == name)))
                {
                    MaterialList[i].TotalWeight = totalWeigth;
                    break;
                }
            }
        }

        public double GetTotalWeight(string name, bool isCodeName = true)
        {
            for (int i = 0; i < MaterialList.Count; i++)
            {
                var mat = MaterialList[i];
                if ((isCodeName && (mat.CodeName == name)) || ((!isCodeName) && (mat.SystemName == name)))
                {
                    return MaterialList[i].TotalWeight;
                }
            }
            return 0.0;
        }

        public string GetSynonym(string name, bool isCodeName)
        {
            for (int i = 0; i < MaterialList.Count; i++)
            {
                var mat = MaterialList[i];
                if ((isCodeName && (mat.CodeName == name)) || ((!isCodeName) && (mat.SystemName == name)))
                {
                    return isCodeName ? MaterialList[i].SystemName : MaterialList[i].CodeName;
                }
            }
            return "";
        }
    }
}
