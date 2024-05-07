using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp3.Modeller;

namespace WinFormsApp3.Veri_Katmani
{
    public class Veritabani : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Sinif> Siniflar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Okul;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public BindingList<Ogrenci> Ogrencilerin_VerileriniYukle() {
            Ogrenciler.Load();
            return Ogrenciler.Local.ToBindingList();
        }
        public List<Ogrenci> Ogrencilerin_VerileriniYukle(object filtre)
        {
            Ogrenciler.Load();
            if(filtre.ToString()!="")
                return Ogrenciler.Local.Where(x => x.Sinif == (Sinif)filtre).ToList();
            else
                return Ogrenciler.Local.ToList();
        }
        public BindingList<Sinif> Siniflarin_VerileriniYukle()
        {
            Siniflar.Load();
            return Siniflar.Local.ToBindingList();
        }
        public void Yeni_Ogrenci_Ekle(Ogrenci ogr) { 
            Ogrenciler.Add(ogr);
            SaveChanges();
        }
        public void Ogrenci_Guncelle(int secilen,Ogrenci ogr) {
            var _ogr = Ogrenciler.First(x => x.OkulNo == secilen);
            if (_ogr != null) {
                _ogr.Ad=ogr.Ad;
                _ogr.Soyad=ogr.Soyad;
                _ogr.OkulNo=ogr.OkulNo;
                _ogr.Sinif=ogr.Sinif;
            }
                SaveChanges();
        }
        public void Ogrenci_Sil(int okulno)
        {
            var o=Ogrenciler.First(x => x.OkulNo==okulno);
            Ogrenciler.Remove(o); 
            SaveChanges();
        }
        public void Yeni_Sinif_Ekle(Sinif snf) {
            Siniflar.Add(snf);
            SaveChanges();
        }
        public void Sinif_Guncelle(int secilen,Sinif snf)
        {
            var _snf = Siniflar.First(x => x.Id == secilen);
            if (_snf != null)
            {
                _snf.Seviye = snf.Seviye;
                _snf.Sube = snf.Sube;               
            }
            SaveChanges();            
        }
        public void Sinif_sil(int Id) {
            var s = Siniflar.First(x => x.Id == Id);
            Siniflar.Remove(s);
            SaveChanges();
        }
    }
}
