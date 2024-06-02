using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SignatureCapture
{
    public partial class Form2 : Form
    {
        // Import the necessary methods from the DLL
        [DllImport("evolis.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadEvolisDll();

        [DllImport("evolis.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartCapture();

        [DllImport("evolis.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopCapture();

        [DllImport("evolis.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetSignatureData();

        public Form2()
        {
            InitializeComponent();
            Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBoxSignature.Size = new Size(ClientSize.Width - 30, ClientSize.Height - 100);
            btnSave.Location = new Point(ClientSize.Width - 180, ClientSize.Height - 40);
            btnClear.Location = new Point(ClientSize.Width - 90, ClientSize.Height - 40);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                int result = LoadEvolisDll();
                if (result != 0)
                {
                    Console.WriteLine("> Error loading Evolis DLL");
                    return;
                }

                // Start the signature capture process
                StartSignatureCapture();
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine("DllNotFoundException: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void StartSignatureCapture()
        {
            // Start capturing the signature
            int result = StartCapture();
            if (result != 0)
            {
                Console.WriteLine("> Error starting capture");
                return;
            }

            // Add code here to handle real-time signature data if needed
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Stop capturing the signature
            int stopResult = StopCapture();
            if (stopResult != 0)
            {
                Console.WriteLine("> Error stopping capture");
                return;
            }

            // Retrieve the signature data
            IntPtr signatureDataPtr = GetSignatureData();
            if (signatureDataPtr == IntPtr.Zero)
            {
                Console.WriteLine("> Error getting signature data");
                return;
            }

            // Convert the signature data to a bitmap
            Bitmap signatureBitmap = MarshalBitmap(signatureDataPtr);
            if (signatureBitmap != null)
            {
                // Display the signature in a PictureBox
                pictureBoxSignature.Image = signatureBitmap;
            }
        }

        private Bitmap MarshalBitmap(IntPtr bitmapPtr)
        {
            // This example assumes the SDK provides raw bitmap data
            // Replace this with actual conversion logic as per SDK documentation
            // Here's a dummy implementation
            Bitmap bitmap = new Bitmap(300, 150); // Use actual dimensions
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Dummy drawing, replace with actual bitmap data processing
                g.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
                g.DrawString("Signature", new Font("Arial", 20), Brushes.Black, new PointF(50, 50));
            }
            return bitmap;
        }
    }
}
