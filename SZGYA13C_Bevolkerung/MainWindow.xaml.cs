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
            var csoportositva = bevolkerung.GroupBy(cs => cs.Tartomany)
                                           .Select(c => new {c.Key, Fő = c.Count()})
                                           .ToList();
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
            var nemDohanyzo = bevolkerung.Where(n => n.Dohanyzik == false)
                                         .Select(n => new {n.Id, Havi_Jövedelem = n.NettoJovedelem / 12 });
            MegoldasLista.ItemsSource = nemDohanyzo;
        }
        private void Feladat7() 
        {
            var bajorPenzes = bevolkerung.Where(b => b.Tartomany.Contains("Bajorország") && b.NettoJovedelem > 30000)
                                         .OrderBy(b => b.IskolaiVegzettseg)
                                         .ToList();
            foreach (var i in bajorPenzes)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat8() 
        {
            var ferfiak = bevolkerung.Where(f => f.Nem.Contains("férfi")).Select(f => f.ToString(true));
            MegoldasLista.ItemsSource = ferfiak;
        }
        private void Feladat9() 
        {
            var nok = bevolkerung.Where(n => n.Nem.Contains("nő") && n.Tartomany.Contains("Bajorország"))
                                 .Select(n => n.ToString(false));
            MegoldasLista.ItemsSource = nok;
        }
        private void Feladat10() 
        {
            var nemDohanyzo = bevolkerung.Where(n => !n.Dohanyzik).OrderByDescending(n => n.NettoJovedelem).Take(10);
            MegoldasLista.ItemsSource = nemDohanyzo;
        }
        private void Feladat11() 
        {
            var legidosebb = bevolkerung.OrderByDescending(l => l.Kor()).Take(5);
            foreach (var i in legidosebb)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat12() 
        {
            var szavazok = bevolkerung.Where(sz => sz.Nemzetiseg.Contains("német"))
                                      .OrderBy(sz => sz.Nepcsoport).Select(sz => new {Népcsoport = sz.Nepcsoport,
                                                                                      Szavazó = sz.AktivSzavazo ? "Aktív Szavazó" : "Nem Aktív Szavazó", 
                                                                                      Politikai_Nézet = sz.PolitikaiNezet });
            MegoldasLista.ItemsSource = szavazok;
        }
        private void Feladat13() 
        {
            var atlagSorFogyasztasF = bevolkerung.Where(a => a.Nem.Contains("férfi"))
                                                 .Average(a => a.SorFogyasztasEvente);
            MegoldasMondatos.Content = $"Férfiak átlagos sörfogyasztása: {atlagSorFogyasztasF:F2}.";
        }
        private void Feladat14() 
        {
            var iskolaiVegzettseg = bevolkerung/*.Where(i => !string.IsNullOrEmpty(i.IskolaiVegzettseg))*/.OrderBy(i => i.IskolaiVegzettseg);
            foreach (var i in iskolaiVegzettseg)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat15() 
        {
            var legmagasabbJovedelem = bevolkerung.OrderByDescending(l => l.NettoJovedelem).Select(l => l.ToString(false)).Take(3);
            var legalacsonyabbJovedelem = bevolkerung.OrderBy(l => l.NettoJovedelem).Select(l => l.ToString(false)).Take(3);
            var egyutt = legmagasabbJovedelem.Concat(legalacsonyabbJovedelem);
            MegoldasLista.ItemsSource = egyutt;
        }
        private void Feladat16() 
        {
            var aktivSzavazok = bevolkerung.Count(a => a.AktivSzavazo);
            var osszes = bevolkerung.Count();
            MegoldasMondatos.Content = $"Az állampolgaárok {osszes/aktivSzavazok:F2}%-a aktív szavazó.";
        }
        private void Feladat17() 
        {
            var rendezes = bevolkerung.OrderBy(r => r.Tartomany).Where(r => r.AktivSzavazo).ToList();
            foreach (var i in rendezes)
            {
                MegoldasTeljes.Items.Add(i);
            }
        }
        private void Feladat18() 
        {
            var atlagosKor = bevolkerung.Average(a => a.Kor());
            MegoldasMondatos.Content = $"Az állampolgárok átlagos kora: {atlagosKor:F2}.";
        }
        private void Feladat19() 
        {
            var tartomanyLegmagasabbJovedelem = bevolkerung.GroupBy(t => t.Tartomany)
                                                           .Select(t => new { Tartomany = t.Key, 
                                                                              ÁtlagosJövedelem = Math.Round(t.Average(a => a.NettoJovedelem), 2), 
                                                                              Lakosok = t.Count() })
                                                           .OrderByDescending(t => t.ÁtlagosJövedelem)
                                                           .ThenByDescending(t => t.Lakosok)
                                                           .ToList();
            MegoldasLista.ItemsSource = tartomanyLegmagasabbJovedelem;
        }
        private void Feladat20() 
        {
            var atlagSuly = bevolkerung.Average(a => a.Suly);
            var rendezettSuly = bevolkerung.Select(a => a.Suly).OrderBy(s => s).ToList();
            double medianSuly;
            if (rendezettSuly.Count % 2 == 1)
            {
                medianSuly = rendezettSuly[rendezettSuly.Count / 2];
            }
            else
            {
                medianSuly = rendezettSuly[(rendezettSuly.Count / 2) - 1] + rendezettSuly[rendezettSuly.Count / 2] / 2.0;
            }
            MegoldasMondatos.Content = $"Az állampolgárok átlagos súlya: {atlagSuly:F2}KG, és a medián: {medianSuly:F2}KG."; 
        }
        private void Feladat21() 
        {
            var aktivSor = bevolkerung.Where(a => a.AktivSzavazo).Average(a => a.SorFogyasztasEvente);
            var nemAktivSor = bevolkerung.Where(a => a.AktivSzavazo == false).Average(a => a.SorFogyasztasEvente);
            var magasabbSor = aktivSor > nemAktivSor ? "aktív szavazók" : "nem aktív szavazók";
            MegoldasMondatos.Content = $"Az aktív szavazók átlagos sörfogyasztása: {aktivSor:F2}.\nA nem aktív szavazók átlagos sörfogyasztása: {nemAktivSor:F2}.\nA magasabb átlagos sörfogyasztás a(z) {magasabbSor} csoportja.";
        }
        private void Feladat22() 
        {
            var ferfiMagassagAtlag = bevolkerung.Where(f => f.Nem.Contains("férfi")).Average(f => f.Magassag);
            var noMagassagAtlag = bevolkerung.Where(n => n.Nem.Contains("nő")).Average(n => n.Magassag);
            MegoldasMondatos.Content = $"A férfiak átlag magassága: {ferfiMagassagAtlag:F2}.\nA nők átlag magassága: {noMagassagAtlag:F2}.";
        }
        private void Feladat23() 
        {
            var legtobbNepcsoport = bevolkerung.GroupBy(l => l.Nepcsoport)
                                               .Select(l => new { Népcsoport = l.Key, 
                                                                  Emberek = l.Count(),
                                                                  ÁtlagosKor = Math.Round(l.Average(a => a.Kor()), 2) })
                                               .OrderByDescending(l => l.Emberek)
                                               .ThenByDescending(l => l.ÁtlagosKor)
                                               .Take(1);
            MegoldasLista.ItemsSource = legtobbNepcsoport;
        }
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