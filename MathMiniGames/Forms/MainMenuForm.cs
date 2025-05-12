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
    public partial class MainMenuForm : Form
    {
        private int  UserID;
        public MainMenuForm(int userId)
        {
            InitializeComponent();
            UserID = userId;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            GameListForm gameListForm = new GameListForm();
            gameListForm.Show();
        }

        private void RaytingFormBtn_Click(object sender, EventArgs e)
        {
            RatingForm ratingForm = new RatingForm();
            ratingForm.Show();
        }
    }
}
