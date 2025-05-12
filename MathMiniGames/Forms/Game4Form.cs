using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MathMiniGames.Forms
{
    public partial class Game4Form : Form
    {
        private Random random = new Random();
        private int correctAnswer;
        private int score = 0;
        private int timeLeft = 30;
        private Timer timer = new Timer();

        // UI elementlari
        private Label lblExpression;
        private Button[] answerButtons = new Button[4];
        private Label lblScore;
        private Label lblTime;
        private Label lblResult;
        private int currentUserID;
        private string difficulty;
        private string conn;
        public Game4Form(string difficulty, int userID)
        {
            InitializeComponent();
            conn = ConfigurationManager.ConnectionStrings["connectDB"].ToString();
            this.Text = "Matematik mashq";
            this.difficulty = difficulty;
            this.currentUserID = userID;
            SetupUI();
            StartGame();
        }

        private void SetupUI()
        {
            // Asosiy formani sozlash
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Ifoda ko'rsatiladigan label
            lblExpression = new Label
            {
                Size = new Size(460, 60),
                Location = new Point(20, 20),
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(lblExpression);

            // Javob tugmalari
            for (int i = 0; i < 4; i++)
            {
                answerButtons[i] = new Button
                {
                    Size = new Size(220, 60),
                    Location = new Point(20 + (i % 2) * 240, 100 + (i / 2) * 80),
                    Font = new Font("Arial", 16)
                };
                answerButtons[i].Click += AnswerButton_Click;
                this.Controls.Add(answerButtons[i]);
            }

            // Ball va vaqt labellar
            lblScore = new Label
            {
                Size = new Size(220, 30),
                Location = new Point(20, 280),
                Font = new Font("Arial", 12),
                Text = "Ball: 0"
            };
            this.Controls.Add(lblScore);

            lblTime = new Label
            {
                Size = new Size(220, 30),
                Location = new Point(260, 280),
                Font = new Font("Arial", 12),
                Text = "Vaqt: 30"
            };
            this.Controls.Add(lblTime);

            // Natija ko'rsatuvchi label
            lblResult = new Label
            {
                Size = new Size(460, 30),
                Location = new Point(20, 320),
                Font = new Font("Arial", 12),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblResult);
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTime.Text = $"Vaqt: {timeLeft}";

            if (timeLeft <= 0)
            {
                timer.Stop();
                SaveGameStats();
                MessageBox.Show($"O'yin tugadi!\nSizning ballingiz: {score}", "O'yin tugadi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBox.Show("Yangi o'yin boshlashni xohlaysizmi?", "Yangi o'yin",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    score = 0;
                    timeLeft = 30;
                    lblScore.Text = "Ball: 0";
                    lblTime.Text = "Vaqt: 30";

                    GenerateQuestion();
                    timer.Start();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void StartGame()
        {
            score = 0;
            timeLeft = 30;
            lblScore.Text = "Ball: 0";
            lblTime.Text = "Vaqt: 30";
            GenerateQuestion();
            timer.Start();
        }

        private void GenerateQuestion()
        {
            // Ifoda yaratish
            int num1 = random.Next(1, 10);
            int num2 = random.Next(1, 10);
            string[] operations = { "+", "-", "×" };
            string operation = operations[random.Next(operations.Length)];
            switch (operation)
            {
                case "+":
                    correctAnswer = num1 + num2;
                    break;
                case "-":
                    if (num1 < num2)
                    {
                        int temp = num1;
                        num1 = num2;
                        num2 = temp;
                    }
                    correctAnswer = num1 - num2;
                    break;
                case "×":
                    correctAnswer = num1 * num2;
                    break;
                default:
                    correctAnswer = 0;
                    break;
            }
            lblExpression.Text = $"{num1} {operation} {num2} = ?";
            GenerateAnswerOptions();
            lblResult.Text = "";
        }

        private void GenerateAnswerOptions()
        {
            int correctButtonIndex = random.Next(4);
            for (int i = 0; i < 4; i++)
            {
                if (i == correctButtonIndex)
                {
                    answerButtons[i].Text = correctAnswer.ToString();
                    answerButtons[i].Tag = true;
                }
                else
                {
                    int wrongAnswer;
                    do
                    {
                        wrongAnswer = correctAnswer + random.Next(-5, 6);
                    } while (wrongAnswer < 0 || wrongAnswer == correctAnswer ||
                             Array.Exists(answerButtons, b => b != null && b.Text == wrongAnswer.ToString()));

                    answerButtons[i].Text = wrongAnswer.ToString();
                    answerButtons[i].Tag = false;
                }
            }
        }

        private void AnswerButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool isCorrect = (bool)clickedButton.Tag;

            if (isCorrect)
            {
                score += 10;
                lblScore.Text = $"Ball: {score}";
                timeLeft += 3; 
                lblTime.Text = $"Vaqt: {timeLeft}";
                lblResult.Text = "To'g'ri!";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.Text = $"Xato! To'g'ri javob: {correctAnswer}";
                lblResult.ForeColor = Color.Red;
                timeLeft -= 2; 
                if (timeLeft < 0) timeLeft = 0;
                lblTime.Text = $"Vaqt: {timeLeft}";
            }
            GenerateQuestion();
        }
        private void SaveGameStats()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    string query = "INSERT INTO GameStats (UserID, GameName, Score, Difficulty, TimeTaken, DatePlayed) " +
                                   "VALUES (@UserID, @GameName, @Score, @Difficulty, @TimeTaken, @DatePlayed)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserID); 
                        command.Parameters.AddWithValue("@GameName", "Sonni top"); 
                        command.Parameters.AddWithValue("@Score", score); 
                        command.Parameters.AddWithValue("@Difficulty", difficulty);
                        command.Parameters.AddWithValue("@TimeTaken", 30 - timeLeft); 
                        command.Parameters.AddWithValue("@DatePlayed", DateTime.Now); 

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("O'yin natijalari saqlandi!", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xatolik yuz berdi: " + ex.Message, "Xato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}