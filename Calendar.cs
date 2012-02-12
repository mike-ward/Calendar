// Copyright (c) 2008 Blue Onion Software, All rights reserved

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BlueOnion
{
    [XmlDSigLic()]
    [LicenseProvider(typeof(XmlDSigLicProvider))]
    internal class Calendar : System.Windows.Forms.Form
    {
        private EventsCollection events = new EventsCollection();
        private Settings settings = new Settings();
        private Settings settingsRead = new Settings();
        private Hashtable currentEvents = null;

        private static readonly string ApplicationDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Blue Onion Software\\Calendar");

        private static readonly string ConfigFileName = "Calendar.config";
        private static readonly string EventsFileName = "Calendar.events";

        public static readonly string WebSite = "http://blueonionsoftware.com";
        public static readonly string EmailAddress = "mailto:support@blueonionsoftware.com";

        private SelectionRange lastRange = new SelectionRange();

        private readonly int hotKeyID = 20;
        private string configFile;
        private string eventsFile;
        private bool disposed;

        private MonthCalendarEx calendarControl;
        private System.Windows.Forms.MenuItem menuItemShowToday;
        private System.Windows.Forms.MenuItem menuItemCircleToday;
        private System.Windows.Forms.MenuItem menuItemWeekNumbers;
        private System.Windows.Forms.MenuItem menuItemFirstDayOfWeek;
        private System.Windows.Forms.MenuItem menuItemSunday;
        private System.Windows.Forms.MenuItem menuItemMonday;
        private System.Windows.Forms.MenuItem menuItemTuesday;
        private System.Windows.Forms.MenuItem menuItemWednesday;
        private System.Windows.Forms.MenuItem menuItemThursday;
        private System.Windows.Forms.MenuItem menuItemFriday;
        private System.Windows.Forms.MenuItem menuItemSaturday;
        private System.Windows.Forms.MenuItem menuItemTopMost;
        private System.Windows.Forms.MenuItem menuItemColors;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemEvents;
        private System.Windows.Forms.Timer welcomeTimer;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MenuItem menuItemShowInTray;
        private System.Windows.Forms.MenuItem menuItemQuickTips;
        private System.Windows.Forms.MenuItem menuItemGoToToday;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemBorder;
        private System.Windows.Forms.MenuItem menuItemBorderThick;
        private System.Windows.Forms.MenuItem menuItemBorderThin;
        private System.Windows.Forms.MenuItem menuItemBorderNone;
        private System.Windows.Forms.MenuItem menuItem4;
        private Timer timer;

        public enum BorderStyle
        {
            None = 0,
            Thick = 1,
            Thin = 2
        }

        private BorderStyle borderStyle;

        // ---------------------------------------------------------------------
        public Calendar(string[] args)
        {
            InitializeComponent();
            Initialize(args);
        }

        // ---------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            // Bug in IDE set date to current edit time whenever designer is used
            // Override here.
            calendarControl.TodayDate = DateTime.Now;

            ReadConfigFile();
            PositionCalendar();
            CalendarOptions();
            ReadEventsFile();
            CalendarDates(true);

            base.OnLoad(e);
        }

        // ---------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (this.disposed == true)
            {
                return;
            }

            disposed = true;

            if (disposing == true)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            NativeMethods.UnregisterHotKey(this.Handle, this.hotKeyID);
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        // ---------------------------------------------------------------------
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calendar));
            this.calendarControl = new BlueOnion.MonthCalendarEx();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemGoToToday = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemEvents = new System.Windows.Forms.MenuItem();
            this.menuItemColors = new System.Windows.Forms.MenuItem();
            this.menuItemQuickTips = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemShowToday = new System.Windows.Forms.MenuItem();
            this.menuItemCircleToday = new System.Windows.Forms.MenuItem();
            this.menuItemWeekNumbers = new System.Windows.Forms.MenuItem();
            this.menuItemTopMost = new System.Windows.Forms.MenuItem();
            this.menuItemShowInTray = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemBorder = new System.Windows.Forms.MenuItem();
            this.menuItemBorderThick = new System.Windows.Forms.MenuItem();
            this.menuItemBorderThin = new System.Windows.Forms.MenuItem();
            this.menuItemBorderNone = new System.Windows.Forms.MenuItem();
            this.menuItemFirstDayOfWeek = new System.Windows.Forms.MenuItem();
            this.menuItemSunday = new System.Windows.Forms.MenuItem();
            this.menuItemMonday = new System.Windows.Forms.MenuItem();
            this.menuItemTuesday = new System.Windows.Forms.MenuItem();
            this.menuItemWednesday = new System.Windows.Forms.MenuItem();
            this.menuItemThursday = new System.Windows.Forms.MenuItem();
            this.menuItemFriday = new System.Windows.Forms.MenuItem();
            this.menuItemSaturday = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.welcomeTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // calendarControl
            // 
            this.calendarControl.BoldedDates = null;
            this.calendarControl.ColoredDates = null;
            this.calendarControl.ContextMenu = this.contextMenu;
            this.calendarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarControl.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.calendarControl.Gridlines = false;
            this.calendarControl.GridlinesColor = System.Drawing.SystemColors.GrayText;
            this.calendarControl.HighlightDayTextColor = System.Drawing.Color.DarkRed;
            this.calendarControl.Location = new System.Drawing.Point(0, 0);
            this.calendarControl.MaxSelectionCount = 1;
            this.calendarControl.Name = "calendarControl";
            this.calendarControl.ShowToday = true;
            this.calendarControl.ShowTodayCircle = false;
            this.calendarControl.ShowWeekNumbers = false;
            this.calendarControl.Size = new System.Drawing.Size(181, 158);
            this.calendarControl.TabIndex = 0;
            this.calendarControl.TitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.calendarControl.TitleForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calendarControl.TrailingForeColor = System.Drawing.SystemColors.GrayText;
            this.calendarControl.WeekdayBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calendarControl.WeekdayBarColor = System.Drawing.SystemColors.WindowText;
            this.calendarControl.WeekdayForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.calendarControl.WeeknumberBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.calendarControl.WeeknumberForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.calendarControl.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.calendarControl_HelpRequested);
            this.calendarControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.calendarControl_MouseMove);
            this.calendarControl.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calendarControl_DateChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGoToToday,
            this.menuItem3,
            this.menuItemEvents,
            this.menuItemColors,
            this.menuItemQuickTips,
            this.menuItemHelp,
            this.menuItemAbout,
            this.menuItem1,
            this.menuItemShowToday,
            this.menuItemCircleToday,
            this.menuItemWeekNumbers,
            this.menuItemTopMost,
            this.menuItemShowInTray,
            this.menuItem4,
            this.menuItemBorder,
            this.menuItemFirstDayOfWeek,
            this.menuItem2,
            this.menuItemExit});
            this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
            // 
            // menuItemGoToToday
            // 
            this.menuItemGoToToday.Index = 0;
            this.menuItemGoToToday.Text = "Go to Today";
            this.menuItemGoToToday.Click += new System.EventHandler(this.menuItemGoToToday_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // menuItemEvents
            // 
            this.menuItemEvents.Index = 2;
            this.menuItemEvents.Text = "Events...";
            this.menuItemEvents.Click += new System.EventHandler(this.menuItemDates_Click);
            // 
            // menuItemColors
            // 
            this.menuItemColors.Index = 3;
            this.menuItemColors.Text = "Appearance...";
            this.menuItemColors.Click += new System.EventHandler(this.menuItemColors_Click);
            // 
            // menuItemQuickTips
            // 
            this.menuItemQuickTips.Index = 4;
            this.menuItemQuickTips.Text = "Quick Tips...";
            this.menuItemQuickTips.Click += new System.EventHandler(this.menuItemQuickTips_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 5;
            this.menuItemHelp.Text = "Help...";
            this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 6;
            this.menuItemAbout.Text = "About...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 7;
            this.menuItem1.Text = "-";
            // 
            // menuItemShowToday
            // 
            this.menuItemShowToday.Index = 8;
            this.menuItemShowToday.Text = "Show Today";
            this.menuItemShowToday.Click += new System.EventHandler(this.menuItemShowToday_Click);
            // 
            // menuItemCircleToday
            // 
            this.menuItemCircleToday.Index = 9;
            this.menuItemCircleToday.Text = "Circle Today";
            this.menuItemCircleToday.Click += new System.EventHandler(this.menuItemCircleToday_Click);
            // 
            // menuItemWeekNumbers
            // 
            this.menuItemWeekNumbers.Index = 10;
            this.menuItemWeekNumbers.Text = "Week Numbers";
            this.menuItemWeekNumbers.Click += new System.EventHandler(this.menuItemWeekNumbers_Click);
            // 
            // menuItemTopMost
            // 
            this.menuItemTopMost.Index = 11;
            this.menuItemTopMost.Text = "Top Most";
            this.menuItemTopMost.Click += new System.EventHandler(this.menuItemTopMost_Click);
            // 
            // menuItemShowInTray
            // 
            this.menuItemShowInTray.Index = 12;
            this.menuItemShowInTray.Text = "Show in Tray";
            this.menuItemShowInTray.Click += new System.EventHandler(this.menuItemShowInTray_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 13;
            this.menuItem4.Text = "-";
            // 
            // menuItemBorder
            // 
            this.menuItemBorder.Index = 14;
            this.menuItemBorder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemBorderThick,
            this.menuItemBorderThin,
            this.menuItemBorderNone});
            this.menuItemBorder.Text = "Border";
            // 
            // menuItemBorderThick
            // 
            this.menuItemBorderThick.Index = 0;
            this.menuItemBorderThick.Text = "Thick";
            this.menuItemBorderThick.Click += new System.EventHandler(this.menuItemBorderThick_Click);
            // 
            // menuItemBorderThin
            // 
            this.menuItemBorderThin.Index = 1;
            this.menuItemBorderThin.Text = "Thin";
            this.menuItemBorderThin.Click += new System.EventHandler(this.menuItemBorderThin_Click);
            // 
            // menuItemBorderNone
            // 
            this.menuItemBorderNone.Index = 2;
            this.menuItemBorderNone.Text = "None";
            this.menuItemBorderNone.Click += new System.EventHandler(this.menuItemBorderNone_Click);
            // 
            // menuItemFirstDayOfWeek
            // 
            this.menuItemFirstDayOfWeek.Index = 15;
            this.menuItemFirstDayOfWeek.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSunday,
            this.menuItemMonday,
            this.menuItemTuesday,
            this.menuItemWednesday,
            this.menuItemThursday,
            this.menuItemFriday,
            this.menuItemSaturday});
            this.menuItemFirstDayOfWeek.Text = "First Day of Week";
            // 
            // menuItemSunday
            // 
            this.menuItemSunday.Index = 0;
            this.menuItemSunday.Text = "Sunday";
            this.menuItemSunday.Click += new System.EventHandler(this.menuItemSunday_Click);
            // 
            // menuItemMonday
            // 
            this.menuItemMonday.Index = 1;
            this.menuItemMonday.Text = "Monday";
            this.menuItemMonday.Click += new System.EventHandler(this.menuItemMonday_Click);
            // 
            // menuItemTuesday
            // 
            this.menuItemTuesday.Index = 2;
            this.menuItemTuesday.Text = "Tuesday";
            this.menuItemTuesday.Click += new System.EventHandler(this.menuItemTuesday_Click);
            // 
            // menuItemWednesday
            // 
            this.menuItemWednesday.Index = 3;
            this.menuItemWednesday.Text = "Wednesday";
            this.menuItemWednesday.Click += new System.EventHandler(this.menuItemWednesday_Click);
            // 
            // menuItemThursday
            // 
            this.menuItemThursday.Index = 4;
            this.menuItemThursday.Text = "Thursday";
            this.menuItemThursday.Click += new System.EventHandler(this.menuItemThursday_Click);
            // 
            // menuItemFriday
            // 
            this.menuItemFriday.Index = 5;
            this.menuItemFriday.Text = "Friday";
            this.menuItemFriday.Click += new System.EventHandler(this.menuItemFriday_Click);
            // 
            // menuItemSaturday
            // 
            this.menuItemSaturday.Index = 6;
            this.menuItemSaturday.Text = "Saturday";
            this.menuItemSaturday.Click += new System.EventHandler(this.menuItemSaturday_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 16;
            this.menuItem2.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 17;
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // welcomeTimer
            // 
            this.welcomeTimer.Enabled = true;
            this.welcomeTimer.Interval = 1000;
            this.welcomeTimer.Tick += new System.EventHandler(this.welcomeTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Calendar";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 60000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Calendar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(181, 158);
            this.Controls.Add(this.calendarControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Calendar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Calendar";
            this.SizeChanged += new System.EventHandler(this.Calendar_SizeChanged);
            this.Closed += new System.EventHandler(this.Calendar_Closed);
            this.LocationChanged += new System.EventHandler(this.Calendar_LocationChanged);
            this.ResumeLayout(false);

        }
        #endregion

        // ---------------------------------------------------------------------
        private void Welcome()
        {
            WelcomeForm welcome = new WelcomeForm();
            welcome.ShowDialog(this);
        }

        // ---------------------------------------------------------------------
        private void ReadConfigFile()
        {
            if (File.Exists(this.configFile) == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(this.configFile))
                    {
                        this.settings = Settings.Deserialize(sr);
                        this.settingsRead = this.settings.Clone() as Settings;
                    }
                }

                catch (System.IO.FileNotFoundException e)
                {
                    Log.Error(e.ToString());
                }
            }
        }

        // ---------------------------------------------------------------------
        private void PositionCalendar()
        {
            this.DesktopLocation = settings.Position.Location;
            Size minimumSize = SystemInformation.MinimizedWindowSize;

            if (settings.Position.Size.IsEmpty == true ||
                settings.Position.Size.Width < minimumSize.Width ||
                settings.Position.Size.Height < minimumSize.Height)
            {
                Size size = this.calendarControl.SingleMonthSize;

                // Pad the bottom with some whitespace to balance the 
                // appearance
                size.Height += this.calendarControl.TodayHeight / 2;
                this.ClientSize = size;
            }

            else
            {
                this.ClientSize = settings.Position.Size;
            }

            this.TopMost = settings.Topmost;
            this.ShowInTray(this.settings.TrayIcon);
            this.Border = settings.Border;
        }

        // ---------------------------------------------------------------------
        private void ReadEventsFile()
        {
            try
            {
                if (File.Exists(eventsFile))
                {
                    using (var sr = new StreamReader(this.eventsFile))
                    {
                        events.Deserialize(sr);
                    }

                }

                else
                {
                    var exe = System.Reflection.Assembly.GetExecutingAssembly();
                    var holidays = exe.GetManifestResourceStream("BlueOnion.Calendar.events");

                    using (var stream = new StreamReader(holidays))
                    {
                        events.Deserialize(stream);
                    }
                }
            }

            catch (System.Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        // ---------------------------------------------------------------------
        public void WriteEventsFile()
        {
            try
            {
                string path = Path.GetDirectoryName(this.eventsFile);

                if (path.Length != 0 && Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter sw = new StreamWriter(this.eventsFile))
                {
                    events.Serialize(sw);
                }
            }

            catch (System.Exception e)
            {
                Log.Error(e.ToString());
                MessageBox.Show(this, e.Message);
            }
        }

        // ---------------------------------------------------------------------
        private void CalendarOptions()
        {
            calendarControl.ShowWeekNumbers = settings.ShowWeekNumbers;
            calendarControl.ShowToday = settings.ShowToday;
            calendarControl.ShowTodayCircle = settings.ShowTodayCircle;
            calendarControl.FirstDayOfWeek = settings.FirstDay;

            calendarControl.ForeColor = settings.ColorFore;
            calendarControl.BackColor = settings.ColorBack;
            calendarControl.TitleForeColor = settings.ColorTitleFore;
            calendarControl.TitleBackColor = settings.ColorTitleBack;
            calendarControl.TrailingForeColor = settings.ColorTrailingFore;
            calendarControl.HighlightDayTextColor = settings.ColorHighlightDayFore;
            calendarControl.GridlinesColor = settings.ColorGridlines;
            calendarControl.Font = settings.Font;
            calendarControl.Gridlines = settings.Gridlines;
            calendarControl.WeekdayForeColor = settings.ColorWeekdayFore;
            calendarControl.WeekdayBackColor = settings.ColorWeekdayBack;
            calendarControl.WeeknumberForeColor = settings.ColorWeeknumberFore;
            calendarControl.WeeknumberBackColor = settings.ColorWeeknumberBack;
            calendarControl.WeekdayBarColor = settings.ColorWeekdayBar;

            this.Opacity = settings.Opacity;
            this.BackColor = settings.ColorBack;

            if (settings.StartMonthJanuary == true)
            {
                DateTime january = new DateTime(DateTime.Today.Year, 1, 1);
                calendarControl.GoToDate(january);
            }

            else if (settings.StartMonthPrevious != 0)
            {
                DateTime previous =
                    DateTime.Today.AddMonths(-settings.StartMonthPrevious);

                calendarControl.GoToDate(previous);
            }
        }

        // ---------------------------------------------------------------------
        private void CalendarDates(bool rebuild)
        {
            SelectionRange range = this.calendarControl.GetDisplayRange();
            SelectionRange rng = new SelectionRange(range.Start, range.End);

            range.Start = new DateTime(range.Start.Year, 1, 1);
            range.End = new DateTime(range.End.Year, 12, 31);

            if (lastRange.Start != range.Start || lastRange.End != range.End ||
                rebuild == true)
            {
                this.currentEvents =
                    this.events.GetDateDescriptions(range.Start, range.End);

                if (this.currentEvents != null)
                {
                    this.calendarControl.BoldedDates =
                        new DateTime[this.currentEvents.Keys.Count];

                    this.currentEvents.Keys.CopyTo
                        (this.calendarControl.BoldedDates, 0);

                    this.calendarControl.ColoredDates =
                        this.events.GetColoredDates(range.Start, range.End);

                    this.calendarControl.UpdateBoldedDates();
                    this.lastRange = range;
                }
            }

            this.Invalidate(true);
        }

        // ---------------------------------------------------------------------
        public Calendar.BorderStyle Border
        {
            get
            {
                return this.borderStyle;
            }

            set
            {
                const string name = "Calendar";
                this.borderStyle = value;

                switch (value)
                {
                    case BorderStyle.None:
                        if (this.FormBorderStyle != FormBorderStyle.None)
                            this.FormBorderStyle = FormBorderStyle.None;

                        break;

                    case BorderStyle.Thick:
                    default:
                        if (this.FormBorderStyle != FormBorderStyle.Sizable)
                            this.FormBorderStyle = FormBorderStyle.Sizable;

                        if (this.ControlBox != true)
                            this.ControlBox = true;

                        if (this.Text != name)
                            this.Text = name;

                        break;

                    case BorderStyle.Thin:
                        if (this.FormBorderStyle != FormBorderStyle.FixedSingle)
                            this.FormBorderStyle = FormBorderStyle.FixedSingle;

                        if (this.ControlBox != false)
                            this.ControlBox = false;

                        if (this.Text != string.Empty)
                            this.Text = string.Empty;

                        break;
                }
            }
        }

        // ---------------------------------------------------------------------
        private void Calendar_Closed(object sender, System.EventArgs e)
        {
            WriteConfigFile();
        }

        // ---------------------------------------------------------------------
        private void WriteConfigFile()
        {
            try
            {
                string path = Path.GetDirectoryName(this.configFile);

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                if (this.settings != this.settingsRead)
                {
                    using (StreamWriter sw = new StreamWriter(this.configFile))
                    {
                        settings.Serialize(sw);
                    }
                }
            }

            catch (System.Exception e)
            {
                Log.Error(e.ToString());
                MessageBox.Show(this, e.Message);
            }
        }

        // ---------------------------------------------------------------------
        private void timer_Tick(object sender, System.EventArgs e)
        {
            // Check if we have crossed midnight into a new day. If so, update
            // the today member of the calendar
            if (this.calendarControl.TodayDate.DayOfYear != DateTime.Now.DayOfYear)
            {
                this.calendarControl.TodayDate = DateTime.Now;
                this.calendarControl.SetDate(DateTime.Now);
            }

            SetWorkingSetSize();
        }

        // ---------------------------------------------------------------------
        private void contextMenu_Popup(object sender, System.EventArgs e)
        {
            this.menuItemShowToday.Checked = this.calendarControl.ShowToday;
            this.menuItemCircleToday.Checked = this.calendarControl.ShowTodayCircle;
            this.menuItemWeekNumbers.Checked = this.calendarControl.ShowWeekNumbers;
            this.menuItemTopMost.Checked = this.TopMost;
            this.menuItemShowInTray.Checked = this.notifyIcon.Visible;
            this.menuItemBorderThick.Checked = this.borderStyle == BorderStyle.Thick;
            this.menuItemBorderThin.Checked = this.borderStyle == BorderStyle.Thin;
            this.menuItemBorderNone.Checked = this.borderStyle == BorderStyle.None;

            DayOfWeek day = this.calendarControl.FirstDayOfWeek;
            this.menuItemSunday.Checked = (day == DayOfWeek.Sunday);
            this.menuItemMonday.Checked = (day == DayOfWeek.Monday);
            this.menuItemTuesday.Checked = (day == DayOfWeek.Tuesday);
            this.menuItemWednesday.Checked = (day == DayOfWeek.Wednesday);
            this.menuItemThursday.Checked = (day == DayOfWeek.Thursday);
            this.menuItemFriday.Checked = (day == DayOfWeek.Friday);
            this.menuItemSaturday.Checked = (day == DayOfWeek.Saturday);
        }

        // ---------------------------------------------------------------------
        private void menuItemTopMost_Click(object sender, System.EventArgs e)
        {
            this.TopMost = !this.TopMost;
            settings.Topmost = this.TopMost;
        }

        // ---------------------------------------------------------------------
        private void menuItemWeekNumbers_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.ShowWeekNumbers = !this.calendarControl.ShowWeekNumbers;
            settings.ShowWeekNumbers = this.calendarControl.ShowWeekNumbers;
        }

        // ---------------------------------------------------------------------
        private void menuItemShowToday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.ShowToday = !this.calendarControl.ShowToday;
            settings.ShowToday = this.calendarControl.ShowToday;
        }

        // ---------------------------------------------------------------------
        private void menuItemCircleToday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.ShowTodayCircle = !this.calendarControl.ShowTodayCircle;
            settings.ShowTodayCircle = this.calendarControl.ShowTodayCircle;
        }

        // ---------------------------------------------------------------------
        private void menuItemSunday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Sunday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemMonday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Monday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemTuesday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Tuesday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemWednesday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Wednesday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemThursday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Thursday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemFriday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Friday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemSaturday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.FirstDayOfWeek = DayOfWeek.Saturday;
            settings.FirstDay = this.calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void Calendar_LocationChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Rectangle position = settings.Position;
                position.Location = this.Location;
                settings.Position = position;
            }
        }

        // ---------------------------------------------------------------------
        private void Calendar_SizeChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (this.notifyIcon.Visible == true)
                {
                    Hide();
                }
            }

            else if (this.WindowState == FormWindowState.Normal)
            {
                Rectangle position = settings.Position;
                position.Size = this.ClientSize;
                settings.Position = position;
                CalendarDates(false);
            }

            else if (this.WindowState == FormWindowState.Maximized)
            {
                CalendarDates(false);
            }
        }

        // ---------------------------------------------------------------------
        private void menuItemColors_Click(object sender, System.EventArgs e)
        {
            using (Colors colors = new Colors(calendarControl, settings, this))
            {
                colors.ShowDialog(this);

                if (settings.Border != this.borderStyle)
                {
                    this.Border = settings.Border;
                }
            }
        }

        // ---------------------------------------------------------------------
        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        static Point MousePoint = new Point();
        // ---------------------------------------------------------------------
        private void calendarControl_MouseMove(object sender, MouseEventArgs e)
        {
            MonthCalendarEx.HitTestInfoEx hti = this.calendarControl.HitTest
                    (this.calendarControl.PointToClient(MousePosition));

            if (hti.Time != DateTime.MinValue)
            {
                if (this.currentEvents != null)
                {
                    string events = this.currentEvents[hti.Time] as string;

                    if (events != null)
                    {
                        string tooltipEvents = this.toolTip.GetToolTip(this.calendarControl);

                        if (tooltipEvents != events || MousePosition != MousePoint) 
                        {
                            this.toolTip.UseFading = false;
                            this.toolTip.UseAnimation = false;
                            this.toolTip.ShowAlways = true;
                            this.toolTip.AutomaticDelay = 1;
                            this.toolTip.AutoPopDelay = Int32.MaxValue;
                            this.toolTip.Active = true;
                            this.toolTip.SetToolTip(this.calendarControl, events);
                        }
                    }

                    else
                    {
                        this.toolTip.Active = false;
                        this.toolTip.SetToolTip(this.calendarControl, string.Empty);
                    }
                }
            }

            else
            {
                this.toolTip.Active = false;
                this.toolTip.SetToolTip(this.calendarControl, string.Empty);
            }

            MousePoint = MousePosition;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        // ---------------------------------------------------------------------
        private void calendarControl_DateChanged(object sender, DateRangeEventArgs e)
        {
            CalendarDates(false);
        }

        // ---------------------------------------------------------------------
        private void menuItemDates_Click(object sender, System.EventArgs e)
        {
            using (EventsForm eventsForm = new EventsForm(this.events, this.settings.ColorHighlightDayFore))
            {
                if (eventsForm.ShowDialog(this) == DialogResult.OK)
                {
                    Event[] allEvents = eventsForm.GetEvents();

                    if (allEvents != null)
                    {
                        this.events.SetEvents(allEvents);
                        this.WriteEventsFile();
                        CalendarDates(true);
                    }
                }
            }
        }

        // ---------------------------------------------------------------------
        private void welcomeTimer_Tick(object sender, System.EventArgs e)
        {
            this.welcomeTimer.Stop();

            if (File.Exists(this.configFile) == false)
            {
                Welcome();
            }

            SetWorkingSetSize();
        }

        // ---------------------------------------------------------------------
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x312;

            if (m.Msg == WM_HOTKEY)
            {
                Show();
                Activate();
                this.WindowState = FormWindowState.Normal;
                return;
            }

            base.WndProc(ref m);
        }

        // ---------------------------------------------------------------------
        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            About aboutDlg = new About();

            if (aboutDlg.ShowDialog(this) == DialogResult.Retry)
            {
                System.Diagnostics.Process.Start(Calendar.WebSite);
            }
        }

        // ---------------------------------------------------------------------
        private void RegisterHotkey()
        {
            NativeMethods.UnregisterHotKey(this.Handle, this.hotKeyID);

            if (NativeMethods.RegisterHotKey(this.Handle, this.hotKeyID,
                NativeMethods.FSModifiers.MOD_SHIFT, Keys.F11) == false)
            {
                int lastError = Marshal.GetLastWin32Error();

                string error = lastError.ToString("x",
                    CultureInfo.InvariantCulture);

                Log.Warning("RegisterHotKey failed (0x" + error + ")");
            }
        }

        // ---------------------------------------------------------------------
        public static Point PositionAdjacent(Form form)
        {
            Rectangle parent = form.Owner.DesktopBounds;
            Rectangle test = new Rectangle(parent.Location, form.Size);
            Rectangle desktop = SystemInformation.WorkingArea;

            test.Offset(parent.Width, 0);

            if (test.Right > desktop.Right)
            {
                test.Offset(-parent.Width, 0);
                test.Offset(-form.Width, 0);
            }

            int bottom = test.Top + form.Height;

            if (bottom > desktop.Bottom)
            {
                test.Offset(0, desktop.Bottom - bottom);
            }

            if (desktop.Contains(test) == false)
            {
                if (test.X < desktop.X)
                {
                    test.X = desktop.X;
                }

                if (test.Right > desktop.Right)
                {
                    test.X = desktop.Right - form.Width;
                }

                if (test.Bottom < desktop.Top)
                {
                    test.X = desktop.Top;
                }

                if (test.Bottom > desktop.Bottom)
                {
                    test.Y = desktop.Bottom - form.Height;
                }
            }

            return test.Location;
        }

        // ---------------------------------------------------------------------
        private void Initialize(string[] args)
        {
            this.configFile = (args.Length > 0 && args[0] != null)
                ? Path.Combine(args[0], ConfigFileName)
                : Path.Combine(ApplicationDataPath, ConfigFileName);

            this.eventsFile = (args.Length > 0 && args[0] != null)
                ? Path.Combine(args[0], EventsFileName)
                : Path.Combine(ApplicationDataPath, EventsFileName);
        }

        // ---------------------------------------------------------------------
        private void menuItemShowInTray_Click(object sender, System.EventArgs e)
        {
            this.settings.TrayIcon = !this.settings.TrayIcon;
            this.ShowInTray(this.settings.TrayIcon);
        }

        // ---------------------------------------------------------------------
        private void notifyIcon_Click(object sender, System.EventArgs e)
        {
            Show();
            Activate();
            this.WindowState = FormWindowState.Normal;
        }

        // ---------------------------------------------------------------------
        private void menuItemQuickTips_Click(object sender, System.EventArgs e)
        {
            Welcome();
        }

        // ---------------------------------------------------------------------
        private void menuItemHelp_Click(object sender, System.EventArgs e)
        {
            InvokeHelp();
        }

        // ---------------------------------------------------------------------
        private void calendarControl_HelpRequested(object sender,
            System.Windows.Forms.HelpEventArgs hlpevent)
        {
            InvokeHelp();
        }

        // ---------------------------------------------------------------------
        private void InvokeHelp()
        {
            string helpFile = Application.StartupPath + @"\Calendar.mht";

            if (File.Exists(helpFile) == true)
            {
                System.Diagnostics.Process.Start(helpFile);
            }

            else
            {
                MessageBox.Show(this, "Cannot find " + helpFile, this.Text);
            }
        }

        // ---------------------------------------------------------------------
        private void ShowInTray(bool show)
        {
            this.settings.TrayIcon = show;
            this.notifyIcon.Visible = show;
            this.ShowInTaskbar = !show;

            // Tool tips and hot keys get bonked when changing taskbar state
            this.toolTip.Dispose();
            this.toolTip = new ToolTip(this.components);
            this.toolTip.ShowAlways = true;
            RegisterHotkey();
        }

        // ---------------------------------------------------------------------
        private void menuItemGoToToday_Click(object sender, System.EventArgs e)
        {
            this.calendarControl.GoToToday();
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderThick_Click(object sender, System.EventArgs e)
        {
            this.Border = BorderStyle.Thick;
            this.settings.Border = BorderStyle.Thick;
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderThin_Click(object sender, System.EventArgs e)
        {
            this.Border = BorderStyle.Thin;
            this.settings.Border = BorderStyle.Thin;
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderNone_Click(object sender, System.EventArgs e)
        {
            this.Border = BorderStyle.None;
            this.settings.Border = BorderStyle.None;
        }

        static void SetWorkingSetSize()
        {
            var size = new UIntPtr(UInt32.MaxValue);
            NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, size, size);
        }
    }

    // -------------------------------------------------------------------------
    public static class NativeMethods
    {
        [Flags]
        internal enum FSModifiers
        {
            MOD_ALT = 0x0001,
            MOD_CONTROL = 0x0002,
            MOD_SHIFT = 0x0004,
            MOD_WIN = 0x0008
        };

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr windowHandle, int id,
            FSModifiers fsModifiers, Keys virtualKey);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr windowHandle, int id);

        [DllImport("gdi32.dll")]
        internal static extern bool PatBlt(IntPtr hdc,
            int nXLeft, int nYLeft, int nWidth, int nHeight, uint dwRop);

        [DllImport("kernel32.dll")]
        [CLSCompliant(false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessWorkingSetSize(
            IntPtr hProcess,
            UIntPtr dwMinimumWorkingSetSize,
            UIntPtr dwMaximumWorkingSetSize);
    }
}
