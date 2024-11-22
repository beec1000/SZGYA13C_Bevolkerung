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
using System.Reflection;

namespace SZGYA13C_Bevolkerung
{

    public partial class MainWindow : Window
    {
        List<Bevolkerung> bevolkerung = new List<Bevolkerung>();
        public MainWindow()
        {
            InitializeComponent();

            bevolkerung = Bevolkerung.FromFile("..\\..\\..\\src\\bevölkerung.txt");

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 40; i++)
            {
                ComboBoxFeladatok.Items.Add(i.ToString());
            }

            ComboBoxFeladatok.SelectedIndex = 0;
        }

        private void ComboBoxFeladatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MegoldasMondatos.Content = string.Empty;
            MegoldasTeljes.Items.Clear();
            MegoldasLista.Items.Clear();

            int feladatSzam = int.Parse(ComboBoxFeladatok.SelectedItem.ToString());
            ValaszthatFeladat(feladatSzam);
        }

        private void ValaszthatFeladat(int selectedTask)
        {
            var methodName = $"Feladat{selectedTask}";
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(this, null);
        }

        private void Feladat1()
        {
            foreach (var allampolgar in lista)
            {
                MegoldasTeljes.Items.Add(allampolgar.ToString(false));
            }
        }

        private void Feladat2()
        {
            foreach (var allampolgar in lista)
            {
                MegoldasLista.Items.Add($"{allampolgar.Nev}: {allampolgar.GetEletkor()} év");
            }
        }

        private void Feladat3()
        {
            foreach (var allampolgar in lista)
            {
                if (allampolgar.Sorfogyaszt)
                {
                    MegoldasLista.Items.Add($"{allampolgar.Nev}: Igen");
                }
                else
                {
                    MegoldasLista.Items.Add($"{allampolgar.Nev}: Nem");
                }
            }
        }

        private void Feladat4()
        {
            foreach (var allampolgar in lista)
            {
                if (allampolgar.Krumplifogyaszt)
                {
                    MegoldasLista.Items.Add($"{allampolgar.Nev}: Igen");
                }
                else
                {
                    MegoldasLista.Items.Add($"{allampolgar.Nev}: Nem");
                }
            }
        }
    }
}