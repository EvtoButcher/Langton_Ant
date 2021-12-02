﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Langton__s_Ant
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();

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

            if (timer1.Enabled) { return; }

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            comboBox1.Enabled = false;

            Resolution = (int)nudResolution.Value;
            if(Resolution == 1) 
            { 
                Resolution++; 
            }
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

            timer1.Start();
            //Fast();
        }

        private void GameStop()
        {
            if (!timer1.Enabled) { return; }

            timer1.Stop();

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
        /*
        private void Fast()
        {
            while (true)
            {
                for (int i = 0; i < Ants.Count; i++)
                {
                    Ants[i].NewPos();
                    Ants[i].NextStep();
                }
                GameDraw();
            }
        }
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled) { return; }

            if(e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / Resolution;
                var y = e.Location.Y / Resolution;

                Ants.Add(new Ant(x, y));
            }
        }
        */
    }
}
