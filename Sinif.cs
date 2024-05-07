using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3.Modeller
{
    public class Sinif
    {
        public int Id { get; set; }
        public int Seviye { get; set; }
        public string? Sube { get; set; }

        [NotMapped]

        public string? SinifAd
        {
            get
            {
                string? sonuc = Seviye != 0 ? Seviye + "-" + Sube : Sube;
                return sonuc;
            }
        }

        [NotMapped]

        public Sinif Kendisi
        {
            get { return this; }
        }

        public virtual ObservableCollectionListSource<Ogrenci>? Ogrenciler {  get; set; }
    }
}
