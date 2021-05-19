using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Drawing.Imaging;

namespace EW1_0
{
    public partial class Viewer : Form
    {
        public Viewer()
        {
            InitializeComponent();
        }
        public IP IP = new IP();
        private delegate void Updatee();
        Bitmap bp;
        public void Connections()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<List<byte[]>>("DataBlock", ViewData);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<int>>("ScreenSize", SetSize);           
        }

        public void UpdatePic(Bitmap[] block, Point[] Points)
        {
            try
            {
                for (int i = 0; i < block.Length; i++)
                {
                    using (Graphics g = Graphics.FromImage(bp))
                    {
                        g.DrawImageUnscaled(block[i], Points[i].X, Points[i].Y);
                    }
                }
                Pic_view.Invoke(new Updatee(() => Pic_view.Image = bp));
            }
            catch
            {

            }
        }
        public void ViewData(PacketHeader header, Connection connection, List<byte[]> List_Data)
        {
            Bitmap[] Blocks = new Bitmap[List_Data.Count / 3];
            Point[] Points = new Point[List_Data.Count / 3];
            for (int i = 0; i < List_Data.Count / 3; i++)
            {
                Blocks[i] = IP.ConvertByteToBitmap(List_Data[3 * i]);
                Points[i].X = BitConverter.ToInt32(List_Data[3 * i + 1], 0);
                Points[i].Y = BitConverter.ToInt32(List_Data[3 * i + 2], 0);
            }
            UpdatePic(Blocks, Points);
        }
        public void SetSize(PacketHeader header, Connection connection, List<int> rt)
        {
            bp = new Bitmap(rt[0], rt[1], PixelFormat.Format24bppRgb);
        }        
        private void Viewer_Load(object sender, EventArgs e)
        {
            Connections();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
