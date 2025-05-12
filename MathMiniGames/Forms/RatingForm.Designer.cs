namespace MathMiniGames.Forms
{
    partial class RatingForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.TogriBajar = new System.Windows.Forms.DataGridView();
            this.RaqamliSudoku = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.RaqamliLabirint = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.SonniTop = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TogriBajar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaqamliSudoku)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaqamliLabirint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SonniTop)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(384, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sizning Reyting Natijalaringiz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "To\'gri bajar! oyinidagi rekordingiz :";
            // 
            // TogriBajar
            // 
            this.TogriBajar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TogriBajar.Location = new System.Drawing.Point(51, 118);
            this.TogriBajar.Name = "TogriBajar";
            this.TogriBajar.ReadOnly = true;
            this.TogriBajar.RowHeadersWidth = 51;
            this.TogriBajar.RowTemplate.Height = 24;
            this.TogriBajar.Size = new System.Drawing.Size(519, 205);
            this.TogriBajar.TabIndex = 2;
            // 
            // RaqamliSudoku
            // 
            this.RaqamliSudoku.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RaqamliSudoku.Location = new System.Drawing.Point(576, 118);
            this.RaqamliSudoku.Name = "RaqamliSudoku";
            this.RaqamliSudoku.ReadOnly = true;
            this.RaqamliSudoku.RowHeadersWidth = 51;
            this.RaqamliSudoku.RowTemplate.Height = 24;
            this.RaqamliSudoku.Size = new System.Drawing.Size(519, 205);
            this.RaqamliSudoku.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(571, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(348, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Raqamli Sudoku oyinidagi rekordingiz :";
            // 
            // RaqamliLabirint
            // 
            this.RaqamliLabirint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RaqamliLabirint.Location = new System.Drawing.Point(51, 354);
            this.RaqamliLabirint.Name = "RaqamliLabirint";
            this.RaqamliLabirint.ReadOnly = true;
            this.RaqamliLabirint.RowHeadersWidth = 51;
            this.RaqamliLabirint.RowTemplate.Height = 24;
            this.RaqamliLabirint.Size = new System.Drawing.Size(519, 205);
            this.RaqamliLabirint.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 562);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(343, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Raqamli Labirint oyinidagi rekordingiz :";
            // 
            // SonniTop
            // 
            this.SonniTop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SonniTop.Location = new System.Drawing.Point(576, 354);
            this.SonniTop.Name = "SonniTop";
            this.SonniTop.ReadOnly = true;
            this.SonniTop.RowHeadersWidth = 51;
            this.SonniTop.RowTemplate.Height = 24;
            this.SonniTop.Size = new System.Drawing.Size(519, 205);
            this.SonniTop.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(571, 562);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Sonni top oyinidagi rekordingiz :";
            // 
            // RatingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 684);
            this.Controls.Add(this.SonniTop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RaqamliLabirint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RaqamliSudoku);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TogriBajar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RatingForm";
            this.Text = "RatingForm";
            ((System.ComponentModel.ISupportInitialize)(this.TogriBajar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaqamliSudoku)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaqamliLabirint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SonniTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView TogriBajar;
        private System.Windows.Forms.DataGridView RaqamliSudoku;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView RaqamliLabirint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView SonniTop;
        private System.Windows.Forms.Label label5;
    }
}