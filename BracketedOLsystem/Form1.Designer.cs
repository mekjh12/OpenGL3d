namespace LindenmayerSystem
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.nbrNum = new System.Windows.Forms.NumericUpDown();
            this.nbrDelta = new System.Windows.Forms.NumericUpDown();
            this.nbrLength = new System.Windows.Forms.NumericUpDown();
            this.tbGrammer = new System.Windows.Forms.TextBox();
            this.tbAxiom = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLength)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(665, 756);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "생성";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nbrNum
            // 
            this.nbrNum.Location = new System.Drawing.Point(12, 12);
            this.nbrNum.Name = "nbrNum";
            this.nbrNum.Size = new System.Drawing.Size(75, 21);
            this.nbrNum.TabIndex = 2;
            this.nbrNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nbrDelta
            // 
            this.nbrDelta.DecimalPlaces = 1;
            this.nbrDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nbrDelta.Location = new System.Drawing.Point(93, 12);
            this.nbrDelta.Name = "nbrDelta";
            this.nbrDelta.Size = new System.Drawing.Size(75, 21);
            this.nbrDelta.TabIndex = 3;
            this.nbrDelta.Value = new decimal(new int[] {
            225,
            0,
            0,
            65536});
            // 
            // nbrLength
            // 
            this.nbrLength.Location = new System.Drawing.Point(174, 12);
            this.nbrLength.Name = "nbrLength";
            this.nbrLength.Size = new System.Drawing.Size(75, 21);
            this.nbrLength.TabIndex = 4;
            this.nbrLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tbGrammer
            // 
            this.tbGrammer.Location = new System.Drawing.Point(13, 67);
            this.tbGrammer.Multiline = true;
            this.tbGrammer.Name = "tbGrammer";
            this.tbGrammer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGrammer.Size = new System.Drawing.Size(237, 81);
            this.tbGrammer.TabIndex = 5;
            // 
            // tbAxiom
            // 
            this.tbAxiom.Location = new System.Drawing.Point(13, 40);
            this.tbAxiom.Name = "tbAxiom";
            this.tbAxiom.Size = new System.Drawing.Size(236, 21);
            this.tbAxiom.TabIndex = 6;
            this.tbAxiom.Text = "X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 756);
            this.Controls.Add(this.tbAxiom);
            this.Controls.Add(this.tbGrammer);
            this.Controls.Add(this.nbrLength);
            this.Controls.Add(this.nbrDelta);
            this.Controls.Add(this.nbrNum);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nbrNum;
        private System.Windows.Forms.NumericUpDown nbrDelta;
        private System.Windows.Forms.NumericUpDown nbrLength;
        private System.Windows.Forms.TextBox tbGrammer;
        private System.Windows.Forms.TextBox tbAxiom;
    }
}

