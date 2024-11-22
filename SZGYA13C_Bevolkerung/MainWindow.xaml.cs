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

            for (int i = 1; i <= 40; i++)
            {
                ComboBoxFeladatok.Items.Add(i.ToString());
            }
        }

        private void ComboBoxFeladatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MegoldasMondatos.Content = string.Empty;
            MegoldasTeljes.Items.Clear();
            MegoldasLista.ItemsSource = string.Empty;

            int feladatSzam = ComboBoxFeladatok.SelectedIndex + 1;
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
            var legnagyobbBejovetel = bevolkerung.OrderByDescending(l => l.NettoJovedelem).First();
            MegoldasMondatos.Content = $"A legmagasabb nettó éves jövedelem: {legnagyobbBejovetel.NettoJovedelem:F2} Eur.";
        }
        private void Feladat2() 
        {
            var atlagosNettoJovedelem = bevolkerung.Average(a => a.NettoJovedelem);
            MegoldasMondatos.Content = $"Az állampolgárok átlagos nettó jövedelme: {atlagosNettoJovedelem:F2} Eur.";
        }
        private void Feladat3() 
        {
            var csoportositva = bevolkerung.GroupBy(cs => cs.Tartomany).Select(c => new {c.Key, Fő = c.Count()}).ToList();
            MegoldasLista.ItemsSource = csoportositva;
        }
        private void Feladat4() 
        {
            var anglolok = bevolkerung.Where(a => a.Nemzetiseg == "angolai");
            foreach (var i in anglolok)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat5() 
        {
            var legfiatalabbEve = bevolkerung.Max(l => l.SzuletesiEv);
            var legfiatalabb = bevolkerung.Where(l => l.SzuletesiEv == legfiatalabbEve).ToList();
            foreach (var i in legfiatalabb)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat6() 
        {
            var nemDohanyzo = bevolkerung.Where(n => n.Dohanyzik == false).Select(n => new {n.Id, Havi_Jövedelem = n.NettoJovedelem / 12 });
            MegoldasLista.ItemsSource = nemDohanyzo;
        }
        private void Feladat7() 
        { 
            
        }
        private void Feladat8() { }
        private void Feladat9() { }
        private void Feladat10() { }
        private void Feladat11() { }
        private void Feladat12() { }
        private void Feladat13() { }
        private void Feladat14() { }
        private void Feladat15() { }
        private void Feladat16() { }
        private void Feladat17() { }
        private void Feladat18() { }
        private void Feladat19() { }
        private void Feladat20() { }
        private void Feladat21() { }
        private void Feladat22() { }
        private void Feladat23() { }
        private void Feladat24() { }
        private void Feladat25() { }
        private void Feladat26() { }
        private void Feladat27() { }
        private void Feladat28() { }
        private void Feladat29() { }
        private void Feladat30() { }
        private void Feladat31() { }
        private void Feladat32() { }
        private void Feladat33() { }
        private void Feladat34() { }
        private void Feladat35() { }
        private void Feladat36() { }
        private void Feladat37() { }
        private void Feladat38() { }
        private void Feladat39() { }
        private void Feladat40() { }
        private void Feladat41() { }
        private void Feladat42() { }
        private void Feladat43() { }
        private void Feladat44() { }
        private void Feladat45() { }
    }
}