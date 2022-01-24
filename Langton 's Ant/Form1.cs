using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Langton__s_Ant
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();

        private Thread thread;
        private bool flag = false;

        private int Resolution;
        private int Density;
        private bool StartLocation;

        private Color color;

        private List<Brush> Colors; 
        private List<Ant> Ants;

        private Graphics graphics;

        public Form1()
        { 
            InitializeComponent();
        }
        
        public void GameStart()
        {
            if (flag) { return; }

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            comboBox1.Enabled = false;

            Resolution = (int)nudResolution.Value;
            Density = (int)nudDensity.Value;
            StartLocation = Convert.ToBoolean(comboBox1.SelectedIndex);

            color = new Color();

            Colors = new List<Brush>();
            Ants = new List<Ant>();

            for (int i = 0; i < Density; i++)
            {
                Ants.Add(new Ant(pictureBox1.Height / Resolution, pictureBox1.Width / Resolution, StartLocation));

                color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                Colors.Add(new SolidBrush(color));
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.Black);

            flag = true;
            thread.Start();
        }

        private void GameStop()
        {
            if (!flag) { return; }

            flag = false;

            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
            comboBox1.Enabled = true;
        }
 
        private void GameDraw()
        {
            var Fild = Ant.GetFildCopy();

            for (int i = 0; i < Ants.Count; i++)
            {
                if (!Fild[Ants[i].GetOldAntPos().X, Ants[i].GetOldAntPos().Y])
                {
                    graphics.FillRectangle(Colors[i], Ants[i].GetOldAntPos().X * Resolution, Ants[i].GetOldAntPos().Y * Resolution, Resolution - 1, Resolution - 1);
                }
                else
                {
                    graphics.FillRectangle(Brushes.Black, Ants[i].GetOldAntPos().X * Resolution, Ants[i].GetOldAntPos().Y * Resolution, Resolution - 1, Resolution - 1);
                }
            }
            pictureBox1.Refresh();
        }
        
        private void bStart_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            thread = new Thread(
                delegate() 
                {
                    AtntStep();
                });

            GameStart();            
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            GameStop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private void AtntStep()
        {
            while (flag)
            {
                for (int i = 0; i < Ants.Count; i++)
                {
                    Ants[i].NewPos();
                    Ants[i].NextStep();
                }
                GameDraw();
            }
        }

    }
}
