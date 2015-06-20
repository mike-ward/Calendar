using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;

namespace BlueOnion
{
    public sealed class XmlDSigLic : License
    {
        private readonly string licenseKey;
        private string productID;
        private string name;
        private string email;
        private DateTime expirationDate = DateTime.MinValue;
        private bool keyParseAttempted;

        // ---------------------------------------------------------------------
        public XmlDSigLic(string licenseKey)
        {
            this.licenseKey = licenseKey;
        }

        // ---------------------------------------------------------------------
        public override string LicenseKey
        {
            get { return licenseKey; }
        }

        // ---------------------------------------------------------------------
        public bool IsValid()
        {
            ParseKey();

            var assembly = Assembly.GetExecutingAssembly();

            var productId = assembly.GetCustomAttributes
                (typeof (ProductIdAttribute), false)[0] as ProductIdAttribute;

            if (productID != productId.ProductId)
            {
                return false;
            }

            // Once an expiration is detected, never allow it to be true
            if (DateTime.Today >= expirationDate)
            {
                expirationDate = DateTime.MinValue;
                return false;
            }

            return true;
        }

        // ---------------------------------------------------------------------
        public string Name
        {
            get
            {
                ParseKey();
                return name;
            }
        }

        // ---------------------------------------------------------------------
        public string Email
        {
            get
            {
                ParseKey();
                return email;
            }
        }

        // ---------------------------------------------------------------------
        public DateTime Expires
        {
            get
            {
                ParseKey();
                return expirationDate;
            }
        }

        // ---------------------------------------------------------------------
        private void ParseKey()
        {
            if (keyParseAttempted)
            {
                return;
            }

            keyParseAttempted = true;

            if (licenseKey.Length == 0)
            {
                return;
            }

            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(LicenseKey);
                var licenseElement = doc["license"];

                productID = licenseElement["product_id"].InnerText;

                name = licenseElement["first_name"].InnerText + " " +
                    licenseElement["last_name"].InnerText;

                email = licenseElement["email"].InnerText;

                var expireb = Convert.FromBase64String
                    (licenseElement["d1"].InnerText);

                var expires = Encoding.UTF8.GetString(expireb);

                expirationDate = DateTime.ParseExact
                    (expires, "s", CultureInfo.InvariantCulture,
                        DateTimeStyles.NoCurrentDateDefault |
                            DateTimeStyles.AllowLeadingWhite |
                            DateTimeStyles.AllowTrailingWhite);
            }
            catch (XmlException e)
            {
                Log.Error(e.Message);
            }
            catch (NullReferenceException e)
            {
                Log.Error(e.Message);
            }
            catch (FormatException e)
            {
                Log.Error(e.Message);
            }
        }

        // ---------------------------------------------------------------------
        public override void Dispose()
        {
        }
    }
}