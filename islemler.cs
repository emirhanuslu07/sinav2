using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp3.Modeller;
using WinFormsApp3.Veri_Katmani;

namespace WinFormsApp3.Is_Katmani
{
    internal class islemler
    {
        Veritabani vt =new Veritabani();
        public BindingList<Ogrenci> ogrenci_Listesi() => vt.Ogrencilerin_VerileriniYukle();
        public BindingList<Sinif> sinif_Listesi() => vt.Siniflarin_VerileriniYukle();
        public List<Ogrenci> ogrenci_Listesi(Sinif filtre) {
            if (filtre.Id != 0) 
                return vt.Ogrencilerin_VerileriniYukle(filtre);
            else 
                return vt.Ogrencilerin_VerileriniYukle("");
        } 
        public void ogr_Ekle(int secilen,object Ad, object Soyad,object okulno,object snf) {
            if (Ad == null || Soyad == null || okulno == null || snf == null)
            {
                MessageBox.Show("Tüm alanların Doldurulması zorunludur");
            }
            else
            {
                Ogrenci _ogr = new Ogrenci();
                _ogr.Ad = Ad.ToString();
                _ogr.Soyad = Soyad.ToString();
                _ogr.OkulNo = Convert.ToInt32(okulno);
                _ogr.Sinif = (Sinif)snf;
                if (secilen==0)
                {
                    vt.Yeni_Ogrenci_Ekle(_ogr);
                    MessageBox.Show("Yeni Öğrenci Eklendi"); 
                }
                else
                {
                    vt.Ogrenci_Guncelle(secilen,_ogr);
                    MessageBox.Show("İlgili Öğrenci Bilgileri Güncellendi");

                }
            }

        }
        public void ogr_Sil(int okulno) {
            vt.Ogrenci_Sil(okulno);
        }

        public void sinif_Ekle(int secilen, object seviye, object sube)
        {
            if (seviye == null || sube == null)
            {
                MessageBox.Show("Tüm alanların Doldurulması zorunludur");
            }
            else
            {
                Sinif _snf = new Sinif();
                _snf.Seviye = Int32.Parse(seviye.ToString());
                _snf.Sube = sube.ToString();
                if (secilen == 0)
                {
                    vt.Yeni_Sinif_Ekle(_snf);
                    MessageBox.Show("Yeni Sınıf Eklendi");
                }
                else
                {
                    vt.Sinif_Guncelle(secilen, _snf);
                    MessageBox.Show("İlgili Sınıf Bilgileri Güncellendi");

                }
            }
        }
        public void snf_Sil(int id) {
            vt.Sinif_sil(id);
        }
    }
}
