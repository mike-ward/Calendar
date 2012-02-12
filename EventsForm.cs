// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace BlueOnion
{
	/// <summary>
	/// Summary description for EventsForm.
	/// </summary>
	public class EventsForm : System.Windows.Forms.Form
	{
        private bool suspendUpdates;
        private Event[] events;
        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button newEventButton;
        private System.Windows.Forms.Button deleteEventButton;
        private System.Windows.Forms.RadioButton movableRadioButton;
        private System.Windows.Forms.RadioButton specialRadioButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label weekdayLabel;
        private System.Windows.Forms.Label monthLlabel;
        private System.Windows.Forms.ComboBox weekdayComboBox;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.Label weekLabel;
        private System.Windows.Forms.ComboBox weekComboBox;
        private System.Windows.Forms.GroupBox movableGroupBox;
        private System.Windows.Forms.NumericUpDown specialDaysUpDown;
        private System.Windows.Forms.Label specialDaysLabel;
        private System.Windows.Forms.GroupBox specialGoupBox;
        private System.Windows.Forms.ComboBox recurringComboBox;
        private System.Windows.Forms.DateTimePicker recurringDateTimePicker;
        private System.Windows.Forms.GroupBox recurringGroupBox;
        private System.Windows.Forms.RadioButton recurringRadioButton;
        private System.Windows.Forms.GroupBox descriptionGroupBox;
        private System.Windows.Forms.ComboBox specialComboBox;
		private System.Windows.Forms.PictureBox pictureBoxHighlightColor;
		private System.Windows.Forms.CheckBox checkBoxHighlightDayForeColor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        // ---------------------------------------------------------------------
        public EventsForm(EventsCollection events, Color colorHighlightDate)
		{
			InitializeComponent();

            this.weekdayComboBox.Items.AddRange
                (DateTimeFormatInfo.CurrentInfo.DayNames);

            this.monthComboBox.Items.AddRange(Event.AbbreviatedMonthNames);

            this.recurringComboBox.DataSource = RecurringEventNameValue.List();
            this.recurringComboBox.DisplayMember = "Name";
            this.recurringComboBox.ValueMember = "Value";

            this.weekComboBox.Items.AddRange(Event.WeekNumbers());

            this.specialComboBox.DataSource = SpecialEventNameValue.List();
            this.specialComboBox.DisplayMember = "Name";
            this.specialComboBox.ValueMember = "Value";

            if (events != null)
            {
                this.eventsListBox.Items.AddRange(events.GetEvents());

                if (this.eventsListBox.Items.Count != 0)
                {
                    this.eventsListBox.SelectedIndex = 0;
                }
            }

			this.pictureBoxHighlightColor.BackColor = colorHighlightDate;
		}

        // ---------------------------------------------------------------------
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
        // ---------------------------------------------------------------------
        private void InitializeComponent()
		{
			this.eventsListBox = new System.Windows.Forms.ListBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.newEventButton = new System.Windows.Forms.Button();
			this.deleteEventButton = new System.Windows.Forms.Button();
			this.movableRadioButton = new System.Windows.Forms.RadioButton();
			this.specialRadioButton = new System.Windows.Forms.RadioButton();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.weekdayLabel = new System.Windows.Forms.Label();
			this.monthLlabel = new System.Windows.Forms.Label();
			this.weekdayComboBox = new System.Windows.Forms.ComboBox();
			this.monthComboBox = new System.Windows.Forms.ComboBox();
			this.weekLabel = new System.Windows.Forms.Label();
			this.weekComboBox = new System.Windows.Forms.ComboBox();
			this.movableGroupBox = new System.Windows.Forms.GroupBox();
			this.specialComboBox = new System.Windows.Forms.ComboBox();
			this.specialDaysUpDown = new System.Windows.Forms.NumericUpDown();
			this.specialDaysLabel = new System.Windows.Forms.Label();
			this.specialGoupBox = new System.Windows.Forms.GroupBox();
			this.recurringComboBox = new System.Windows.Forms.ComboBox();
			this.recurringDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.recurringGroupBox = new System.Windows.Forms.GroupBox();
			this.recurringRadioButton = new System.Windows.Forms.RadioButton();
			this.descriptionGroupBox = new System.Windows.Forms.GroupBox();
			this.pictureBoxHighlightColor = new System.Windows.Forms.PictureBox();
			this.checkBoxHighlightDayForeColor = new System.Windows.Forms.CheckBox();
			this.movableGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.specialDaysUpDown)).BeginInit();
			this.specialGoupBox.SuspendLayout();
			this.recurringGroupBox.SuspendLayout();
			this.descriptionGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// eventsListBox
			// 
			this.eventsListBox.HorizontalScrollbar = true;
			this.eventsListBox.Location = new System.Drawing.Point(24, 8);
			this.eventsListBox.Name = "eventsListBox";
			this.eventsListBox.Size = new System.Drawing.Size(296, 95);
			this.eventsListBox.TabIndex = 0;
			this.eventsListBox.SelectedIndexChanged += new System.EventHandler(this.eventsListBox_SelectedIndexChanged);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(80, 448);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 11;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelButton.Location = new System.Drawing.Point(176, 448);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 12;
			this.cancelButton.Text = "Cancel";
			// 
			// newEventButton
			// 
			this.newEventButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.newEventButton.Location = new System.Drawing.Point(45, 112);
			this.newEventButton.Name = "newEventButton";
			this.newEventButton.Size = new System.Drawing.Size(112, 24);
			this.newEventButton.TabIndex = 1;
			this.newEventButton.Text = "New Event";
			this.newEventButton.Click += new System.EventHandler(this.newEventButton_Click);
			// 
			// deleteEventButton
			// 
			this.deleteEventButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.deleteEventButton.Location = new System.Drawing.Point(173, 112);
			this.deleteEventButton.Name = "deleteEventButton";
			this.deleteEventButton.Size = new System.Drawing.Size(112, 24);
			this.deleteEventButton.TabIndex = 2;
			this.deleteEventButton.Text = "Delete Event";
			this.deleteEventButton.Click += new System.EventHandler(this.deleteEventButton_Click);
			// 
			// movableRadioButton
			// 
			this.movableRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.movableRadioButton.Location = new System.Drawing.Point(8, 296);
			this.movableRadioButton.Name = "movableRadioButton";
			this.movableRadioButton.Size = new System.Drawing.Size(16, 16);
			this.movableRadioButton.TabIndex = 7;
			this.movableRadioButton.CheckedChanged += new System.EventHandler(this.eventTypeRadioButton_CheckedChanged);
			// 
			// specialRadioButton
			// 
			this.specialRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.specialRadioButton.Location = new System.Drawing.Point(8, 384);
			this.specialRadioButton.Name = "specialRadioButton";
			this.specialRadioButton.Size = new System.Drawing.Size(16, 16);
			this.specialRadioButton.TabIndex = 9;
			this.specialRadioButton.CheckedChanged += new System.EventHandler(this.eventTypeRadioButton_CheckedChanged);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Location = new System.Drawing.Point(32, 168);
			this.descriptionTextBox.MaxLength = 50;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(280, 20);
			this.descriptionTextBox.TabIndex = 4;
			this.descriptionTextBox.Text = "event description";
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
			// 
			// weekdayLabel
			// 
			this.weekdayLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.weekdayLabel.Location = new System.Drawing.Point(104, 24);
			this.weekdayLabel.Name = "weekdayLabel";
			this.weekdayLabel.Size = new System.Drawing.Size(72, 16);
			this.weekdayLabel.TabIndex = 2;
			this.weekdayLabel.Text = "Weekday";
			// 
			// monthLlabel
			// 
			this.monthLlabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.monthLlabel.Location = new System.Drawing.Point(208, 24);
			this.monthLlabel.Name = "monthLlabel";
			this.monthLlabel.Size = new System.Drawing.Size(72, 16);
			this.monthLlabel.TabIndex = 4;
			this.monthLlabel.Text = "Month";
			// 
			// weekdayComboBox
			// 
			this.weekdayComboBox.AllowDrop = true;
			this.weekdayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.weekdayComboBox.ItemHeight = 13;
			this.weekdayComboBox.Location = new System.Drawing.Point(104, 48);
			this.weekdayComboBox.MaxDropDownItems = 12;
			this.weekdayComboBox.Name = "weekdayComboBox";
			this.weekdayComboBox.Size = new System.Drawing.Size(80, 21);
			this.weekdayComboBox.TabIndex = 3;
			this.weekdayComboBox.SelectedIndexChanged += new System.EventHandler(this.weekdayComboBox_SelectedIndexChanged);
			// 
			// monthComboBox
			// 
			this.monthComboBox.AllowDrop = true;
			this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.monthComboBox.ItemHeight = 13;
			this.monthComboBox.Location = new System.Drawing.Point(208, 48);
			this.monthComboBox.MaxDropDownItems = 12;
			this.monthComboBox.Name = "monthComboBox";
			this.monthComboBox.Size = new System.Drawing.Size(80, 21);
			this.monthComboBox.TabIndex = 5;
			this.monthComboBox.SelectedIndexChanged += new System.EventHandler(this.monthComboBox_SelectedIndexChanged);
			// 
			// weekLabel
			// 
			this.weekLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.weekLabel.Location = new System.Drawing.Point(8, 24);
			this.weekLabel.Name = "weekLabel";
			this.weekLabel.Size = new System.Drawing.Size(72, 16);
			this.weekLabel.TabIndex = 0;
			this.weekLabel.Text = "Week";
			// 
			// weekComboBox
			// 
			this.weekComboBox.AllowDrop = true;
			this.weekComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.weekComboBox.ItemHeight = 13;
			this.weekComboBox.Location = new System.Drawing.Point(8, 48);
			this.weekComboBox.MaxDropDownItems = 12;
			this.weekComboBox.Name = "weekComboBox";
			this.weekComboBox.Size = new System.Drawing.Size(80, 21);
			this.weekComboBox.TabIndex = 1;
			this.weekComboBox.SelectedIndexChanged += new System.EventHandler(this.weekComboBox_SelectedIndexChanged);
			// 
			// movableGroupBox
			// 
			this.movableGroupBox.Controls.Add(this.weekdayLabel);
			this.movableGroupBox.Controls.Add(this.monthLlabel);
			this.movableGroupBox.Controls.Add(this.weekdayComboBox);
			this.movableGroupBox.Controls.Add(this.monthComboBox);
			this.movableGroupBox.Controls.Add(this.weekLabel);
			this.movableGroupBox.Controls.Add(this.weekComboBox);
			this.movableGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.movableGroupBox.Location = new System.Drawing.Point(24, 296);
			this.movableGroupBox.Name = "movableGroupBox";
			this.movableGroupBox.Size = new System.Drawing.Size(296, 80);
			this.movableGroupBox.TabIndex = 8;
			this.movableGroupBox.TabStop = false;
			this.movableGroupBox.Text = "Movable";
			// 
			// specialComboBox
			// 
			this.specialComboBox.ItemHeight = 13;
			this.specialComboBox.Location = new System.Drawing.Point(8, 24);
			this.specialComboBox.Name = "specialComboBox";
			this.specialComboBox.Size = new System.Drawing.Size(144, 21);
			this.specialComboBox.TabIndex = 0;
			this.specialComboBox.SelectedIndexChanged += new System.EventHandler(this.specialComboBox_SelectedIndexChanged);
			// 
			// specialDaysUpDown
			// 
			this.specialDaysUpDown.Location = new System.Drawing.Point(224, 24);
			this.specialDaysUpDown.Maximum = new System.Decimal(new int[] {
																			  365,
																			  0,
																			  0,
																			  0});
			this.specialDaysUpDown.Minimum = new System.Decimal(new int[] {
																			  365,
																			  0,
																			  0,
																			  -2147483648});
			this.specialDaysUpDown.Name = "specialDaysUpDown";
			this.specialDaysUpDown.Size = new System.Drawing.Size(48, 20);
			this.specialDaysUpDown.TabIndex = 2;
			this.specialDaysUpDown.ValueChanged += new System.EventHandler(this.specialDaysUpDown_ValueChanged);
			// 
			// specialDaysLabel
			// 
			this.specialDaysLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.specialDaysLabel.Location = new System.Drawing.Point(168, 26);
			this.specialDaysLabel.Name = "specialDaysLabel";
			this.specialDaysLabel.Size = new System.Drawing.Size(48, 16);
			this.specialDaysLabel.TabIndex = 1;
			this.specialDaysLabel.Text = "+/- days";
			// 
			// specialGoupBox
			// 
			this.specialGoupBox.Controls.Add(this.specialComboBox);
			this.specialGoupBox.Controls.Add(this.specialDaysUpDown);
			this.specialGoupBox.Controls.Add(this.specialDaysLabel);
			this.specialGoupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.specialGoupBox.Location = new System.Drawing.Point(24, 384);
			this.specialGoupBox.Name = "specialGoupBox";
			this.specialGoupBox.Size = new System.Drawing.Size(296, 56);
			this.specialGoupBox.TabIndex = 10;
			this.specialGoupBox.TabStop = false;
			this.specialGoupBox.Text = "Special";
			// 
			// recurringComboBox
			// 
			this.recurringComboBox.ItemHeight = 13;
			this.recurringComboBox.Location = new System.Drawing.Point(120, 24);
			this.recurringComboBox.Name = "recurringComboBox";
			this.recurringComboBox.Size = new System.Drawing.Size(120, 21);
			this.recurringComboBox.TabIndex = 1;
			this.recurringComboBox.SelectedIndexChanged += new System.EventHandler(this.recurringComboBox_SelectedIndexChanged);
			// 
			// recurringDateTimePicker
			// 
			this.recurringDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.recurringDateTimePicker.Location = new System.Drawing.Point(8, 24);
			this.recurringDateTimePicker.Name = "recurringDateTimePicker";
			this.recurringDateTimePicker.Size = new System.Drawing.Size(96, 20);
			this.recurringDateTimePicker.TabIndex = 0;
			this.recurringDateTimePicker.Value = new System.DateTime(2005, 3, 11, 19, 52, 15, 920);
			this.recurringDateTimePicker.ValueChanged += new System.EventHandler(this.recurringDateTimePicker_ValueChanged);
			// 
			// recurringGroupBox
			// 
			this.recurringGroupBox.Controls.Add(this.recurringComboBox);
			this.recurringGroupBox.Controls.Add(this.recurringDateTimePicker);
			this.recurringGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.recurringGroupBox.Location = new System.Drawing.Point(24, 232);
			this.recurringGroupBox.Name = "recurringGroupBox";
			this.recurringGroupBox.Size = new System.Drawing.Size(296, 56);
			this.recurringGroupBox.TabIndex = 6;
			this.recurringGroupBox.TabStop = false;
			this.recurringGroupBox.Text = "Recurring";
			// 
			// recurringRadioButton
			// 
			this.recurringRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.recurringRadioButton.Location = new System.Drawing.Point(8, 232);
			this.recurringRadioButton.Name = "recurringRadioButton";
			this.recurringRadioButton.Size = new System.Drawing.Size(16, 16);
			this.recurringRadioButton.TabIndex = 5;
			this.recurringRadioButton.CheckedChanged += new System.EventHandler(this.eventTypeRadioButton_CheckedChanged);
			// 
			// descriptionGroupBox
			// 
			this.descriptionGroupBox.Controls.Add(this.pictureBoxHighlightColor);
			this.descriptionGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.descriptionGroupBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.descriptionGroupBox.Location = new System.Drawing.Point(24, 144);
			this.descriptionGroupBox.Name = "descriptionGroupBox";
			this.descriptionGroupBox.Size = new System.Drawing.Size(296, 80);
			this.descriptionGroupBox.TabIndex = 3;
			this.descriptionGroupBox.TabStop = false;
			this.descriptionGroupBox.Text = "Description";
			// 
			// pictureBoxHighlightColor
			// 
			this.pictureBoxHighlightColor.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBoxHighlightColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBoxHighlightColor.Location = new System.Drawing.Point(264, 56);
			this.pictureBoxHighlightColor.Name = "pictureBoxHighlightColor";
			this.pictureBoxHighlightColor.Size = new System.Drawing.Size(24, 16);
			this.pictureBoxHighlightColor.TabIndex = 14;
			this.pictureBoxHighlightColor.TabStop = false;
			// 
			// checkBoxHighlightDayForeColor
			// 
			this.checkBoxHighlightDayForeColor.Location = new System.Drawing.Point(32, 200);
			this.checkBoxHighlightDayForeColor.Name = "checkBoxHighlightColor";
			this.checkBoxHighlightDayForeColor.Size = new System.Drawing.Size(152, 16);
			this.checkBoxHighlightDayForeColor.TabIndex = 13;
			this.checkBoxHighlightDayForeColor.Text = "Use highlight color";
			this.checkBoxHighlightDayForeColor.CheckedChanged += new System.EventHandler(this.checkBoxHighlightDayForeColor_CheckedChanged);
			// 
			// EventsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(330, 480);
			this.ControlBox = false;
			this.Controls.Add(this.checkBoxHighlightDayForeColor);
			this.Controls.Add(this.deleteEventButton);
			this.Controls.Add(this.specialRadioButton);
			this.Controls.Add(this.movableRadioButton);
			this.Controls.Add(this.recurringRadioButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.recurringGroupBox);
			this.Controls.Add(this.movableGroupBox);
			this.Controls.Add(this.specialGoupBox);
			this.Controls.Add(this.eventsListBox);
			this.Controls.Add(this.newEventButton);
			this.Controls.Add(this.descriptionTextBox);
			this.Controls.Add(this.descriptionGroupBox);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EventsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Events";
			this.Load += new System.EventHandler(this.EventsForm_Load);
			this.movableGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.specialDaysUpDown)).EndInit();
			this.specialGoupBox.ResumeLayout(false);
			this.recurringGroupBox.ResumeLayout(false);
			this.descriptionGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

        // ---------------------------------------------------------------------
        private void eventsListBox_SelectedIndexChanged(object sender, 
            System.EventArgs e)
        {
            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev == null)
            {
                return;
            }

            this.suspendUpdates = true;

            // Recurring section
            this.descriptionTextBox.Text = ev.Description;

			// The DateTimePicker control's minimum date time is not the same as DateTime.MinValue
			DateTime eventDate = ev.Date();

			if (eventDate < this.recurringDateTimePicker.MinDate)
			{
				eventDate = this.recurringDateTimePicker.MinDate;
			}

            this.recurringDateTimePicker.Value = eventDate;

            this.recurringComboBox.SelectedIndex = 
                Convert.ToInt32(ev.Recurring, CultureInfo.InvariantCulture);

            // Movable section
            this.weekComboBox.SelectedIndex = 
                (ev.Week >= 1 && ev.Week <= Event.WeekNumbers().Length) 
                ? ev.Week - 1 : 0;

            this.weekdayComboBox.SelectedIndex = 
                Convert.ToInt32(ev.Weekday, CultureInfo.CurrentCulture);

            this.monthComboBox.SelectedIndex = 
				(ev.EventType == EventType.Movable && 
				ev.Recurring == RecurringEvent.Monthly)? 0 : ev.Month;

            // Special section
            this.specialComboBox.SelectedValue = ev.Special;            
            this.specialDaysUpDown.Value = ev.Days;

            // Activate dialog section based on event type
            switch (ev.EventType)
            {
                case EventType.Recurring:
                    this.recurringRadioButton.Checked = true;
                    break;

                case EventType.Movable:
                    this.movableRadioButton.Checked = true;
                    break;

                case EventType.Special:
                    this.specialRadioButton.Checked = true;
                    break;
            }

			// Highlight color check box
			this.checkBoxHighlightDayForeColor.Checked = ev.HighlightColor;

            this.suspendUpdates = false;
        }

        // ---------------------------------------------------------------------
        private void eventTypeRadioButton_CheckedChanged(object sender, 
            System.EventArgs e)
        {
            bool recurring = this.recurringRadioButton.Checked;
            bool movable = this.movableRadioButton.Checked;
            bool special = this.specialRadioButton.Checked;

            this.recurringGroupBox.Enabled = recurring;
            this.recurringDateTimePicker.Enabled = recurring;
            this.recurringComboBox.Enabled = recurring;

            this.movableGroupBox.Enabled = movable;
            this.weekComboBox.Enabled = movable;
            this.weekdayComboBox.Enabled = movable;
            this.monthComboBox.Enabled = movable;

            this.specialGoupBox.Enabled = special;
            this.specialComboBox.Enabled = special;
            this.specialDaysLabel.Enabled = special;
            this.specialDaysUpDown.Enabled = special;

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                if (recurring == true)
                {
                    ev.EventType = EventType.Recurring;
                }

                if (movable == true)
                {
                    ev.EventType = EventType.Movable;
                }

                if (special == true)
                {
                    ev.EventType = EventType.Special;
                }

                this.suspendUpdates = true;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
                this.suspendUpdates = false;
            }            
        }

        // ---------------------------------------------------------------------
        private void newEventButton_Click(object sender, System.EventArgs e)
        {
            Event ev = new Event();
            ev.Description = "New Event";
            this.eventsListBox.Items.Add(ev);

            this.eventsListBox.SelectedIndex = 
                this.eventsListBox.Items.Count - 1;

			this.descriptionTextBox.Focus();
			this.descriptionTextBox.SelectAll();
        }

        // ---------------------------------------------------------------------
        private void deleteEventButton_Click(object sender, System.EventArgs e)
        {
            int selectedIndex = this.eventsListBox.SelectedIndex;
            this.eventsListBox.Items.RemoveAt(selectedIndex);

            if (this.eventsListBox.Items.Count > 0)
            {
                this.eventsListBox.SelectedIndex = 
                    Math.Max(0, selectedIndex - 1);
            }

            else
            {
                newEventButton_Click(sender, e);
            }
        }

        // ---------------------------------------------------------------------
        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.events = new Event[this.eventsListBox.Items.Count];
        
            for (int i = 0 ; i < this.eventsListBox.Items.Count ; ++i)
            {
                this.events.SetValue(this.eventsListBox.Items[i], i);
            }            
        }

        // ---------------------------------------------------------------------
        public Event[] GetEvents()
        {
            return this.events;
        }

        // ---------------------------------------------------------------------
        private void descriptionTextBox_TextChanged(object sender, 
            System.EventArgs e)
        {
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Description = this.descriptionTextBox.Text;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }
        }

        // ---------------------------------------------------------------------
        private void recurringDateTimePicker_ValueChanged(object sender, 
            System.EventArgs e)
        {
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                DateTime date = recurringDateTimePicker.Value;
                ev.Day = date.Day;
                ev.Month = date.Month;
                ev.Year = date.Year;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }            
        }

        // ---------------------------------------------------------------------
        private void recurringComboBox_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Recurring = (RecurringEvent)this.recurringComboBox.SelectedValue; 
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }
        }

        // ---------------------------------------------------------------------
        private void weekComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Week = this.weekComboBox.SelectedIndex + 1;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }        
        }

        // ---------------------------------------------------------------------
        private void weekdayComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Weekday = (DayOfWeek)this.weekdayComboBox.SelectedIndex;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }                
        }

        // ---------------------------------------------------------------------
        private void monthComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {        
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
				if (this.monthComboBox.SelectedIndex == 0)
				{
					ev.Month = 1;
					ev.Recurring = RecurringEvent.Monthly;
				}

				else
				{
					ev.Month = this.monthComboBox.SelectedIndex;
					ev.Recurring = RecurringEvent.Annually;
				}

                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }                
        }

        // ---------------------------------------------------------------------
        private void specialComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {        
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Special = (SpecialEvent)this.specialComboBox.SelectedValue;
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }                
        }

        // ---------------------------------------------------------------------
        private void specialDaysUpDown_ValueChanged(object sender, System.EventArgs e)
        {        
            if (this.suspendUpdates == true)
            {
                return;
            }

            Event ev = this.eventsListBox.SelectedItem as Event;

            if (ev != null)
            {
                ev.Days = Convert.ToInt32(this.specialDaysUpDown.Value);
                this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
            }                
        }

		// ---------------------------------------------------------------------
		private void EventsForm_Load(object sender, System.EventArgs e)
        {
            this.Location = Calendar.PositionAdjacent(this);
        }

		// ---------------------------------------------------------------------
		private void checkBoxHighlightDayForeColor_CheckedChanged(object sender, System.EventArgs e)
		{
            Event ev = this.eventsListBox.SelectedItem as Event;

			if (ev != null)
			{
				ev.HighlightColor = this.checkBoxHighlightDayForeColor.Checked;
				this.eventsListBox.Items[this.eventsListBox.SelectedIndex] = ev;
			}
		}
    }
}
