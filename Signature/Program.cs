using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Signature
{
    class Program
    {
        static void Main(string[] args)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string privateKey = "<RSAKeyValue><Modulus>xIG4S5uIRjiqbbkZ+v4yTVbF8uL7ogESwZPIzH7oAIbVqwnmNqLCRvKlB1ugValVjFWOScZ1KHngIZyuZgCSDQedynk6Z/l20OeBq4r3dMiKlTOidZeYTopdWynM63JhPAZTVvyqXq1FeI2q//PjMVKM5qUyUb5CDJr6Kyb1F9U=</Modulus><Exponent>AQAB</Exponent><P>8BLCOFdgLowgGov+z4oIDWwDn4VE2gVEzp2D+RUmSCTGKPu5H80MXOAR8UKvy1mxLz0GlP1Ah7GoVgmcpx8gXw==</P><Q>0YsOcjxjiN5hFck/aqxp4dclueKJSK4M+rvCTZQL1yvNyckaBG+ThTbnMOH3maFfkCq0jGTxQn3HZyAa/z7kSw==</Q><DP>4+SqwdkP0J/sCcdDR7f8FOGoPtG/nkbKHmigUt6kzG88PMNX5Lw9NBzwa1mmjx7Bd9oyWVRe4XDgH2xYbLy7eQ==</DP><DQ>t0sLHPQkIQJdLEB436fniqy3DG3TpqbRJbZ91XyOCu7/OOZXgs/S2/FVtBXFjzZwsFwayMA3pfD+LwAPfXyXFw==</DQ><InverseQ>JYNoCY67zf9LDTTLMJHkhQTBqXyhVmhwj9bd/JZqpGcyFULBitnu8Fjg1C6Oa0k8w5CehkpsdR+b/A3PNaPFOw==</InverseQ><D>W/z5n/MsBOtL5NdMsTFDrO6c9YmKEBl+hT3ANvKFepGj/lBBA3yHg5zc4ifjU5ZUZMA8Po73kz4STMnC3h8QPtTC8lbprWT5zGtMEdRGtiWuiG99G5zeAO/+ECUQORYIaxXUliW/ohwNJTrZhiVHxs/7C3bF+99Owz4W8arVjfU=</D></RSAKeyValue>";


            rsa.FromXmlString(privateKey);

            string source_str = "316CD78795BD9122D411B986FDE401A10441CF73";

            // 加密对象 
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsa);
            f.SetHashAlgorithm("SHA1");
            byte[] source = Encoding.ASCII.GetBytes(source_str);
            SHA1Managed sha = new SHA1Managed();
            byte[] result = sha.ComputeHash(source);
            string s = Convert.ToBase64String(result);

            byte[] b = f.CreateSignature(result);

            using (StreamWriter sw = new StreamWriter("license.dat"))
            {
                //sw.WriteLine(Encoding.ASCII.GetString(b));
                string license_str = Convert.ToBase64String(b);
                sw.Write(license_str);
                sw.Close();
            }
        }

    }
}
