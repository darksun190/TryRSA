using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace GenerateKey
{
    class Program
    {
        static void Main(string[] args)
        {
            var RSA = new RSACryptoServiceProvider();
            using (StreamWriter writer = new StreamWriter("PrivateKey.xml"))  //这个文件要保密...
            {
                string priKey = RSA.ToXmlString(true);
                writer.Write(priKey);

            }
            using (StreamWriter writer = new StreamWriter("PublicKey.xml"))
            {
                string pubKey = RSA.ToXmlString(false);
                writer.Write(pubKey);

            }
        }
    }
}
