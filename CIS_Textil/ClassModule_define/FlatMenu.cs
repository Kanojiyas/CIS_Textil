using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace System.Windows.Forms
{

    public class MyRenderer : ToolStripProfessionalRenderer
    {
        public MyRenderer()
            : base(new MyColorTable())
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected) base.OnRenderMenuItemBackground(e);
            else
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(Brushes.LightSteelBlue, rc);
                //Color c = (e.Item.Selected ? Color.LightSteelBlue : Color.w );
                //using (SolidBrush brush = new SolidBrush(c))
                //{
                //    e.Graphics.FillRectangle(brush, rc);
                //}
                e.Graphics.DrawRectangle(Pens.AliceBlue, 1, 0, rc.Width - 2, rc.Height - 1);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ArrowRectangle.Location, e.ArrowRectangle.Size);
            r.Inflate(-2, -6);
            e.Graphics.DrawLines(Pens.Black, new Point[]{
        new Point(r.Left, r.Top),
        new Point(r.Right, r.Top + r.Height /2), 
        new Point(r.Left, r.Top+ r.Height)});
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            r.Inflate(-4, -6);
            e.Graphics.DrawLines(Pens.Black, new Point[]{
        new Point(r.Left, r.Bottom - r.Height /2),
        new Point(r.Left + r.Width /3,  r.Bottom), 
        new Point(r.Right, r.Top)});
        }
    }

    public class MyColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return SystemColors.Desktop; }
        }

        //public override Color ToolStripDropDownBackground
        //{
        //    get { return Color.AliceBlue; }
        //}

        public override Color ImageMarginGradientBegin
        {
            get { return Color.LightSteelBlue; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.LightSteelBlue; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.LightSteelBlue; }
        }
    }

}