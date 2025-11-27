using DoctorManagement.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;


namespace DoctorManagement.Presentation.Components.Payfast
{
    public partial class SimplePayment
    {
        [Parameter]
        public AppointmentDto? Appointment { get; set; }
        [Parameter]
        public string ReturnUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;

        public Dictionary<string, string> FormData { get; set; } = new()
        {
            ["merchant_id"] = "15517635",
            ["merchant_key"] = "4mhc1jef80pti",
            
        };

        protected override void OnInitialized()
        {
            base.OnInitialized();
            FormData["return_url"] = ReturnUrl;
            FormData["amount"] = $"{Appointment?.Doctor?.NumericDisplayedConsultationFee:F2}";
            FormData["item_name"] = $"{Appointment?.Id}";
            FormData["signature"] = GenerateSignature(FormData);
        }
        private  string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new StringBuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
        private string GenerateSignature(Dictionary<string, string> data, string passPhrase = null)
        {
            // Create parameter string
            
            string pfOutput = "";
            foreach ( var entry in data) {
                if ( !string.IsNullOrEmpty(entry.Value)) {
                    if (entry.Key.Equals("return_url", StringComparison.OrdinalIgnoreCase)) {
                        pfOutput += entry.Key + "=" + UpperCaseUrlEncode(entry.Value.Trim()) + "&";
                    }
                    else
                    {
                        pfOutput += entry.Key + "=" + HttpUtility.UrlEncode(entry.Value.Trim()) + "&";
                    }
                }
            }
            //// Remove last ampersand
            var getString = pfOutput.Trim('&');
            if ( !string.IsNullOrWhiteSpace(passPhrase)) {
                getString+= "&passphrase="+HttpUtility.UrlEncode(passPhrase.Trim());
            }
                return GetMd5Hash(getString);
           
        }

        private string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

    }
}
