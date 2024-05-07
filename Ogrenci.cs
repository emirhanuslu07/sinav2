using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3.Modeller
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public int OkulNo { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public virtual Sinif?  Sinif{  get; set; }

    }
}
