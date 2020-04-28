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
    public partial class fParam : Form
    {
        public fParam()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Victorina.allDirectories = cbAllDir.Checked;
            Victorina.gameDuration = Convert.ToInt32(cbGameDuration.Text);
            Victorina.musicDuration = Convert.ToInt32(cbMusicDuration.Text);
            Victorina.randomStart = cbRandomStart.Checked;
            Victorina.WriteParam();
            this.Hide();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Set();   
            this.Hide();
        }

        private void btCooseDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                if(cbAllDir.Checked == true)
                {
                    string[] music_list = System.IO.Directory.GetFiles(fbd.SelectedPath, "*.mp3", System.IO.SearchOption.AllDirectories);
                    Victorina.lastFolder = fbd.SelectedPath;
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(music_list);
                    Victorina.list.Clear();
                    Victorina.list.AddRange(music_list);
                }
                else
                {
                    string[] music_list = System.IO.Directory.GetFiles(fbd.SelectedPath, "*.mp3", System.IO.SearchOption.TopDirectoryOnly);
                    Victorina.lastFolder = fbd.SelectedPath;
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(music_list);
                    Victorina.list.Clear();
                    Victorina.list.AddRange(music_list);
                }
            }
        }

        private void btClearList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
        void Set()
        {
            cbAllDir.Checked = Victorina.allDirectories;
            cbGameDuration.Text = Victorina.gameDuration.ToString();
            cbMusicDuration.Text = Victorina.musicDuration.ToString();
            cbRandomStart.Checked = Victorina.randomStart;
        }
        private void fParam_Load(object sender, EventArgs e)
        {
            Set();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Victorina.list.ToArray());
        }
    }
}
