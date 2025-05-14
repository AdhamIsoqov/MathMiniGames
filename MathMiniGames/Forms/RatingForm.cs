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
                    string query = @"
                SELECT 
                    u.Username AS 'Foydalanuvchi',
                    gs.GameName AS 'O‘yin nomi',
                    gs.Score AS 'Ball',
                    gs.Difficulty AS 'Qiyinlik',
                    gs.TimeTaken AS 'Sarflangan vaqt (soniya)',
                    gs.DatePlayed AS 'O‘ynagan sana'
                FROM GameStats gs
                INNER JOIN Users u ON gs.UserID = u.UserID
                WHERE gs.GameName = @GameName
                ORDER BY gs.Score DESC, gs.TimeTaken ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GameName", gameName);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        gridView.DataSource = dataTable;

                        if (dataTable.Rows.Count > 0)
                        {
                            int bestScore = Convert.ToInt32(dataTable.Rows[0]["Ball"]);
                            string bestUser = dataTable.Rows[0]["Foydalanuvchi"].ToString();
                            recordLabel.Text = $"Eng yaxshi natija: {bestScore} (Foydalanuvchi: {bestUser})";
                        }
                        else
                        {
                            recordLabel.Text = $"{gameName} uchun hech qanday natija topilmadi.";
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
