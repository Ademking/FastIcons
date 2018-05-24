using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastIcon
{
    public partial class Form1 : Form
    {
        Icons icons;

        public Boolean pinned = false;
        public List<string> urls_list { get; set; }
        public Form1()
        {
            InitializeComponent();
            new ToolTip().SetToolTip(pictureBox6, "About FastIcons");
            new ToolTip().SetToolTip(pictureBox5, "My Facebook Profil");
            new ToolTip().SetToolTip(pictureBox7, "Contact Me");
            new ToolTip().SetToolTip(pinbutton, "Always On Top");
            new ToolTip().SetToolTip(pictureBox9, "Welcome Page");
            new ToolTip().SetToolTip(pictureBox9, "Github");

        }




        private void Form1_Load(object sender, EventArgs e)
        {
            noresultpic.Visible = false;
           TopMost = false;

        }




        // make form draggable and no maximizing
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }


            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }


        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;

        //shadow
        /*protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }*/







        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void MakeSearch(string search_query)
        {
           try
            {
                listView1.Items.Clear();
                icons = new Icons(search_query);
                icons.Download_json();
                icons.make_url();
                urls_list = new List<string>();
                urls_list = icons.urls_list;

                //Image shit
                List<string> imageList = new List<string>();

                foreach (var url in icons.urls_list)
                {
                    imageList.Add(url.ToString());
                }

                ImageList img = new ImageList();
                img.ImageSize = new Size(50, 50);
                img.ColorDepth = ColorDepth.Depth32Bit;


                for (int i = 0; i < imageList.Count; i++)
                {
                    WebClient w = new WebClient();
                    byte[] imageByte = w.DownloadData(imageList[i]);
                    MemoryStream stream = new MemoryStream(imageByte);

                    Image im = Image.FromStream(stream);
                    img.Images.Add(im);

                    listView1.Items.Add("", i);

                }
                listView1.LargeImageList = img;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error! Please Check your Internet connection and Try Again ...\n if this problem persists contact me \nademkingnew@gmail.com ", "Error" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                Console.WriteLine(exp);
                return;
            }
           
      
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_KeyUp(object sender, KeyEventArgs e)
        {
            /*e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Enter)
            {
             
                //Loading loading = new Loading();
                //loading.Show();
                loadingControl1.Visible = true;
                Application.DoEvents();
                MakeSearch(bunifuMaterialTextbox1.Text);
                loadingControl1.Visible = false;
                //loading.Close();
                e.Handled = true;


            }*/
        }


        public void Download_png_in_temp(string url)
        {
            WebClient webClient = new WebClient();
            {
                webClient.DownloadFile(new Uri(url), "picture.png");
            }
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<string> selection = new List<string>();
           
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                
                int imgIndex = item.ImageIndex;
                string url_pic = urls_list[imgIndex];
                Download_png_in_temp(url_pic);
                selection.Add(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\picture.png");
            }

            DataObject data = new DataObject(DataFormats.FileDrop, selection.ToArray());
            DoDragDrop(data, DragDropEffects.Copy);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WelcomePic.Visible = false;
            noresultpic.Visible = false;
            meow.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
                //Loading loading = new Loading();
                //loading.Show();
                loadingControl1.Visible = true;
                Application.DoEvents();
                MakeSearch(bunifuMaterialTextbox1.Text);
                loadingControl1.Visible = false;
                 //loading.Close();
                Cursor.Current = Cursors.Default;
            if (urls_list.Count == 0)
            {
                noresultpic.Visible = true;
            }


        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            About about_frm = new About();
            about_frm.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (!pinned)
            {
                pinned = true;
                TopMost = true;
                pin_status.Text = "ON";
                pinbutton.Image = FastIcon.Properties.Resources.pinned;
            }
            else
            {
                pinned = false;
                TopMost = false;
                pin_status.Text = "OFF";
                pinbutton.Image = FastIcon.Properties.Resources.not_pined;
            }
        }

        private void bunifuMaterialTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            if (e.KeyChar == (char)13)
            {
                WelcomePic.Visible = false;
                noresultpic.Visible = false;
                meow.Visible = false;
                Cursor.Current = Cursors.WaitCursor;
                //Loading loading = new Loading();
                //loading.Show();
                loadingControl1.Visible = true;
                Application.DoEvents();
                MakeSearch(bunifuMaterialTextbox1.Text);
                loadingControl1.Visible = false;

                if (urls_list.Count == 0)
                {
                    noresultpic.Visible = true;
                }

                //loading.Close();
                Cursor.Current = Cursors.Default;
                e.Handled = true;


            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://web.facebook.com/AdemKouki.Officiel");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:ademkingnew@gmail.com?subject=About%20FastIcons");
       
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
       
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
        
        }

        private void bunifuMaterialTextbox1_Leave(object sender, EventArgs e)
        {
            if(bunifuMaterialTextbox1.Text == "")
            {
                bunifuMaterialTextbox1.Text = "Search ...";
                bunifuMaterialTextbox1.ForeColor = Color.Silver;
            }


        }

        private void bunifuMaterialTextbox1_Enter(object sender, EventArgs e)
        {
            if (bunifuMaterialTextbox1.Text == "Search ...")
            {
                bunifuMaterialTextbox1.Text = "";
                bunifuMaterialTextbox1.ForeColor = Color.Black;
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged_1(object sender, EventArgs e)
        {

        }

        private void meow_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.Stream = Properties.Resources.meow1;
            player.Play();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WelcomePic.Visible = true;
            meow.Visible = true;
            noresultpic.Visible = false;
            listView1.Clear();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("__________________________\n\n         Coming Soon\n__________________________\n\nYou will be able to search for :\n\n•    Background Pictures (HD)\n•    SVG - GIF - ICO Files\n\nAnd Much More...", "Coming Soon" , MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
