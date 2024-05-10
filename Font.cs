using LockSmart.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockSmart
{
    internal class PersonalFont
    {
        public PrivateFontCollection QuickSand = new PrivateFontCollection();

        public PersonalFont()
        {
            string filename = "MainFont.ttf";
            try { File.WriteAllBytes(filename, Resources.MainFont); }
            catch { }
            this.QuickSand.AddFontFile(filename);
        }

    }
}
