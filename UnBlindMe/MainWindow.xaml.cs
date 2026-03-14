using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;
using System.Drawing;

namespace UnBlindMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppSettings _settings;
        private Forms.NotifyIcon? _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            _settings = SettingsManager.LoadSettings();
            ApplySettings();
            
            this.Loaded += MainWindow_Loaded;
            this.LocationChanged += MainWindow_LocationChanged;
            this.SizeChanged += MainWindow_SizeChanged;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_settings.RememberPosition)
            {
                this.Top = _settings.WindowTop;
                this.Left = _settings.WindowLeft;
                this.Width = _settings.WindowWidth;
                this.Height = _settings.WindowHeight;
            }
            
            MenuAlwaysOnTop.IsChecked = _settings.AlwaysOnTop;
            MenuRememberPosition.IsChecked = _settings.RememberPosition;
            this.Topmost = _settings.AlwaysOnTop;

            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon = new Forms.NotifyIcon();
            
            try 
            {
                var iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/headlight.png")).Stream;
                using (var bitmap = new Bitmap(iconStream))
                {
                    _notifyIcon.Icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                }
            }
            catch (Exception)
            {
                // Fallback or handle error
            }

            _notifyIcon.Visible = true;
            _notifyIcon.Text = "UnBlindMe";

            var contextMenu = new Forms.ContextMenuStrip();
            contextMenu.Items.Add("Show", null, (s, e) => { this.Show(); this.WindowState = WindowState.Normal; this.Activate(); });
            contextMenu.Items.Add("Hide", null, (s, e) => { this.Hide(); });
            contextMenu.Items.Add(new Forms.ToolStripSeparator());

            var opacityMenu = new Forms.ToolStripMenuItem("Opacity");
            for (int i = 1; i <= 10; i++)
            {
                double op = i / 10.0;
                var item = new Forms.ToolStripMenuItem($"{i * 10}%");
                item.Click += (s, e) => SetOpacity(op);
                opacityMenu.DropDownItems.Add(item);
            }
            contextMenu.Items.Add(opacityMenu);

            var aotItem = new Forms.ToolStripMenuItem("Always on Top");
            aotItem.CheckOnClick = true;
            aotItem.Checked = _settings.AlwaysOnTop;
            aotItem.Click += (s, e) => 
            {
                _settings.AlwaysOnTop = aotItem.Checked;
                this.Topmost = _settings.AlwaysOnTop;
                MenuAlwaysOnTop.IsChecked = _settings.AlwaysOnTop;
                SettingsManager.SaveSettings(_settings);
            };
            contextMenu.Items.Add(aotItem);

            contextMenu.Items.Add(new Forms.ToolStripSeparator());
            contextMenu.Items.Add("Exit", null, (s, e) => { Application.Current.Shutdown(); });

            _notifyIcon.ContextMenuStrip = contextMenu;
            
            // To update AOT check state in tray if changed elsewhere
            MenuAlwaysOnTop.Checked += (s, e) => aotItem.Checked = true;
            MenuAlwaysOnTop.Unchecked += (s, e) => aotItem.Checked = false;

            _notifyIcon.DoubleClick += (s, e) => 
            {
                if (this.IsVisible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate();
                }
            };
        }

        private void ApplySettings()
        {
            byte alpha = (byte)(_settings.DimmingOpacity * 255);
            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(alpha, 0, 0, 0));
        }

        private void MainWindow_LocationChanged(object? sender, EventArgs e)
        {
            if (_settings.RememberPosition)
            {
                _settings.WindowTop = this.Top;
                _settings.WindowLeft = this.Left;
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_settings.RememberPosition)
            {
                _settings.WindowWidth = this.ActualWidth;
                _settings.WindowHeight = this.ActualHeight;
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsManager.SaveSettings(_settings);
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MenuOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && double.TryParse(menuItem.Tag.ToString(), out double opacity))
            {
                _settings.DimmingOpacity = opacity;
                ApplySettings();
                SettingsManager.SaveSettings(_settings);
            }
        }

        private void MenuAlwaysOnTop_Click(object sender, RoutedEventArgs e)
        {
            _settings.AlwaysOnTop = MenuAlwaysOnTop.IsChecked;
            this.Topmost = _settings.AlwaysOnTop;
            SettingsManager.SaveSettings(_settings);
        }

        private void MenuRememberPosition_Click(object sender, RoutedEventArgs e)
        {
            _settings.RememberPosition = MenuRememberPosition.IsChecked;
            SettingsManager.SaveSettings(_settings);
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void CtrShortcut1(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.1);
        public void CtrShortcut2(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.2);
        public void CtrShortcut3(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.3);
        public void CtrShortcut4(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.4);
        public void CtrShortcut5(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.5);
        public void CtrShortcut6(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.6);
        public void CtrShortcut7(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.7);
        public void CtrShortcut8(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.8);
        public void CtrShortcut9(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(0.9);
        public void CtrShortcut0(Object sender, ExecutedRoutedEventArgs e) => SetOpacity(1.0);

        private void SetOpacity(double opacity)
        {
            _settings.DimmingOpacity = opacity;
            ApplySettings();
            SettingsManager.SaveSettings(_settings);
        }

        public void CtrShortcutA(Object sender, ExecutedRoutedEventArgs e)
        {
            _settings.AlwaysOnTop = !_settings.AlwaysOnTop;
            this.Topmost = _settings.AlwaysOnTop;
            MenuAlwaysOnTop.IsChecked = _settings.AlwaysOnTop;
            SettingsManager.SaveSettings(_settings);
        }
    }
}
