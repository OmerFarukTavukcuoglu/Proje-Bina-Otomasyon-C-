using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraWaitForm;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraPrintingLinks;
using System.Data.SqlClient;



namespace Proje_Bina_Otomasyon
{
    public partial class Ana_Form : Form
    {
        public Ana_Form()
        {
            InitializeComponent();
        }
        SqlConnection d_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Demir_APT;Integrated Security=True");
        SqlConnection g_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Gül_APT;Integrated Security=True");

        //2 tablonun bağlantısını gerçekleştir



        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Fatura fatura = new Fatura(); 
            if (Existform(fatura)) return;
            fatura.MdiParent = this;
            fatura.Show();
        }

        private bool Existform(Fatura ftr) //1'den fazla sekme açılmaması için 2 durum olustur(bool)
        {
            foreach (var sekme in MdiChildren) //Foreach döngüsü ile oluşturulan sekme değişkenine mdichildren(sekme) ı döndür
            {
                if (sekme.Name == ftr.Name) //eğer sekme ismi ile form ismi aynı ise
                {
                    sekme.Activate(); //sekmeyi aktif et
                    return true; //true döndür

                }
            }

            return false; //değilse false döndür

        }

        private bool Existform(Kira kira)  //1'den fazla sekme açılmaması için 2 durum olustur(bool)
        {
            //Mdi chield anaform üzerine dışarıdan istediğimiz formları üzerine çağırabildiğimiz nesne(tarayıcı gibi düşünülebilir)
            foreach (var sekme in MdiChildren)   //Foreach döngüsü ile oluşturulan sekme değişkenine mdichildren(sekme) ı döndür  
            {
                if (sekme.Name == kira.Name)  //foreach ile sekme'nin içine attıgımız mdichieldren'in ismi kira formu ile aynı ise
                {
                    sekme.Activate(); //sekmeyi aktif et(aç)
                    return true; //dışarıya döndür
                }
            }
            return false; //degilse false döndür.Bu gereksiz sekme açılımını engelleyecektir.
        }


        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Kira kr = new Kira(); //kira formunu oluştur
            if (Existform(kr)) return; //fonksiyonla eşleşirse döndür
            kr.MdiParent = this; //mdiparent container'da
            kr.Show(); //göster
        }


        private void tarih_karsılastır()
        {

            try
            {
                
                if (label1.Text == "Demir") //Label1'deki değer Demir ise
                {
                    if (d_baglanti.State == ConnectionState.Closed) //bağlantı açık değil ise
                    {
                        d_baglanti.Open(); //bağlantıyı aç
                        SqlCommand veri = new SqlCommand("Select * from Daire" + label2.Text + "_Fatura ORDER BY ID DESC", d_baglanti); //Hangi kullanıcının son günü oldugunu bulmak için Giriş tablosunda Daire no'sunu label2 ile eşleştirdim . Son fatura tarihini almak istediğim için order by komutu kullandım
                        SqlDataReader d_read = veri.ExecuteReader(); // veriyi oku
                        if (d_read.Read()) //eğer okuuyorsa
                        {
                            maskedTextBox2.Text = d_read["Son_Tarih"].ToString(); //maskedtextbox2'ye veri tabanındaki son_tarihin değerini aktar.
                        }
                        if(maskedTextBox1.Text == maskedTextBox2.Text) //Aktarılan tarih değeri bugünün tarihi ile eşleşiyorsa
                        {
                            MessageBox.Show("Bugün Son Gün!"); 
                        }
                        /*
                        else if (int.Parse(maskedTextBox1.Text) < int.Parse(label3.Text)) //eğer bugünün tarihi , veri tabanındaki son_tarihten büyükse
                        {
                            MessageBox.Show("Geç kaldın!");
                        }
                        d_baglanti.Close(); //bağlantı kapat
                        */
                    }
                }

            }

            catch (Exception)
            {

            }

            try
            {
                if (label1.Text == "Gül")
                {
                    if (g_baglanti.State == ConnectionState.Closed)
                    {
                        g_baglanti.Open();
                        SqlCommand veri = new SqlCommand("Select * from Daire" + label2.Text + "_Fatura ORDER BY ID DESC", g_baglanti); //Hangi kullanıcının son günü oldugunu bulmak için Giriş tablosunda Daire no'sunu label2 ile eşleştirdim . Son fatura tarihini almak istediğim için de order by komutu kullandım
                        SqlDataReader g_read = veri.ExecuteReader(); //veriyi okut
                        if (g_read.Read()) //eğer veri okunuyosa
                        {
                            maskedTextBox2.Text = g_read["Son_Tarih"].ToString();  //maskedtextbox2'ye veri tabanındaki son_tarihin değerini aktar.

                        }
                        if (maskedTextBox1.Text == maskedTextBox2.Text)  //Aktarılan tarih değeri bugünün tarihi ile eşleşiyorsa
                        {
                            MessageBox.Show("Bugün Son Gün!");
                        }
                        /*
                        else if (int.Parse(maskedTextBox1.Text) < int.Parse(label3.Text))  //eğer bugünün tarihi , veri tabanındaki son_tarihten büyükse
                        {
                            MessageBox.Show("Geç kaldın!");
                        }
                        */
                        g_baglanti.Close();
                    }
                }

            }
            catch (Exception)
            {
                
            }

        }
    

        private void Ana_Form_Load(object sender, EventArgs e)
        {
            //form yüklendiğinde çalıştırılacak yordamlar
            son_gun();
            tarih_karsılastır(); 
        
        }



        private void son_gun()
        {
            DateTime d = DateTime.Now;  //Şuanki zamanı içeren datetime nesnesi oluştur

            string str = String.Format("{0:00}.{1:00}.{2:0000}", d.Day, d.Month, d.Year); //String formatın'a çevir ve değerleri değişkene at
            maskedTextBox1.Text = str; //str değişkenindeki tarih formatını maskedtextbox1'e at
            maskedTextBox2.Text = str;
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Kisisel_notlar nt = new Kisisel_notlar();
            if (Existform(nt)) return;
            nt.MdiParent = this;
            nt.Show();
        }

        private bool Existform(Kisisel_notlar nt)
        {
            foreach (var sekme in MdiChildren)
            {
                if (sekme.Name == nt.Name)
                {
                    sekme.Activate();
                    return true;
                }
            }
            return false;
        }

        private bool Existform(Grafik grfk)
        {
            foreach (var sekme in MdiChildren)
            {
                if (sekme.Name == grfk.Name)
                {
                    sekme.Activate();
                    return true;
                }
            }
            return false;
        }


        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Grafik grfk = new Grafik();
            if (Existform(grfk)) return;
            grfk.MdiParent = this;
            grfk.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime trh1 = Convert.ToDateTime(" " + maskedTextBox1.Text);
            DateTime trh2 = Convert.ToDateTime(" " + maskedTextBox2.Text);
            TimeSpan sonuc = trh2 - trh1; // Büyük tarihten küçük tarihi çıkart
            label3.Text = sonuc.ToString(); //sonucu label3'e ktar
            if (trh1 > trh2) //Bugünün tarihi , veri tabanındaki son_tarihten büyükse 
            {
                MessageBox.Show(sonuc + "Gün Önce Faturayı Yatırman Gerekiyordu! \n Geç Kaldın!");
            }
            else if (trh1 < trh2) //Bugünün tarihi , veri tabanındaki son_tarihten küçükse
            {
                MessageBox.Show(sonuc + "Gün Var.");
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            DateTime trh1 = Convert.ToDateTime(" " + maskedTextBox1.Text);
            DateTime trh2 = Convert.ToDateTime(" " + maskedTextBox2.Text);
            TimeSpan sonuc = trh2 - trh1; 
            label3.Text = sonuc.ToString();
            if (trh1 > trh2)
            {
                MessageBox.Show(sonuc + "Gün Önce Faturayı Yatırman Gerekiyordu! \n Geç Kaldın!");
            }
            else if (trh1 < trh2)
            {
                MessageBox.Show(sonuc + "Gün Var.");
            }
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBox.Show("Yazılım:Ömer Faruk Tavukçuoğlu NO:16010501074 \n Tasarım: Berkay Muratcan Yılmaz NO:160105033 , Ömer Faruk Tavukçuoğlu  ", "Projeyi Hazırlayanlar..",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Alisveris_notlar an = new Alisveris_notlar(); //butona tıklandıgında formu oluştur
            if (Existform(an)) return; //fonksiyonla eşleşirse döndür
            an.MdiParent = this; //mdiparent'ta 
            an.Show(); //göster
        }

        private bool Existform(Alisveris_notlar an)
        {
            foreach (var sekme in MdiChildren)
            {
                if (sekme.Name == an.Name)
                {
                    sekme.Activate();
                    return true;
                }
            }
            return false;
        }
    }
}
