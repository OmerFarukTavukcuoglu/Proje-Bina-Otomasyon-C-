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
using DevExpress.XtraEditors.ButtonPanel;

namespace Proje_Bina_Otomasyon
{
    public partial class Fatura : Form
    {
        public Fatura()
        {
            InitializeComponent();
        }
        Form1 giris = new Form1();

        SqlConnection d_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Demir_APT;Integrated Security=True");
        SqlConnection g_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Gül_APT;Integrated Security=True");

        //sql baglantilarını ekle

        Ana_Form transition = new Ana_Form(); //anaform ile bağlantı kur

        int toplam = 0;
        int t1, t2, t3, t4, t5;

        private void top()
        {
            //Faturanın toplam tutarını hesaplamak için nesnelere girilen değerleri topla
            t1 = int.Parse(textBox4.Text);
            t2 = int.Parse(textBox5.Text);
            t3 = int.Parse(textBox6.Text);
            t4 = int.Parse(textBox7.Text);
            t5 = int.Parse(textBox8.Text);
            toplam =  t1 + t2 + t3 + t4 + t5;
            label20.Text = toplam.ToString();
        }

        public bool f_kilit_d; //global bool değişkeni tanımla

        public void dogrulama()
        {
            //Kullanıcının güvenliği için doğrulama işlemi gerçekleştir
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
                if (oku.Read()) //eğer okuma başarılıysa
                {
                    MessageBox.Show("Doğrulama Başarılı" + " " + textBox2.Text);
                    dogrulama_kilit(); //doğrulama panelini göz önünden kaldır
                    textBox4.Focus(); //textbox4'e odaklan
                    fatura_kilit_ac(); //fatura'nın kilidini aç
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş");
                }
                d_baglanti.Close();
            }
            else if (comboBoxEdit1.Text == "Gül")
            {

                g_baglanti.Open();
                SqlCommand g_veri = new SqlCommand("Select * from g_giris where Bina_ad =@ad AND Sifre=@sifre AND Daire_no=@daire AND Kullanici_ad=@kullanici", g_baglanti);
                SqlParameter g_bina = new SqlParameter("@ad", comboBoxEdit1.Text);
                SqlParameter g_daire = new SqlParameter("@daire", textBox1.Text);
                SqlParameter g_kullanici = new SqlParameter("@kullanici", textBox2.Text);
                SqlParameter g_sifre = new SqlParameter("@sifre", textBox3.Text);
                g_veri.Parameters.Add(g_bina);
                g_veri.Parameters.Add(g_daire);
                g_veri.Parameters.Add(g_kullanici);
                g_veri.Parameters.Add(g_sifre);
                SqlDataReader read = g_veri.ExecuteReader();
                if (read.Read())
                {
                    MessageBox.Show("Doğrulama Başarılı" + textBox2.Text);
                    dogrulama_kilit();
                    textBox4.Focus();
                    fatura_kilit_ac();
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş!");
                }
                g_baglanti.Close();
            }
        }

        
        public void fatura_kilit()
        {
            //Fatura nesnelerini kilitle

            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            maskedTextBox1.Enabled = false;
            simpleButton1.Enabled = false;
            f_kilit_d = false;
        }


        public void fatura_kilit_ac()
        {

            //Fatura nesnelerini aktifleştir

            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            simpleButton1.Enabled = true;
            maskedTextBox1.Enabled = true;
            f_kilit_d = true;
           
        }


        private void dogrulama_kilit()
        {
            //doğrulama panelinin görünürlüğünü kapat
            comboBoxEdit1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            label24.Visible = false;
            label23.Visible = false;
            label22.Visible = false;
            label21.Visible = false;
            groupBox1.Left = 195; //sayfayı ortala

        }


        private void dogrulama_kilit_ac()
        {
            comboBoxEdit1.Enabled = true; 
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            dogrulama(); //Buton1'e tıklandığında doğrulama fonksiyonunu gerçekleştir
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label15.Text = textBox4.Text + "TL";

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {

                textBox5.Clear();
                textBox4.Focus();
                MessageBox.Show("Lütfen Boş Geçmeyin!");
            }
            else
                label16.Text = textBox5.Text + "TL";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Lütfen Boş Geçmeyin!");
                textBox6.Clear();
                textBox5.Focus();
            }

