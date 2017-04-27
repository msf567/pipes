using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NamedPipeWrapper;
using Finger;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace CustomForms
{
    public partial class DisplayWindow : Form
    {
        private readonly NamedPipeClient<SVRMSG> _client = new NamedPipeClient<SVRMSG>("my_fucking_pipe");
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public DisplayWindow()
        {
            InitializeComponent();
            Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _client.ServerMessage += OnServerMessage;
            _client.Start();

            BackColor = Color.Aqua;
        }

        private void OnServerMessage(NamedPipeConnection<SVRMSG, SVRMSG> connection, SVRMSG message)
        {
            if (message.text == "blue")
                BackColor = Color.RoyalBlue;
            else
                BackColor = Color.LightGoldenrodYellow;

            if (message.text == "quit")
                Close();
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int x = r.Next(0, 1000);
            int y = r.Next(0, 1000);
            SetDesktopLocation(x, y);

            textBox1.Text = message.text;

            if (message.text == "click")
            {
                uint X =(uint)r.Next(0, 1000);
                uint Y = (uint)r.Next(0, 1000);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP,(uint) x - 2,(uint) y-2, 0, 0);
            }
        }
        /*store windows off screen and switch with a bitmap
         * release cursor and draw fake one
         *  must be free to move the real cursor to any position and retain control over the cursor
         *  
         * every frame take a screengrab of the window and draw through the bitmap masks
         * mouse position is offset and snapped to the relative control of the pixel of the window shown, clicks are passed through that window
         * 
         * 
         */

    }
}
