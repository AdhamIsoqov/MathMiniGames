using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
namespace MathMiniGames.Forms
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string fullName = txtFullName.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString;
            string query = "INSERT INTO Users (Username, Password, Email, FullName) VALUES (@Username, @Password, @Email, @FullName)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); 
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Roʻyxatdan oʻtish muvaffaqiyatli!");
                    }
                    else
                    {
                        MessageBox.Show("Ro‘yxatdan o‘tish amalga oshmadi. Iltimos, qayta urinib koʻring.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xato: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
