using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPC.Data;
using OPC.Common;
using OPC.Data.Interface;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using CommonTypes;
using Core;
using Implements;

namespace OPC
{
    public class Group
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public Type Type { get; set; }
        public List<Point> Points { get; set; }
        public List<OPCItemDef> ItemDefs { get; set; }
        public OpcGroup ServerGroup { get; set; }
        public BaseEvent Event { get; set; }
        public string FilterPropertyName { get; set; }
        public string FilterPropertyValue { get; set; }
        public bool IsWriteble { get; set; }
        public bool FirstSend { get; set; }
        public Group()
        {
            Points = new List<Point>();
            ItemDefs = new List<OPCItemDef>();
            FirstSend = true;
        }

        public static ICollection<Group> GetGroupsFromAttribytes(Type[] types)
        {
            List<Group> result = new List<Group>();
            foreach (var item in types)
            {
                result.AddRange(GetGroupFromAttribytes(item));
            }

            return result;
        }

        private static ICollection<Group> GetGroupFromAttribytes(Type type)
        {
            List<Group> groups = new List<Group>();
            var data = type.GetCustomAttributes(false).Where(x => x.GetType().Name == "PLCGroup").ToArray();
            foreach (PLCGroup plcg in data)
            {
                if (plcg == null) continue;

                Group group = new Group();
                group.Name = plcg.Name;
                group.Destination = plcg.Destination;
                group.Location = plcg.Location;
                group.FilterPropertyName = plcg.FilterPropertyName;
                group.FilterPropertyValue = plcg.FilterPropertyValue;
                group.Type = type;

                group.Event = (BaseEvent)Activator.CreateInstance(group.Type);
                int handleClientPoint = 0;

                foreach (PropertyInfo property in type.GetProperties())
                {
                    PLCPoint plcp = (PLCPoint)property.GetCustomAttributes(false).Where(x => x.GetType().Name == "PLCPoint").FirstOrDefault();
                    if (plcp == null) continue;
                    OPC.Point newPoint = new OPC.Point();
                    newPoint.FieldName = property.Name;
                    newPoint.Location = plcp.Location;
                    newPoint.Encoding = plcp.Encoding;
                    newPoint.Type = property.PropertyType;
                    newPoint.IsBoolean = plcp.IsBoolean;
                    newPoint.BitNumber = plcp.BitNumber;
                    group.Points.Add(newPoint);
                    //group.ItemDefs.Add(new OPCItemDef("S7:[" + group.Location + "]" + newPoint.OPCLocation, true, handleClientPoint, VarEnum.VT_EMPTY));
                    group.ItemDefs.Add(new OPCItemDef(group.Location + "." + newPoint.Location.Replace(',', '.'), true, handleClientPoint, VarEnum.VT_EMPTY));
                    handleClientPoint++;
                }
                groups.Add(group);
            }

            return groups;
        }

