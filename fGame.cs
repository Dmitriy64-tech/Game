using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessTune
{
    public partial class fGame : Form
    {
        Random rnd = new Random();
        int durationMusic = Victorina.musicDuration;
        public fGame()
        {
            InitializeComponent();
        }
        void MakeMusic()
        {
            if (Victorina.list.Count == 0)
            {
                EndGame();
            }
            else
            {
                durationMusic = Victorina.musicDuration;
                int i = rnd.Next(0, Victorina.list.Count);
                WMP.URL = Victorina.list[i];
                Victorina.list.RemoveAt(i);
                lblMelodyCount.Text = Victorina.list.Count.ToString();
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            timer1.Start();
            MakeMusic();
        }

        private void WMP_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.pause();
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.play();
            timer1.Start();
        }

        private void fGame_Load(object sender, EventArgs e)
        {
            lblMelodyCount.Text = Victorina.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Maximum = Victorina.gameDuration;

        }
        void EndGame()
        {
            timer1.Stop();
            WMP.Ctlcontrols.stop();
            if (Convert.ToInt32(labelCounter1.Text) > Convert.ToInt32(labelCounter2.Text))
            {
                MessageBox.Show("Победил Игрок 1", "Игра окончена!", MessageBoxButtons.OK);
            }
            if (Convert.ToInt32(labelCounter1.Text) < Convert.ToInt32(labelCounter2.Text))
            {
                MessageBox.Show("Победил Игрок 2", "Игра окончена!", MessageBoxButtons.OK);
            }
            if(Convert.ToInt32(labelCounter1.Text) == Convert.ToInt32(labelCounter2.Text))
            {
                MessageBox.Show("Ничья!", "Игра окончена!", MessageBoxButtons.OK);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            durationMusic--;
            lbTimeRem.Text = durationMusic.ToString();
            if (progressBar1.Value == progressBar1.Maximum)
            {
                EndGame();
                return;
            }
            if (durationMusic == 0) MakeMusic();
        }
        void GamePause()
        {
            timer1.Stop();
            WMP.Ctlcontrols.pause();
        }
        void GamePlay()
        {
            timer1.Start();
            WMP.Ctlcontrols.play();
        }

        private void fGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.A)
            {
                GamePause();
                Message fm = new Message();
                fm.lblMessage.Text = "Игрок1";
                if (fm.ShowDialog() == DialogResult.Yes)
                {
                    labelCounter1.Text = Convert.ToString(Convert.ToInt32(labelCounter1.Text) + 1);
                }
                else
                {
                    GamePlay();
                }
            }

            if (e.KeyData == Keys.P)
            {
                GamePause();
                Message fm = new Message();
                fm.lblMessage.Text = "Игрок2";
                if (fm.ShowDialog() == DialogResult.Yes)
                {
                    labelCounter2.Text = Convert.ToString(Convert.ToInt32(labelCounter2.Text) + 1);
                }
                else
                {
                    GamePlay();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            EndGame();
        }

        private void fGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }
        private void fGame_FormClosing(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void WMP_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (Victorina.randomStart)
            {
                if (WMP.openState == WMPLib.WMPOpenState.wmposMediaOpen)
                {
                    WMP.Ctlcontrols.currentPosition = rnd.Next(0, (int)WMP.currentMedia.duration / 2);
                }
            }
        }
    }
}
