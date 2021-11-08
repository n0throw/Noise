using Noise.Service;
using Noise.VM;
using System.Windows;

namespace Noise
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataVM(new PngFileService(), new DefaultDialogService()) { Height = "50", Width = "50", Seed = "Noise" };
        }
    }
}
