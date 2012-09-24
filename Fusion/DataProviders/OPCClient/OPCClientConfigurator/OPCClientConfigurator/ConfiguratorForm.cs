using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;
using System.Reflection;
using OPC;
using OPC.Data;
using System.Runtime.InteropServices;
using CommonTypes;

namespace OPCClientConfigurator
{
    public enum WorkStatus
    {
        NoEventDll = 0,
        NoEventChecked = 1,
        EditGroup = 2,
        NoGroupExist = 3,
        AddGroup = 4,
        EditProperty = 5,
    }



    public partial class ConfiguratorForm : Form
    {
        private Dictionary<WorkStatus, string> WorkStatusStrings { get; set; }
        private Type[] m_Events;

        private WorkStatus m_CurrentStatus;
        private WorkStatus CurrentStatus { get { return m_CurrentStatus; } set { m_CurrentStatus = value; if (OnCurrentStatusChanged != null) OnCurrentStatusChanged(); } }

        private List<Group> m_OPCGroups = new List<Group>();
        private Group m_CurrentOPCGroup = null;
        private List<object> m_Components = new List<object>();

        private delegate void OnChanged();
        private event OnChanged OnCurrentStatusChanged;

        public ConfiguratorForm()
        {
            InitializeComponent();
            OnCurrentStatusChanged += new OnChanged(ConfiguratorForm_OnCurrentStatusChanged);

            #region Список компонентов
            m_Components.AddRange(new object[] { textBoxModule, comboBoxEvents, comboBoxGroups, textBoxGroupLocation, textBoxGroupDestination, 
                comboBoxGroupFilterPropertyName, textBoxGroupFilterPropertyValue, textBoxPointLocation, textBoxPointType, textBoxPointName,
                comboBoxPointEncoding, listBoxPoints,listBoxProperties,textBoxBitNumber});
            #endregion

            #region Статусы
            WorkStatusStrings = new Dictionary<WorkStatus, string>();
            WorkStatusStrings.Add(WorkStatus.NoEventDll, "Загрузите файл сборки ... ");
            WorkStatusStrings.Add(WorkStatus.NoEventChecked, "Выберите событие из списка ... ");
            WorkStatusStrings.Add(WorkStatus.EditGroup, "Редактирование группы ");
            WorkStatusStrings.Add(WorkStatus.NoGroupExist, "Создайте группу ... ");
            WorkStatusStrings.Add(WorkStatus.AddGroup, "Введите имя группы, PLC адрес, имя Core и по необходимости заполните данные по фильтру... ");
            WorkStatusStrings.Add(WorkStatus.EditProperty, "Редактирование точки [Enter] потверждение ввода. ");
            #endregion


            CurrentStatus = WorkStatus.NoEventDll;
        }

