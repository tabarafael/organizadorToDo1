using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.ComponentModel;

namespace OrganizadorV1._0
{
    class MainPanel : Panel
    {
        public MainPanel()
        {
            base.AutoScroll = true;
        }

        [DefaultValue(true)]
        public new bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = value; }
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                return StackLayout.Instance;
            }
        }

        private class StackLayout : LayoutEngine
        {
            internal static readonly StackLayout Instance;
            static StackLayout()
            {
                Instance = new StackLayout();
            }
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                MainPanel stackPanel = container as MainPanel;
                if (stackPanel == null)
                    return false;
                Rectangle displayRectangle = stackPanel.DisplayRectangle;
                Point nextControlLocation = displayRectangle.Location;

                foreach (Control control in stackPanel.Controls)
                {
                    if (control.Visible == false)
                        continue;
                    nextControlLocation.Offset(control.Margin.Left, control.Margin.Top);
                    control.Location = nextControlLocation;
                    Size size = control.GetPreferredSize(displayRectangle.Size);
                    if (!control.AutoSize)
                        size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right;
                    control.Size = size;

                    nextControlLocation.X = displayRectangle.X;
                    nextControlLocation.Y += control.Height + control.Margin.Bottom;
                }

                if (stackPanel.AutoScroll && nextControlLocation.Y > displayRectangle.Height)
                {
                    displayRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
                    foreach (Control control in stackPanel.Controls)
                    {
                        if (control.Visible)
                        {
                            Size size = control.GetPreferredSize(displayRectangle.Size);
                            if (!control.AutoSize)
                                size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right;
                            control.Size = size;
                        }
                    }
                }

                return false;

            }
        }
    }
}
