namespace LSystem
{
    partial class Form3D
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
            glControl1 = new OpenGL.GlControl();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.Animation = true;
            glControl1.BackColor = System.Drawing.Color.Gray;
            glControl1.ColorBits = 24U;
            glControl1.DepthBits = 24U;
            glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            glControl1.Location = new System.Drawing.Point(0, 0);
            glControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            glControl1.MultisampleBits = 0U;
            glControl1.Name = "glControl1";
            glControl1.Size = new System.Drawing.Size(800, 450);
            glControl1.StencilBits = 8U;
            glControl1.TabIndex = 0;
            glControl1.Render += glControl1_Render;
            glControl1.KeyDown += glControl1_KeyDown;
            glControl1.MouseMove += glControl1_MouseMove;
            glControl1.MouseWheel += glControl1_MouseWheel;
            // 
            // Form3D
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(glControl1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form3D";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form3D";
            Load += Form3D_Load;
            ResumeLayout(false);
        }

        #endregion

        private OpenGL.GlControl glControl1;
    }
}