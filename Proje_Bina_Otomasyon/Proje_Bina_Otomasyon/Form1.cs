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
using System.Threading;


namespace Proje_Bina_Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection d_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Demir_APT;Integrated Security=True");
        SqlConnection g_baglanti = new SqlConnection(@"Data Source=OMAR\OMAR;Initial Catalog=Gül_APT;Integrated Security=True");
        //sql bağlantılarını oluştur

        Ana_Form transition = new Ana_Form();
        public string de; //global değişken oluştur
        public void giris()
        {
            if (comboBoxEdit1.Text == "Demir") //eğer Demir Seçili ise
            {
                d_baglanti.Open(); //bağlantıyı aç
                SqlCommand veri = new SqlCommand("Select * from d_giris where Bina_ad =@ad AND Sifre=@sifre AND Daire_no=@daire AND Kullanici_ad=@kullanici", d_baglanti); //d_giris tablosundaki verileri oluşturulan parametrelerle eşleştir
                SqlParameter p1 = new SqlParameter("@ad", comboBoxEdit1.Text); //nesnelere parametre ata
                SqlParameter p2 = new SqlParameter("@daire", textBox1.Text);
                SqlParameter p3 = new SqlParameter("@kullanici", textBox2.Text);
                SqlParameter p4 = new SqlParameter("@sifre", textBox3.Text);
                veri.Parameters.Add(p1); //Atanan değerleri veri parametresine aktar
                veri.Parameters.Add(p2);
                veri.Parameters.Add(p3);
                veri.Parameters.Add(p4);
                SqlDataReader oku = veri.ExecuteReader();  // oku
                if (oku.Read()) //eğer okuma işlemi gerçekleşiyorsa
                {
                    MessageBox.Show("Giriş Başarılı. Hoşgeldiniz" + " " + textBox2.Text);
                    transition.label1.Text = comboBoxEdit1.Text; //anaformdaki label1'e bina ad
                    transition.label2.Text = textBox1.Text;    //anaformdaki label2'ye daire no 
                    transition.Show(); //form göster
                    this.Hide(); //bu formu kapat
                }
                else //değilse
                {
                    MessageBox.Show("Hatalı Giriş");
                }
                d_baglanti.Close(); //bağlantıyı kapat
            }
            else if (comboBoxEdit1.Text == "Gül") //eğer combobox'ta gül seçiliyse
            {

                g_baglanti.Open();
                SqlCommand g_veri = new SqlCommand("Select * from g_giris where Bina_ad =@ad AND Sifre=@sifre AND Daire_no=@daire AND Kullanici_ad=@kullanici", g_baglanti);  //g_giris tablosundaki verileri oluşturulan parametrelerle eşleştir
                SqlParameter g_bina = new SqlParameter("@ad", comboBoxEdit1.Text); // g_bina isimli parametre değişkeni oluştur.@ad parametresini comboboxedit1 ile eşleştir
                SqlParameter g_daire = new SqlParameter("@daire", textBox1.Text);
                SqlParameter g_kullanici = new SqlParameter("@kullanici", textBox2.Text);
                SqlParameter g_sifre = new SqlParameter("@sifre", textBox3.Text);
                g_veri.Parameters.Add(g_bina); //oluşturulan komuta parametreleri ekle
                g_veri.Parameters.Add(g_daire);
                g_veri.Parameters.Add(g_kullanici);
                g_veri.Parameters.Add(g_sifre);
                SqlDataReader read = g_veri.ExecuteReader(); //oku
                if (read.Read()) //eğer okuma işlemi gerçekleşiyorsa
                {
                    MessageBox.Show("Giriş Başarılı. Hoşgeldiniz" + " " + textBox2.Text);
                    transition.label1.Text = comboBoxEdit1.Text;  //anaformdaki label1'ye bina ad
                    transition.label2.Text = textBox1.Text; //anaformdaki label2'ye daire no
                    transition.Show(); //formu aç
                    this.Hide(); //açık olan formu kapat
                }
                else //diğer durumda
                {
                    MessageBox.Show("Hatalı Giriş!");
                }
                g_baglanti.Close(); //bağlantıyı kapat
            }
            if (comboBoxEdit1.Text == "" && textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "") //eğer comboboxedit1 , textbox1 , 2, 3 boş ise uyarı mesajı ver
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz!");
                if (comboBoxEdit1.Text == "") //sadece comboboxedit1 boşsa
                {
                    comboBoxEdit1.Focus(); //comboboxedit1 ' odaklan
                }

                else if (textBox1.Text == "") //sadece textbox1 boşsa
                {
                    textBox1.Focus(); //""
                }

                else if (textBox2.Text == "") //sadece textbox2 boşsa
                {
                    textBox2.Focus(); // ""
                }

                else if (textBox3.Text == "") //sadece textbox3 boşsa
                {
                    textBox3.Focus(); //""
                }

            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            for(int i=0; i<10; i++)
            {
                Thread.Sleep(400);
            }
            */
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            giris(); //butona tıklama işlemi gerçekleştiğinde giris yordamını çağır

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) //eğer textbox1 üzerindeyken enter tuşuna basıldıysa
            {
                simpleButton1_Click(simpleButton1, new EventArgs()); //Butona basma işlemini aktif et
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                simpleButton1_Click(simpleButton1, new EventArgs());
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1_Click(simpleButton1, new EventArgs());
            }
        }


        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "Demir") //if comboboxedit1 demir stringine eşitse
            {
                d_baglanti.Open(); //bağlantı aç
                SqlCommand data = new SqlCommand("SELECT * FROM d_giris ORDER BY ID DESC", d_baglanti); //Veri tabanından giris tablosunu çek
                SqlDataReader read = data.ExecuteReader(); // çekilen veriyi oku
                while (read.Read()) //okuma işlemi gerçekleşiyorsa
                {
                    notifyIcon1.ShowBalloonTip(1000, "Varsayılan Bina:" + read["Bina_ad"].ToString() + "\n" + "Varsayılan Daireler:" + read["Daire_no"],  "\n" + "Örnek Giriş:  Daire:Demir" + "\n" + "Daire_No:1" + "\n" + "Kullanıcı Adı:Ali" + "\n" + "Şifre" + "123" , ToolTipIcon.Info); //sağ altta bildirim olarak okunan verileri göster
                    notifyIcon1.Visible = false; //görünürlük kapalı
                }
                d_baglanti.Close(); //kapa
            }

           else if (comboBoxEdit1.Text == "Gül")  //if comboboxedit1 demir stringine eşitse
            {
                g_baglanti.Open(); //bağlantı aç
                SqlCommand data = new SqlCommand("SELECT * FROM g_giris ORDER BY ID DESC", g_baglanti); //Veri tabanından giris tablosunu çek
                SqlDataReader read = data.ExecuteReader();  // çekilen veriyi oku
                while (read.Read()) //okuma işlemi gerçekleşiyorsa
                {
                   
                    notifyIcon1.ShowBalloonTip(1000, "Varsayılan Bina:" + read["Bina_ad"].ToString() + "\n" + "Varsayılan Daireler:" + read["Daire_no"], "Örnek Giriş: Daire: Gül" + "\n" + "Daire_No: 1" + "\n" + "Kullanıcı Adı: Aylin" + "\n" + "Şifre" + "123" , ToolTipIcon.Info); //sağ altta bildirim olarak okunan verileri göster
                    notifyIcon1.Visible = false;
                }
                g_baglanti.Close(); //kapa
            }

        }
    }
}
