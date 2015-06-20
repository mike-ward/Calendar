using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BlueOnion
{
    internal class Colors : Form
    {
        private readonly Settings settings;
        private readonly MonthCalendarEx monthCalendar;
        private readonly Calendar form;
        private ColorDialog colorDialog;
        private GroupBox groupBox;
        private GroupBox groupBoxOpacity;
        private TrackBar trackBar;
        private Button buttonRestoreDefaults;
        private Button buttonOk;
        private Button buttonCancel;
        private Button buttonText;
        private Label labelText;
        private Label labelBackground;
        private Button buttonBackground;
        private Label labelTitleText;
        private Button buttonTitleText;
        private Label labelTitleBackground;
        private Button buttonTitleBackground;
        private Label labelTrailingText;
        private Button buttonTrailingText;
        private Label labelHighlight;
        private Button buttonHighlight;
        private Button buttonFont;
        private Button buttonGridlines;
        private Label labelGridlines;
        private CheckBox checkBoxGridlines;
        private Label labelWeekDayText;
        private Button buttonWeekdayText;
        private Label labelWeekdayBackground;
        private Button buttonWeekdayBackground;
        private Label labelWeeknumberBackground;
        private Button buttonWeeknumberBackground;
        private Label labelWeeknumberText;
        private Button buttonWeeknumberText;
        private Label labelWeekdayBar;
        private Button buttonWeekdayBar;
        private GroupBox groupBoxStart;
        private CheckBox checkBoxJanuary;
        private NumericUpDown numericUpDownStart;
        private Label labelPreviousMonths;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public Colors(MonthCalendarEx monthCalendar, Settings settings, Calendar form)
        {
            InitializeComponent();
            this.monthCalendar = monthCalendar;
            this.settings = settings;
            this.form = form;
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

                if (colorDialog != null)
                {
                    colorDialog.Dispose();
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.labelWeekdayBar = new System.Windows.Forms.Label();
            this.buttonWeekdayBar = new System.Windows.Forms.Button();
            this.labelWeeknumberBackground = new System.Windows.Forms.Label();
            this.buttonWeeknumberBackground = new System.Windows.Forms.Button();
            this.labelWeeknumberText = new System.Windows.Forms.Label();
            this.buttonWeeknumberText = new System.Windows.Forms.Button();
            this.labelGridlines = new System.Windows.Forms.Label();
            this.buttonGridlines = new System.Windows.Forms.Button();
            this.labelWeekdayBackground = new System.Windows.Forms.Label();
            this.buttonWeekdayBackground = new System.Windows.Forms.Button();
            this.labelWeekDayText = new System.Windows.Forms.Label();
            this.buttonWeekdayText = new System.Windows.Forms.Button();
            this.labelHighlight = new System.Windows.Forms.Label();
            this.buttonHighlight = new System.Windows.Forms.Button();
            this.labelTrailingText = new System.Windows.Forms.Label();
            this.buttonTrailingText = new System.Windows.Forms.Button();
            this.labelTitleBackground = new System.Windows.Forms.Label();
            this.buttonTitleBackground = new System.Windows.Forms.Button();
            this.labelTitleText = new System.Windows.Forms.Label();
            this.buttonTitleText = new System.Windows.Forms.Button();
            this.labelBackground = new System.Windows.Forms.Label();
            this.buttonBackground = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.buttonText = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBoxOpacity = new System.Windows.Forms.GroupBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.buttonRestoreDefaults = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonFont = new System.Windows.Forms.Button();
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.checkBoxGridlines = new System.Windows.Forms.CheckBox();
            this.checkBoxJanuary = new System.Windows.Forms.CheckBox();
            this.numericUpDownStart = new System.Windows.Forms.NumericUpDown();
            this.labelPreviousMonths = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.groupBoxOpacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar)).BeginInit();
            this.groupBoxStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDownStart)).BeginInit();
            this.SuspendLayout();
            //
            // groupBox
            //
            this.groupBox.Controls.Add(this.labelWeekdayBar);
            this.groupBox.Controls.Add(this.buttonWeekdayBar);
            this.groupBox.Controls.Add(this.labelWeeknumberBackground);
            this.groupBox.Controls.Add(this.buttonWeeknumberBackground);
            this.groupBox.Controls.Add(this.labelWeeknumberText);
            this.groupBox.Controls.Add(this.buttonWeeknumberText);
            this.groupBox.Controls.Add(this.labelGridlines);
            this.groupBox.Controls.Add(this.buttonGridlines);
            this.groupBox.Controls.Add(this.labelWeekdayBackground);
            this.groupBox.Controls.Add(this.buttonWeekdayBackground);
            this.groupBox.Controls.Add(this.labelWeekDayText);
            this.groupBox.Controls.Add(this.buttonWeekdayText);
            this.groupBox.Controls.Add(this.labelHighlight);
            this.groupBox.Controls.Add(this.buttonHighlight);
            this.groupBox.Controls.Add(this.labelTrailingText);
            this.groupBox.Controls.Add(this.buttonTrailingText);
            this.groupBox.Controls.Add(this.labelTitleBackground);
            this.groupBox.Controls.Add(this.buttonTitleBackground);
            this.groupBox.Controls.Add(this.labelTitleText);
            this.groupBox.Controls.Add(this.buttonTitleText);
            this.groupBox.Controls.Add(this.labelBackground);
            this.groupBox.Controls.Add(this.buttonBackground);
            this.groupBox.Controls.Add(this.labelText);
            this.groupBox.Controls.Add(this.buttonText);
            this.groupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox.Location = new System.Drawing.Point(8, 8);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(296, 176);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Colors";
            //
            // labelWeekdayBar
            //
            this.labelWeekdayBar.Location = new System.Drawing.Point(168, 144);
            this.labelWeekdayBar.Name = "labelWeekdayBar";
            this.labelWeekdayBar.Size = new System.Drawing.Size(120, 16);
            this.labelWeekdayBar.TabIndex = 29;
            this.labelWeekdayBar.Text = "Weekday Bar";
            //
            // buttonWeekdayBar
            //
            this.buttonWeekdayBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWeekdayBar.Location = new System.Drawing.Point(144, 144);
            this.buttonWeekdayBar.Name = "buttonWeekdayBar";
            this.buttonWeekdayBar.Size = new System.Drawing.Size(16, 16);
            this.buttonWeekdayBar.TabIndex = 28;
            this.buttonWeekdayBar.Click += new System.EventHandler(this.buttonWeekdayBar_Click);
            //
            // labelWeeknumberBackground
            //
            this.labelWeeknumberBackground.Location = new System.Drawing.Point(168, 120);
            this.labelWeeknumberBackground.Name = "labelWeeknumberBackground";
            this.labelWeeknumberBackground.Size = new System.Drawing.Size(120, 16);
            this.labelWeeknumberBackground.TabIndex = 27;
            this.labelWeeknumberBackground.Text = "Week Numbers Back";
            //
            // buttonWeeknumberBackground
            //
            this.buttonWeeknumberBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWeeknumberBackground.Location = new System.Drawing.Point(144, 120);
            this.buttonWeeknumberBackground.Name = "buttonWeeknumberBackground";
            this.buttonWeeknumberBackground.Size = new System.Drawing.Size(16, 16);
            this.buttonWeeknumberBackground.TabIndex = 26;
            this.buttonWeeknumberBackground.Click += new System.EventHandler(this.buttonWeeknumberBackground_Click);
            //
            // labelWeeknumberText
            //
            this.labelWeeknumberText.Location = new System.Drawing.Point(40, 120);
            this.labelWeeknumberText.Name = "labelWeeknumberText";
            this.labelWeeknumberText.Size = new System.Drawing.Size(96, 16);
            this.labelWeeknumberText.TabIndex = 25;
            this.labelWeeknumberText.Text = "Week Numbers";
            //
            // buttonWeeknumberText
            //
            this.buttonWeeknumberText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWeeknumberText.Location = new System.Drawing.Point(16, 120);
            this.buttonWeeknumberText.Name = "buttonWeeknumberText";
            this.buttonWeeknumberText.Size = new System.Drawing.Size(16, 16);
            this.buttonWeeknumberText.TabIndex = 24;
            this.buttonWeeknumberText.Click += new System.EventHandler(this.buttonWeeknumberText_Click);
            //
            // labelGridlines
            //
            this.labelGridlines.Location = new System.Drawing.Point(40, 144);
            this.labelGridlines.Name = "labelGridlines";
            this.labelGridlines.Size = new System.Drawing.Size(96, 16);
            this.labelGridlines.TabIndex = 19;
            this.labelGridlines.Text = "Gridlines";
            //
            // buttonGridlines
            //
            this.buttonGridlines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGridlines.Location = new System.Drawing.Point(16, 144);
            this.buttonGridlines.Name = "buttonGridlines";
            this.buttonGridlines.Size = new System.Drawing.Size(16, 16);
            this.buttonGridlines.TabIndex = 18;
            this.buttonGridlines.Click += new System.EventHandler(this.buttonGridlines_Click);
            //
            // labelWeekdayBackground
            //
            this.labelWeekdayBackground.Location = new System.Drawing.Point(168, 48);
            this.labelWeekdayBackground.Name = "labelWeekdayBackground";
            this.labelWeekdayBackground.Size = new System.Drawing.Size(120, 16);
            this.labelWeekdayBackground.TabIndex = 23;
            this.labelWeekdayBackground.Text = "Weekday Background";
            //
            // buttonWeekdayBackground
            //
            this.buttonWeekdayBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWeekdayBackground.Location = new System.Drawing.Point(144, 48);
            this.buttonWeekdayBackground.Name = "buttonWeekdayBackground";
            this.buttonWeekdayBackground.Size = new System.Drawing.Size(16, 16);
            this.buttonWeekdayBackground.TabIndex = 22;
            this.buttonWeekdayBackground.Click += new System.EventHandler(this.buttonWeekdayBackground_Click);
            //
            // labelWeekDayText
            //
            this.labelWeekDayText.Location = new System.Drawing.Point(40, 48);
            this.labelWeekDayText.Name = "labelWeekDayText";
            this.labelWeekDayText.Size = new System.Drawing.Size(96, 16);
            this.labelWeekDayText.TabIndex = 21;
            this.labelWeekDayText.Text = "Weekday Text";
            //
            // buttonWeekdayText
            //
            this.buttonWeekdayText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWeekdayText.Location = new System.Drawing.Point(16, 48);
            this.buttonWeekdayText.Name = "buttonWeekdayText";
            this.buttonWeekdayText.Size = new System.Drawing.Size(16, 16);
            this.buttonWeekdayText.TabIndex = 20;
            this.buttonWeekdayText.Click += new System.EventHandler(this.buttonWeekdayText_Click);
            //
            // labelHighlight
            //
            this.labelHighlight.Location = new System.Drawing.Point(168, 96);
            this.labelHighlight.Name = "labelHighlight";
            this.labelHighlight.Size = new System.Drawing.Size(120, 16);
            this.labelHighlight.TabIndex = 17;
            this.labelHighlight.Text = "Highlight Text";
            //
            // buttonHighlight
            //
            this.buttonHighlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHighlight.Location = new System.Drawing.Point(144, 96);
            this.buttonHighlight.Name = "buttonHighlight";
            this.buttonHighlight.Size = new System.Drawing.Size(16, 16);
            this.buttonHighlight.TabIndex = 16;
            this.buttonHighlight.Click += new System.EventHandler(this.buttonHighlight_Click);
            //
            // labelTrailingText
            //
            this.labelTrailingText.Location = new System.Drawing.Point(40, 96);
            this.labelTrailingText.Name = "labelTrailingText";
            this.labelTrailingText.Size = new System.Drawing.Size(96, 16);
            this.labelTrailingText.TabIndex = 15;
            this.labelTrailingText.Text = "Trailing Text";
            //
            // buttonTrailingText
            //
            this.buttonTrailingText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTrailingText.Location = new System.Drawing.Point(16, 96);
            this.buttonTrailingText.Name = "buttonTrailingText";
            this.buttonTrailingText.Size = new System.Drawing.Size(16, 16);
            this.buttonTrailingText.TabIndex = 14;
            this.buttonTrailingText.Click += new System.EventHandler(this.buttonTrailingText_Click);
            //
            // labelTitleBackground
            //
            this.labelTitleBackground.Location = new System.Drawing.Point(168, 24);
            this.labelTitleBackground.Name = "labelTitleBackground";
            this.labelTitleBackground.Size = new System.Drawing.Size(120, 16);
            this.labelTitleBackground.TabIndex = 13;
            this.labelTitleBackground.Text = "Title Background";
            //
            // buttonTitleBackground
            //
            this.buttonTitleBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTitleBackground.Location = new System.Drawing.Point(144, 24);
            this.buttonTitleBackground.Name = "buttonTitleBackground";
            this.buttonTitleBackground.Size = new System.Drawing.Size(16, 16);
            this.buttonTitleBackground.TabIndex = 12;
            this.buttonTitleBackground.Click += new System.EventHandler(this.buttonTitleBackground_Click);
            //
            // labelTitleText
            //
            this.labelTitleText.Location = new System.Drawing.Point(40, 24);
            this.labelTitleText.Name = "labelTitleText";
            this.labelTitleText.Size = new System.Drawing.Size(96, 16);
            this.labelTitleText.TabIndex = 11;
            this.labelTitleText.Text = "Title Text";
            //
            // buttonTitleText
            //
            this.buttonTitleText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTitleText.Location = new System.Drawing.Point(16, 24);
            this.buttonTitleText.Name = "buttonTitleText";
            this.buttonTitleText.Size = new System.Drawing.Size(16, 16);
            this.buttonTitleText.TabIndex = 10;
            this.buttonTitleText.Click += new System.EventHandler(this.buttonTitleText_Click);
            //
            // labelBackground
            //
            this.labelBackground.Location = new System.Drawing.Point(168, 72);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new System.Drawing.Size(120, 16);
            this.labelBackground.TabIndex = 9;
            this.labelBackground.Text = "Month Background";
            //
            // buttonBackground
            //
            this.buttonBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackground.Location = new System.Drawing.Point(144, 72);
            this.buttonBackground.Name = "buttonBackground";
            this.buttonBackground.Size = new System.Drawing.Size(16, 16);
            this.buttonBackground.TabIndex = 8;
            this.buttonBackground.Click += new System.EventHandler(this.buttonBackground_Click);
            //
            // labelText
            //
            this.labelText.Location = new System.Drawing.Point(40, 72);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(100, 16);
            this.labelText.TabIndex = 7;
            this.labelText.Text = "Month Text";
            //
            // buttonText
            //
            this.buttonText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonText.Location = new System.Drawing.Point(16, 72);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(16, 16);
            this.buttonText.TabIndex = 6;
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            //
            // groupBoxOpacity
            //
            this.groupBoxOpacity.Controls.Add(this.trackBar);
            this.groupBoxOpacity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxOpacity.Location = new System.Drawing.Point(8, 200);
            this.groupBoxOpacity.Name = "groupBoxOpacity";
            this.groupBoxOpacity.Size = new System.Drawing.Size(152, 72);
            this.groupBoxOpacity.TabIndex = 1;
            this.groupBoxOpacity.TabStop = false;
            this.groupBoxOpacity.Text = "Opacity";
            //
            // trackBar
            //
            this.trackBar.Location = new System.Drawing.Point(8, 24);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(136, 45);
            this.trackBar.TabIndex = 0;
            this.trackBar.TickFrequency = 10;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            //
            // buttonRestoreDefaults
            //
            this.buttonRestoreDefaults.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRestoreDefaults.Location = new System.Drawing.Point(8, 320);
            this.buttonRestoreDefaults.Name = "buttonRestoreDefaults";
            this.buttonRestoreDefaults.Size = new System.Drawing.Size(152, 23);
            this.buttonRestoreDefaults.TabIndex = 2;
            this.buttonRestoreDefaults.Text = "Restore Defaults...";
            this.buttonRestoreDefaults.Click += new System.EventHandler(this.buttonRestoreDefaults_Click);
            //
            // buttonOk
            //
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOk.Location = new System.Drawing.Point(77, 360);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(72, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            //
            // buttonCancel
            //
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(165, 360);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            //
            // buttonFont
            //
            this.buttonFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonFont.Location = new System.Drawing.Point(8, 288);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(152, 23);
            this.buttonFont.TabIndex = 5;
            this.buttonFont.Text = "Font...";
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            //
            // groupBoxStart
            //
            this.groupBoxStart.Controls.Add(this.labelPreviousMonths);
            this.groupBoxStart.Controls.Add(this.numericUpDownStart);
            this.groupBoxStart.Controls.Add(this.checkBoxJanuary);
            this.groupBoxStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxStart.Location = new System.Drawing.Point(176, 200);
            this.groupBoxStart.Name = "groupBoxStart";
            this.groupBoxStart.Size = new System.Drawing.Size(128, 112);
            this.groupBoxStart.TabIndex = 6;
            this.groupBoxStart.TabStop = false;
            this.groupBoxStart.Text = "Start Month";
            //
            // checkBoxGridlines
            //
            this.checkBoxGridlines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxGridlines.Location = new System.Drawing.Point(192, 320);
            this.checkBoxGridlines.Name = "checkBoxGridlines";
            this.checkBoxGridlines.Size = new System.Drawing.Size(112, 24);
            this.checkBoxGridlines.TabIndex = 7;
            this.checkBoxGridlines.Text = "Show Gridlines";
            this.checkBoxGridlines.Click += new System.EventHandler(this.checkBoxGridlines_Click);
            //
            // checkBoxJanuary
            //
            this.checkBoxJanuary.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxJanuary.Location = new System.Drawing.Point(16, 24);
            this.checkBoxJanuary.Name = "checkBoxJanuary";
            this.checkBoxJanuary.TabIndex = 0;
            this.checkBoxJanuary.Text = "January";
            //
            // numericUpDownStart
            //
            this.numericUpDownStart.Location = new System.Drawing.Point(72, 68);
            this.numericUpDownStart.Minimum = new System.Decimal(new int[]
            {
                100,
                0,
                0,
                -2147483648
            });
            this.numericUpDownStart.Name = "numericUpDownStart";
            this.numericUpDownStart.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownStart.TabIndex = 1;
            //
            // labelPreviousMonths
            //
            this.labelPreviousMonths.Location = new System.Drawing.Point(16, 64);
            this.labelPreviousMonths.Name = "labelPreviousMonths";
            this.labelPreviousMonths.Size = new System.Drawing.Size(48, 24);
            this.labelPreviousMonths.TabIndex = 2;
            this.labelPreviousMonths.Text = "Previous Months";
            //
            // Colors
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(314, 400);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxGridlines);
            this.Controls.Add(this.groupBoxStart);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonRestoreDefaults);
            this.Controls.Add(this.groupBoxOpacity);
            this.Controls.Add(this.groupBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Colors";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Appearance";
            this.Load += new System.EventHandler(this.Colors_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBoxOpacity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.trackBar)).EndInit();
            this.groupBoxStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.numericUpDownStart)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private void Colors_Load(object sender, EventArgs e)
        {
            Location = Calendar.PositionAdjacent(this);
            SetButtonColors();
            checkBoxGridlines.Checked = settings.Gridlines;
            trackBar.Value = (int) (form.Opacity*100);
            checkBoxJanuary.Checked = settings.StartMonthJanuary;
            numericUpDownStart.Value = settings.StartMonthPrevious;
        }

        private void SetButtonColors()
        {
            buttonText.BackColor = monthCalendar.ForeColor;
            buttonBackground.BackColor = monthCalendar.BackColor;
            buttonTitleText.BackColor = monthCalendar.TitleForeColor;
            buttonTitleBackground.BackColor = monthCalendar.TitleBackColor;
            buttonTrailingText.BackColor = monthCalendar.TrailingForeColor;
            buttonHighlight.BackColor = monthCalendar.HighlightDayTextColor;
            buttonGridlines.BackColor = monthCalendar.GridlinesColor;
            buttonWeekdayText.BackColor = monthCalendar.WeekdayForeColor;
            buttonWeekdayBackground.BackColor = monthCalendar.WeekdayBackColor;
            buttonWeeknumberText.BackColor = monthCalendar.WeeknumberForeColor;
            buttonWeeknumberBackground.BackColor = monthCalendar.WeeknumberBackColor;
            buttonWeekdayBar.BackColor = monthCalendar.WeekdayBarColor;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            settings.ColorFore = monthCalendar.ForeColor;
            settings.ColorBack = monthCalendar.BackColor;
            settings.ColorTitleFore = monthCalendar.TitleForeColor;
            settings.ColorTitleBack = monthCalendar.TitleBackColor;
            settings.ColorTrailingFore = monthCalendar.TrailingForeColor;
            settings.ColorHighlightDayFore = monthCalendar.HighlightDayTextColor;
            settings.ColorGridlines = monthCalendar.GridlinesColor;
            settings.ColorWeekdayFore = monthCalendar.WeekdayForeColor;
            settings.ColorWeekdayBack = monthCalendar.WeekdayBackColor;
            settings.ColorWeeknumberFore = monthCalendar.WeeknumberForeColor;
            settings.ColorWeeknumberBack = monthCalendar.WeeknumberBackColor;
            settings.ColorWeekdayBar = monthCalendar.WeekdayBarColor;
            settings.Opacity = trackBar.Value/(double) 100;
            settings.Font = monthCalendar.Font;
            settings.Gridlines = monthCalendar.Gridlines;
            settings.StartMonthJanuary = checkBoxJanuary.Checked;
            settings.StartMonthPrevious = Convert.ToInt32(numericUpDownStart.Value);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            settingsToCalendar(settings);
        }

        private void buttonRestoreDefaults_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settingsToCalendar(settings);
            SetButtonColors();
        }

        private void settingsToCalendar(Settings settings)
        {
            monthCalendar.ForeColor = settings.ColorFore;
            monthCalendar.BackColor = settings.ColorBack;
            monthCalendar.TitleForeColor = settings.ColorTitleFore;
            monthCalendar.TitleBackColor = settings.ColorTitleBack;
            monthCalendar.TrailingForeColor = settings.ColorTrailingFore;
            monthCalendar.HighlightDayTextColor = settings.ColorHighlightDayFore;
            monthCalendar.GridlinesColor = settings.ColorGridlines;
            monthCalendar.WeekdayForeColor = settings.ColorWeekdayFore;
            monthCalendar.WeekdayBackColor = settings.ColorWeekdayBack;
            monthCalendar.WeeknumberForeColor = settings.ColorWeeknumberFore;
            monthCalendar.WeeknumberBackColor = settings.ColorWeeknumberBack;
            monthCalendar.WeekdayBarColor = settings.ColorWeekdayBar;
            form.Opacity = settings.Opacity;
            form.BackColor = settings.ColorBack;
            trackBar.Value = (int) (settings.Opacity*100);
            monthCalendar.Font = settings.Font;
            monthCalendar.Gridlines = settings.Gridlines;
            checkBoxJanuary.Checked = settings.StartMonthJanuary;
            numericUpDownStart.Value = settings.StartMonthPrevious;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            var opacity = (double) trackBar.Value/100;
            form.Opacity = opacity;
        }

        private void buttonText_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.ForeColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.ForeColor = colorDialog.Color;
                buttonText.BackColor = colorDialog.Color;
            }
        }

        private void buttonBackground_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.BackColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.BackColor = colorDialog.Color;
                buttonBackground.BackColor = colorDialog.Color;
            }
        }

        private void buttonTitleText_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.TitleForeColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.TitleForeColor = colorDialog.Color;
                buttonTitleText.BackColor = colorDialog.Color;
            }
        }

        private void buttonTitleBackground_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.TitleBackColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.TitleBackColor = colorDialog.Color;
                buttonTitleBackground.BackColor = colorDialog.Color;
            }
        }

        private void buttonTrailingText_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.TrailingForeColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.TrailingForeColor = colorDialog.Color;
                buttonTrailingText.BackColor = colorDialog.Color;
            }
        }

        private void buttonHighlight_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.HighlightDayTextColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.HighlightDayTextColor = colorDialog.Color;
                buttonHighlight.BackColor = colorDialog.Color;
            }
        }

        private void buttonGridlines_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.GridlinesColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.GridlinesColor = colorDialog.Color;
                buttonGridlines.BackColor = colorDialog.Color;
            }
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.Font = monthCalendar.Font;
            fontDialog.ShowEffects = false;
            fontDialog.ScriptsOnly = true;

            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.Font = fontDialog.Font;
            }
        }

        private void checkBoxGridlines_Click(object sender, EventArgs e)
        {
            monthCalendar.Gridlines = checkBoxGridlines.Checked;
        }

        private void buttonWeekdayText_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.WeekdayForeColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.WeekdayForeColor = colorDialog.Color;
                buttonWeekdayText.BackColor = colorDialog.Color;
            }
        }

        private void buttonWeekdayBackground_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.WeekdayBackColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.WeekdayBackColor = colorDialog.Color;
                buttonWeekdayBackground.BackColor = colorDialog.Color;
            }
        }

        private void buttonWeeknumberText_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.WeeknumberForeColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.WeeknumberForeColor = colorDialog.Color;
                buttonWeeknumberText.BackColor = colorDialog.Color;
            }
        }

        private void buttonWeeknumberBackground_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.WeeknumberBackColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.WeeknumberBackColor = colorDialog.Color;
                buttonWeeknumberBackground.BackColor = colorDialog.Color;
            }
        }

        private void buttonWeekdayBar_Click(object sender, EventArgs e)
        {
            colorDialog.Color = monthCalendar.WeekdayBarColor;

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                monthCalendar.WeekdayBarColor = colorDialog.Color;
                buttonWeekdayBar.BackColor = colorDialog.Color;
            }
        }
    }
}