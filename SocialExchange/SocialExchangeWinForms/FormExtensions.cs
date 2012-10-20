using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocialExchangeWinForms
{
    public static class FormExtensions
    {

        public static void CenterWithinParent(this Control control)
        {
            control.Left =
                (
                    control.Parent.Width -
                    control.Parent.Padding.Left -
                    control.Width
                ) / 2;
        }
    }
}
