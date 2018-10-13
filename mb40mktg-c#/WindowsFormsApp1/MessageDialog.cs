using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Message : Form
    {
        public string message = string.Empty;
        public bool error = false;

        public Message()
        {
            InitializeComponent();
        }

        private void Message_Load(object sender, EventArgs e)
        {
            message_lbl.Text = message;
            if (!error)
            {
                error_pb.Visible = false;
                success_pb.Visible = true;
            }
            else
            {
                error_pb.Visible = true;
                success_pb.Visible = false;
            }
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
