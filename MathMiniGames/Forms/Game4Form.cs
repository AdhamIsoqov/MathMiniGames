using System;
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

        public Game4Form(string difficulty)
        {
            InitializeComponent();
            this.Text = "Matematik mashq";
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

            // Taymerni sozlash
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
                MessageBox.Show($"O'yin tugadi!\nSizning ballingiz: {score}", "O'yin tugadi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Yangi o'yin boshlash
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

            // To'g'ri javobni hisoblash
            switch (operation)
            {
                case "+":
                    correctAnswer = num1 + num2;
                    break;
                case "-":
                    // Manfiy bo'lmasligi uchun
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

            // Ifodani labelga yozish
            lblExpression.Text = $"{num1} {operation} {num2} = ?";

            // Javob variantlarini yaratish
            GenerateAnswerOptions();

            // Natija labelini tozalash
            lblResult.Text = "";
        }

        private void GenerateAnswerOptions()
        {
            // To'g'ri javobni qaysi tugmaga qo'yishni aniqlash
            int correctButtonIndex = random.Next(4);

            // Har bir tugma uchun
            for (int i = 0; i < 4; i++)
            {
                if (i == correctButtonIndex)
                {
                    // To'g'ri javobli tugma
                    answerButtons[i].Text = correctAnswer.ToString();
                    answerButtons[i].Tag = true;
                }
                else
                {
                    // Noto'g'ri javoblar
                    int wrongAnswer;
                    do
                    {
                        // 5 gacha farq qiluvchi noto'g'ri javob yaratish
                        wrongAnswer = correctAnswer + random.Next(-5, 6);
                        // 0 dan kichik bo'lmasligi va takrorlanmasligi kerak
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
                // To'g'ri javob uchun
                score += 10;
                lblScore.Text = $"Ball: {score}";
                timeLeft += 3; // Vaqt qo'shiladi
                lblTime.Text = $"Vaqt: {timeLeft}";
                lblResult.Text = "To'g'ri!";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                // Xato javob uchun
                lblResult.Text = $"Xato! To'g'ri javob: {correctAnswer}";
                lblResult.ForeColor = Color.Red;
                timeLeft -= 2; // Vaqt kamayadi
                if (timeLeft < 0) timeLeft = 0;
                lblTime.Text = $"Vaqt: {timeLeft}";
            }

            // Yangi savol generatsiya qilish
            GenerateQuestion();
        }
    }
}