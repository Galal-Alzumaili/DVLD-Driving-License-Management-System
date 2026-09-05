using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11___DVLD_Project
{
    public class DVLDMenuColors : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => Color.Transparent;
        public override Color MenuStripGradientEnd => Color.Transparent;

        public override Color ToolStripDropDownBackground => Color.FromArgb(15, 23, 42);

        public override Color MenuItemSelected => Color.FromArgb(40, 40, 40);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(40, 40, 40);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(40, 40, 40);

        public override Color MenuItemBorder => Color.FromArgb(212, 175, 55); // ذهبي
    }
}
