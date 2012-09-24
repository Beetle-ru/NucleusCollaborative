using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter 
{

   // факт.данные от PLC x.1						
   // Von:	PLC x.1	(x=номер конвертера)
   // Данные по главной продувке
   [Serializable]
   [DataContract]
   public class AdditionsEvent : ConverterBaseEvent {
      
      [DataMember]
      public string MaterialName { set; get; }

      [DataMember]
      public string Destination { set; get; }

      [DataMember]
      public DateTime Date { set; get; }

      [DataMember]
      public int StringNo { set; get; }

      [DataMember]
      public int PortionWeight { set; get; }

      [DataMember]
      public int TotalWeight { set; get; }

      [DataMember]
      public int DryingDuration { set; get; }      // время сушки(сек),используется только при легирующих

      [DataMember]
      public Dictionary<string,double> ChemestryAttributes { set; get; }
      }
   }
