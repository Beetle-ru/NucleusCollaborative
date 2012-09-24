using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using DTO;
using Data;
using Data.Model;
using Implements;
using Common;

namespace ModelRunner
{
    class DynPrepare
    {
        private const int _N_matElements = 74;

        private static HeatCharge.FPCarrier fp = new HeatCharge.FPCarrier();
        private static MINP_GD_MaterialElementDTO[] mael = new MINP_GD_MaterialElementDTO[_N_matElements];
        private static MINP_GD_MaterialItemsDTO ps(string key, double val)
        {
            var vx = new MINP_GD_MaterialItemsDTO();
            vx.Amount_p = val;
            vx.MINP_GD_MaterialElement = mael[fp.name2ix(key)];
            return vx;
        }

        private static MINP_GD_MaterialItemsDTO ps(int ix, double val)
        {
            var vx = new MINP_GD_MaterialItemsDTO();
            vx.Amount_p = val;
            vx.MINP_GD_MaterialElement = mael[ix];
            return vx;
        }

        static void Main(string[] args)
        {
            using (var l = new Logger("ModelRunner"))
            {
                try
                {
                    var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.PerSecond");
                    var fexi = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.PerSecond.Index");
                    fexi.AddArg("C", 0);
                    fexi.AddArg("Т", 69);
                    fexi.AddArg("Si", 1);
                    fexi.AddArg("Mn", 2);
                    fexi.AddArg("P", 3);
                    fexi.AddArg("Fe", 32);
                    fexi.AddArg("FeO", 61);
                    fexi.AddArg("CaO", 50);
                    fexi.AddArg("SiO2", 51);
                    fexi.AddArg("MnO", 53);
                    fexi.AddArg("MgO", 63);
                    fexi.AddArg("CaO/SiO2", 66);

                    for (var i = 0; i < _N_matElements; i++)
                    {
                        mael[i] = new MINP_GD_MaterialElementDTO();
                        mael[i].Vector = (int)(1.0 / fp.fp[i]._rcv);
                        mael[i].Mm = fp.fp[i].Mm;
                        mael[i].O2 = fp.fp[i].O2Stoichio;
                        mael[i].E_Ox1 = fp.fp[i].E_ox1;
                        mael[i].E_Ox2 = fp.fp[i].E_ox2;
                        mael[i].Eta_Ox1 = fp.fp[i].Eta_ox1;
                        mael[i].Eta_Ox2 = fp.fp[i].Eta_ox2;
                        mael[i].Index = i;
                    }
                    MINP.MINP_GD_MaterialElements = new Dictionary<int, DTO.MINP_GD_MaterialElementDTO>();
                    for (var i = 0; i < _N_matElements; i++)
                    {
                        MINP.MINP_GD_MaterialElements.Add(i, mael[i]);
                    }

                    var aInputData = new DynamicInput();

                    # region Charging

                    aInputData.ChargedMaterials = new List<DTO.MINP_MatAddDTO>();

                    // Iron
                    var matIron = new DTO.MINP_MatAddDTO();
                    matIron.ShortCode = "01Metal";
                    matIron.Amount_kg = 300000;
                    matIron.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1400.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 340.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 4.75));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.55));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.27));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.062));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.017));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.02));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.007));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.005));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.01));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 10.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 93.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9921));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 100.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 99.0));
                    matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 7000.0));
                    aInputData.ChargedMaterials.Add(matIron);

                    // Scrap
                    var matScrap = new DTO.MINP_MatAddDTO();
                    matScrap.ShortCode = "02Scrap";
                    matScrap.Amount_kg = 146000;
                    matScrap.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 380.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.01));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.2));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.2));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.01));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.005));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al", 0.03));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.01));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.005));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.005));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.005));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.1));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 50.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 96.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 0.5));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 0.5));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 2.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9962));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 1.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 1200.0));
                    aInputData.ChargedMaterials.Add(matScrap);

                    MINP.MINP_GD_ModelMaterials =
                        new Dictionary<Common.Enumerations.MINP_GD_Material_ModelMaterial, DTO.MINP_GD_MaterialDTO>();
                    // CaO ИЗВЕСТ
                    var matCaO = new DTO.MINP_MatAddDTO();
                    matCaO.ShortCode = "04CaO";
                    matCaO.Amount_kg = 8647;
                    matCaO.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 96.5));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 0.5));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 2.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    aInputData.ChargedMaterials.Add(matCaO);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.CaO, matCaO.MINP_GD_Material);

                    // Dolom ДОЛМИТ
                    var matDolom = new DTO.MINP_MatAddDTO();
                    matDolom.ShortCode = "04Dolom";
                    matDolom.Amount_kg = 7569;
                    matDolom.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 58.47));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 4.43));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 1.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 35.25));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    aInputData.ChargedMaterials.Add(matDolom);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Dolomite, matDolom.MINP_GD_Material);

                    // DolomS ДОЛОМС
                    var matDolomS = new DTO.MINP_MatAddDTO();
                    matDolomS.ShortCode = "04DolomS";
                    matDolomS.Amount_kg = 500;
                    matDolomS.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 20.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 30.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    aInputData.ChargedMaterials.Add(matDolomS);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1, matDolomS.MINP_GD_Material);

                    // FOM
                    var matFom = new DTO.MINP_MatAddDTO();
                    matFom.ShortCode = "04Fom";
                    matFom.Amount_kg = 500;
                    matFom.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.4));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 8.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 3.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MnO", 0.5));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.03));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 6.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MgO", 77.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9852));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    aInputData.ChargedMaterials.Add(matFom);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.FOM, matFom.MINP_GD_Material);

                    // Coke
                    var matCoke = new DTO.MINP_MatAddDTO();
                    matCoke.ShortCode = "05koks";
                    matCoke.Amount_kg = 300;
                    matCoke.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 350.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 97.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 85.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
                    matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    aInputData.ChargedMaterials.Add(matCoke);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Coke, matCoke.MINP_GD_Material);
                    # endregion

                    #region Model materials
                    // Odprasky
                    var matOdpr = new DTO.MINP_MatAddDTO();
                    matOdpr.ShortCode = "21ODPRA";
                    matOdpr.Amount_kg = 3000;
                    matOdpr.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 15.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 430.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.008));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.05));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.02));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.02));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cu", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mo", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ni", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sn", 0.005));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Sb", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Zn", 0.01));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 50.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 84.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 0.3));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 3.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 9.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 2.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 0.1));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 93.0));
                    matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 3000.0));
                    aInputData.ChargedMaterials.Add(matOdpr);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Odprasky, matOdpr.MINP_GD_Material);

                    // Slag
                    var matSlag = new DTO.MINP_MatAddDTO();
                    matSlag.ShortCode = "22strst";
                    matSlag.Amount_kg = 30000;
                    matSlag.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1640));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 50.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", 15.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 20.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MnO", 3.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 2.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 10.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 3.33));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 1.0));
                    matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
                    aInputData.ChargedMaterials.Add(matSlag);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Slag, matSlag.MINP_GD_Material);

                    // Steel
                    var matSteel = new DTO.MINP_MatAddDTO();
                    matSteel.ShortCode = "Final";
                    matSteel.Amount_kg = 420000;
                    matSteel.MINP_GD_Material = new MINP_GD_MaterialDTO();
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1500.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 380.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 0.03));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.03));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.02));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Cr", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("V", 0.01));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", 0.005));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Nb", 0.005));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ca", 0.0001));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mg", 0.0001));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("O", 750.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("N", 50.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("H", 5.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 100.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.002));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 100.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 99.0));
                    matSteel.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 7000.0));
                    aInputData.ChargedMaterials.Add(matSteel);
                    MINP.MINP_GD_ModelMaterials.Add(Enumerations.MINP_GD_Material_ModelMaterial.Steel, matSteel.MINP_GD_Material);
                    #endregion

                    aInputData.HotMetal_Temperature = 1440;
                    aInputData.Scrap_Temperature = 15;

                    #region Define Oxygen Blowing Phases
                    aInputData.OxygenBlowingPhases = new List<PhaseItem>();

                    var ph1 = new PhaseItemL1Command();
                    ph1.L1Command = Enumerations.L2L1_Command.OxygenBlowingStart;
                    ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph1);

                    var ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 300;
                    ph2.O2Amount_Nm3 = 15000;
                    ph2.O2Flow_Nm3_min = 1000;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 450;
                    ph2.O2Amount_Nm3 = 1500;
                    ph2.O2Flow_Nm3_min = 1000;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 360;
                    ph2.O2Amount_Nm3 = 3000;
                    ph2.O2Flow_Nm3_min = 1200;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 250;
                    ph2.O2Amount_Nm3 = 5000;
                    ph2.O2Flow_Nm3_min = 1200;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 230;
                    ph2.O2Amount_Nm3 = 8000;
                    ph2.O2Flow_Nm3_min = 1200;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 220;
                    ph2.O2Amount_Nm3 = 11000;
                    ph2.O2Flow_Nm3_min = 1200;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowing;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph1 = new PhaseItemL1Command();
                    ph1.L1Command = Enumerations.L2L1_Command.TemperatureMeasurement;
                    ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
                    aInputData.OxygenBlowingPhases.Add(ph1);

                    ph2 = new PhaseItemOxygenBlowing();
                    ph2.LanceDistance_mm = 220;
                    ph2.O2Amount_Nm3 = 11000;
                    ph2.O2Flow_Nm3_min = 1200;
                    ph2.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
                    aInputData.OxygenBlowingPhases.Add(ph2);

                    ph1 = new PhaseItemL1Command();
                    ph1.L1Command = Enumerations.L2L1_Command.OxygenLanceToParkingPosition;
                    ph1.PhaseGroup = PhasePrimaryDivision.OxygenBlowingCorrection;
                    aInputData.OxygenBlowingPhases.Add(ph1);

                    #endregion

                    var cyclic = new MINP_CyclicDTO();
                    cyclic.OxygenConsumption_m3 = 10000;
                    cyclic.OxygenFlow_Nm3_min = 1300;
                    cyclic.OxygenPressure_MPa = 100;
                    cyclic.LanceDistance_mm = 200;
                    cyclic.WastegasFlow_Nm3_min = 375000;
                    cyclic.Wastegas_CO2_p = 15;
                    cyclic.Wastegas_CO_p = 60;
                    cyclic.Wastegas_T_C = 70;
                    MINP.MINP_Cyclic.Add(cyclic);

                    MINP.HeatAimData = new MINP_HeatAimDataDTO();
                    MINP.HeatAimData.FinalTemperature = 1700;

                    var heat = new Models.Dynamic(aInputData, 1 /* sec */);
                    heat.Start();

                    var Count = 0;
                    var hcOld = 0;
                    while (Count < 1200)
                    {
                        Thread.Sleep(1014);
                        ++Count;
                        //Console.Write(++Count + " : ");

                        var hc = heat.OutputData.Count;
                        if (hc != hcOld)
                        {
                            hcOld = hc;
                            //Console.WriteLine(hc);
                            fex.ClearArgs();
                            fex.AddArg("@RelativeSecond", hc);
                            foreach (var eix in fexi.evt.Arguments)
                            {
                                fex.AddArg(eix.Key, heat.LastOutputData.FP_Tavby[Convert.ToInt32(eix.Value)]);
                            }
                            Console.WriteLine(fex.evt);
                        }
                        //else
                        //{
                        //    Console.WriteLine("---");
                        //}
                        //if (0 == Count % 60) Console.WriteLine("===========================");
                    }
                    throw new Exception("************** End Of Heat ***************");


                }
                catch (Exception e)
                {
                    l.err("ModelRunner.Main exception {0}", e);
                }
            }
        }
    }
}
