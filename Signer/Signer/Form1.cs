using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Signer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e) // making key
        {
            if (textBox1.Text != "")
            {
                label2.Text = "Loading";
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Please enter some input", "Signer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static string SHA1 (string input)
        {
            byte[] hash;

            using (var sha1 = new SHA256CryptoServiceProvider())
            {
                hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
            }
            var sb = new StringBuilder();

            foreach (byte b in hash) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            int value = progressBar1.Value;
            label1.Text = value + "%";
            if (value == 100)
            {
                label2.Text = "loaded";
                richTextBox1.Text = SHA1(textBox1.Text);
                StreamWriter sw = new StreamWriter("output.txt");
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
            }
        }
    }
}
