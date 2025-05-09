using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathMiniGames.Forms
{
    public partial class DifficultyForm : Form
    {
        public DifficultyForm()
        {
            InitializeComponent();
        }
        string difficulty;
        private void StartGame_Click(object sender, EventArgs e)
        {
            difficulty = "easy";
            Game1Form game1Form = new Game1Form(difficulty);
            game1Form.Show();
            this.Hide();
        }

        private void MiddleBtn_Click(object sender, EventArgs e)
        {
            difficulty = "middle";
            Game1Form game1Form = new Game1Form(difficulty);
            game1Form.Show();
            this.Hide();
        }

        private void HardBtn_Click(object sender, EventArgs e)
        {
            difficulty = "hard";
            Game1Form game1Form = new Game1Form(difficulty);    
            game1Form.Show();
            this.Hide();
        }
    }
}
