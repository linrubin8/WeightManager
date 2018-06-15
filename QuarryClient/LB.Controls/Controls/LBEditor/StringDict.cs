using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Controls.LBEditor
{
    public class StringDict : Dictionary<string, string>
    {
        public void AddRange(Dictionary<string, string> range)
        {
            this.AddRange(range, false);
        }

        public void AddRange(Dictionary<string, string> range, bool bOverride)
        {
            foreach (KeyValuePair<string, string> pair in range)
            {
                if (this.ContainsKey(pair.Key))
                {
                    if (bOverride)
                    {
                        this[pair.Key] = pair.Value;
                    }
                }
                else
                {
                    this.Add(pair.Key, pair.Value);
                }
            }
        }

        public void AddRange(params string[] parmsAtFront)
        {
            for (int i = 0, j = parmsAtFront.Length / 2; i < j; i++)
            {
                string key = parmsAtFront[i * 2];
                string value = parmsAtFront[i * 2 + 1];
                if (this.ContainsKey(key))
                {
                    this[key] = value;
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        public static string[] ToPageParmsArray(StringDict dictParmsAtBack, params string[] parmsAtFront)
        {
            int iFrontLength = 0;
            if (parmsAtFront != null)
            {
                iFrontLength = parmsAtFront.Length;
            }

            string[] strResutl = new string[iFrontLength + dictParmsAtBack.Count * 2];
            if (parmsAtFront != null)
            {
                parmsAtFront.CopyTo(strResutl, 0);
            }

            int index = iFrontLength;
            foreach (KeyValuePair<string, string> pair in dictParmsAtBack)
            {
                strResutl[index] = pair.Key;
                index++;

                strResutl[index] = pair.Value;
                index++;
            }

            return strResutl;
        }
    }
}
