using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Bina_Otomasyon
{
    public partial class Alisveris_notlar : Form
    {
        public Alisveris_notlar()
        {
            InitializeComponent();
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); //richtextbox'ı temizle
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //eğer açılan pencere diyalogçözümü ile aynı ise
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,//dosya ismi yükle
                     RichTextBoxStreamType.RichText);  //richtext akıs türü
                }
                catch
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,
                     RichTextBoxStreamType.PlainText);
                }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = ".txt|.txt"; //kayıt ederken filtrele(uzantı)
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName,
                 RichTextBoxStreamType.RichText);
                if (saveFileDialog1.FileName == "") //eğer kayıt işlemi yaparken isimlendirme boş ise mesaj ekranı aç
                {
                    MessageBox.Show("İsimlendirme gerekli"); //mesaj kutusu ver
                }

            }
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog(); //sayfayı yazdır
        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo(); //geri al
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut(); //kes
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy(); //kopyala
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();  //yapıştır
        }

        private void saatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString());  //Şuanki tarihi ve zamanını al     
        }

        private void yazıTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog(); //yazı tipi penceresini aç
            richTextBox1.SelectionFont = fontDialog1.Font;  //seçilen yazı tipini uygula          
        }

        private void Alisveris_notlar_Load(object sender, EventArgs e)
        {

        }
    }
}
