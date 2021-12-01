using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Langton__s_Ant
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Ant> Ants;
        private int Resolution;
        private int Density;
       
        public Form1()
        { 
            InitializeComponent();
        }
        
        public void GameStart()
        {
            if (timer1.Enabled) { return; }

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;

            Resolution = (int)nudResolution.Value;
            Density = (int)nudDensity.Value;

            Ants = new List<Ant>();

            for (int i = 0; i < Density; i++)
            {
                Ants.Add(new Ant(pictureBox1.Height / Resolution, pictureBox1.Width / Resolution));
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            
            timer1.Start();
        }

        private void GameStop()
        {
            if (!timer1.Enabled) { return; }
             
            timer1.Stop();

            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
        }
 
        private void GameDraw()
        {

            graphics.Clear(Color.Black);

            var Fild = Ant.GetFildCopy();

            for (int x = 0; x < Ant.GetCols(); x++)
            {
                for (int y = 0; y < Ant.GetRows(); y++)
                {
                    if (Fild[x, y])
                    {
                        graphics.FillRectangle(Brushes.Goldenrod, x * Resolution, y * Resolution, Resolution - 1, Resolution - 1);
                    }
                }
                /*
                for (int i = 0; i < Ants.Count; i++)
                {
                    graphics.FillRectangle(Brushes.White, Ants[i].GetAntPos().X * Resolution, Ants[i].GetAntPos().Y * Resolution, Resolution - 1, Resolution - 1);
                }
                */
            }
            pictureBox1.Refresh();
        }
        
        private void bStart_Click(object sender, EventArgs e)
        {
            GameStart();            
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            GameStop();
        }

        private void timer1_Tick(object sender, EventArgs e)
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
