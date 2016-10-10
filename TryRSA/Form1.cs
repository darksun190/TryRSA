using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace TryRSA
{
    public partial class Form1 : Form
    {
        string prikey, pubkey;
        public Form1()
        {
            InitializeComponent();
            pubkey = "<RSAKeyValue><Modulus>xe3teTUwLgmbiwFJwWEQnshhKxgcasglGsfNVFTk0hdqKc9i7wb+gG7HOdPZLh65QyBcFfzdlrawwVkiPEL5kNTX1q3JW5J49mTVZqWd3w49reaLd8StHRYJdyGAL4ZovBhSTThETi+zYvgQ5SvCGkM6/xXOz+lkMaEgeFcjQQs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            prikey = "<RSAKeyValue><Modulus>xe3teTUwLgmbiwFJwWEQnshhKxgcasglGsfNVFTk0hdqKc9i7wb+gG7HOdPZLh65QyBcFfzdlrawwVkiPEL5kNTX1q3JW5J49mTVZqWd3w49reaLd8StHRYJdyGAL4ZovBhSTThETi+zYvgQ5SvCGkM6/xXOz+lkMaEgeFcjQQs=</Modulus><Exponent>AQAB</Exponent><P>5flMAd7IrUTx92yomBdJBPDzp1Kclpaw4uXB1Ht+YXqwLW/9icI6mcv7d2O0kuVLSWj8DPZJol9V8AtvHkC3oQ==</P><Q>3FRA9UWcFrVPvGR5bewcL7YqkCMZlybV/t6nCH+gyMfbEvgk+p04F+j8WiHDykWj+BahjScjwyF5SGADbrfJKw==</Q><DP>b4WOU1XbERNfF3JM67xW/5ttPNX185zN2Ko8bbMZXWImr1IgrD5RNqXRo1rphVbGRKoxmIOSv7flr8uLrisKIQ==</DP><DQ>otSZlSq2qomgvgg7PaOLSS+F0TQ/i1emO0/tffhkqT4ah7BgE97xP6puJWZivjAteAGxrxHH+kPY0EY1AzRMNQ==</DQ><InverseQ>Sxyz0fEf5m7GrzAngLDRP/i+QDikJFfM6qPyr3Ub6Y5RRsFbeOWY1tX3jmV31zv4cgJ6donH7W2dSBPi67sSsw==</InverseQ><D>nVqofsIgSZltxTcC8fA/DFz1kxMaFHKFvSK3RKIxQC1JQ3ASkUEYN/baAElB0f6u/oTNcNWVPOqE31IDe7ErQelVc4D26RgFd5V7dSsF3nVz00s4mq1qUBnCBLPIrdb0rcQZ8FUQTsd96qW8Foave4tm8vspbM65iVUBBVdSYYE=</D></RSAKeyValue>";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(prikey);
                // 加密对象 
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] source = System.Text.ASCIIEncoding.ASCII.GetBytes(textBox1.Text);
                SHA1Managed sha = new SHA1Managed();
                byte[] result = sha.ComputeHash(source);
                string s = Convert.ToBase64String(result);
                MessageBox.Show("s=" + s.Length);
                byte[] b = f.CreateSignature(result);
                textBox2.Text = "";
                textBox2.Text = Convert.ToBase64String(b);
                //string s = System.Text.Encoding.Default.GetString(b, 0, 9);
            }
            string str = textBox2.Text;
            // string aa=MD5.Create(str).ToString();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(pubkey);
                RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsa);
                f.SetHashAlgorithm("SHA1");
                byte[] key = Convert.FromBase64String(textBox2.Text);
                SHA1Managed sha = new SHA1Managed();
                byte[] name = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox1.Text));
                string s = Convert.ToBase64String(name);
                if (f.VerifySignature(name, key))
                    MessageBox.Show("Succese!");
                else
                    MessageBox.Show("Falied!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
