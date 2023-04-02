using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Runtime.InteropServices;

namespace WpfWolume
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);


        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        public void IncreaseVolume()
        {
            SendMessageW(new IntPtr(0xffff), WM_APPCOMMAND, new IntPtr(0x30292), new IntPtr(APPCOMMAND_VOLUME_UP));
        }

        public void DecreaseVolume()
        {
            SendMessageW(new IntPtr(0xffff), WM_APPCOMMAND, new IntPtr(0x30292), new IntPtr(APPCOMMAND_VOLUME_DOWN));
        }

        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void IncreaseProgressBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (progressBar.Value < 100)
            {
                progressBar.Value += 10;
            }

            if (progressBar.Value + 10 <= progressBar.Maximum)
            {
                progressBar.Value += 10;
            }
            IncreaseVolume();
        }

        private void DecreaseProgressBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (progressBar.Value > 0)
            {
                progressBar.Value -= 10;
            }

            if (progressBar.Value - 10 >= progressBar.Minimum)
            {
                progressBar.Value -= 10;
            }
            DecreaseVolume();
        }

    }
}
