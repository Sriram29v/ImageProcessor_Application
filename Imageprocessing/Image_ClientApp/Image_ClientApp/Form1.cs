using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Image_ClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TcpClientChannel objclientTcp = null;
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                    Image dst = ImgCL.ImageProcess.ResizeAsNew(new Bitmap(src), 250, 250);
                    dst.Save(target);
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ChannelServices.RegisteredChannels.Length == 0)
            {
                objclientTcp = new TcpClientChannel();
                ChannelServices.RegisterChannel(objclientTcp);
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
           
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                    Image dst = ImgCL.ImageProcess.FlipHorizontal(new Bitmap(src));
                    dst.Save(target);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
       
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                     Image dst = ImgCL.ImageProcess.FlipVertical(new Bitmap(src));
                    dst.Save(target);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
         
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                    Image dst = ImgCL.ImageProcess.RotateImage(new Bitmap(src), float.Parse(TxtAngle.Text));
                    dst.Save(target);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                    Image dst = ImgCL.ImageProcess.ConvertToGray(new Bitmap(src));
                    dst.Save(target);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ImgCL.ImageProcess objimgresize = (ImgCL.ImageProcess)Activator.GetObject(typeof(ImgCL.ImageProcess), "tcp://" + txtIpAddress.Text + ":9090/RemotingServer");
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string source = fd.FileName;
                Image src = Image.FromFile(source);
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Images|*.png;*.bmp;*.jpg";
                if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string target = sd.FileName;
                    Image dst = ImgCL.ImageProcess.CreateThumb(src);
                    dst.Save(target);
                }
            }
        }
    }
}