        public static ICollection<Group> LoadGroupsFromXmlFile(string fileName, out string module, out Type[] eventTypes)
        {
            // Создаем экземпляр класса
            XmlDocument xmlDoc = new XmlDocument();
            eventTypes = null;
            // Загружаем XML-документ из файла

            xmlDoc.Load(fileName);
            module = string.Format(@"{0}\{1}", xmlDoc.DocumentElement.Attributes["Path"].Value, xmlDoc.DocumentElement.Attributes["Name"].Value);
            try
            {
                Assembly a = null;
                a = Assembly.LoadFrom(module);
                eventTypes = BaseEvent.GetEvents();
            }
            catch
            {
                return new List<Group>();
            }
            InstantLogger.log(string.Format("eventtypes is null {0}", eventTypes == null));

            // Получаем всех детей корневого элемента
            // xmlDoc.DocumentElement - корневой элемент

            List<Group> resultGroups = new List<Group>();
            foreach (XmlNode table in xmlDoc.DocumentElement.ChildNodes)
            {
                // перебираем все атрибуты элемента
                Group group = new Group()
                {
                    Name = table.Attributes["Name"].Value,
                    Type = eventTypes.FirstOrDefault(p => p.Name == table.Attributes["Type"].Value),
                    Location = table.Attributes["Location"].Value,
                    Destination = table.Attributes["Destination"].Value,
                    FilterPropertyName = table.Attributes["FilterPropertyName"].Value,
                    FilterPropertyValue = table.Attributes["FilterPropertyValue"].Value,
                    IsWriteble = bool.Parse(table.Attributes["IsWriteble"].Value)
                };
                group.Event = (BaseEvent)Activator.CreateInstance(group.Type);
                int handleClientPoint = 0;
                // перебираем всех детей текущего узла
                foreach (XmlNode ch in table.ChildNodes)
                {
                    if (ch.Name == "Point")
                    {
                        Point newPoint = new Point();
                        newPoint.FieldName = ch.Attributes["Name"].Value;
                        newPoint.Location = ch.Attributes["Location"].Value;
                        newPoint.Encoding = ch.Attributes["Encoding"].Value;
                        newPoint.Type = (group.Type.GetProperties().Select(a => a.PropertyType).FirstOrDefault(p => p.Name == ch.Attributes["Type"].Value));
                        newPoint.IsBoolean = bool.Parse(ch.Attributes["IsBoolean"].Value);
                        newPoint.BitNumber = int.Parse(ch.Attributes["BitNumber"].Value);
                        group.Points.Add(newPoint);
                        //group.ItemDefs.Add(new OPCItemDef("S7:[" + group.Location + "]" + newPoint.OPCLocation, true, handleClientPoint, VarEnum.VT_EMPTY));
                        group.ItemDefs.Add(new OPCItemDef(group.Location + "." + newPoint.Location.Replace(',', '.'), true, handleClientPoint, VarEnum.VT_EMPTY));
                        handleClientPoint++;
                    }
                }
                resultGroups.Add(group);
            }
            return resultGroups;
        }

        public static ICollection<Group> LoadGroupsFromXmlFile(string fileName)
        {
            // Создаем экземпляр класса
            XmlDocument xmlDoc = new XmlDocument();
            Type[] eventTypes = null;
            // Загружаем XML-документ из файла
            xmlDoc.Load(fileName);
            try
            {
                Assembly a = null;
                InstantLogger.log(string.Format(@"{0}\{1}", xmlDoc.DocumentElement.Attributes["Path"].Value, xmlDoc.DocumentElement.Attributes["Name"].Value));
                a = Assembly.LoadFrom(string.Format(@"{0}\{1}", xmlDoc.DocumentElement.Attributes["Path"].Value, xmlDoc.DocumentElement.Attributes["Name"].Value));
                eventTypes = BaseEvent.GetEvents();
            }
            catch
            {
                return new List<Group>();
            }
            InstantLogger.log(string.Format("eventtypes is null {0} count {1}", eventTypes == null, eventTypes.Count()));

            // Получаем всех детей корневого элемента
            // xmlDoc.DocumentElement - корневой элемент

            List<Group> resultGroups = new List<Group>();
            foreach (XmlNode table in xmlDoc.DocumentElement.ChildNodes)
            {
                // перебираем все атрибуты элемента
                Group group = new Group()
                {
                    Name = table.Attributes["Name"].Value,
                    Type = eventTypes.FirstOrDefault(p => p.Name == table.Attributes["Type"].Value),
                    Location = table.Attributes["Location"].Value,
                    Destination = table.Attributes["Destination"].Value,
                    FilterPropertyName = table.Attributes["FilterPropertyName"].Value,
                    FilterPropertyValue = table.Attributes["FilterPropertyValue"].Value,
                    IsWriteble = bool.Parse(table.Attributes["IsWriteble"].Value)
                };
                group.Event = (BaseEvent)Activator.CreateInstance(group.Type);
                int handleClientPoint = 0;
                // перебираем всех детей текущего узла
                foreach (XmlNode ch in table.ChildNodes)
                {
                    if (ch.Name == "Point")
                    {
                        Point newPoint = new Point();
                        newPoint.FieldName = ch.Attributes["Name"].Value;
                        newPoint.Location = ch.Attributes["Location"].Value;
                        newPoint.Encoding = ch.Attributes["Encoding"].Value;
                        newPoint.Type = (group.Type.GetProperties().Select(a => a.PropertyType).FirstOrDefault(p => p.Name == ch.Attributes["Type"].Value));
                        newPoint.IsBoolean = bool.Parse(ch.Attributes["IsBoolean"].Value);
                        newPoint.BitNumber = int.Parse(ch.Attributes["BitNumber"].Value);
                        group.Points.Add(newPoint);
                        //group.ItemDefs.Add(new OPCItemDef("S7:[" + group.Location + "]" + newPoint.OPCLocation, true, handleClientPoint, VarEnum.VT_EMPTY));
                        group.ItemDefs.Add(new OPCItemDef(group.Location + "." + newPoint.Location.Replace(',', '.'), true, handleClientPoint, VarEnum.VT_EMPTY));

                        handleClientPoint++;
                    }
                }
                resultGroups.Add(group);
            }
            return resultGroups;
        }

