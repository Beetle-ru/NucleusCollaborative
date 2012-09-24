using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace PipeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool IsNotified = false;
        OracleConnection con = null;
        OracleDependency dep = null;

        public static void OnMyNotificaton(object src,
      EventArgs arg)
        {
            MessageBox.Show("Notification Received");
            IsNotified = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                con = new OracleConnection("DATA SOURCE=172.16.2.77;PERSIST SECURITY INFO=True;USER ID=SMK; Password=smk");
                OracleCommand cmd = new OracleCommand("select * from atest", con);
                con.Open();

                // Set the port number for the listener to listen for the notification
                // request
                OracleDependency.Port =1521;

                // Create an OracleDependency instance and bind it to an OracleCommand
                // instance.
                // When an OracleDependency instance is bound to an OracleCommand
                // instance, an OracleNotificationRequest is created and is set in the
                // OracleCommand's Notification property. This indicates subsequent 
                // execution of command will register the notification.
                // By default, the notification request is using the Database Change
                // Notification.
                dep = new OracleDependency(cmd);

                // Add the event handler to handle the notification. The 
                // OnMyNotification method will be invoked when a notification message
                // is received from the database
                dep.OnChange +=
                  new OnChangeEventHandler(OnMyNotificaton);

                // The notification registration is created and the query result sets 
                // associated with the command can be invalidated when there is a 
                // change.  When the first notification registration occurs, the 
                // notification listener is started and the listener port number 
                // will be 1005.
                cmd.ExecuteNonQuery();
                /*
                // Updating emp table so that a notification can be received when
                // the emp table is updated.
                // Start a transaction to update emp table
                OracleTransaction txn = con.BeginTransaction();
                // Create a new command which will update emp table
                string updateCmdText =
                  "update emp set sal = sal + 10 where empno = 7782";
                OracleCommand updateCmd = new OracleCommand(updateCmdText, con);
                // Update the emp table
                updateCmd.ExecuteNonQuery();
                //When the transaction is committed, a notification will be sent from
                //the database
                txn.Commit();
                 * */
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            // Loop while waiting for notification
            while (IsNotified == false)
            {
                Thread.Sleep(100);
            }
        }
    }
}

/*
 public static bool IsNotified = false;
 
    public static void Main(string[] args) 
    {
      //To Run this sample, make sure that the change notification privilege
      //is granted to scott.
      string constr = "User Id=scott;Password=tiger;Data Source=oracle";
      OracleConnection con = null;
      OracleDependency dep = null;
 
      try
      {
        con = new OracleConnection(constr);
        OracleCommand cmd = new OracleCommand("select * from emp", con);
        con.Open();
 
        // Set the port number for the listener to listen for the notification
        // request
        OracleDependency.Port = 1005; 
 
        // Create an OracleDependency instance and bind it to an OracleCommand
        // instance.
        // When an OracleDependency instance is bound to an OracleCommand
        // instance, an OracleNotificationRequest is created and is set in the
        // OracleCommand's Notification property. This indicates subsequent 
        // execution of command will register the notification.
        // By default, the notification request is using the Database Change
        // Notification.
        dep = new OracleDependency(cmd);
 
        // Add the event handler to handle the notification. The 
        // OnMyNotification method will be invoked when a notification message
        // is received from the database
        dep.OnChange += 
          new OnChangeEventHandler(MyNotificationSample.OnMyNotificaton);
 
        // The notification registration is created and the query result sets 
        // associated with the command can be invalidated when there is a 
        // change.  When the first notification registration occurs, the 
        // notification listener is started and the listener port number 
        // will be 1005.
        cmd.ExecuteNonQuery();
 
        // Updating emp table so that a notification can be received when
        // the emp table is updated.
        // Start a transaction to update emp table
        OracleTransaction txn = con.BeginTransaction();
        // Create a new command which will update emp table
        string updateCmdText = 
          "update emp set sal = sal + 10 where empno = 7782";
        OracleCommand updateCmd = new OracleCommand(updateCmdText, con);
        // Update the emp table
        updateCmd.ExecuteNonQuery();
        //When the transaction is committed, a notification will be sent from
        //the database
        txn.Commit();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
 
      con.Close();
      // Loop while waiting for notification
      while(MyNotificationSample.IsNotified == false)
      {
        Thread.Sleep(100);
      }
    }
 
    public static void OnMyNotificaton(object src, 
      OnChangeEventArgs arg) 
    {
      Console.WriteLine("Notification Received");
      DataTable changeDetails = arg.Details;
      Console.WriteLine("Data has changed in {0}", 
        changeDetails.Rows[0]["ResourceName"]);
      MyNotificationSample.IsNotified = true;
    }
  }
*/