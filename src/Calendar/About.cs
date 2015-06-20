using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace BlueOnion
{
    /// <summary>
    /// Summary description for About.
    /// </summary>
    public class About : Form
    {
        private Label labelTitle;
        private LinkLabel linkWebsite;
        private Label label1;
        private Label labelEmail2;
        private LinkLabel linkEmail;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public About()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.linkWebsite = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEmail2 = new System.Windows.Forms.Label();
            this.linkEmail = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            //
            // labelTitle
            //
            this.labelTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelTitle.Location = new System.Drawing.Point(8, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(216, 16);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Calendar";
            //
            // linkWebsite
            //
            this.linkWebsite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.linkWebsite.LinkArea = new System.Windows.Forms.LinkArea(0, 28);
            this.linkWebsite.Location = new System.Drawing.Point(8, 51);
            this.linkWebsite.Name = "linkWebsite";
            this.linkWebsite.Size = new System.Drawing.Size(216, 16);
            this.linkWebsite.TabIndex = 7;
            this.linkWebsite.TabStop = true;
            this.linkWebsite.Text = "http://mike-ward.net";
            this.linkWebsite.UseCompatibleTextRendering = true;
            this.linkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWebsite_LinkClicked);
            //
            // label1
            //
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Website:";
            //
            // labelEmail2
            //
            this.labelEmail2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelEmail2.Location = new System.Drawing.Point(8, 75);
            this.labelEmail2.Name = "labelEmail2";
            this.labelEmail2.Size = new System.Drawing.Size(100, 16);
            this.labelEmail2.TabIndex = 8;
            this.labelEmail2.Text = "Email:";
            //
            // linkEmail
            //
            this.linkEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.linkEmail.Location = new System.Drawing.Point(8, 91);
            this.linkEmail.Name = "linkEmail";
            this.linkEmail.Size = new System.Drawing.Size(216, 16);
            this.linkEmail.TabIndex = 9;
            this.linkEmail.TabStop = true;
            this.linkEmail.Text = "mailto:mike@mike-ward.net";
            this.linkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEmail_LinkClicked);
            //
            // About
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(242, 129);
            this.Controls.Add(this.linkEmail);
            this.Controls.Add(this.labelEmail2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkWebsite);
            this.Controls.Add(this.labelTitle);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About Calendar";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private void About_Load(object sender, EventArgs e)
        {
            Location = Calendar.PositionAdjacent(this);
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName();
            labelTitle.Text = name.Name + " " + name.Version;
        }

        private void linkWebsite_LinkClicked(object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Calendar.WebSite);
        }

        private void linkEmail_LinkClicked(object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Calendar.EmailAddress);
        }
    }
}