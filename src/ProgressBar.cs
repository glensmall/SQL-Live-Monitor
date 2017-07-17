using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace VistaStyleProgressBar
{
	/// <summary>
	/// A replacement for the default ProgressBar control.
	/// </summary>
	[DefaultEvent("ValueChanged")]
	public class ProgressBar : System.Windows.Forms.UserControl 
	{
        // Globals for the class
        // optimised form the default where each variable is created at runtime in each function
        // here they are created 1 and initialised once
        private Rectangle r; 
		private GraphicsPath rr;
        private Rectangle lr; 
        private LinearGradientBrush lg;
        private Rectangle rrr; 
        private LinearGradientBrush rg;
        private Rectangle rrrr; 
        private Rectangle llr; 
		private LinearGradientBrush llg; 
		private ColorBlend lc; 
        private Rectangle rrrrr; 
		private LinearGradientBrush rrg; 
		private ColorBlend rc;
        private Rectangle tr; 
        private GraphicsPath tp;
        private LinearGradientBrush tg;
        private Rectangle br; 
        private GraphicsPath bp;
        private LinearGradientBrush bg; 
        private Rectangle gr;
		private LinearGradientBrush lgb; 
		private ColorBlend ccb;
        private Rectangle clip; 
        private Rectangle dir; 
        private GraphicsPath dirr; 
        private Rectangle dor; 
        private GraphicsPath dorr; 
				

		#region -  Designer  -

			/// <summary> 
			/// Required designer variable.
			/// </summary>
			private System.ComponentModel.Container components = null;

			/// <summary>
			/// Create the control and initialize it.
			/// </summary>
			public ProgressBar()
			{
				// This call is required by the Windows.Forms Form Designer.
				InitializeComponent();
				this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				this.SetStyle(ControlStyles.DoubleBuffer, true);
				this.SetStyle(ControlStyles.ResizeRedraw, true);
				this.SetStyle(ControlStyles.Selectable, true);
				//this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				this.SetStyle(ControlStyles.UserPaint, true);
				//this.BackColor = Color.Transparent;
				if (!InDesignMode())
				{
					mGlowAnimation.Tick += new EventHandler(mGlowAnimation_Tick);
					mGlowAnimation.Interval = 15;
					if (Value < MaxValue) {mGlowAnimation.Start();}
				}
			}

			/// <summary> 
			/// Clean up any resources being used.
			/// </summary>
			protected override void Dispose( bool disposing )
			{
		        rr.Dispose();
                lg.Dispose();
                rg.Dispose();
		        llg.Dispose(); 
		        rrg.Dispose(); 
                tp.Dispose();
                tg.Dispose();
                bp.Dispose();
                bg.Dispose();
		        lgb.Dispose();                 

				if( disposing )
				{
					if(components != null)
					{
						components.Dispose();
					}
				}
				base.Dispose( disposing );
			}

			#region -  Component Designer  -
			
				/// <summary> 
				/// Required method for Designer support - do not modify 
				/// the contents of this method with the code editor.
				/// </summary>
				private void InitializeComponent()
				{
					// 
					// ProgressBar
					// 
					this.Name = "ProgressBar";
					this.Size = new System.Drawing.Size(264, 32);
					this.Paint +=new PaintEventHandler(ProgressBar_Paint);


                    // init all my variables
                    r = this.ClientRectangle; r.Width--; r.Height--;
                    rr = RoundRect(r, 2, 2, 2, 2);
                    lr = new Rectangle(2, 2, 10, this.Height - 5);
                    lg = new LinearGradientBrush(lr, Color.FromArgb(30, 0, 0, 0), Color.Transparent, LinearGradientMode.Horizontal);
                    rrr = new Rectangle(this.Width - 12, 2, 10, this.Height - 5);
                    rg = new LinearGradientBrush(rrr, Color.Transparent, Color.FromArgb(20, 0, 0, 0), LinearGradientMode.Horizontal);
                    rrrr = new Rectangle(1, 2, this.Width - 3, this.Height - 3);
                    llr = new Rectangle(1, 2, 15, this.Height - 3);
                    llg = new LinearGradientBrush(llr, Color.White, Color.White, LinearGradientMode.Horizontal);
                    lc = new ColorBlend(3);
                    lc.Colors = new Color[] {Color.Transparent, Color.FromArgb(40, 0, 0, 0), Color.Transparent};
				    lc.Positions = new float[] {0.0F, 0.2F, 1.0F};
                    llg.InterpolationColors = lc;
                    rrrrr = new Rectangle(this.Width - 3, 2, 15, this.Height - 3); 
				    rrrrr.X = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width) - 14;
                    rrg = new LinearGradientBrush(rrrrr, Color.Black,Color.Black, LinearGradientMode.Horizontal);
                    rc = new ColorBlend(3);
                    rc.Colors = new Color[] { Color.Transparent, Color.FromArgb(40, 0, 0, 0), Color.Transparent };
                    rc.Positions = new float[] { 0.0F, 0.8F, 1.0F };
                    rrg.InterpolationColors = rc;
                    tr = new Rectangle(1, 1, this.Width - 1, 6);
                    tp = RoundRect(tr, 2, 2, 0, 0);
                    tg = new LinearGradientBrush(tr, Color.White, Color.FromArgb(128, Color.White), LinearGradientMode.Vertical);
                    br = new Rectangle(1, this.Height - 8, this.Width - 1, 6);
                    bp = RoundRect(br, 0, 0, 2, 2);
                    bg = new LinearGradientBrush(br, Color.Transparent, Color.FromArgb(100, this.HighlightColor), LinearGradientMode.Vertical);
                    gr = new Rectangle(mGlowPosition, 0, 60, this.Height);
                    lgb = new LinearGradientBrush(gr, Color.White, Color.White, LinearGradientMode.Horizontal);
                    ccb = new ColorBlend(4);
                    ccb.Colors = new Color[] { Color.Transparent, this.GlowColor, this.GlowColor, Color.Transparent };
                    ccb.Positions = new float[] { 0.0F, 0.5F, 0.6F, 1.0F };
                    lgb.InterpolationColors = ccb;
                    clip = new Rectangle(1, 2, this.Width - 3, this.Height - 3);
                    dir = this.ClientRectangle;
                    dir.X++; dir.Y++; dir.Width -= 3; dir.Height -= 3;
                    //dirr = RoundRect(dir, 2, 2, 2, 2);
                    dor = this.ClientRectangle; dor.Width--; dor.Height--;
                    //dorr = RoundRect(dor, 2, 2, 2, 2);
                    
				}

			#endregion

		#endregion

		#region -  Properties  -

			private int mGlowPosition = -325;
			private Timer mGlowAnimation = new Timer();

			#region -  Value  -

				private int mValue = 0;
				/// <summary>
				/// The value that is displayed on the progress bar.
				/// </summary>
				[Category("Value"), 
				DefaultValue(0),
				Description("The value that is displayed on the progress bar.")]
				public int Value
				{
					get { return mValue; }
					set 
					{ 
						if (value > MaxValue || value < MinValue){ return; }
						mValue = value;
						if (value < MaxValue) {mGlowAnimation.Start();}
						if (value == MaxValue) {mGlowAnimation.Stop();}
						ValueChangedHandler vc = ValueChanged;
						if (vc != null){vc(this, new System.EventArgs());}
						this.Invalidate(); 
					}
				}

                private Color mNewColor = Color.Green;
                /// <summary>
                /// This will be the color that we want to draw
                /// </summary>
                [Category("Value"),
                Description("The Color that the bar will be draw in")]
                public Color NewColor
                {
                    get { return mNewColor; }

                    set
                    {
                        mNewColor = value;
                        this.Invalidate();
                    }
                }
		
				private int mMaxValue = 100;
				/// <summary>
				/// The maximum value for the Value property.
				/// </summary>
				[Category("Value"), 
				DefaultValue(100), 
				Description("The maximum value for the Value property.")]
				public int MaxValue
				{
					get { return mMaxValue; }
					set 
					{ 
						mMaxValue = value; 
						if (value > MaxValue) {Value = MaxValue;}
						if (Value < MaxValue) {mGlowAnimation.Start();} 
						MaxChangedHandler mc = MaxChanged;
						if (mc != null){mc(this, new System.EventArgs());}
						this.Invalidate(); 
					}
				}

				private int mMinValue = 0;
				/// <summary>
				/// The minimum value for the Value property.
				/// </summary>
				[Category("Value"), 
				DefaultValue(0), 
				Description("The minimum value for the Value property.")]
				public int MinValue
				{
					get { return mMinValue; }
					set 
					{
						mMinValue = value; 
						if (value < MinValue) {Value = MinValue;}
						MinChangedHandler mc = MinChanged;
						if (mc != null){mc(this, new System.EventArgs());}
						this.Invalidate(); 
					}
				}

			#endregion
		
			#region -  Bar  -

				private Color mStartColor = Color.FromArgb(210, 0, 0);
				/// <summary>
				/// The start color for the progress bar.
				/// 210, 000, 000 = Red
				/// 210, 202, 000 = Yellow
				/// 000, 163, 211 = Blue
				/// 000, 211, 040 = Green
				/// </summary>
				[Category("Bar"), 
				DefaultValue(typeof(Color), "210, 0, 0"),
				Description("The start color for the progress bar." + 
							"210, 000, 000 = Red\n" + 
							"210, 202, 000 = Yellow\n" + 
							"000, 163, 211 = Blue\n" + 
							"000, 211, 040 = Green\n")]
				public Color StartColor
				{
					get { return mStartColor; }
					set { mStartColor = value; this.Invalidate(); }
				}

				private Color mEndColor = Color.FromArgb(0, 211, 40);
				/// <summary>
				/// The end color for the progress bar.
				/// 210, 000, 000 = Red
				/// 210, 202, 000 = Yellow
				/// 000, 163, 211 = Blue
				/// 000, 211, 040 = Green
				/// </summary>
				[Category("Bar"), 
				DefaultValue(typeof(Color), "0, 211, 40"),
				Description("The end color for the progress bar." + 
					"210, 000, 000 = Red\n" + 
					"210, 202, 000 = Yellow\n" + 
					"000, 163, 211 = Blue\n" + 
					"000, 211, 040 = Green\n")]
				public Color EndColor
				{
					get { return mEndColor; }
					set { mEndColor = value; this.Invalidate(); }
				}

			#endregion

			#region -  Highlights and Glows  -

				private Color mHighlightColor = Color.White;
				/// <summary>
				/// The color of the highlights.
				/// </summary>
				[Category("Highlights and Glows"),
				DefaultValue(typeof(Color),"White"),
				Description("The color of the highlights.")]
				public Color HighlightColor
				{
					get { return mHighlightColor; }
					set { mHighlightColor = value; this.Invalidate(); }
				}

				private Color mBackgroundColor = Color.FromArgb(201,201,201);
				/// <summary>
				/// The color of the background.
				/// </summary>
				[Category("Highlights and Glows"),
				DefaultValue(typeof(Color),"201,201,201"),
				Description("The color of the background.")]
				public Color BackgroundColor
				{
					get { return mBackgroundColor; }
					set { mBackgroundColor = value; this.Invalidate(); }
				}

				private bool mAnimate = true;
				/// <summary>
				/// Whether the glow is animated.
				/// </summary>
				[Category("Highlights and Glows"),
				DefaultValue(typeof(bool), "true"),
				Description("Whether the glow is animated or not.")]
				public bool Animate
				{
					get { return mAnimate; }
					set {
							mAnimate = value; 
							if (value) {mGlowAnimation.Start();} else {mGlowAnimation.Stop();}
							this.Invalidate(); 
						}
				}

				private Color mGlowColor = Color.FromArgb(150, 255, 255, 255);
				/// <summary>
				/// The color of the glow.
				/// </summary>
				[Category("Highlights and Glows"),
				DefaultValue(typeof(Color),"150, 255, 255, 255"),
				Description("The color of the glow.")]
				public Color GlowColor
				{
					get { return mGlowColor; }
					set { mGlowColor = value; this.Invalidate(); }
				}
				
			#endregion

		#endregion

		#region -  Drawing  -

			private void DrawBackground(Graphics g)
			{
				
				g.FillPath(new SolidBrush(this.BackgroundColor), rr);
			}

			private void DrawBackgroundShadows(Graphics g)
			{
				lr.X--;
				g.FillRectangle(lg, lr);				
				g.FillRectangle(rg, rrr);
			}

			private void DrawBar(Graphics g)
			{				 
				rrrr.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);
                g.FillRectangle(new SolidBrush(this.NewColor), rrrr);
                
			}

			private void DrawBarShadows(Graphics g)
			{
				llr.X--;
				g.FillRectangle(lg, llr);
				g.FillRectangle(rrg, rrrrr);
			}

			private void DrawHighlight(Graphics g)
			{
				g.SetClip(tp);
				
				g.FillPath(tg, tp);
				g.ResetClip();
    
				g.SetClip(bp);
				
				g.FillPath(bg, bp);
				g.ResetClip();
			}

			private void DrawInnerStroke(Graphics g)
			{
				Rectangle r = this.ClientRectangle; 
				r.X++; r.Y++; r.Width-=3; r.Height-=3;
				GraphicsPath rr = RoundRect(r, 2, 2, 2, 2);
				g.DrawPath(new Pen(Color.FromArgb(100, Color.White)), rr);
			}

			private void DrawGlow(Graphics g)
			{
	
				clip.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);
				g.SetClip(clip);
				g.FillRectangle(lgb,r);
				g.ResetClip();
			}

			private void DrawOuterStroke(Graphics g)
			{
				Rectangle r = this.ClientRectangle; r.Width--; r.Height--;
				GraphicsPath rr = RoundRect(r, 2, 2, 2, 2);
				g.DrawPath(new Pen(Color.FromArgb(178, 178, 178)), rr);
			}

           

		#endregion

		#region -  Functions  -

			private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
			{
				float x = r.X, y = r.Y, w = r.Width, h = r.Height;
				GraphicsPath rr = new GraphicsPath();
				rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
				rr.AddLine(x + r1, y, x + w - r2, y);
				rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
				rr.AddLine(x + w, y + r2, x + w, y + h - r3);
				rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
				rr.AddLine(x + w - r3, y + h, x + r4, y + h);
				rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
				rr.AddLine(x, y + h - r4, x, y + r1);
				return rr;
			}

			private bool InDesignMode()
			{
				return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
			}

			private Color GetIntermediateColor()
			{
				Color c = this.StartColor;
				Color c2 = this.EndColor;

				float pc = this.Value * 1.0F / (this.MaxValue - this.MinValue);

				int ca = c.A, cr = c.R, cg = c.G, cb = c.B;
				int c2a = c2.A, c2r = c2.R, c2g = c2.G, c2b = c2.B;
				
				int a = (int)Math.Abs(ca + (ca - c2a) * pc);
				int r = (int)Math.Abs(cr - ((cr - c2r) * pc));
				int g = (int)Math.Abs(cg - ((cg - c2g) * pc));
				int b = (int)Math.Abs(cb - ((cb - c2b) * pc));

				if (a > 255) {a = 255;}
				if (r > 255) {r = 255;}
				if (g > 255) {g = 255;}
				if (b > 255) {b = 255;}

				return (Color.FromArgb(a, r, g, b));
			}

		#endregion

		#region -  Other  -

			private void ProgressBar_Paint(object sender, PaintEventArgs e)
			{
                e.Graphics.SmoothingMode = SmoothingMode.Default;
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                

				//DrawBackground(e.Graphics);
                e.Graphics.FillPath(new SolidBrush(this.BackgroundColor), rr);

				//DrawBackgroundShadows(e.Graphics);
                llr.X--;
                e.Graphics.FillRectangle(lg, llr);
                e.Graphics.FillRectangle(rrg, rrrrr);

				//DrawBar(e.Graphics);
                rrrr.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);
                e.Graphics.FillRectangle(new SolidBrush(this.NewColor), rrrr);

				//DrawBarShadows(e.Graphics);
                llr.X--;
                e.Graphics.FillRectangle(lg, llr);
                e.Graphics.FillRectangle(rrg, rrrrr);

				//DrawHighlight(e.Graphics);
                e.Graphics.SetClip(tp);
                e.Graphics.FillPath(tg, tp);
                e.Graphics.ResetClip();
                e.Graphics.SetClip(bp);
                e.Graphics.FillPath(bg, bp);
                e.Graphics.ResetClip();

				//DrawInnerStroke(e.Graphics);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(100, Color.White)), rr);

				//DrawGlow(e.Graphics);
				//DrawOuterStroke(e.Graphics);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(178, 178, 178)), rr);
			}

			private void mGlowAnimation_Tick(object sender, EventArgs e)
			{
				if (this.Animate)
				{
					mGlowPosition += 4;
					if (mGlowPosition > this.Width)
					{
						mGlowPosition = -300;
					}
					this.Invalidate();
				}
				else
				{
					mGlowAnimation.Stop();
					mGlowPosition = -320;
				}
			}

		#endregion

		#region -  Events  -

			/// <summary>
			/// When the Value property is changed.
			/// </summary>
			public delegate void ValueChangedHandler(object sender, EventArgs e);
			/// <summary>
			/// When the Value property is changed.
			/// </summary>
			public event ValueChangedHandler ValueChanged;

			/// <summary>
			/// When the MinValue property is changed.
			/// </summary>
			public delegate void MinChangedHandler(object sender, EventArgs e);
			/// <summary>
			/// When the MinValue property is changed.
			/// </summary>
			public event MinChangedHandler MinChanged;
				
			/// <summary>
			/// When the MaxValue property is changed.
			/// </summary>
			public delegate void MaxChangedHandler(object sender, EventArgs e);
			/// <summary>
			/// When the MaxValue property is changed.
			/// </summary>
			public event MaxChangedHandler MaxChanged;

		#endregion

	}
}