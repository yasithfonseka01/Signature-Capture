namespace SignatureCapture
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn1 = new Button();
            btn2 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Location = new Point(170, 161);
            btn1.Name = "btn1";
            btn1.Size = new Size(198, 101);
            btn1.TabIndex = 0;
            btn1.Text = "BY DEVICE";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += button1_Click;
            // 
            // btn2
            // 
            btn2.Location = new Point(420, 161);
            btn2.Name = "btn2";
            btn2.Size = new Size(198, 101);
            btn2.TabIndex = 1;
            btn2.Text = "BY MOUSE";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(140, 25);
            label1.Name = "label1";
            label1.Size = new Size(511, 50);
            label1.TabIndex = 2;
            label1.Text = "CAPTURE YOUR SIGNATURE";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn1;
        private Button btn2;
        private Label label1;
    }
}