using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using ConnectionProvider;
using Converter;
using DTO;
using Data;
using Data.Model;
using Common;
using Implements;
using Models;
using System.Linq;

namespace ModelRunner
{
    partial class DynPrepare
    {
        public static int CurrentTemp = 0; /* Celsius */
        public static FlexHelper matRegEvent = null;
        public static DTO.MINP_MatAddDTO AddIron(int weight)
        {
            var matIron = new DTO.MINP_MatAddDTO();
            matIron.ShortCode = "01Metal";
            matIron.Amount_kg = weight;
            matIron.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matIron.MINP_GD_Material.ShortCode = matIron.ShortCode;
            matIron.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            if (fxeIron == null)
            {
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, Listener.IronTemp));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", 4.75));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", 0.55));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", 0.27));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", 0.062));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", 0.017));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", 0.1));
            }
            else
            {
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, fxeIron.GetDbl("HM_TEMP")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("C", fxeIron.GetDbl("ANA_C")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Si", fxeIron.GetDbl("ANA_SI")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Mn", fxeIron.GetDbl("ANA_MN")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("P", fxeIron.GetDbl("ANA_P")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("S", fxeIron.GetDbl("ANA_S")));
                matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Ti", fxeIron.GetDbl("ANA_TI")));
            }
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 340.0));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
            matIron.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
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
            return matIron;
        }
        public static DTO.MINP_MatAddDTO AddScrap(int weight)
        {
            var matScrap = new DTO.MINP_MatAddDTO();
            matScrap.ShortCode = "02Scrap";
            matScrap.Amount_kg = weight;
            matScrap.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matScrap.MINP_GD_Material.ShortCode = matScrap.ShortCode;
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, Listener.ScrapTemp));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 380.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.22));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            if (null == fxeScrap)
            {
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
            }
            else
            {
                foreach (var scel in fxeScrap.evt.Arguments)
                {
                    matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(scel.Key, (double)scel.Value));
                }
            }
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 0.9962));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 1.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 99.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 95.0));
            matScrap.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 1200.0));
            return matScrap;
        }
        public static DTO.MINP_MatAddDTO AddCaO(int weight)
        {
            var matCaO = new DTO.MINP_MatAddDTO();
            matCaO.ShortCode = "04CaO";
            matCaO.Amount_kg = weight;
            matCaO.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matCaO.MINP_GD_Material.ShortCode = matCaO.ShortCode;
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>(); // ИЗВЕСТЬ протокол №72
            Tbl.Clear();
            Adapter.Fill(Tbl, "LIME.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Tbl.Clear();
            Adapter.Fill(Tbl, "LIME");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matCaO.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matCaO;
        }
        public static DTO.MINP_MatAddDTO AddMaxG(int weight)
        {
            var matMaxG = new DTO.MINP_MatAddDTO();
            matMaxG.ShortCode = "04MaxG";
            matMaxG.Amount_kg = weight;
            matMaxG.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matMaxG.MINP_GD_Material.ShortCode = matMaxG.ShortCode;
            matMaxG.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>(); // МАХГ протокол №34
            Adapter.Fill(Tbl, "MAXG.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matMaxG.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Adapter.Fill(Tbl, "MAXG");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matMaxG.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matMaxG.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matMaxG;
        }

        public static DTO.MINP_MatAddDTO AddDolom(int weight)
        {
            var matDolom = new DTO.MINP_MatAddDTO();
            matDolom.ShortCode = "04Dolom";
            matDolom.Amount_kg = weight;
            matDolom.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matDolom.MINP_GD_Material.ShortCode = matDolom.ShortCode;
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>(); // МАХГ протокол №34
            Adapter.Fill(Tbl, "DOLMIT.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Adapter.Fill(Tbl, "DOLMIT");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matDolom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matDolom;
        }

        public static DTO.MINP_MatAddDTO AddDolomS(int weight)
        {
            var matDolomS = new DTO.MINP_MatAddDTO();
            matDolomS.ShortCode = "04DolomS";
            matDolomS.Amount_kg = weight;
            matDolomS.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matDolomS.MINP_GD_Material.ShortCode = matDolomS.ShortCode;
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>(); // ДОЛОМС протокол №1867
            Adapter.Fill(Tbl, "DOLOMS.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Adapter.Fill(Tbl, "DOLOMS");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matDolomS.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matDolomS;
        }

        public static DTO.MINP_MatAddDTO AddFom(int weight)
        {
            var matFom = new DTO.MINP_MatAddDTO();
            matFom.ShortCode = "04Fom";
            matFom.Amount_kg = weight;
            matFom.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matFom.MINP_GD_Material.ShortCode = matFom.ShortCode;
            matFom.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>(); // ФОМ протокол№48
            Adapter.Fill(Tbl, "FOM.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Adapter.Fill(Tbl, "FOM");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matFom.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matFom;
        }

        public static DTO.MINP_MatAddDTO AddCoke(int weight)
        {
            var matCoke = new DTO.MINP_MatAddDTO();
            matCoke.ShortCode = "05koks";
            matCoke.Amount_kg = weight;
            matCoke.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matCoke.MINP_GD_Material.ShortCode = matCoke.ShortCode;
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            Tbl.Clear();
            Adapter.Fill(Tbl, "COKE.props");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            Tbl.Clear();
            Adapter.Fill(Tbl, "COKE");
            for (int j = 0; j < Tbl.Rows.Count; j++)
            {
                matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(Tbl[j].Name, Tbl[j].Value));
            }
            matCoke.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
            return matCoke;
        }

        public static DTO.MINP_MatAddDTO AddOdprasky(int weight)
        {
            var matOdpr = new DTO.MINP_MatAddDTO();
            matOdpr.ShortCode = "21ODPRA";
            matOdpr.Amount_kg = weight;
            //if (matRegEvent != null) matRegEvent.AddInt(matOdpr.ShortCode, weight);
            matOdpr.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matOdpr.MINP_GD_Material.ShortCode = matOdpr.ShortCode;
            matOdpr.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matOdpr.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, CurrentTemp));
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
            return matOdpr;
        }

        public static DTO.MINP_MatAddDTO AddSlag(int weight)
        {
            var matSlag = new DTO.MINP_MatAddDTO();
            matSlag.ShortCode = "22strst";
            matSlag.Amount_kg = weight;
            //if (matRegEvent != null) matRegEvent.AddInt(matSlag.ShortCode, weight);
            matSlag.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matSlag.MINP_GD_Material.ShortCode = matSlag.ShortCode;
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems = new List<MINP_GD_MaterialItemsDTO>();
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(69, 1640));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(70, 420.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(71, 0.35));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(72, 1550.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("CaO", 50.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("SiO2", CurrentTemp));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("FeO", 20.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("MnO", 3.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Fe", 2.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Al2O3", 10.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("TOTAL", 1.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Basiticy", 3.33));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Yield", 1.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("Steel", 1.0));
            matSlag.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps("ro", 2000.0));
            return matSlag;
        }

        public static DTO.MINP_MatAddDTO AddSteel(int weight)
        {
            var matSteel = new DTO.MINP_MatAddDTO();
            matSteel.ShortCode = "Final";
            matSteel.Amount_kg = weight;
            //if (matRegEvent != null) matRegEvent.AddInt(matSteel.ShortCode, weight);
            matSteel.MINP_GD_Material = new MINP_GD_MaterialDTO();
            matSteel.MINP_GD_Material.ShortCode = matSteel.ShortCode;
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
            return matSteel;
        }

        public static Dictionary<string, double> dic(string name)
        {
            return null;
        }

        public static DTO.MINP_MatAddDTO AddMaterial(string name, int weight)
        {
            var mat = new DTO.MINP_MatAddDTO();
            foreach (var v in dic(name))
            {
                mat.MINP_GD_Material.MINP_GD_MaterialItems.Add(ps(v.Key, v.Value));
            }
            return mat;
        }
    }
}