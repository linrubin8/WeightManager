using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastReport.Barcode.QRCode
{
    abstract class QRData
    {
        public string Data;

        public QRData() { }

        public QRData(string data)
        {
            Unpack(data);
        }

        public virtual string Pack()
        {
            return Data;
        }

        public virtual void Unpack(string data)
        {
            Data = data;
        }

        public static QRData Parse(string data)
        {
            if (data.StartsWith("BEGIN:VCARD"))
                return new QRvCard(data);
            else if (data.StartsWith("MATMSG:"))
                return new QREmailMessage(data);
            else if (data.StartsWith("geo:"))
                return new QRGeo(data);
            else if (data.StartsWith("SMSTO:"))
                return new QRSMS(data);
            else if (data.StartsWith("tel:"))
                return new QRCall(data);
            else if (data.StartsWith("BEGIN:VEVENT"))
                return new QREvent(data);
            else if (data.StartsWith("WIFI:"))
                return new QRWifi(data);
            else if (Uri.IsWellFormedUriString(data, UriKind.Absolute))
                return new QRURI(data);
            else if (Regex.IsMatch(data, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                return new QREmailAddress(data);
            else
                return new QRText(data);
        }
    }

    class QRText : QRData
    {
        public QRText() : base() { }
        public QRText(string data) : base(data) { }
    }

    class QRvCard : QRData
    {
        public string FirstName;
        public string LastName;
        public string Street;
        public string ZipCode;
        public string City;
        public string Country;

        public string FN;
        public string TITLE;
        public string ORG;
        public string URL;
        public string TEL_CELL;
        public string TEL_WORK_VOICE;
        public string TEL_HOME_VOICE;
        public string EMAIL_HOME_INTERNET;
        public string EMAIL_WORK_INTERNET;

        public QRvCard() : base() { }

        public QRvCard(string data) : base(data) { }

        public override string Pack()
        {
            StringBuilder data = new StringBuilder("BEGIN:VCARD\nVERSION:2.1\n");

            if ((FirstName != null && FirstName != "") ||
                (LastName  != null && LastName  != ""))
            {
                data.Append("FN:" + FirstName + " " + LastName + "\n");
                data.Append("N:" + LastName + ";" + FirstName + "\n");
            }

            data.Append(Append("TITLE:", TITLE));
            data.Append(Append("ORG:", ORG));
            data.Append(Append("URL:", URL));
            data.Append(Append("TEL;CELL:", TEL_CELL));
            data.Append(Append("TEL;WORK;VOICE:", TEL_WORK_VOICE));
            data.Append(Append("TEL;HOME;VOICE:", TEL_HOME_VOICE));
            data.Append(Append("EMAIL;HOME;INTERNET:", EMAIL_HOME_INTERNET));
            data.Append(Append("EMAIL;WORK;INTERNET:", EMAIL_WORK_INTERNET));

            if ((Street  != null && Street  != "") ||
                (ZipCode != null && ZipCode != "") ||
                (City    != null && City    != "") ||
                (Country != null && Country != ""))
            {
                data.Append("ADR:;;" + Street + ";" + City + ";;" + ZipCode + ";" + Country + "\n");
            }

            data.Append("END:VCARD");

            return data.ToString();
        }

        private string Append(string name, string data)
        {
            if (data != null && data != "")
                return name + data + "\n";
            
            return "";
        }

        public override void Unpack(string data)
        {
            string[] lines = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] s = line.Split(new string[] { ":" }, 2, StringSplitOptions.None);

                switch (s[0])
                {
                    case "FN":
                        FN = s[1];
                        break;
                    case "N":
                        string[] n = s[1].Split(new string[] { ";" }, StringSplitOptions.None);
                        LastName  = n[0];
                        FirstName = n[1];
                        break;
                    case "TITLE":
                        TITLE = s[1];
                        break;
                    case "ORG":
                        ORG = s[1];
                        break;
                    case "URL":
                        URL = s[1];
                        break;
                    case "TEL;CELL":
                        TEL_CELL = s[1];
                        break;
                    case "TEL;WORK;VOICE":
                        TEL_WORK_VOICE = s[1];
                        break;
                    case "TEL;HOME;VOICE":
                        TEL_HOME_VOICE = s[1];
                        break;
                    case "EMAIL;HOME;INTERNET":
                        EMAIL_HOME_INTERNET = s[1];
                        break;
                    case "EMAIL;WORK;INTERNET":
                        EMAIL_WORK_INTERNET = s[1];
                        break;
                    case "ADR":
                        string[] adr = s[1].Split(new string[] { ";" }, StringSplitOptions.None);
                        Street = adr[2];
                        City = adr[3];
                        ZipCode = adr[5];
                        Country = adr[6];
                        break;
                }
            }
        }
    }

    class QRURI : QRData
    {
        public QRURI() : base() { }
        public QRURI(string data) : base(data) { }
    }

    class QREmailAddress : QRData
    {
        public QREmailAddress() : base() { }
        public QREmailAddress(string data) : base(data) { }
    }

    class QREmailMessage : QRData
    {
        public string TO;
        public string SUB;
        public string BODY;

        public QREmailMessage() : base() { }

        public QREmailMessage(string data) : base(data) { }

        public override string Pack()
        {
            return "MATMSG:TO:" + TO + ";SUB:" + SUB + ";BODY:" + BODY + ";;";
        }

        public override void Unpack(string data)
        {
            string[] s = data.Split(new string[] { "MATMSG:TO:", ";SUB:", ";BODY:" }, 4, StringSplitOptions.None);

            TO = s[1];
            SUB = s[2];
            BODY = s[3].Remove(s[3].Length - 2, 2);
        }
    }

    class QRGeo : QRData
    {
        public string Latitude;
        public string Longitude;
        public string Meters;

        public QRGeo() : base() { }

        public QRGeo(string data) : base(data) { }

        public override string Pack()
        {
            return "geo:" + Latitude + "," + Longitude + "," + Meters;
        }

        public override void Unpack(string data)
        {
            string[] s = data.Split(new string[] { "geo:", "," }, 4, StringSplitOptions.None);

            Latitude = s[1];
            Longitude = s[2];
            Meters = s[3];
        }
    }

    class QRSMS : QRData
    {
        public string TO;
        public string TEXT;

        public QRSMS() : base() { }

        public QRSMS(string data) : base(data) { }

        public override string Pack()
        {
            return "SMSTO:" + TO + ":" + TEXT;
        }

        public override void Unpack(string data)
        {
            string[] s = data.Split(new string[] { "SMSTO:", ":" }, StringSplitOptions.None);

            TO = s[1];
            TEXT = s[2];
        }
    }

    class QRCall : QRData
    {
        public string Tel;

        public QRCall() : base() { }

        public QRCall(string data) : base(data) { }

        public override string Pack()
        {
            return "tel:" + Tel;
        }

        public override void Unpack(string data)
        {
            Tel = data.Remove(0, 4);
        }
    }

    class QREvent : QRData
    {
        public string SUMMARY;
        public DateTime DTSTART;
        public DateTime DTEND;

        public QREvent() : base() { }

        public QREvent(string data) : base(data) { }

        public override string Pack()
        {
            return "BEGIN:VEVENT\nSUMMARY:" + SUMMARY +
                   "\nDTSTART:" + DTSTART.Year.ToString("D4") +
                                  DTSTART.Month.ToString("D2") +
                                  DTSTART.Day.ToString("D2") + "T" +
                                  DTSTART.Hour.ToString("D2") +
                                  DTSTART.Minute.ToString("D2") +
                                  DTSTART.Second.ToString("D2") + "Z" +
                   "\nDTEND:" + DTEND.Year.ToString("D4") +
                                DTEND.Month.ToString("D2") +
                                DTEND.Day.ToString("D2") + "T" +
                                DTEND.Hour.ToString("D2") +
                                DTEND.Minute.ToString("D2") +
                                DTEND.Second.ToString("D2") + "Z" +
                   "\nEND:VEVENT";
        }

        public override void Unpack(string data)
        {
            string[] lines = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string From = "";
            string To = "";

            foreach (string line in lines)
            {
                string[] attr = line.Split(new string[] { ":" }, 2, StringSplitOptions.None);

                switch (attr[0])
                {
                    case "SUMMARY":
                        SUMMARY = attr[1];
                        break;
                    case "DTSTART":
                        From = attr[1];
                        break;
                    case "DTEND":
                        To = attr[1];
                        break;
                }
            }

            DTSTART = new DateTime(int.Parse(From.Substring(0, 4)),
                                   int.Parse(From.Substring(4, 2)),
                                   int.Parse(From.Substring(6, 2)),
                                   int.Parse(From.Substring(9, 2)),
                                   int.Parse(From.Substring(11, 2)),
                                   int.Parse(From.Substring(13, 2)));

            DTEND = new DateTime(int.Parse(To.Substring(0, 4)),
                                 int.Parse(To.Substring(4, 2)),
                                 int.Parse(To.Substring(6, 2)),
                                 int.Parse(To.Substring(9, 2)),
                                 int.Parse(To.Substring(11, 2)),
                                 int.Parse(To.Substring(13, 2)));
        }
    }

    class QRWifi : QRData
    {
        public string Encryption;
        public string NetworkName;
        public string Password;
        public bool Hidden;

        public QRWifi() : base() { }

        public QRWifi(string data) : base(data) { }

        public override string Pack()
        {
            return "WIFI:T:" + (Encryption == "unencrypted" ? "nopass" : Encryption) +
                   ";S:" + NetworkName + ";P:" + (Encryption == "unencrypted" ? "" : Password) +
                   (Hidden ? ";H:true;" : ";;");
        }

        public override void Unpack(string data)
        {
            string[] s = data.Split(new string[] { "WIFI:T:", ";S:", ";P:", ";H:", ";;" }, StringSplitOptions.None);

            Encryption = s[1] == "nopass" ? "unencrypted" : s[1];
            NetworkName = s[2];
            Password = s[3];
            Hidden = s[4] == "true;" ? true : false;
        }
    }
}
