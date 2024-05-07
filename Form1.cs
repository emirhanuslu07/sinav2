using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Windows.Forms;
using WinFormsApp3.Modeller;
using static Azure.Core.HttpHeader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using WinFormsApp3.Veri_Katmani;
using WinFormsApp3.Is_Katmani;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        islemler _is = new islemler();
        int secilen = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void FiltreCombox_ayarla()
        {
            Sinif tumList = new Sinif();
            tumList.Sube = "Tüm Öðrenciler";
            tumList.Seviye = 0;
            tumList.Id = 0;
            comboBoxFiltre.Items.Add(tumList);
            comboBoxFiltre.SelectedIndex = 0;
            foreach (var item in _is.sinif_Listesi())
            {
                comboBoxFiltre.Items.Add(item);
            }
            comboBoxFiltre.DisplayMember = "SinifAd";
        }
        private void Ogrenciler_DataGrid_ayarla(DataGridView d)
        {
            d.DataSource = _is.ogrenci_Listesi();
            d.Columns[0].Visible = false;
            d.Columns[4].Visible = false;
            d.ColumnHeadersVisible = true;

            //ozel comboBox
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            foreach (var item in _is.sinif_Listesi())
            {
                combo.Items.Add(item);
            }
            combo.ReadOnly = true;
            combo.HeaderText = "Sýnýf";
            combo.DataPropertyName = "Sinif";
            combo.DisplayMember = "SinifAd";
            combo.ValueMember = "Kendisi";

            d.Columns.Add(combo);
            //ozel düzenleme dügmesi
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "Düzenle";
            d.Columns.Add(btn);
            //ozel silme dügmesi
            btn = new DataGridViewButtonColumn();
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "Sil";
            d.Columns.Add(btn);
        }
        private void OgrenciDuzenleme_safyasiniAyarla()
        {
            Ogrenciler_DataGrid_ayarla(dataGridView2);
            comboBoxOgr_sinif.DataSource = _is.sinif_Listesi();
            comboBoxOgr_sinif.DisplayMember = "SinifAd";
        }
        public void ogrenciKayit_duzenle(DataGridViewRow kayit)
        {
            txtOkulNo.Text = kayit.Cells[1].Value.ToString();
            secilen = Int32.Parse(txtOkulNo.Text);
            txtAd.Text = kayit.Cells[2].Value.ToString();
            txtSoyad.Text = kayit.Cells[3].Value.ToString();
            comboBoxOgr_sinif.SelectedItem = kayit.Cells[4].Value;
            tabControl1.SelectedIndex = 1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FiltreCombox_ayarla();
            Ogrenciler_DataGrid_ayarla(dataGridView_ogrenciler);
            OgrenciDuzenleme_safyasiniAyarla();
            SinifDuzenleme_safyasiniAyarla();
            Siniflar_Datagrid_ayarla();
        }

        private void SinifDuzenleme_safyasiniAyarla()
        {
            //comboboxlarý aayrladým
            List<int> _seviye = [9, 10, 11, 12];
            List<string> _sube = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++) { _sube.Add(c.ToString()); }
            comboBox_SnfSeviye.DataSource = _seviye;
            comboBox_SnfSube.DataSource = _sube;
            comboBox_SnfSeviye.SelectedIndex = -1;
            comboBox_SnfSube.SelectedIndex = -1;

        }

        private void Siniflar_Datagrid_ayarla()
        {
            dataGridView_siniflar.DataSource = _is.sinif_Listesi();
            dataGridView_siniflar.Columns[0].Visible = false;
            dataGridView_siniflar.Columns[3].Visible = false;
            dataGridView_siniflar.Columns[4].Visible = false;
            dataGridView_siniflar.ColumnHeadersVisible = true;

            //ozel düzenleme dügmesi
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "Düzenle";
            dataGridView_siniflar.Columns.Add(btn);
            //ozel silme dügmesi
            btn = new DataGridViewButtonColumn();
            btn.UseColumnTextForButtonValue = true;
            btn.Text = "Sil";
            dataGridView_siniflar.Columns.Add(btn);
        }

        //yeni
        private void btnEkle_Ogr_Click(object sender, EventArgs e)
        {
            _is.ogr_Ekle(secilen, txtAd.Text, txtSoyad.Text, txtOkulNo.Text, comboBoxOgr_sinif.SelectedItem);
            tabControl1.SelectedIndex = 0;
        }
        //yeni
        private void comboBoxFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_ogrenciler.DataSource = _is.ogrenci_Listesi((Sinif)comboBoxFiltre.SelectedItem);
        }
        //yeni
        private void dataGridView_ogrenciler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //düzenle buttonu için kullanýyorum
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                var kayit = dataGridView_ogrenciler.Rows[e.RowIndex];
                ogrenciKayit_duzenle(kayit);
            }
            //Silme buttonu için kullanýyorum
            if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                var kayit = dataGridView_ogrenciler.Rows[e.RowIndex].Cells[1].Value;
                _is.ogr_Sil(Int32.Parse(kayit.ToString()));
            }
        }
        //yeni
        private void btn_yeni_ogr_Click(object sender, EventArgs e)
        {
            secilen = 0;
            txtOkulNo.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            comboBoxOgr_sinif.SelectedIndex = -1;
        }


        private void dataGridView_siniflar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //düzenle buttonu için kullanýyorum
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var kayit = dataGridView_siniflar.Rows[e.RowIndex];
                sinifKayit_duzenle(kayit);
            }
            //Silme buttonu için kullanýyorum
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                //sýnýfýn idsini alýyorunm
                var kayit = dataGridView_siniflar.Rows[e.RowIndex].Cells[2].Value;
                _is.snf_Sil(Int32.Parse(kayit.ToString()));
            }
        }

        private void sinifKayit_duzenle(DataGridViewRow kayit)
        {
            comboBox_SnfSeviye.SelectedItem = kayit.Cells[3].Value;
            comboBox_SnfSube.SelectedItem = kayit.Cells[4].Value;
            secilen = Int32.Parse(kayit.Cells[2].Value.ToString());


        }

        private void button_snfYeni_Click(object sender, EventArgs e)
        {
            comboBox_SnfSeviye.SelectedIndex = -1;
            comboBox_SnfSube.SelectedIndex = -1;
            secilen = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //karýþýklýðý gidermek için
            secilen = 0;
        }

        private void button_snfKaydet_Click(object sender, EventArgs e)
        {
            _is.sinif_Ekle(secilen, comboBox_SnfSeviye.SelectedItem, comboBox_SnfSube.SelectedItem);
            button_snfYeni_Click(sender, e);
            dataGridView_siniflar.Refresh();

        }
    }
}
