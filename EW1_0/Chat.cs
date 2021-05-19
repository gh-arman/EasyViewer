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

namespace EW1_0
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
        }                
        public void MsgReciever(PacketHeader header, Connection connection, string Msg)
        {
            ChatList.Items.Add(Msg);
        }
        public void MsgSender(PacketHeader header, Connection connection, string Msg)
        {
            //NetworkComms.SendObject("MsgReciever", Form1.i, 9999, "Request");
        }
        private void Chat_Load(object sender, EventArgs e)
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("MsgReciever", MsgReciever);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("MsgSender", MsgSender);
        }
    }
}
