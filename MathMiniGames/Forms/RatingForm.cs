using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MathMiniGames.Forms
{
    public partial class RatingForm : Form
    {
        private int currentUserID;
        private string connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();

        public RatingForm(int userID)
        {
            InitializeComponent();
            this.currentUserID = userID;
            LoadGameData();
        }

        private void LoadGameData()
        {
            LoadGameStats("To'gri bajar!", TogriBajar, label2);
            LoadGameStats("Raqamli Sudoku", RaqamliSudoku, label3);
            LoadGameStats("Raqamli Labirint", RaqamliLabirint, label4);
            LoadGameStats("Sonni top", SonniTop, label5);
        }

        private void LoadGameStats(string gameName, DataGridView gridView, Label recordLabel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT GameName, Score, Difficulty, TimeTaken, DatePlayed 
                                     FROM GameStats 
                                     WHERE UserID = @UserID AND GameName = @GameName 
                                     ORDER BY Score DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserID);
                        command.Parameters.AddWithValue("@GameName", gameName);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gridView.DataSource = dataTable;

                        if (dataTable.Rows.Count > 0)
                        {
                            int bestScore = Convert.ToInt32(dataTable.Rows[0]["Score"]);
                            recordLabel.Text = $"{gameName} oyinidagi eng yaxshi natija: {bestScore}";
                        }
                        else
                        {
                            recordLabel.Text = $"{gameName} uchun rekord topilmadi.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
