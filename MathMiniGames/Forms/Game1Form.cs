﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    namespace MathMiniGames.Forms
    {
        public partial class Game1Form : Form
        {
            private string difficulty;
            private Random random = new Random();
            private int targetNumber;
            private List<int> availableNumbers = new List<int>();
            private List<Button> numberButtons = new List<Button>();
            private List<int> usedNumberIndices = new List<int>();
            private List<string> expressionParts = new List<string>();
            private int timeLeft;
            private int score = 0;
            private bool gameActive = false;
            private string[] operations = { "+", "-", "×", "÷", "(", ")" };
            private int currentUserId;
            private string connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();

        public Game1Form(string difficulty, int userID)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            this.currentUserId = userID;
            SetupGame();
            CreateOperationButtons();
            btnNewGame.Click += BtnNewGame_Click;
            btnClear.Click += BtnClear_Click;
            btnUndo.Click += BtnUndo_Click;
            timer.Tick += Timer_Tick;
            lblDifficulty.Text = $"Daraja: {difficulty}";
            StartNewGame();
        }

        private string GetDifficultyName()
        {
            switch (difficulty.ToLower())
            {
                case "oson": return "easy";
                case "o'rta": return "medium";
                case "qiyin": return "hard";
                default: return "easy";
            }
        }

        private void SetupGame()
        {
            switch (GetDifficultyName())
            {
                case "easy":
                    timeLeft = 180;
                    break;
                case "medium":
                    timeLeft = 120;
                    break;
                case "hard":
                    timeLeft = 90;
                    break;
                default:
                    timeLeft = 110;
                    break;
            }

                lblTime.Text = $"Vaqt: {timeLeft}s";
            }

            private void CreateOperationButtons()
            {
                pnlOperations.Controls.Clear();

                foreach (string op in operations)
                {
                    Button btn = new Button
                    {
                        Text = op,
                        Width = 60,
                        Height = 40,
                        Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                        Margin = new Padding(10, 5, 10, 5)
                    };

                    btn.Click += OperationButton_Click;
                    pnlOperations.Controls.Add(btn);
                }
            }

    private void GenerateNumbers()
    {
        pnlNumbers.Controls.Clear();
        numberButtons.Clear();
        availableNumbers.Clear();

        int count, minValue, maxValue;

        switch (GetDifficultyName())
        {
            case "easy":
                count = 4;
                minValue = 1;
                maxValue = 9;
                break;

            case "medium":
                count = 5;
                minValue = 10;
                maxValue = 20;
                break;

            case "hard":
                count = 6;
                minValue = 20;
                maxValue = 30; 
                break;

            default:
                count = 4;
                minValue = 1;
                maxValue = 12;
                break;
        }

        for (int i = 0; i < count; i++)
        {
            int number = random.Next(minValue, maxValue + 1);
            availableNumbers.Add(number);

            Button btn = new Button
            {
                Text = number.ToString(),
                Width = 70,
                Height = 60,
                Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold),
                Tag = i,
                Margin = new Padding(15, 10, 15, 10),
                BackColor = Color.LightGray
            };

            btn.Click += NumberButton_Click;
            numberButtons.Add(btn);
            pnlNumbers.Controls.Add(btn);
        }

        GenerateTargetNumber(); 
    }
    private void GenerateTargetNumber()
        {
            int minTarget, maxTarget;

            switch (GetDifficultyName())
            {
                case "easy":
                    minTarget = 10;
                    maxTarget = 50;
                    break;
                case "medium":
                    minTarget = 30;
                    maxTarget = 100;
                    break;
                case "hard":
                    minTarget = 50;
                    maxTarget = 200;
                    break;
                default:
                    minTarget = 10;
                    maxTarget = 50;
                    break;
            }
            targetNumber = random.Next(minTarget, maxTarget + 1);
            lblTarget.Text = $"Maqsad: {targetNumber}";
        }

        private void StartNewGame()
        {
            expressionParts.Clear();
            usedNumberIndices.Clear();
            lblExpression.Text = "";
            lblGameStatus.Text = "";
            GenerateNumbers();
            SetupGame();
            gameActive = true;
            timer.Start();
        }

    private void SaveGameStats()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO GameStats (UserID, GameName, Score, Difficulty, TimeTaken) " +
                                "VALUES (@UserID, @GameName, @Score, @Difficulty, @TimeTaken)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", currentUserId);
                    command.Parameters.AddWithValue("@GameName", "To'gri bajar!");
                    command.Parameters.AddWithValue("@Score", score);
                    command.Parameters.AddWithValue("@Difficulty", difficulty);
                    command.Parameters.AddWithValue("@TimeTaken", timeLeft);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Statistika saqlandi!", "Ma'lumot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Statistika saqlanmadi!", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnNewGame_Click(object sender, EventArgs e)
            {
                if (gameActive)
                {
                    SaveGameStats();
                }
                StartNewGame();
            }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Ifodani tozalash
            expressionParts.Clear();
            lblExpression.Text = "";

            // Sonlar tugmalarini qayta aktivlashtirish
            foreach (int index in usedNumberIndices)
            {
                if (index < numberButtons.Count)
                {
                    numberButtons[index].Enabled = true;
                }
            }

            usedNumberIndices.Clear();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (expressionParts.Count > 0)
            {
                // Oxirgi qo'shilgan elementni olish
                string lastPart = expressionParts[expressionParts.Count - 1];
                expressionParts.RemoveAt(expressionParts.Count - 1);

                // Agar son bo'lsa, uning tugmasini qayta aktivlashtirish
                if (int.TryParse(lastPart, out int number))
                {
                    if (usedNumberIndices.Count > 0)
                    {
                        int lastIndex = usedNumberIndices[usedNumberIndices.Count - 1];
                        usedNumberIndices.RemoveAt(usedNumberIndices.Count - 1);

                        if (lastIndex < numberButtons.Count)
                        {
                            numberButtons[lastIndex].Enabled = true;
                        }
                    }
                }

                // Ifodani yangilash
                UpdateExpression();
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (!gameActive) return;

            Button btn = (Button)sender;
            int index = (int)btn.Tag;

            // Sonni ifodaga qo'shish
            expressionParts.Add(btn.Text);
            usedNumberIndices.Add(index);

            // Tugmani o'chirish
            btn.Enabled = false;

            // Ifodani yangilash
            UpdateExpression();

            // Natijani tekshirish
            CheckResult();
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            if (!gameActive) return;

            Button btn = (Button)sender;

            // Amal qo'shish
            expressionParts.Add(btn.Text);

            // Ifodani yangilash
            UpdateExpression();
        }

        private void UpdateExpression()
        {
            lblExpression.Text = string.Join(" ", expressionParts);
        }

        private void CheckResult()
        {
            try
            {
                // Ifodani hisoblash
                string expression = string.Join("", expressionParts)
                    .Replace("×", "*")
                    .Replace("÷", "/");

                // Natijani hisoblash
                var result = EvaluateExpression(expression);

                // Agar natija maqsad songa teng bo'lsa
                if (result == targetNumber)
                {
                    // O'yinni to'xtatish
                    timer.Stop();
                    gameActive = false;

                    // Ballni oshirish
                    int timeBonus = timeLeft;
                    int difficultyMultiplier = GetDifficultyMultiplier();
                    int pointsEarned = 100 + timeBonus * difficultyMultiplier;
                    score += pointsEarned;

                    lblScore.Text = $"Ball: {score}";
                    lblGameStatus.Text = $"Tabriklaymiz! +{pointsEarned} ball qo'shildi";
                    lblGameStatus.ForeColor = Color.Green;
                    SaveGameStats();
                }
            }
            catch (Exception)
            {
                // Noto'g'ri ifoda, hech narsa qilmaymiz
            }
        }

        private int GetDifficultyMultiplier()
        {
            switch (difficulty.ToLower())
            {
                case "easy": return 1;
                case "medium": return 2;
                case "hard": return 3;
                default: return 1;
            }
        }

        private double EvaluateExpression(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTime.Text = $"Vaqt: {timeLeft}s";

            if (timeLeft <= 0)
            {
                timer.Stop();
                gameActive = false;
                lblGameStatus.Text = "Vaqt tugadi!";
                lblGameStatus.ForeColor = Color.Red;
                SaveGameStats();
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (!gameActive || expressionParts.Count == 0)
            {
                lblGameStatus.Text = "Ifoda kiritilmagan!";
                lblGameStatus.ForeColor = Color.Red;
                return;
            }
            try
            {
                string expression = string.Join("", expressionParts)
                    .Replace("×", "*")
                    .Replace("÷", "/");
                var result = EvaluateExpression(expression);
                if (result == targetNumber)
                {
                    timer.Stop();
                    gameActive = false;
                    int timeBonus = timeLeft;
                    int difficultyMultiplier = GetDifficultyMultiplier();
                    int pointsEarned = 100 + timeBonus * difficultyMultiplier;
                    score += pointsEarned;

                    lblScore.Text = $"Ball: {score}";
                    lblGameStatus.Text = $"Tabriklaymiz! +{pointsEarned} ball qo'shildi";
                    lblGameStatus.ForeColor = Color.Green;
                    SaveGameStats();
                }
                else
                {
                    lblGameStatus.Text = $"Natija: {result} ≠ {targetNumber}. Qayta urinib ko'ring!";
                    lblGameStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblGameStatus.Text = "Noto'g'ri ifoda. Tekshirib qayta kiriting!";
                lblGameStatus.ForeColor = Color.Red;
            }
        }
}
}