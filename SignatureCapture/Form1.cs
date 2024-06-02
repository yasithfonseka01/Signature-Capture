using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SignatureCapture
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point lastPoint = Point.Empty;
        private Bitmap signatureBitmap;
        private Pen drawingPen = new Pen(Color.Black, 3); // Change the thickness as desired
        private List<Point> points = new List<Point>();
        private Bitmap doubleBufferBitmap; // Bitmap for double buffering

        public Form1()
        {
            InitializeComponent();
            Resize += Form1_Resize;
            doubleBufferBitmap = new Bitmap(panelSignature.Width, panelSignature.Height);
            signatureBitmap = new Bitmap(panelSignature.Width, panelSignature.Height);
            panelSignature.MouseDown += PanelSignature_MouseDown;
            panelSignature.MouseMove += PanelSignature_MouseMove;
            panelSignature.MouseUp += PanelSignature_MouseUp;
            panelSignature.Paint += panelSignature_Paint;
            btnSave.Click += btnSave_Click;
            btnClear.Click += btnClear_Click;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Adjust the size and position of controls based on the form's size
            panelSignature.Size = new Size(ClientSize.Width - 30, ClientSize.Height - 100);
            btnSave.Location = new Point(ClientSize.Width - 180, ClientSize.Height - 40);
            btnClear.Location = new Point(ClientSize.Width - 90, ClientSize.Height - 40);
        }

        private void PanelSignature_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void PanelSignature_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Add the current point to the list of points
                points.Add(e.Location);

                using (Graphics g = Graphics.FromImage(signatureBitmap))
                {
                    // Draw a curve through the points using spline interpolation
                    if (points.Count >= 2)
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.DrawCurve(drawingPen, points.ToArray());
                    }
                }

                // Invalidate the panel to redraw
                panelSignature.Invalidate();
            }
        }

        private void PanelSignature_MouseUp(object sender, MouseEventArgs e)
        {
            // Clear the list of points when mouse is released
            isDrawing = false;
            points.Clear();
        }

        private void panelSignature_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(signatureBitmap, Point.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Files|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    signatureBitmap.Save(sfd.FileName);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            signatureBitmap = new Bitmap(panelSignature.Width, panelSignature.Height);
            panelSignature.Invalidate(); // Causes panel to redraw
        }
    }
}