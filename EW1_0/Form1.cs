using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;

namespace EW1_0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        public static Rectangle ScreenRectangle = Screen.GetBounds(Screen.GetBounds(Point.Empty));
        Bitmap[] OriginalBimtaps = new Bitmap[2];
        Bitmap[][] SplitBitmaps = new Bitmap[2][];
        List<byte[]> Clones;
        List<int> ScreenSize = new List<int>();
        int[] MousePoints = new int[4];
        // 0 -> just Pointer
        // 1 -> single left click
        // 2 -> double left click
        // 3 -> single Right Click
        // 4 -> Long Left Click
        // Last index for do left click up;
        Point[] PointsArray;
        int state = 0;
        int CutSize = 100;
        int Bios = 0;
        string IPClnt;
        int connectionstate = 0;
        Bitmap bp;
        private delegate void Updatee();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public Point MouseLocation()
        {
            Point MousePoint = new Point(0,0);
            try
            {
                MousePoints[0] = 1;
                float KasrX = (float)(Cursor.Position.X - panel1.Location.X - this.Location.X) / (float)Pic_view.Width;
                MousePoints[1] = (int)(KasrX * ScreenSize[0]);

                float KasrY = (float)(Cursor.Position.Y - panel1.Location.Y - this.Location.Y) / (float)Pic_view.Height;
                MousePoints[2] = (int)(KasrY * ScreenSize[1]);
                NetworkComms.SendObject("Pointer", IPClnt, 9999, MousePoints);                
            }
            catch
            {

            }
            return MousePoint;
        }
        public void StartTimer()
        {
            while (true)
            {
                switch (state)
                {
                    case 0:
                        state = 1;
                        break;
                    case 1:
                        state = 0;
                        break;
                }
                IP.TakeScreenShot(OriginalBimtaps[state]);
                SplitBitmaps[state] = IP.SplitImage(CutSize, IP.To8BPP(OriginalBimtaps[state]));
                PointsArray = IP.DiffPoints(SplitBitmaps[0], SplitBitmaps[1], Bios, OriginalBimtaps[state].Height, CutSize);
                Clones = IP.ImageClones(OriginalBimtaps[state], PointsArray, CutSize);
                Sender(Clones, PointsArray);                
                Thread.Sleep(80);
            }

        }
        public void Connections()
        {
            Viewer vw = new Viewer();
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ConnectAnswer", ConnectAnswer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ConnectRequest", Validation);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<byte[]>>("DataBlock", ViewData);
            NetworkComms.AppendGlobalIncomingPacketHandler<List<int>>("ScreenSize", SetSize);
            NetworkComms.AppendGlobalIncomingPacketHandler<int[]>("Pointer", Pointer);
            NetworkComms.AppendGlobalIncomingPacketHandler<int[]>("PointerUP", PointerUP);
            NetworkComms.AppendGlobalIncomingPacketHandler<int[]>("KeyDown", KKeyDown);
            NetworkComms.AppendGlobalIncomingPacketHandler<int[]>("KeyUP", KKeyUP);


            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 9999));
        }
        
        public void KKeyDown(PacketHeader header, Connection connection, int[] Points)
        {
            Cursor.Position = new Point(Points[1], Points[2]);
            if (Points[0] == 1)
                MouseEvents.DoMouseClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 2)
                MouseEvents.DoMouseDoubleClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 3)
                MouseEvents.DoMouseRightClick((uint)Points[1], (uint)Points[2]);

        }
        public void KKeyUP(PacketHeader header, Connection connection, int[] Points)
        {
            Cursor.Position = new Point(Points[1], Points[2]);
            if (Points[0] == 1)
                MouseEvents.DoMouseClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 2)
                MouseEvents.DoMouseDoubleClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 3)
                MouseEvents.DoMouseRightClick((uint)Points[1], (uint)Points[2]);

        }
        public void Pointer(PacketHeader header, Connection connection, int[] Points)
        {
            Cursor.Position = new Point(Points[1], Points[2]);
            if (Points[0] == 1)
                MouseEvents.DoMouseClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 2)
                MouseEvents.DoMouseDoubleClick((uint)Points[1], (uint)Points[2]);
            else if (Points[0] == 3)
                MouseEvents.DoMouseRightClick((uint)Points[1], (uint)Points[2]);

        }
        public void PointerUP(PacketHeader header, Connection connection, int[] Points)
        {
            MouseEvents.DoMouseUp((uint)Points[1], (uint)Points[2]);
        }
        public void Validation(PacketHeader header, Connection connection, string Anss)
        {
            IPClnt = connection.ConnectionInfo.RemoteEndPoint.ToString();
            IPClnt = IPClnt.Remove(IPClnt.IndexOf(':'));
            DialogResult dr = MessageBox.Show("A request from " + IPClnt + " Recieved.", "Request Recieved!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
            {
                NetworkComms.SendObject("ConnectAnswer", IPClnt, 9999, "OK");
                Status.Invoke(new Updatee(() => Status.Text = IPClnt + " Connected..."));
                Bitmap bm = new Bitmap(ScreenRectangle.Width, ScreenRectangle.Height, PixelFormat.Format24bppRgb);
                Bitmap bp = IP.TakeScreenShot(bm);
                List<int> rt = new List<int>();
                rt.Add(ScreenRectangle.Width);
                rt.Add(ScreenRectangle.Height);
                NetworkComms.SendObject("ScreenSize", IPClnt, 9999, rt);
                connectionstate = 1;
                //Viewer vw = new Viewer();
                StartTimer();
                //vw.ShowDialog();
            }
            else
            {
                NetworkComms.SendObject("ConnectAnswer", IPClnt, 9999, "NO");
                connection.CloseConnection(false);
            }
        }
        public void ConnectAnswer(PacketHeader header, Connection connection, string Answer)
        {
            IPClnt = connection.ConnectionInfo.RemoteEndPoint.ToString();
            IPClnt = IPClnt.Remove(IPClnt.IndexOf(':'));
            if (Answer == "OK")
            {

                //this.Invoke(new Updatee(() => this.WindowState = FormWindowState.Minimized));
                //Viewer view = new Viewer();
                //view.Connections();
                // view.Show();
                panel1.Invoke(new Updatee(() => panel1.Dock = DockStyle.Fill));
                panel1.Invoke(new Updatee(() => panel1.Visible = true));
                ConnectPnl.Invoke(new Updatee(() => ConnectPnl.Visible = false));


            }
            else
            {
                MessageBox.Show("Connect Request Declined !");
            }
        }
        public void Sender(List<byte[]> Clones, Point[] Points)
        {
            List<byte[]> List_Data = new List<byte[]>();
            if (Clones.Count != 0)
            {
                int[] PointX = new int[Points.Length];
                int[] PointY = new int[Points.Length];
                for (int i = 0; i < Clones.Count; i++)
                {
                    PointX[i] = Points[i].X;
                    PointY[i] = Points[i].Y;
                    //List_Data.Add((byte[])TypeDescriptor.GetConverter(Clones[i]).ConvertTo(Clones[i], typeof(byte[])));
                    List_Data.Add(Clones[i]);
                    List_Data.Add(BitConverter.GetBytes(PointX[i]));
                    List_Data.Add(BitConverter.GetBytes(PointY[i]));
                }
                NetworkComms.SendObject("DataBlock", IPClnt, 9999, List_Data);
                

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OriginalBimtaps[0] = new Bitmap(ScreenRectangle.Width, ScreenRectangle.Height, PixelFormat.Format24bppRgb);
            OriginalBimtaps[1] = new Bitmap(ScreenRectangle.Width, ScreenRectangle.Height, PixelFormat.Format24bppRgb);
            Bitmap bp = new Bitmap(ScreenRectangle.Width, ScreenRectangle.Height, PixelFormat.Format8bppIndexed);
            bp = IP.CreateBlank(bp);
            SplitBitmaps[0] = IP.SplitImage(CutSize, bp);
            Connections();
        }
        public void UpdatePic(Bitmap[] block, Point[] Points)
        {
            try
            {
                label3.Invoke(new Updatee(() => label3.Text = block.Length.ToString()));
                for (int i = 0; i < block.Length; i++)
                {
                    using (Graphics g = Graphics.FromImage(bp))
                    {
                        g.DrawImageUnscaled(block[i], Points[i].X, Points[i].Y);
                    }
                }
                Pic_view.Invoke(new Updatee(() => Pic_view.Image = bp));                

                //bp.Save(@"D:\TestIMG\10.jpg");
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
            ScreenSize = rt;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Invoke(new Updatee(() => label4.Text = Cursor.Position.X + ", " + Cursor.Position.Y));
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Pic_view_MouseMove_1(object sender, MouseEventArgs e)
        {
            try
            {
                MousePoints[0] = 0;
                label4.Invoke(new Updatee(() => label4.Text = Cursor.Position.X + ", " + Cursor.Position.Y));
                float KasrX = (float)(Cursor.Position.X - panel1.Location.X - this.Location.X) / (float)Pic_view.Width;
                MousePoints[1] = (int)(KasrX * ScreenSize[0]);

                float KasrY = (float)(Cursor.Position.Y - panel1.Location.Y - this.Location.Y) / (float)Pic_view.Height;
                MousePoints[2] = (int)(KasrY * ScreenSize[1]);
                NetworkComms.SendObject("Pointer", IPClnt, 9999, MousePoints);
            }
            catch
            {

            }            
        }


        private void Connect_BTN_Click(object sender, EventArgs e)
        {
            string ConnectIP = textBox1.Text;
            NetworkComms.SendObject("ConnectRequest", ConnectIP, 9999, "Request");
        }

        private void Pic_view_MouseUp(object sender, MouseEventArgs e)
        {
            MousePoints[3] = -1;
            try
            {
                //MessageBox.Show("up");
                NetworkComms.SendObject("PointerUP", IPClnt, 9999, MousePoints);
            }
            catch
            {

            }            
        }

        private void Pic_view_MouseDown(object sender, MouseEventArgs e)
        {           
            MousePoints[3] = 0;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                try
                {
                    MousePoints[0] = 1;
                    float KasrX = (float)(Cursor.Position.X - panel1.Location.X - this.Location.X) / (float)Pic_view.Width;
                    MousePoints[1] = (int)(KasrX * ScreenSize[0]);

                    float KasrY = (float)(Cursor.Position.Y - panel1.Location.Y - this.Location.Y) / (float)Pic_view.Height;
                    MousePoints[2] = (int)(KasrY * ScreenSize[1]);
                    NetworkComms.SendObject("Pointer", IPClnt, 9999, MousePoints);
                }
                catch
                {

                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    MousePoints[0] = 3;
                    float KasrX = (float)(Cursor.Position.X - panel1.Location.X) / (float)Pic_view.Width;
                    MousePoints[1] = (int)(KasrX * ScreenSize[0]);

                    float KasrY = (float)(Cursor.Position.Y - panel1.Location.Y) / (float)Pic_view.Height;
                    MousePoints[2] = (int)(KasrY * ScreenSize[1]);
                    NetworkComms.SendObject("Pointer", IPClnt, 9999, MousePoints);
                }
                catch
                {

                }
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void Pic_View_MouseWheel(object sender, MouseEventArgs e)
        {
            // Update the drawing based upon the mouse wheel scrolling.
            MessageBox.Show("a");
            //int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            //int numberOfPixelsToMove = numberOfTextLinesToMove * 14;

            //if (numberOfPixelsToMove != 0)
            //{
            //    System.Drawing.Drawing2D.Matrix translateMatrix = new System.Drawing.Drawing2D.Matrix();
            //    translateMatrix.Translate(0, numberOfPixelsToMove);
            //    mousePath.Transform(translateMatrix);
            //}
            //panel1.Invalidate();
        }

        private void P_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void P_Maximize_Click(object sender, EventArgs e)
        {            
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void P_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
