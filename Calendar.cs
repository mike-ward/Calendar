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
    [XmlDSigLic]
    [LicenseProvider(typeof (XmlDSigLicProvider))]
    internal class Calendar : Form
    {
        private readonly EventsCollection events = new EventsCollection();
        private Settings settings = new Settings();
        private Settings settingsRead = new Settings();
        private Hashtable currentEvents;

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
        private MenuItem menuItemShowToday;
        private MenuItem menuItemCircleToday;
        private MenuItem menuItemWeekNumbers;
        private MenuItem menuItemFirstDayOfWeek;
        private MenuItem menuItemSunday;
        private MenuItem menuItemMonday;
        private MenuItem menuItemTuesday;
        private MenuItem menuItemWednesday;
        private MenuItem menuItemThursday;
        private MenuItem menuItemFriday;
        private MenuItem menuItemSaturday;
        private MenuItem menuItemTopMost;
        private MenuItem menuItemColors;
        private MenuItem menuItemAbout;
        private ContextMenu contextMenu;
        private IContainer components;
        private MenuItem menuItemExit;
        private MenuItem menuItemEvents;
        private Timer welcomeTimer;
        private ToolTip toolTip;
        private NotifyIcon notifyIcon;
        private MenuItem menuItemShowInTray;
        private MenuItem menuItemQuickTips;
        private MenuItem menuItemGoToToday;
        private MenuItem menuItemHelp;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItemBorder;
        private MenuItem menuItemBorderThick;
        private MenuItem menuItemBorderThin;
        private MenuItem menuItemBorderNone;
        private MenuItem menuItem4;
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
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            NativeMethods.UnregisterHotKey(Handle, hotKeyID);
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof (Calendar));
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
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
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
                this.menuItemExit
            });
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
            this.menuItemBorder.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this.menuItemBorderThick,
                this.menuItemBorderThin,
                this.menuItemBorderNone
            });
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
            this.menuItemFirstDayOfWeek.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                this.menuItemSunday,
                this.menuItemMonday,
                this.menuItemTuesday,
                this.menuItemWednesday,
                this.menuItemThursday,
                this.menuItemFriday,
                this.menuItemSaturday
            });
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
            this.notifyIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("notifyIcon.Icon")));
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
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
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
            var welcome = new WelcomeForm();
            welcome.ShowDialog(this);
        }

        // ---------------------------------------------------------------------
        private void ReadConfigFile()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    using (var sr = new StreamReader(configFile))
                    {
                        settings = Settings.Deserialize(sr);
                        settingsRead = settings.Clone() as Settings;
                    }
                }
                catch (FileNotFoundException e)
                {
                    Log.Error(e.ToString());
                }
            }
        }

        // ---------------------------------------------------------------------
        private void PositionCalendar()
        {
            DesktopLocation = settings.Position.Location;
            var minimumSize = SystemInformation.MinimizedWindowSize;

            if (settings.Position.Size.IsEmpty ||
                settings.Position.Size.Width < minimumSize.Width ||
                settings.Position.Size.Height < minimumSize.Height)
            {
                var size = calendarControl.SingleMonthSize;

                // Pad the bottom with some whitespace to balance the
                // appearance
                size.Height += calendarControl.TodayHeight/2;
                ClientSize = size;
            }
            else
            {
                ClientSize = settings.Position.Size;
            }

            TopMost = settings.Topmost;
            ShowInTray(settings.TrayIcon);
            Border = settings.Border;
        }

        // ---------------------------------------------------------------------
        private void ReadEventsFile()
        {
            try
            {
                if (File.Exists(eventsFile))
                {
                    using (var sr = new StreamReader(eventsFile))
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
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        // ---------------------------------------------------------------------
        public void WriteEventsFile()
        {
            try
            {
                var path = Path.GetDirectoryName(eventsFile);

                if (path.Length != 0 && Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                using (var sw = new StreamWriter(eventsFile))
                {
                    events.Serialize(sw);
                }
            }
            catch (Exception e)
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

            Opacity = settings.Opacity;
            BackColor = settings.ColorBack;

            if (settings.StartMonthJanuary)
            {
                var january = new DateTime(DateTime.Today.Year, 1, 1);
                calendarControl.GoToDate(january);
            }
            else if (settings.StartMonthPrevious != 0)
            {
                var previous =
                    DateTime.Today.AddMonths(-settings.StartMonthPrevious);

                calendarControl.GoToDate(previous);
            }
        }

        // ---------------------------------------------------------------------
        private void CalendarDates(bool rebuild)
        {
            var range = calendarControl.GetDisplayRange();
            var rng = new SelectionRange(range.Start, range.End);

            range.Start = new DateTime(range.Start.Year, 1, 1);
            range.End = new DateTime(range.End.Year, 12, 31);

            if (lastRange.Start != range.Start || lastRange.End != range.End ||
                rebuild)
            {
                currentEvents =
                    events.GetDateDescriptions(range.Start, range.End);

                if (currentEvents != null)
                {
                    calendarControl.BoldedDates =
                        new DateTime[currentEvents.Keys.Count];

                    currentEvents.Keys.CopyTo
                        (calendarControl.BoldedDates, 0);

                    calendarControl.ColoredDates =
                        events.GetColoredDates(range.Start, range.End);

                    calendarControl.UpdateBoldedDates();
                    lastRange = range;
                }
            }

            Invalidate(true);
        }

        // ---------------------------------------------------------------------
        public BorderStyle Border
        {
            get { return borderStyle; }

            set
            {
                const string name = "Calendar";
                borderStyle = value;

                switch (value)
                {
                    case BorderStyle.None:
                        if (FormBorderStyle != FormBorderStyle.None)
                            FormBorderStyle = FormBorderStyle.None;

                        break;

                    case BorderStyle.Thick:
                    default:
                        if (FormBorderStyle != FormBorderStyle.Sizable)
                            FormBorderStyle = FormBorderStyle.Sizable;

                        if (ControlBox != true)
                            ControlBox = true;

                        if (Text != name)
                            Text = name;

                        break;

                    case BorderStyle.Thin:
                        if (FormBorderStyle != FormBorderStyle.FixedSingle)
                            FormBorderStyle = FormBorderStyle.FixedSingle;

                        if (ControlBox)
                            ControlBox = false;

                        if (Text != string.Empty)
                            Text = string.Empty;

                        break;
                }
            }
        }

        // ---------------------------------------------------------------------
        private void Calendar_Closed(object sender, EventArgs e)
        {
            WriteConfigFile();
        }

        // ---------------------------------------------------------------------
        private void WriteConfigFile()
        {
            try
            {
                var path = Path.GetDirectoryName(configFile);

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                if (settings != settingsRead)
                {
                    using (var sw = new StreamWriter(configFile))
                    {
                        settings.Serialize(sw);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                MessageBox.Show(this, e.Message);
            }
        }

        // ---------------------------------------------------------------------
        private void timer_Tick(object sender, EventArgs e)
        {
            // Check if we have crossed midnight into a new day. If so, update
            // the today member of the calendar
            if (calendarControl.TodayDate.DayOfYear != DateTime.Now.DayOfYear)
            {
                calendarControl.TodayDate = DateTime.Now;
                calendarControl.SetDate(DateTime.Now);
            }

            SetWorkingSetSize();
        }

        // ---------------------------------------------------------------------
        private void contextMenu_Popup(object sender, EventArgs e)
        {
            menuItemShowToday.Checked = calendarControl.ShowToday;
            menuItemCircleToday.Checked = calendarControl.ShowTodayCircle;
            menuItemWeekNumbers.Checked = calendarControl.ShowWeekNumbers;
            menuItemTopMost.Checked = TopMost;
            menuItemShowInTray.Checked = notifyIcon.Visible;
            menuItemBorderThick.Checked = borderStyle == BorderStyle.Thick;
            menuItemBorderThin.Checked = borderStyle == BorderStyle.Thin;
            menuItemBorderNone.Checked = borderStyle == BorderStyle.None;

            var day = calendarControl.FirstDayOfWeek;
            menuItemSunday.Checked = (day == DayOfWeek.Sunday);
            menuItemMonday.Checked = (day == DayOfWeek.Monday);
            menuItemTuesday.Checked = (day == DayOfWeek.Tuesday);
            menuItemWednesday.Checked = (day == DayOfWeek.Wednesday);
            menuItemThursday.Checked = (day == DayOfWeek.Thursday);
            menuItemFriday.Checked = (day == DayOfWeek.Friday);
            menuItemSaturday.Checked = (day == DayOfWeek.Saturday);
        }

        // ---------------------------------------------------------------------
        private void menuItemTopMost_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            settings.Topmost = TopMost;
        }

        // ---------------------------------------------------------------------
        private void menuItemWeekNumbers_Click(object sender, EventArgs e)
        {
            calendarControl.ShowWeekNumbers = !calendarControl.ShowWeekNumbers;
            settings.ShowWeekNumbers = calendarControl.ShowWeekNumbers;
        }

        // ---------------------------------------------------------------------
        private void menuItemShowToday_Click(object sender, EventArgs e)
        {
            calendarControl.ShowToday = !calendarControl.ShowToday;
            settings.ShowToday = calendarControl.ShowToday;
        }

        // ---------------------------------------------------------------------
        private void menuItemCircleToday_Click(object sender, EventArgs e)
        {
            calendarControl.ShowTodayCircle = !calendarControl.ShowTodayCircle;
            settings.ShowTodayCircle = calendarControl.ShowTodayCircle;
        }

        // ---------------------------------------------------------------------
        private void menuItemSunday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Sunday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemMonday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Monday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemTuesday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Tuesday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemWednesday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Wednesday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemThursday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Thursday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemFriday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Friday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void menuItemSaturday_Click(object sender, EventArgs e)
        {
            calendarControl.FirstDayOfWeek = DayOfWeek.Saturday;
            settings.FirstDay = calendarControl.FirstDayOfWeek;
        }

        // ---------------------------------------------------------------------
        private void Calendar_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                var position = settings.Position;
                position.Location = Location;
                settings.Position = position;
            }
        }

        // ---------------------------------------------------------------------
        private void Calendar_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (notifyIcon.Visible)
                {
                    Hide();
                }
            }
            else if (WindowState == FormWindowState.Normal)
            {
                var position = settings.Position;
                position.Size = ClientSize;
                settings.Position = position;
                CalendarDates(false);
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                CalendarDates(false);
            }
        }

        // ---------------------------------------------------------------------
        private void menuItemColors_Click(object sender, EventArgs e)
        {
            using (var colors = new Colors(calendarControl, settings, this))
            {
                colors.ShowDialog(this);

                if (settings.Border != borderStyle)
                {
                    Border = settings.Border;
                }
            }
        }

        // ---------------------------------------------------------------------
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static Point MousePoint;

        // ---------------------------------------------------------------------
        private void calendarControl_MouseMove(object sender, MouseEventArgs e)
        {
            var hti = calendarControl.HitTest
                (calendarControl.PointToClient(MousePosition));

            if (hti.Time != DateTime.MinValue)
            {
                if (currentEvents != null)
                {
                    var events = currentEvents[hti.Time] as string;

                    if (events != null)
                    {
                        var tooltipEvents = toolTip.GetToolTip(calendarControl);

                        if (tooltipEvents != events || MousePosition != MousePoint)
                        {
                            toolTip.UseFading = false;
                            toolTip.UseAnimation = false;
                            toolTip.ShowAlways = true;
                            toolTip.AutomaticDelay = 1;
                            toolTip.AutoPopDelay = int.MaxValue;
                            toolTip.Active = true;
                            toolTip.SetToolTip(calendarControl, events);
                        }
                    }
                    else
                    {
                        toolTip.Active = false;
                        toolTip.SetToolTip(calendarControl, string.Empty);
                    }
                }
            }
            else
            {
                toolTip.Active = false;
                toolTip.SetToolTip(calendarControl, string.Empty);
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
        private void menuItemDates_Click(object sender, EventArgs e)
        {
            using (var eventsForm = new EventsForm(events, settings.ColorHighlightDayFore))
            {
                if (eventsForm.ShowDialog(this) == DialogResult.OK)
                {
                    var allEvents = eventsForm.GetEvents();

                    if (allEvents != null)
                    {
                        events.SetEvents(allEvents);
                        WriteEventsFile();
                        CalendarDates(true);
                    }
                }
            }
        }

        // ---------------------------------------------------------------------
        private void welcomeTimer_Tick(object sender, EventArgs e)
        {
            welcomeTimer.Stop();

            if (File.Exists(configFile) == false)
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
                WindowState = FormWindowState.Normal;
                return;
            }

            base.WndProc(ref m);
        }

        // ---------------------------------------------------------------------
        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            var aboutDlg = new About();

            if (aboutDlg.ShowDialog(this) == DialogResult.Retry)
            {
                System.Diagnostics.Process.Start(WebSite);
            }
        }

        // ---------------------------------------------------------------------
        private void RegisterHotkey()
        {
            NativeMethods.UnregisterHotKey(Handle, hotKeyID);

            if (NativeMethods.RegisterHotKey(Handle, hotKeyID,
                NativeMethods.FSModifiers.MOD_SHIFT, Keys.F11) == false)
            {
                var lastError = Marshal.GetLastWin32Error();

                var error = lastError.ToString("x",
                    CultureInfo.InvariantCulture);

                Log.Warning("RegisterHotKey failed (0x" + error + ")");
            }
        }

        // ---------------------------------------------------------------------
        public static Point PositionAdjacent(Form form)
        {
            var parent = form.Owner.DesktopBounds;
            var test = new Rectangle(parent.Location, form.Size);
            var desktop = SystemInformation.WorkingArea;

            test.Offset(parent.Width, 0);

            if (test.Right > desktop.Right)
            {
                test.Offset(-parent.Width, 0);
                test.Offset(-form.Width, 0);
            }

            var bottom = test.Top + form.Height;

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
            configFile = (args.Length > 0 && args[0] != null)
                ? Path.Combine(args[0], ConfigFileName)
                : Path.Combine(ApplicationDataPath, ConfigFileName);

            eventsFile = (args.Length > 0 && args[0] != null)
                ? Path.Combine(args[0], EventsFileName)
                : Path.Combine(ApplicationDataPath, EventsFileName);
        }

        // ---------------------------------------------------------------------
        private void menuItemShowInTray_Click(object sender, EventArgs e)
        {
            settings.TrayIcon = !settings.TrayIcon;
            ShowInTray(settings.TrayIcon);
        }

        // ---------------------------------------------------------------------
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
        }

        // ---------------------------------------------------------------------
        private void menuItemQuickTips_Click(object sender, EventArgs e)
        {
            Welcome();
        }

        // ---------------------------------------------------------------------
        private void menuItemHelp_Click(object sender, EventArgs e)
        {
            InvokeHelp();
        }

        // ---------------------------------------------------------------------
        private void calendarControl_HelpRequested(object sender,
            HelpEventArgs hlpevent)
        {
            InvokeHelp();
        }

        // ---------------------------------------------------------------------
        private void InvokeHelp()
        {
            var helpFile = Application.StartupPath + @"\Calendar.mht";

            if (File.Exists(helpFile))
            {
                System.Diagnostics.Process.Start(helpFile);
            }
            else
            {
                MessageBox.Show(this, "Cannot find " + helpFile, Text);
            }
        }

        // ---------------------------------------------------------------------
        private void ShowInTray(bool show)
        {
            settings.TrayIcon = show;
            notifyIcon.Visible = show;
            ShowInTaskbar = !show;

            // Tool tips and hot keys get bonked when changing taskbar state
            toolTip.Dispose();
            toolTip = new ToolTip(components);
            toolTip.ShowAlways = true;
            RegisterHotkey();
        }

        // ---------------------------------------------------------------------
        private void menuItemGoToToday_Click(object sender, EventArgs e)
        {
            calendarControl.GoToToday();
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderThick_Click(object sender, EventArgs e)
        {
            Border = BorderStyle.Thick;
            settings.Border = BorderStyle.Thick;
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderThin_Click(object sender, EventArgs e)
        {
            Border = BorderStyle.Thin;
            settings.Border = BorderStyle.Thin;
        }

        // ---------------------------------------------------------------------
        private void menuItemBorderNone_Click(object sender, EventArgs e)
        {
            Border = BorderStyle.None;
            settings.Border = BorderStyle.None;
        }

        private static void SetWorkingSetSize()
        {
            var size = new UIntPtr(uint.MaxValue);
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