            else
                label17.Text = textBox6.Text + "TL";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Lütfen Boş Geçmeyin!");
                textBox7.Clear();
                textBox6.Focus();
            }

            else
                label18.Text = textBox7.Text + "TL";
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Lütfen Boş Geçmeyin!");
                textBox8.Clear();
                textBox7.Focus();
            }
            else
                label19.Text = textBox8.Text + "TL";
            top(); //toplama fonksiyonunu çağır
        }

        private void simpleButton2_MouseHover(object sender, EventArgs e)
        {
            if(f_kilit_d == true) //eğer f_kilit_d true ise
            {
                //Butonun üzerine geldiğinde Tarih'in nasıl olması gerektiği hakkında kullanıcıyı bilgilendir
                MessageBox.Show("Tarihi: Gün/Ay/Yıl Şeklinde Giriniz!!!","Dikkat",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else //diğer durumda
            {
                MessageBox.Show("Önce kilidi aç");
            }
        }



        private void Fatura_Load(object sender, EventArgs e)
        {
            //Fatura formu yüklendiğinde
            fatura_kilit(); //Faturayı kilitle
            maskedTextBox1.Mask = "00/00/0000"; //Fatura tarihini biçimlendir(veri tabanına doğru eklenmesi için)
            simpleButton2.BackColor = Color.Transparent; //simplebuton2'nin rengini şeffaf yap
            simpleButton2.LookAndFeel.UseDefaultLookAndFeel = true;
            simpleButton2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat; //Buton Stilini ultra flat yap
        }      

        

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //FATURA KAYIT EKLE//

            if (comboBoxEdit1.Text == "Demir")
            {
                d_baglanti.Open();
                SqlCommand ekle = new SqlCommand("Insert Into Daire" + textBox1.Text + "_Fatura(Elektrik,Su,Doğalgaz,Telefon,Internet,Toplam,Eklenme_Tarih,Son_Tarih) Values(@elektrik,@su,@dogal,@tel,@internet,@toplam,@eklenme_t,@son_t)", d_baglanti);
                ekle.Parameters.AddWithValue("@elektrik", textBox4.Text);
                ekle.Parameters.AddWithValue("@su", textBox5.Text);
                ekle.Parameters.AddWithValue("@dogal", textBox6.Text);
                ekle.Parameters.AddWithValue("@tel", textBox7.Text);
                ekle.Parameters.AddWithValue("@internet", textBox8.Text);
                ekle.Parameters.AddWithValue("@toplam", label20.Text);
                ekle.Parameters.AddWithValue("@eklenme_t", DateTime.Now);
                ekle.Parameters.AddWithValue("@son_t", maskedTextBox1.Text);
                ekle.ExecuteNonQuery();
                MessageBox.Show("Başarılı");
                d_baglanti.Close();
            }
            else if(comboBoxEdit1.Text == "Gül")
            {
                g_baglanti.Open();
                SqlCommand ekle = new SqlCommand("Insert Into Daire" + textBox1.Text + "_Fatura(Elektrik,Su,Doğalgaz,Telefon,Internet,Toplam,Eklenme_Tarih,Son_Tarih) Values(@elektrik,@su,@dogal,@tel,@internet,@toplam,@eklenme_t,@son_t)", g_baglanti);
                ekle.Parameters.AddWithValue("@elektrik", textBox4.Text);
                ekle.Parameters.AddWithValue("@su", textBox5.Text);
                ekle.Parameters.AddWithValue("@dogal", textBox6.Text);
                ekle.Parameters.AddWithValue("@tel", textBox7.Text);
                ekle.Parameters.AddWithValue("@internet", textBox8.Text);
                ekle.Parameters.AddWithValue("@toplam", label20.Text);
                ekle.Parameters.AddWithValue("@eklenme_t", DateTime.Now);
                ekle.Parameters.AddWithValue("@son_t", maskedTextBox1.Text);
                ekle.ExecuteNonQuery();
                MessageBox.Show("Başarılı");
               
                g_baglanti.Close();
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
        /*
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label16.Text = textBox2.Text;
            if(textBox1.Text == "")
            {
                MessageBox.Show("Elektrik Faturasını Boş Bırakma");
                textBox2.Clear();
                textBox1.Focus();
            }
         
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label17.Text = textBox3.Text;
            if (textBox2.Text == "")
            {
                MessageBox.Show("Su FaturasınıBoş Bırakma");
                textBox3.Clear();
                textBox2.Focus();              
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label18.Text = textBox4.Text;
            if (textBox3.Text == "")
            {
                MessageBox.Show("Doğalgaz Boş Bırakma");
                textBox4.Clear();
                textBox3.Focus();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label19.Text = textBox5.Text;
            top();
        }
        */
    }
}
