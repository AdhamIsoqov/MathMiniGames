namespace MathMiniGames.Forms
{
    partial class MainMenuForm
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
            this.StartGame = new System.Windows.Forms.Button();
            this.RaytingFormBtn = new System.Windows.Forms.Button();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Qiziqarli matematik o‘yinlar";
            // 
            // StartGame
            // 
            this.StartGame.BackColor = System.Drawing.Color.Blue;
            this.StartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartGame.ForeColor = System.Drawing.Color.White;
            this.StartGame.Location = new System.Drawing.Point(215, 623);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(230, 59);
            this.StartGame.TabIndex = 1;
            this.StartGame.Text = "O‘yinni boshlash";
            this.StartGame.UseVisualStyleBackColor = false;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // RaytingFormBtn
            // 
            this.RaytingFormBtn.BackColor = System.Drawing.Color.Goldenrod;
            this.RaytingFormBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RaytingFormBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RaytingFormBtn.ForeColor = System.Drawing.Color.White;
            this.RaytingFormBtn.Location = new System.Drawing.Point(451, 623);
            this.RaytingFormBtn.Name = "RaytingFormBtn";
            this.RaytingFormBtn.Size = new System.Drawing.Size(230, 59);
            this.RaytingFormBtn.TabIndex = 2;
            this.RaytingFormBtn.Text = "Reytingni ko‘rish";
            this.RaytingFormBtn.UseVisualStyleBackColor = false;
            this.RaytingFormBtn.Click += new System.EventHandler(this.RaytingFormBtn_Click);
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.Red;
            this.LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoutBtn.ForeColor = System.Drawing.Color.White;
            this.LogoutBtn.Location = new System.Drawing.Point(687, 623);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(230, 59);
            this.LogoutBtn.TabIndex = 3;
            this.LogoutBtn.Text = "Chiqish";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 725);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.RaytingFormBtn);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainMenuForm";
            this.Text = "MainMenuForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartGame;
        private System.Windows.Forms.Button RaytingFormBtn;
        private System.Windows.Forms.Button LogoutBtn;
    }
}