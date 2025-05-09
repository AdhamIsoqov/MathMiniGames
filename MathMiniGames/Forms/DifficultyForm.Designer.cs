namespace MathMiniGames.Forms
{
    partial class DifficultyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.EasyBtn = new System.Windows.Forms.Button();
            this.MiddleBtn = new System.Windows.Forms.Button();
            this.HardBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Qiyinchilik darajasini tanlang";
            // 
            // EasyBtn
            // 
            this.EasyBtn.BackColor = System.Drawing.Color.LimeGreen;
            this.EasyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EasyBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EasyBtn.ForeColor = System.Drawing.Color.White;
            this.EasyBtn.Location = new System.Drawing.Point(77, 85);
            this.EasyBtn.Name = "EasyBtn";
            this.EasyBtn.Size = new System.Drawing.Size(271, 97);
            this.EasyBtn.TabIndex = 2;
            this.EasyBtn.Text = "Oson";
            this.EasyBtn.UseVisualStyleBackColor = false;
            this.EasyBtn.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // MiddleBtn
            // 
            this.MiddleBtn.BackColor = System.Drawing.Color.Orange;
            this.MiddleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MiddleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiddleBtn.ForeColor = System.Drawing.Color.White;
            this.MiddleBtn.Location = new System.Drawing.Point(77, 188);
            this.MiddleBtn.Name = "MiddleBtn";
            this.MiddleBtn.Size = new System.Drawing.Size(271, 97);
            this.MiddleBtn.TabIndex = 3;
            this.MiddleBtn.Text = "O‘rta";
            this.MiddleBtn.UseVisualStyleBackColor = false;
            this.MiddleBtn.Click += new System.EventHandler(this.MiddleBtn_Click);
            // 
            // HardBtn
            // 
            this.HardBtn.BackColor = System.Drawing.Color.Red;
            this.HardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HardBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardBtn.ForeColor = System.Drawing.Color.White;
            this.HardBtn.Location = new System.Drawing.Point(77, 291);
            this.HardBtn.Name = "HardBtn";
            this.HardBtn.Size = new System.Drawing.Size(271, 97);
            this.HardBtn.TabIndex = 4;
            this.HardBtn.Text = "Qiyin";
            this.HardBtn.UseVisualStyleBackColor = false;
            this.HardBtn.Click += new System.EventHandler(this.HardBtn_Click);
            // 
            // DifficultyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 447);
            this.Controls.Add(this.HardBtn);
            this.Controls.Add(this.MiddleBtn);
            this.Controls.Add(this.EasyBtn);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DifficultyForm";
            this.Text = "DifficultyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EasyBtn;
        private System.Windows.Forms.Button MiddleBtn;
        private System.Windows.Forms.Button HardBtn;
    }
}