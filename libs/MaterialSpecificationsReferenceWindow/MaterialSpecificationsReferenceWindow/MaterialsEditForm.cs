using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialSpecificationsReferenceWindow
{
    public partial class MaterialsEditForm : Form
    {
        public MaterialsEditForm()
        {
            InitializeComponent();
            dataGridViewMaterials.DataSource = DBWorker.Instance.MaterialHash;
        }
    }
}