        public static bool SaveGroupToXmlFile(string fileName, ICollection<Group> groups, string modulePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(modulePath);

            XmlWriterSettings settings = new XmlWriterSettings();

            // включаем отступ для элементов XML документа
            // (позволяет наглядно изобразить иерархию XML документа)
            settings.Indent = true;
            settings.IndentChars = "    "; // задаем отступ, здесь у меня 4 пробела

            // задаем переход на новую строку
            settings.NewLineChars = "\n";

            // Нужно ли опустить строку декларации формата XML документа
            // речь идет о строке вида "<?xml version="1.0" encoding="utf-8"?>"
            settings.OmitXmlDeclaration = false;

            using (XmlWriter output = XmlWriter.Create(fileName, settings))
            {
                // Создали открывающийся тег
                output.WriteStartElement("Module");
                // Добавляем атрибут 
                output.WriteAttributeString("Name", fileInfo.Name);
                output.WriteAttributeString("Path", fileInfo.DirectoryName);

                #region Group
                foreach (var group in groups)
                {
                    if (string.IsNullOrEmpty(group.Name)) continue;

                    output.WriteStartElement("Group");
                    output.WriteAttributeString("Name", group.Name);
                    output.WriteAttributeString("Type", group.Type.Name);
                    output.WriteAttributeString("Location", group.Location);
                    output.WriteAttributeString("Destination", group.Destination);
                    output.WriteAttributeString("FilterPropertyName", group.FilterPropertyName);
                    output.WriteAttributeString("FilterPropertyValue", group.FilterPropertyValue);
                    output.WriteAttributeString("IsWriteble", group.IsWriteble.ToString());

                    #region Points
                    foreach (var point in group.Points)
                    {
                        output.WriteStartElement("Point");
                        output.WriteAttributeString("Name", point.FieldName);
                        output.WriteAttributeString("Type", point.Type.Name);
                        output.WriteAttributeString("Location", point.Location);
                        output.WriteAttributeString("Encoding", point.Encoding);
                        output.WriteAttributeString("IsBoolean", point.IsBoolean.ToString());
                        output.WriteAttributeString("BitNumber", point.BitNumber.ToString());
                        output.WriteEndElement();
                    }
                    #endregion
                    output.WriteEndElement();
                }
                #endregion


                output.WriteEndElement();
                // Сбрасываем буфферизированные данные
                output.Flush();
                // Закрываем фаил, с которым связан output
                output.Close();
            }

            return true;
        }
    }
}