        private void buttonGetModule_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_Events = GetListEvents(openFileDlg.FileName);
                if (m_Events.Length > 0)
                {
                    //m_Events = m_Events.OrderBy(p => p.FullName);
                    FillEventsComboBox(m_Events);
                    textBoxModule.Text = openFileDlg.FileName;
                    m_OPCGroups.AddRange(Group.GetGroupsFromAttribytes(m_Events).OrderBy(p => p.Name));
                }
                else
                {
                    MessageBox.Show("В этой сборке нет ни одного события.");
                    CurrentStatus = WorkStatus.NoEventDll;
                }
            }

        }

        private void FillControlsGroups()
        {
            if (m_CurrentOPCGroup == null) return;

            comboBoxGroups.SelectedItem = m_CurrentOPCGroup.Name;
            textBoxGroupLocation.Text = m_CurrentOPCGroup.Location;
            textBoxGroupDestination.Text = m_CurrentOPCGroup.Destination;
            comboBoxGroupFilterPropertyName.SelectedItem = m_CurrentOPCGroup.FilterPropertyName;
            textBoxGroupFilterPropertyValue.Text = m_CurrentOPCGroup.FilterPropertyValue;
            checkBoxIsWriteble.Checked = m_CurrentOPCGroup.IsWriteble;
            listBoxProperties.Items.Clear();
            listBoxProperties.Items.AddRange(((Type)comboBoxEvents.SelectedItem).GetProperties().ToArray());
            UpdatePointsListBox(m_CurrentOPCGroup.Points);

        }

        private void UpdatePointsListBox(ICollection<OPC.Point> points)
        {

            comboBoxPointEncoding.Items.AddRange(System.Text.Encoding.GetEncodings().Select(p => p.GetEncoding()).OrderBy(a => a.WebName).ToArray());
            listBoxPoints.Items.Clear();
            foreach (var item in points)
            {
                listBoxPoints.Items.AddRange(new string[]{ string.Format("[Location={0} {1} {2}]", item.Location, string.IsNullOrEmpty(item.Encoding) ? "": "Encode="+ item.Encoding, 
                    item.IsBoolean ? string.Format("IsBoolean={0} BitNumber={1}", item.IsBoolean, item.BitNumber): ""  ), 
                    string.Format("{0}    {1}", item.Type.Name, item.FieldName )});
            }
        }

        private void FillEventsComboBox(Type[] _events)
        {
            comboBoxEvents.Items.AddRange(m_Events);
        }

        private static bool IsSubClassOfBaseEvent(Type TypeToCheck)
        {
            if (TypeToCheck.IsAbstract) return false;

            while (TypeToCheck != typeof(object))
            {
                if (TypeToCheck.BaseType == null) break;

                if (TypeToCheck.BaseType == typeof(BaseEvent))
                {
                    return true;
                }
                TypeToCheck = TypeToCheck.BaseType;
            }
            return false;
        }

        private Type[] GetListEvents(string fileName)
        {
            Assembly a = null;

            try
            {
                a = Assembly.LoadFrom(fileName);
                // m_Events = a.GetTypes().ToArray();//.Where(t => t.GetInterface("IModule") != null).ToArray(); //;.Where(IsSubClassOfBaseEvent).ToArray();
                CurrentStatus = WorkStatus.NoEventChecked;
            }
            catch
            {
                MessageBox.Show(string.Format("Не могу загрузить сборку \"{0}\"", fileName));
                CurrentStatus = WorkStatus.NoEventDll;
            }

            return BaseEvent.GetEvents();
        }

        private void ClearComponents(string[] parentNames)
        {
            foreach (var item in m_Components)
            {
                if (parentNames != null && parentNames.Length > 0 && parentNames.Contains(((Control)item).Parent.Name))
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Text = string.Empty;
                    }

                    if (item is ComboBox)
                    {
                        ((ComboBox)item).Items.Clear();
                        ((ComboBox)item).Text = string.Empty;
                    }

                    if (item is ListBox)
                    {
                        ((ListBox)item).Items.Clear();
                    }
                }
            }
        }

        private void ConfiguratorForm_OnCurrentStatusChanged()
        {
            toolStripStatusLabel1.Text = WorkStatusStrings[CurrentStatus];

            switch (CurrentStatus)
            {
                case WorkStatus.NoEventDll:
                    buttonGetModule.Enabled = true;
                    comboBoxEvents.Enabled = false;
                    groupBoxGroups.Enabled = false;
                    groupBoxPoints.Enabled = false;
                    сохранитьToolStripMenuItem.Enabled = false;
                    сохранитьКакToolStripMenuItem.Enabled = false;
                    ClearComponents(new string[] { "groupBoxEvents", "groupBoxGroups", "groupBoxPoints" });
                    break;

                case WorkStatus.NoEventChecked:
                    comboBoxEvents.Enabled = true;
                    buttonGetModule.Enabled = false;
                    сохранитьToolStripMenuItem.Enabled = true;
                    сохранитьКакToolStripMenuItem.Enabled = true;
                    break;

                case WorkStatus.NoGroupExist:
                    groupBoxGroups.Enabled = true;
                    buttonAddGroup.Enabled = true;
                    buttonRemoveGroup.Enabled = false;
                    ClearComponents(new string[] { "groupBoxGroups", "groupBoxPoints" });
                    ComponentsTurn(false, new string[] { "groupBoxGroups", "groupBoxPoints" });
                    break;

                case WorkStatus.AddGroup:
                    buttonAddGroup.Enabled = false;
                    buttonRemoveGroup.Enabled = true;
                    ComponentsTurn(true, new string[] { "groupBoxGroups" });
                    ClearComponents(new string[] { "groupBoxGroups", "groupBoxPoints" });
                    groupBoxPoints.Enabled = false;
                    comboBoxGroups.Items.AddRange(m_OPCGroups.Where(p => p.Type == (Type)comboBoxEvents.SelectedItem).Select(a => a.Name).ToArray());
                    comboBoxGroupFilterPropertyName.Items.AddRange(((Type)comboBoxEvents.SelectedItem).GetProperties().Select(p => p.Name).ToArray());
                    break;

                case WorkStatus.EditGroup:
                    buttonAddGroup.Enabled = true;
                    buttonRemoveGroup.Enabled = true;
                    groupBoxPoints.Enabled = true;
                    groupBoxGroups.Enabled = true;
                    ClearComponents(new string[] { "groupBoxPoints" });
                    FillControlsGroups();
                    ComponentsTurn(true, new string[] { "groupBoxGroups", "groupBoxPoints" });
                    comboBoxGroupFilterPropertyName.Items.AddRange(((Type)comboBoxEvents.SelectedItem).GetProperties().Select(p => p.Name).ToArray());
                    break;

                case WorkStatus.EditProperty:
                    FillControlPoints();
                    buttonPointAdd.Enabled = m_CurrentOPCGroup.Points.Where(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name).Count() > 0 ? false : true;
                    buttonPointRemove.Enabled = m_CurrentOPCGroup.Points.Where(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name).Count() > 0 ? true : false;
                    if (buttonPointRemove.Enabled)
                    {
                        listBoxPoints.SelectedIndex = GetIndexOfProperty(((PropertyInfo)listBoxProperties.SelectedItem).Name);
                    }
                    else
                    {
                        listBoxPoints.SelectedIndex = -1;
                    }
                    break;
            }
        }
        private int GetIndexOfProperty(string propertyName)
        {
            foreach (string item in listBoxPoints.Items)
            {
                if (item.IndexOf(propertyName) > 0)
                {
                    return listBoxPoints.Items.IndexOf(item);
                }
            }
            return -1;
        }

        private void FillControlPoints()
        {
            if (listBoxProperties.SelectedItem != null)
            {
                textBoxPointType.Text = ((PropertyInfo)listBoxProperties.SelectedItem).PropertyType.Name;
                textBoxPointName.Text = ((PropertyInfo)listBoxProperties.SelectedItem).Name;
                OPC.Point ppoint = null;
                if (m_CurrentOPCGroup.Points != null)
                {
                    ppoint = m_CurrentOPCGroup.Points.Find(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name);
                }

                if (ppoint != null)
                {
                    textBoxPointLocation.Text = ppoint != null ? ppoint.Location : string.Empty;
                    checkBoxIsBoolean.Checked = ppoint.IsBoolean;
                    textBoxBitNumber.Text = ppoint.BitNumber.ToString();
                    comboBoxPointEncoding.SelectedItem = (ppoint != null && !string.IsNullOrEmpty(ppoint.Encoding)) ? System.Text.Encoding.GetEncoding(ppoint.Encoding) : null;
                }
            }
        }

        private void ComponentsTurn(bool enabled, string[] parentNames)
        {
            foreach (var item in m_Components)
            {
                if (parentNames != null && parentNames.Length > 0 && parentNames.Contains(((Control)item).Parent.Name))
                {
                    ((Control)item).Enabled = enabled;
                }
            }
        }

        private void новыйToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentStatus = WorkStatus.NoEventDll;
        }



        private void comboBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEvents.SelectedItem != null)
            {
                ClearComponents(new string[] { "groupBoxGroups", "groupBoxPoints" });
                comboBoxGroups.Items.AddRange(m_OPCGroups.Where(p => p.Type == (Type)comboBoxEvents.SelectedItem).Select(a => a.Name).ToArray());
                SetCurrentGroup();
                if (m_CurrentOPCGroup != null)
                    comboBoxGroups.SelectedItem = m_CurrentOPCGroup.Name;
            }
        }

        private void SetCurrentGroup()
        {

            m_CurrentOPCGroup = m_OPCGroups.FindLast(p => p.Type == (Type)comboBoxEvents.SelectedItem);

            if (m_CurrentOPCGroup == null)
            {
                CurrentStatus = WorkStatus.NoGroupExist;
            }

        }



        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            CurrentStatus = WorkStatus.AddGroup;
            comboBoxGroups.Text = GetNextGroupName(m_OPCGroups.FindLast(p => p.Type == (Type)comboBoxEvents.SelectedItem));
        }

        private string GetNextGroupName(Group opcGroup)
        {
            int number;
            if (opcGroup == null)
            {
                return string.Format("PLC{0}1", ((Type)comboBoxEvents.SelectedItem).Name);
            }
            try
            {
                string eventNumber = opcGroup.Name.Substring(opcGroup.Name.IndexOf("Event"));
                number = int.Parse(eventNumber.Remove(0, "Event".Length));
                return opcGroup.Name.Replace(eventNumber, "Event" + (number + 1).ToString());
            }
            catch
            {
                number = 0;
                return string.Format("PLC{0}{1}", ((Type)comboBoxEvents.SelectedItem).Name, 1);
            }


        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            if (CurrentStatus == WorkStatus.EditGroup || CurrentStatus == WorkStatus.EditProperty)
            {
                m_OPCGroups.Remove(m_CurrentOPCGroup);
                comboBoxGroups.Items.Remove(m_CurrentOPCGroup.Name);
            }

            m_CurrentOPCGroup = m_OPCGroups.FindLast(p => p.Type == (Type)comboBoxEvents.SelectedItem);

            if (m_CurrentOPCGroup == null)
            {
                CurrentStatus = WorkStatus.NoGroupExist;
            }
            else
            {
                comboBoxGroups.SelectedItem = m_CurrentOPCGroup.Name;
            }
        }

        private void comboBoxGroups_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxEvents.Text) && !string.IsNullOrEmpty(textBoxGroupLocation.Text) &&
                !string.IsNullOrEmpty(textBoxGroupDestination.Text) && CurrentStatus == WorkStatus.AddGroup)
            {
                m_CurrentOPCGroup = new Group();
                FillGroupDataFromControls(ref m_CurrentOPCGroup);
                m_OPCGroups.Add(m_CurrentOPCGroup);
                comboBoxGroups.Items.Add(m_CurrentOPCGroup.Name);
                CurrentStatus = WorkStatus.EditGroup;
            }

            if (CurrentStatus == WorkStatus.EditGroup)
            {
                FillGroupDataFromControls(ref m_CurrentOPCGroup);
            }
        }

        private void FillGroupDataFromControls(ref Group group)
        {
            group.Name = comboBoxGroups.Text;
            group.Location = textBoxGroupLocation.Text;
            group.Destination = textBoxGroupDestination.Text;
            group.Type = (Type)comboBoxEvents.SelectedItem;
            group.FilterPropertyName = string.IsNullOrEmpty((string)comboBoxGroupFilterPropertyName.SelectedItem) ? "" : (string)comboBoxGroupFilterPropertyName.SelectedItem;
            group.FilterPropertyValue = textBoxGroupFilterPropertyValue.Text;
            group.IsWriteble = checkBoxIsWriteble.Checked;
            group.Event = (BaseEvent)Activator.CreateInstance(group.Type);
        }

        private void comboBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_CurrentOPCGroup = m_OPCGroups.Find(p => p.Name == (string)comboBoxGroups.SelectedItem);
            if (m_CurrentOPCGroup == null)
            {
                CurrentStatus = WorkStatus.NoGroupExist;
            }
            else
            {
                CurrentStatus = WorkStatus.EditGroup;
            }
        }

        private void comboBoxGroups_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                comboBoxGroups_Leave(null, null);
            }

        }

        private void listBoxProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentStatus = WorkStatus.EditProperty;
        }



        private void textBoxPointLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (m_CurrentOPCGroup == null || m_CurrentOPCGroup.Points == null) return;
                OPC.Point opcPoint = m_CurrentOPCGroup.Points.Find(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name);
                if (opcPoint != null)
                {
                    opcPoint.Location = textBoxPointLocation.Text;
                    opcPoint.IsBoolean = checkBoxIsBoolean.Checked;
                    opcPoint.BitNumber = int.Parse(textBoxBitNumber.Text);
                    UpdatePointsListBox(m_CurrentOPCGroup.Points);
                }
            }

            if ((int)e.KeyChar == 27)
            {
                CurrentStatus = WorkStatus.EditProperty;
            }
        }

        private void comboBoxPointEncoding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (m_CurrentOPCGroup.Points == null) return;
                OPC.Point opcPoint = m_CurrentOPCGroup.Points.Find(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name);
                if (opcPoint != null)
                {
                    opcPoint.Encoding = comboBoxPointEncoding.Text;
                    UpdatePointsListBox(m_CurrentOPCGroup.Points);
                }
            }

            if ((int)e.KeyChar == 27)
            {
                CurrentStatus = WorkStatus.EditProperty;
            }
        }

        private void textBoxPointLocation_Leave(object sender, EventArgs e)
        {
            if (buttonPointAdd.Enabled == false)
            {
                CurrentStatus = WorkStatus.EditProperty;
            }
        }

        private void buttonPointRemove_Click(object sender, EventArgs e)
        {
            if (m_CurrentOPCGroup.Points == null) return;

            OPC.Point opcPoint = m_CurrentOPCGroup.Points.Find(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name);
            if (opcPoint != null)
            {
                m_CurrentOPCGroup.Points.Remove(opcPoint);
                CurrentStatus = WorkStatus.EditProperty;
                UpdatePointsListBox(m_CurrentOPCGroup.Points);
            }
        }

        private void buttonPointAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPointLocation.Text))
            {
                m_CurrentOPCGroup.Points.Add(new OPC.Point()
                {
                    Encoding = comboBoxPointEncoding.Text,
                    FieldName = ((PropertyInfo)listBoxProperties.SelectedItem).Name,
                    Location = textBoxPointLocation.Text,
                    Type = ((PropertyInfo)listBoxProperties.SelectedItem).PropertyType
                });
                CurrentStatus = WorkStatus.EditProperty;
                UpdatePointsListBox(m_CurrentOPCGroup.Points);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Group.SaveGroupToXmlFile(new System.IO.FileInfo(textBoxModule.Text).Name + ".xml", m_OPCGroups, textBoxModule.Text))
            {
                MessageBox.Show("Файл сохранен.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CurrentStatus = WorkStatus.NoEventChecked;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentStatus = WorkStatus.NoEventDll;
            OpenFileDialog openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string module;
                m_OPCGroups = new List<Group>();
                m_OPCGroups = Group.LoadGroupsFromXmlFile(openDlg.FileName, out module, out m_Events).ToList();
                if (m_OPCGroups.Count > 0)
                {
                    textBoxModule.Text = module;
                    FillEventsComboBox(m_Events);
                    CurrentStatus = WorkStatus.NoEventChecked;
                }
                else
                {
                    MessageBox.Show("Ошибка при загрузке .dll с событиями");
                    CurrentStatus = WorkStatus.NoEventDll;
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.DefaultExt = ".xml";
            saveDlg.AddExtension = true;
            saveDlg.Filter = "xml files (*.xml)|*.xml";

            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Group.SaveGroupToXmlFile(saveDlg.FileName, m_OPCGroups, textBoxModule.Text))
                {
                    MessageBox.Show("Файл сохранен.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CurrentStatus = WorkStatus.NoEventChecked;
                }
            }
        }

        private void checkBoxIsWriteble_CheckedChanged(object sender, EventArgs e)
        {
            m_CurrentOPCGroup.IsWriteble = checkBoxIsWriteble.Checked;
        }

        private void textBoxBitNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (m_CurrentOPCGroup == null || m_CurrentOPCGroup.Points == null) return;
                OPC.Point opcPoint = m_CurrentOPCGroup.Points.Find(p => p.FieldName == ((PropertyInfo)listBoxProperties.SelectedItem).Name);
                if (opcPoint != null)
                {
                    opcPoint.IsBoolean = checkBoxIsBoolean.Checked;
                    opcPoint.BitNumber = int.Parse(textBoxBitNumber.Text);
                    UpdatePointsListBox(m_CurrentOPCGroup.Points);
                }
            }

            if ((int)e.KeyChar == 27)
            {
                CurrentStatus = WorkStatus.EditProperty;
            }
        }












    }
}
