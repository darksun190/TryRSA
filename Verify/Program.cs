using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Verify
{
    class Program
    {
        static void Main(string[] args)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string publicKey = "<RSAKeyValue><Modulus>xIG4S5uIRjiqbbkZ+v4yTVbF8uL7ogESwZPIzH7oAIbVqwnmNqLCRvKlB1ugValVjFWOScZ1KHngIZyuZgCSDQedynk6Z/l20OeBq4r3dMiKlTOidZeYTopdWynM63JhPAZTVvyqXq1FeI2q//PjMVKM5qUyUb5CDJr6Kyb1F9U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


                rsa.FromXmlString(publicKey);

            string source_str = "316CD78795BD9122D411B986FDE401A10441CF73";
            string license_str = "WeU987+i0tG9ZbDB6CAiU4uMzzLCDHwSw6xqkdIUE4s9beYURh7hUSn89stcI2mPv/6tpQ8O1X0tPnPDUWyx/DLzgImO48owPE0nPAgoHfaEj1OeWOf/QrCRCs8UJsOgb1D44xDUk4kjc4zjFHCVWruWBzSsFJFXT8MlHJfyeMk=";



            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsa);
            f.SetHashAlgorithm("SHA1");
            byte[] key = Convert.FromBase64String(license_str);
            SHA1Managed sha = new SHA1Managed();
            byte[] name = sha.ComputeHash(Encoding.ASCII.GetBytes(source_str));
            string s = Convert.ToBase64String(name);
            if (f.VerifySignature(name, key))
            {
                Console.WriteLine("Success!");
                Console.Read();
            }
            else
                throw new Exception();

        }
    }
}
