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

namespace CustomForms
{
    public partial class DisplayWindow : Form
    {
        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("my_fucking_pipe");

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

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            if (message == "blue")
                BackColor = Color.RoyalBlue;
            else
                BackColor = Color.LightGoldenrodYellow;

            if(message == "exit")
                Close();
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int x = r.Next(0,1000);
            int y = r.Next(0,1000);
            SetDesktopLocation(x, y);

            textBox1.Text = message;
        }


    }
}
