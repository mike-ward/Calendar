using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BlueOnion
{
    /// <summary>
    /// Summary description for Welcome.
    /// </summary>
    public class WelcomeForm : Form
    {
        private Button okButton;
        private RichTextBox richTextBox;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public WelcomeForm()
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
            this.okButton = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            //
            // okButton
            //
            this.okButton.BackColor = System.Drawing.SystemColors.Control;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.okButton.Location = new System.Drawing.Point(66, 200);
            this.okButton.Name = "okButton";
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            //
            // richTextBox
            //
            this.richTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.richTextBox.Location = new System.Drawing.Point(8, 8);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(192, 192);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            //
            // WelcomeForm
            //
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(206, 228);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.okButton);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Welcome";
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            var assembly =
                System.Reflection.Assembly.GetEntryAssembly();

            var resource =
                assembly.GetManifestResourceStream("BlueOnion.Tips.rtf");

            richTextBox.LoadFile(resource, RichTextBoxStreamType.RichText);
            Location = Calendar.PositionAdjacent(this);
        }
    }
}