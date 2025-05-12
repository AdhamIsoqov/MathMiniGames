using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MathMiniGames.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Login va parol to'ldirilishi shart.");
                return;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();
            string query = @"
                SELECT UserID, IsActive FROM Users 
                WHERE Username = @Username AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        bool isActive = reader.GetBoolean(1);

                        if (!isActive)
                        {
                            MessageBox.Show("Foydalanuvchi aktiv emas. Administrator bilan bog'laning.");
                            return;
                        }

                        reader.Close();
                        MessageBox.Show("Kirish muvaffaqiyatli!");
                        UpdateLastLoginDate(userId, connectionString);
                        MainMenuForm mainForm = new MainMenuForm(userId);
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login yoki parol noto'g'ri. Iltimos, qaytadan urinib ko'ring.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xato yuz berdi: " + ex.Message);
                }
            }
        }
        private void UpdateLastLoginDate(int userId, string connectionString)
        {
            string updateQuery = "UPDATE Users SET LastLoginDate = GETDATE() WHERE UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Register formReg = new Register();
            formReg.Show();
            this.Hide();
        }
    }
}
