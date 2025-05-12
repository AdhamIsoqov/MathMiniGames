using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MathMiniGames.Forms
{
    public partial class MainMenuForm : Form
    {
        private int UserID;
        private string connectionString;

        public MainMenuForm(int userId)
        {
            InitializeComponent();
            UserID = userId;
            connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();
            LoadUserName();
        }

        private void LoadUserName()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Username FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    conn.Open();
                    string username = cmd.ExecuteScalar()?.ToString();
                    if (!string.IsNullOrEmpty(username))
                    {
                        UserName.Text = $"{username}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xatolik yuz berdi: {ex.Message}");
                }
            }
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            GameListForm gameListForm = new GameListForm(UserID);
            gameListForm.Show();
        }

        private void RaytingFormBtn_Click(object sender, EventArgs e)
        {
            RatingForm ratingForm = new RatingForm(UserID);
            ratingForm.Show();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Close();
        }
    }
}
