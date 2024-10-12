using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CryptOX
{
    public partial class liston : Form
    {
       
        public liston()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ipx = textBox1.Text;
            Properties.Settings.Default.username = textBox3.Text;
            Properties.Settings.Default.porrt =int.Parse(textBox2.Text);
            Properties.Settings.Default.Save();
            Form1 otherForm = new Form1();
            otherForm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                Image originalImage = Image.FromFile(filePath);

                // Resize the image to a specific height and width
                int newWidth = 61;
                int newHeight = 50;
                Bitmap resizedImage = new Bitmap(originalImage, newWidth, newHeight);

       
                string base64String = ImageToBase64(resizedImage, ImageFormat.Jpeg);

                pictureBox1.Image = resizedImage;
                Properties.Settings.Default.photb64 = base64String;
                Properties.Settings.Default.Save();
            }
        }
        private string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
