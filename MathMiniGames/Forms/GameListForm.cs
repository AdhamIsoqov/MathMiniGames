﻿using System;
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
    public partial class GameListForm : Form
    {
        private int UserID;
        public GameListForm(int userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void Game1StartBtn_Click(object sender, EventArgs e)
        {
            if (DifficultLevelComboBox.SelectedItem != null)
            {
                string difficulty = DifficultLevelComboBox.SelectedItem.ToString(); 
                Game1Form game1Form = new Game1Form(difficulty, UserID);
                game1Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Iltimos, qiyinchilik darajasini tanlang.");
            }
        }

        private void Game2StartBtn_Click(object sender, EventArgs e)
        {
            if (DifficultLevelComboBox.SelectedItem != null) 
            {
                string difficulty = DifficultLevelComboBox.SelectedItem.ToString(); 
                Game2Form game2Form = new Game2Form(difficulty, UserID);
                game2Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Iltimos, qiyinchilik darajasini tanlang.");
            }
        }

        private void Game3StartBtn_Click(object sender, EventArgs e)
        {
            if (DifficultLevelComboBox.SelectedItem != null) 
            {
                string difficulty = DifficultLevelComboBox.SelectedItem.ToString();
                Game3Form game3Form = new Game3Form( difficulty, UserID);
                game3Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Iltimos, qiyinchilik darajasini tanlang.");
            }
        }

        private void Game4StartBtn_Click(object sender, EventArgs e)
        {
            if (DifficultLevelComboBox.SelectedItem != null) 
            {
                string difficulty = DifficultLevelComboBox.SelectedItem.ToString();
                Game4Form game4Form = new Game4Form(difficulty, UserID);
                game4Form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Iltimos, qiyinchilik darajasini tanlang.");
            }
        }
    }
}
