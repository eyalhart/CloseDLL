using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace closeApplication_cs
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : System.Windows.Forms.Form
    {
        internal System.Windows.Forms.Button btnCloseAll;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.Button btnLaunch1;
        internal System.Windows.Forms.ListView ListView1;
        internal System.Windows.Forms.Button btnClose1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private Process[] processes;
        private string procName = "Notepad";
        private string specialFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.System);

        public Form1()
        {
            // 
            // Required for Windows Form Designer support.
            // 
            InitializeComponent();
            // 
            // To do: Add any constructor code after the InitializeComponent call.
            // 
        }

        /// <summary>
        /// Clean up any resources that are being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose1 = new System.Windows.Forms.Button();
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.btnLaunch1 = new System.Windows.Forms.Button();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnClose1
            // 
            this.btnClose1.Location = new System.Drawing.Point(160, 176);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(112, 32);
            this.btnClose1.TabIndex = 4;
            this.btnClose1.Text = "Close Selected Process";
            this.btnClose1.Click += new System.EventHandler(this.btnClose1_Click);
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "DLL";
            this.ColumnHeader3.Width = 180;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Process Name";
            this.ColumnHeader2.Width = 180;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Process ID";
            this.ColumnHeader1.Width = 85;
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Location = new System.Drawing.Point(160, 216);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(112, 32);
            this.btnCloseAll.TabIndex = 3;
            this.btnCloseAll.Text = "Close All Processes";
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // btnLaunch1
            // 
            this.btnLaunch1.Location = new System.Drawing.Point(32, 176);
            this.btnLaunch1.Name = "btnLaunch1";
            this.btnLaunch1.Size = new System.Drawing.Size(112, 72);
            this.btnLaunch1.TabIndex = 1;
            this.btnLaunch1.Text = "Refresh";
            this.btnLaunch1.Click += new System.EventHandler(this.btnLaunch1_Click);
            // 
            // ListView1
            // 
            this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3});
            this.ListView1.Location = new System.Drawing.Point(22, 8);
            this.ListView1.MultiSelect = false;
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(450, 152);
            this.ListView1.TabIndex = 7;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            this.ListView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(492, 266);
            this.Controls.Add(this.btnCloseAll);
            this.Controls.Add(this.btnLaunch1);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.btnClose1);
            this.Name = "Form1";
            this.Text = "Process Example";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        #endregion

        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void buildList()
        {
            ListViewItem itemAdd;
            ListView1.Items.Clear();
            processes = Process.GetProcessesByName(procName);
            foreach (Process proc in processes)
            {
                itemAdd = ListView1.Items.Add(proc.MainWindowTitle);
                itemAdd.SubItems.Add(proc.Id.ToString());
            }
        }

        private void btnLaunch1_Click(object sender, System.EventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            ListViewItem itemAdd;
            ListView1.Items.Clear();

            foreach (Process process in processes)
            {
                itemAdd = ListView1.Items.Add(process.Id.ToString());
                itemAdd.SubItems.Add(process.ProcessName.ToString());

                try
                {
                    ProcessModuleCollection myProcessModuleCollection = process.Modules;

                    for (int i = 0; i < myProcessModuleCollection.Count; i++)
                    {
                        ProcessModule myProcessModule = myProcessModuleCollection[i];
                        itemAdd.SubItems.Add(myProcessModule.ModuleName.ToString());
                        if (i > 0)
                            itemAdd.SubItems.Add("Eyal");
                    }
                }
                catch { }
            }
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

        private void btnClose1_Click(object sender, System.EventArgs e)
        {
            try
            {
                int procID = System.Convert.ToInt32(ListView1.SelectedItems[0].SubItems[1].Text);
                Process tempProc = Process.GetProcessById(procID);
                tempProc.CloseMainWindow();
                tempProc.WaitForExit();
                buildList();
            }
            catch
            {
                MessageBox.Show("Please select a process in the ListView before clicking this button." +
                    " Or the Process may have been closed by somebody.");
                buildList();
            }
        }

        private void btnCloseAll_Click(object sender, System.EventArgs e)
        {
            try
            {
                foreach (Process proc in processes)
                {
                    proc.CloseMainWindow();
                    proc.WaitForExit();
                }
                buildList();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("No instances of Notepad running.");
            }
        }

        private void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Make sure that you do not leave any instances running.
            if (processes != null && processes.Length != 0)
                this.btnCloseAll_Click(this, e);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
