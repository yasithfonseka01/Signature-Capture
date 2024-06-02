using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace SignatureCapture
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Resize += Main_Resize; // Handle the Resize event
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            // Calculate the new positions and sizes of controls based on the form's size
            int buttonWidth = ClientSize.Width / 4; // Divide the width equally among four buttons
            int buttonHeight = ClientSize.Height / 6; // Divide the height equally among six buttons
            int labelWidth = ClientSize.Width - 20; // Adjust label width
            int labelHeight = ClientSize.Height / 5; // Adjust label height
            int buttonX = (ClientSize.Width - buttonWidth) / 2; // Center the buttons horizontally
            int buttonY1 = (ClientSize.Height - buttonHeight * 3) / 2; // Position the first button vertically
            int buttonY2 = buttonY1 + buttonHeight + 15; // Add some spacing between buttons
            int buttonY3 = buttonY2 + buttonHeight + 15; // Add some spacing between buttons
            int labelY = 20; // Position the label at the top

            // Set the new positions and sizes of controls
            btn1.SetBounds(buttonX, buttonY1, buttonWidth, buttonHeight);
            btn2.SetBounds(buttonX, buttonY2, buttonWidth, buttonHeight);
            label1.SetBounds((ClientSize.Width - labelWidth) / 2, labelY, labelWidth, labelHeight);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            // Show Form2
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            // Show Form2
            form2.Show();
        }
    }
}
