using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialsWindowTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MaterialSpecificationsReferenceWindow.MaterialsEditForm editForm = new MaterialSpecificationsReferenceWindow.MaterialsEditForm();
            editForm.ShowDialog();
            //Console.ReadLine();
        }
    }
}
