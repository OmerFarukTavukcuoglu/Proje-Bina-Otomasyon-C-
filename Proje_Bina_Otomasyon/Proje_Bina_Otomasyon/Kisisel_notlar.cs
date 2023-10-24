using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proje_Bina_Otomasyon
{
    public partial class Kisisel_notlar : Form
    {

        public Kisisel_notlar()
        {
            InitializeComponent();
        }

        private void Kisisel_notlar_Loads(object sender, EventArgs e)
        {

        }


        private void yazıTipiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Undo(); //geri al
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "|.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) //eğer açılan pencere diyalogçözümü ile aynı ise
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName,//dosya ismi yükle
                 RichTextBoxStreamType.RichText);//richtext akıs türü
                if (saveFileDialog1.FileName == "") //eğer kayıt işlemi yaparken isimlendirme boş ise mesaj ekranı aç
                {
                    MessageBox.Show("İsimlendirme gerekli");
                }
           
            }
        }

        private void kopyalaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Copy(); //kopyala
        }

        private void saatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut(); //kes
        }

        private void yapıştırToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Paste(); //yapıştır
        }

        private void saatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show(DateTime.Now.ToString()); //Şimdiki tarihi al
        }

        private void yazdırToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            printDialog1.ShowDialog(); //yazdırma menüsünü aç
        }

        private void yazıTipiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog(); //yazı tipi menüsünü aç
            richTextBox1.SelectionFont = fontDialog1.Font; //Seçilen yazı tipini uygula
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear(); //richtextbox'ı temizle
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (openFileDialog1.ShowDialog() == DialogResult.OK) //eğer açılan pencere diyalogçözümü ile aynı ise
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,
                     RichTextBoxStreamType.RichText);
                }
                catch
                { 
                        richTextBox1.LoadFile(openFileDialog1.FileName,
                         RichTextBoxStreamType.PlainText);              
                }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
      
        }
    }
}

