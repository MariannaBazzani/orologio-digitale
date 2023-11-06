using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace progetto_info 
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(Orologio);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void Orologio(object sender, EventArgs e)
        {
            lblData.Location = new Point(ClientSize.Width / 2 - (lblData.Width / 2), ClientSize.Height / 2 - 30);

            Graphics orologio = this.CreateGraphics();
            int X_centro = ClientSize.Width / 2;
            int Y_centro = ClientSize.Height / 2;
            int raggio = Math.Min(ClientSize.Height, ClientSize.Width) / 2 - 40;
            orologio.FillEllipse(Brushes.Black, X_centro - raggio - 11, Y_centro - raggio - 11, 2 * raggio + 22, 2 * raggio + 22);
            orologio.FillEllipse(Brushes.DarkGreen, X_centro - raggio - 10, Y_centro - raggio - 10, 2 * raggio + 20, 2 * raggio + 20);
            orologio.FillEllipse(Brushes.Black, X_centro - raggio - 1, Y_centro - raggio - 1, 2 * raggio + 2, 2 * raggio + 2);
            orologio.FillEllipse(Brushes.LightGray, X_centro - raggio, Y_centro - raggio, 2 * raggio, 2 * raggio);

            for (int i = 0; i < 12; i++)
            {
                double punti_angolo = i * Math.PI / 6;
                double punti_centro_X = X_centro - (raggio * 0.95 * Math.Sin(punti_angolo));
                double punti_centro_Y = Y_centro - (raggio * 0.95 * Math.Cos(punti_angolo));
                orologio.FillEllipse(Brushes.Black, (float)punti_centro_X - 4, (float)punti_centro_Y - 4, 8, 8);
            }
            for (int i = 0; i < 60; i++)
            {
                double punti_angolo = i * Math.PI / 30;
                double punti_centro_X = X_centro - raggio * 0.95 * Math.Sin(punti_angolo);
                double punti_centro_Y = Y_centro - raggio * 0.95 * Math.Cos(punti_angolo);
                orologio.FillRectangle(Brushes.Black, (float)punti_centro_X - 1, (float)punti_centro_Y - 1, 2, 2);
            }

            for (int i = 1; i <= 12; i++)
            {
                double numeri_angolo = - i * Math.PI / 6;
                double numeri_centro_X = X_centro - raggio * 0.82 * Math.Sin(numeri_angolo);
                double numeri_centro_Y = Y_centro - raggio * 0.82 * Math.Cos(numeri_angolo);

                Font font = new Font("Arial", 16);
                Brush brush = new SolidBrush(Color.Black);
                int numeri_X;
                int numeri_Y;
                if (i < 10)
                {
                    numeri_X = (int)numeri_centro_X - 10;
                    numeri_Y = (int)numeri_centro_Y - 10;
                } else
                {
                    numeri_X = (int)numeri_centro_X - 15;
                    numeri_Y = (int)numeri_centro_Y - 15;
                }
                Point labelLocation = new Point(numeri_X, numeri_Y);
                orologio.DrawString(i.ToString(), font, brush, labelLocation);
            }

            int h = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;

            lancette(orologio, X_centro, Y_centro, raggio * 0.55, (h + min / 60.0) * 30, Pens.Black, (float)3.5);
            lancette(orologio, X_centro, Y_centro, raggio * 0.85, (min + sec / 60.0) * 6, Pens.Black, (float)3.5);
            lancette(orologio, X_centro, Y_centro, raggio * 0.95, sec * 6, Pens.Orange, 2);
        }

        private void lancette(Graphics orologio, int X_centro, int Y_centro, double length, double ampiezza, Pen pen, float size)
        {
            double angolo_rad = Math.PI / 180 * ampiezza;
            int Coordinata_x = (int)(X_centro + length * Math.Sin(angolo_rad));
            int Coordinata_y = (int)(Y_centro - length * Math.Cos(angolo_rad));
            orologio.DrawLine(new Pen(pen.Color, size), X_centro, Y_centro, Coordinata_x, Coordinata_y);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 1000;
            lblData.Text = DateTime.Now.ToString();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string imagePath = Path.Combine(Application.StartupPath, "..\\..\\..\\background.jpg");
            this.BackgroundImage = Image.FromFile(imagePath);
            this.BackgroundImageLayout = ImageLayout.Stretch;    
        }
        
    }
}