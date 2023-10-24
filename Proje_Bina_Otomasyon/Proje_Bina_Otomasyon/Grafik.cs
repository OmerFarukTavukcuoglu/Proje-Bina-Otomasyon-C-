using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Bina_Otomasyon
{
    public partial class Grafik : Form
    {
        public Grafik()
        {
            InitializeComponent();
        }
        SqlConnection d_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Demir_APT;Integrated Security=True");
        SqlConnection g_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Gül_APT;Integrated Security=True");


        int sayac = 0; //global'de sayac değişkeni olustur

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "Demir") 
            {
                d_baglanti.Open(); //bağlantı aç
                SqlCommand veri = new SqlCommand("Select * from Daire" + textBox1.Text + "_Fatura", d_baglanti); //Girilen doğrulama bilgilerine göre Daireyi seç
                SqlDataReader oku = veri.ExecuteReader(); // veri oku
                while (oku.Read())//veri okunduğu sürece //sayaç mantığının devam edebilmesi için while komutu kullanmak gerekiyor , if komutu kullanılsaydı tek bir sorgu yapılacağından mantık hatası oluşacaktı!
                {

                    sayac = sayac + 1; //sayacı 1'er 1'er arttır


                    //oluşturulan chart1 grafiğinde seçilen tablodaki okunan verileri x-y koordinatlarında göster
                    this.chart1.Series["Elektrik"].Points.AddXY("Fatura" + sayac, oku["Elektrik"]); 
                    this.chart1.Series["Su"].Points.AddXY("Fatura", oku["Su"].ToString());
                    this.chart1.Series["Doğalgaz"].Points.AddXY("Fatura", oku["Doğalgaz"].ToString());
                    this.chart1.Series["Telefon"].Points.AddXY("Fatura", oku["Telefon"].ToString());
                    this.chart1.Series["Internet"].Points.AddXY("Fatura", ("Internet").ToString());
                }
                d_baglanti.Close(); //bağlantı kapat
                simpleButton1.Enabled = false; //aktifliği kapat
            }


            else if (comboBoxEdit1.Text == "Gül")
            {
                g_baglanti.Open();
                SqlCommand veri = new SqlCommand("Select * from Daire" + textBox1.Text + "_Fatura", g_baglanti);  //Girilen doğrulama bilgilerine göre Daireyi seç
                SqlDataReader oku = veri.ExecuteReader(); //verileri oku
                while (oku.Read())//veriler okunduğu sürece
                {

                    sayac = sayac + 1; //sayacı 1'er 1ER arttır


                    //oluşturulan chart1 grafiğinde seçilen tablodaki okunan verileri x-y koordinatlarında göster
                    this.chart1.Series["Elektrik"].Points.AddXY("Fatura" + sayac, oku["Elektrik"]);
                    this.chart1.Series["Su"].Points.AddXY("Fatura", oku["Su"].ToString());
                    this.chart1.Series["Doğalgaz"].Points.AddXY("Fatura", oku["Doğalgaz"].ToString());
                    this.chart1.Series["Telefon"].Points.AddXY("Fatura", oku["Telefon"].ToString());
                    this.chart1.Series["Internet"].Points.AddXY("Fatura", ("Internet").ToString());
                }
                g_baglanti.Close();
                simpleButton1.Enabled = false;
            }

        }



        private void dogrulama()
        {
            //Kullanıcı Doğrulama panel//
            if (comboBoxEdit1.Text == "Demir")
            {
                d_baglanti.Open();
                SqlCommand veri = new SqlCommand("Select * from d_giris where Bina_ad =@ad AND Sifre=@sifre AND Daire_no=@daire AND Kullanici_ad=@kullanici", d_baglanti);
                SqlParameter p1 = new SqlParameter("@ad", comboBoxEdit1.Text);
                SqlParameter p2 = new SqlParameter("@daire", textBox1.Text);
                SqlParameter p3 = new SqlParameter("@kullanici", textBox2.Text);
                SqlParameter p4 = new SqlParameter("@sifre", textBox3.Text);
                veri.Parameters.Add(p1);
                veri.Parameters.Add(p2);
                veri.Parameters.Add(p3);
                veri.Parameters.Add(p4);
                SqlDataReader oku = veri.ExecuteReader();
                if(oku.Read())
                {
                    MessageBox.Show("Doğrulama Başarılı");
                    simpleButton1.Enabled = true; //göster butonunu aktifleştir
                    simpleButton1.Left = 600; //butonu ortala
                    d_gizle(); //dogrulama gizle
                    chart1.Left = 125;    //Tabloyu ortala   
                }
                else
                {
                    MessageBox.Show("Hatalı Giris");
                }

                d_baglanti.Close(); 
        
            }
           

            if (comboBoxEdit1.Text == "Gül")
            {
                g_baglanti.Open();
                SqlCommand veri = new SqlCommand("Select * from g_giris where Bina_ad =@ad AND Sifre=@sifre AND Daire_no=@daire AND Kullanici_ad=@kullanici", g_baglanti);
                SqlParameter p1 = new SqlParameter("@ad", comboBoxEdit1.Text);
                SqlParameter p2 = new SqlParameter("@daire", textBox1.Text);
                SqlParameter p3 = new SqlParameter("@kullanici", textBox2.Text);
                SqlParameter p4 = new SqlParameter("@sifre", textBox3.Text);
                veri.Parameters.Add(p1);
                veri.Parameters.Add(p2);
                veri.Parameters.Add(p3);
                veri.Parameters.Add(p4);
                SqlDataReader oku = veri.ExecuteReader();
                if (oku.Read())
                {
                    MessageBox.Show("Doğrulama Başarılı");
                    simpleButton1.Enabled = true; //göster butonunu aktifleştir
                    simpleButton1.Left = 600; //butonu ortala
                    d_gizle(); //dogrulama gizle
                    chart1.Left = 125; //tabloyu ortala
                }
                else
                {
                    MessageBox.Show("Hatalı Giris");
                }
                g_baglanti.Close();
               
            }
        }

        private void d_gizle()
        {
            //doğrulama gizle//
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            comboBoxEdit1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            simpleButton2.Visible = false;
        }
       

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dogrulama();
            
             
        }

        private void Grafik_Load(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


        /*
                */





    }
    }

