using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SZGYA13C_Bevolkerung
{

    public partial class MainWindow : Window
    {
        List<Bevolkerung> bevolkerung = new List<Bevolkerung>();
        public MainWindow()
        {
            InitializeComponent();

            bevolkerung = Bevolkerung.FromFile("..\\..\\..\\src\\bevölkerung.txt");

            lb1.Content = bevolkerung.Count();

            lb2.Content = bevolkerung.First().ToString().Replace(',', ' ').Replace("True", "Igen").Replace("False", "Nem");
        }
    }
}