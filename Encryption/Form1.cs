using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //string encryptedCnxnString = EncryptProvider.EncryptStringToBytes_Aes("Pretend this is a connection string");

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dec = EncryptProvider.DecryptConnectionString();

            MessageBox.Show(dec);
        }
    }
}
