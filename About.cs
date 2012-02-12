// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;

namespace BlueOnion
{
    /// <summary>
    /// Summary description for About.
    /// </summary>
    public class About : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelRights;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel linkWebsite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEmail2;
        private System.Windows.Forms.LinkLabel linkEmail;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

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
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelRights = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.linkWebsite = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEmail2 = new System.Windows.Forms.Label();
            this.linkEmail = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelTitle.Location = new System.Drawing.Point(8, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(216, 16);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Calendar";
            // 
            // labelCopyright
            // 
            this.labelCopyright.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelCopyright.Location = new System.Drawing.Point(8, 24);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(216, 16);
            this.labelCopyright.TabIndex = 1;
            this.labelCopyright.Text = "Copyright 2008 Blue Onion Software";
            // 
            // labelRights
            // 
            this.labelRights.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelRights.Location = new System.Drawing.Point(8, 40);
            this.labelRights.Name = "labelRights";
            this.labelRights.Size = new System.Drawing.Size(216, 16);
            this.labelRights.TabIndex = 2;
            this.labelRights.Text = "All rights reserved";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(155, 181);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "Donate";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // linkWebsite
            // 
            this.linkWebsite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.linkWebsite.LinkArea = new System.Windows.Forms.LinkArea(0, 28);
            this.linkWebsite.Location = new System.Drawing.Point(8, 108);
            this.linkWebsite.Name = "linkWebsite";
            this.linkWebsite.Size = new System.Drawing.Size(216, 16);
            this.linkWebsite.TabIndex = 7;
            this.linkWebsite.TabStop = true;
            this.linkWebsite.Text = "http://blueonionsoftware.com";
            this.linkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWebsite_LinkClicked);
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(8, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Website:";
            // 
            // labelEmail2
            // 
            this.labelEmail2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelEmail2.Location = new System.Drawing.Point(8, 132);
            this.labelEmail2.Name = "labelEmail2";
            this.labelEmail2.Size = new System.Drawing.Size(100, 16);
            this.labelEmail2.TabIndex = 8;
            this.labelEmail2.Text = "Email:";
            // 
            // linkEmail
            // 
            this.linkEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.linkEmail.Location = new System.Drawing.Point(8, 148);
            this.linkEmail.Name = "linkEmail";
            this.linkEmail.Size = new System.Drawing.Size(216, 16);
            this.linkEmail.TabIndex = 9;
            this.linkEmail.TabStop = true;
            this.linkEmail.Text = "Support@BlueOnionSoftware.com";
            this.linkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEmail_LinkClicked);
            // 
            // About
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(242, 216);
            this.Controls.Add(this.linkEmail);
            this.Controls.Add(this.labelEmail2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkWebsite);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelRights);
            this.Controls.Add(this.labelCopyright);
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

        private void About_Load(object sender, System.EventArgs e)
        {
            this.Location = Calendar.PositionAdjacent(this);
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName name = assembly.GetName();
            this.labelTitle.Text = name.Name + " " + name.Version.ToString();
        }

        private void linkWebsite_LinkClicked(object sender,
            System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Calendar.WebSite);
        }

        private void linkEmail_LinkClicked(object sender,
            System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Calendar.EmailAddress);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=mike%40blueonionsoftware%2ecom&no_shipping=1&cn=Leave%20a%20note&tax=0&currency_code=USD&lc=US&bn=PP%2dDonationsBF&charset=UTF%2d8");
        }
    }
}
