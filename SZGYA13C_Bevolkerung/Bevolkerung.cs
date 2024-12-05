using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZGYA13C_Bevolkerung
{
    class Bevolkerung
    {
        public int Id { get; set; }
        public string Nem { get; set; }
        public int SzuletesiEv { get; set; }
        public int Suly { get; set; }
        public int Magassag { get; set; }
        public bool Dohanyzik { get; set; }
        public string Nemzetiseg { get; set; } //ha nem német akkor nincs nepcsoport
        public string Nepcsoport { get; set; }
        public string Tartomany { get; set; }
        public int NettoJovedelem { get; set; } //EUR
        public string IskolaiVegzettseg { get; set; }
        public string PolitikaiNezet { get; set; }
        public bool AktivSzavazo { get; set; }
        public int SorFogyasztasEvente { get; set; } //NA ha nincs adat
        public int KrumpliFogyasztasEvente { get; set; }

        public Bevolkerung(int id, string nem, int szuletesiEv, int suly, int magassag, bool dohanyzik, string nemzetiseg, string nepcsoport, string tartomany, int nettoJovedelem, string iskolaiVegzettseg, string politikaiNezet, bool aktivSzavazo, int sorFogyasztasEvente, int krumpliFogyasztasEvente)
        {
            Id = id;
            Nem = nem;
            SzuletesiEv = szuletesiEv;
            Suly = suly;
            Magassag = magassag;
            Dohanyzik = dohanyzik;
            Nemzetiseg = nemzetiseg;
            Nepcsoport = nepcsoport;
            Tartomany = tartomany;
            NettoJovedelem = nettoJovedelem;
            IskolaiVegzettseg = iskolaiVegzettseg;
            PolitikaiNezet = politikaiNezet;
            AktivSzavazo = aktivSzavazo;
            SorFogyasztasEvente = sorFogyasztasEvente;
            KrumpliFogyasztasEvente = krumpliFogyasztasEvente;
        }

        public int HaviNetto()
        {
            return NettoJovedelem / 12;
        }

        public int Kor()
        {
            return DateTime.Now.Year - SzuletesiEv;
        }

        public static List<Bevolkerung> FromFile(string path)
        {
            List<Bevolkerung> bevolkerung = new List<Bevolkerung>();

            string[] line = File.ReadAllLines(path);

            foreach (string l in line.Skip(1))
            {
                string[] b = l.Split(';');

                int Id = int.Parse(b[0]);
                string Nem = b[1];
                int SzuletesiEv = int.Parse(b[2]);
                int Suly = int.Parse(b[3]);
                int Magassag = int.Parse(b[4]);
                bool Dohanyzik;
                if (b[5] == "igen")
                {
                    Dohanyzik = true;
                }
                else
                {
                    Dohanyzik = false;
                }
                string Nemzetiseg = b[6];
                string Nepcsoport;
                if (b[6] == "német")
                {
                    Nepcsoport = b[7];
                }
                else
                {
                    Nepcsoport = string.Empty;
                }
                string Tartomany = b[8];
                int NettoJovedelem = int.Parse(b[9]);
                string IskolaiVegzettseg = b[10];
                string PolitikaiNezet = b[11];
                bool AktivSzavazo;
                if (b[12] == "igen")
                {
                    AktivSzavazo = true;
                }
                else
                {
                    AktivSzavazo = false;
                }
                int SorFogyasztasEvente;
                if (b[13] == "NA")
                {
                    SorFogyasztasEvente = 0;
                }
                else
                {
                    SorFogyasztasEvente = int.Parse(b[13]);
                }
                int KrumpliFogyasztasEvente;
                if (b[14] == "NA")
                {
                    KrumpliFogyasztasEvente = 0;
                }
                else
                {
                    KrumpliFogyasztasEvente = int.Parse(b[14]);
                }

                Bevolkerung bevolkerungs = new(Id, Nem, SzuletesiEv, Suly, Magassag, Dohanyzik, Nemzetiseg, Nepcsoport, Tartomany, NettoJovedelem, IskolaiVegzettseg, PolitikaiNezet, AktivSzavazo, SorFogyasztasEvente, KrumpliFogyasztasEvente);
                bevolkerung.Add(bevolkerungs);
            }
            return bevolkerung;
        }

        public override string ToString()
        {
            return $"{Id},{Nem},{SzuletesiEv},{Suly},{Magassag},{Dohanyzik},{Nemzetiseg},{Nepcsoport},{Tartomany},{NettoJovedelem},{IskolaiVegzettseg},{PolitikaiNezet},{AktivSzavazo},{SorFogyasztasEvente},{KrumpliFogyasztasEvente}";
        }
        public string ToString(bool detailed)
        {
            if (detailed)
            {
                return $"{Id}\t{Nem}\t{SzuletesiEv}\t{Suly}\t{Magassag}";
            }
            else
            {
                return $"{Id}\t{Nemzetiseg}\t{Nepcsoport}\t{Tartomany}\t{NettoJovedelem}";
            }
        }
    }
}
