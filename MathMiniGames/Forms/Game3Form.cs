using System;
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
    public partial class Game3Form : Form
    {
        private string difficulty;
        private Random random = new Random();
        private int currentRow = 0;
        private int currentCol = 0;
        private int[,] labyrinth;
        private Button[,] cells;
        private Panel[,] mathPanels;
        private Label[,] questionLabels;
        private TextBox[,] answerTextBoxes;
        private Button[,] checkButtons;
        private int labyrintSize = 5; 
        private int correctAnswers = 0;
        private int totalQuestions = 0;
        private Label scoreLabel;
        private int currentUserID;
        private string connectionString;
        public Game3Form(string difficulty, int userID)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            this.currentUserID = userID;
            connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();
            SetupGame();
        }
        private void SetupGame()
        {
            labyrinth = new int[labyrintSize, labyrintSize];
            GenerateLabyrinth();
            SetupUI();
            MoveToCell(0, 0);
        }
        private void GenerateLabyrinth()
        {
            for (int i = 0; i < labyrintSize; i++)
            {
                for (int j = 0; j < labyrintSize; j++)
                {
                    if (i == 0 && j == 0 || i == labyrintSize - 1 && j == labyrintSize - 1)
                    {
                        labyrinth[i, j] = 0;
                    }
                    else
                    {
                        labyrinth[i, j] = random.Next(10) < 3 ? 1 : 0;
                    }
                }
            }
            EnsurePath();
        }
        private void EnsurePath()
        {
            for (int i = 0; i < labyrintSize; i++)
            {
                labyrinth[i, i] = 0;
            }
        }
        private void SetupUI()
        {
            this.Controls.Clear();
            int cellSize = 500 / labyrintSize;
            this.ClientSize = new Size(labyrintSize * cellSize + 300, labyrintSize * cellSize + 100);
            Panel labyrinthPanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(labyrintSize * cellSize, labyrintSize * cellSize),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(labyrinthPanel);
            scoreLabel = new Label
            {
                Text = "Score: 0/" + totalQuestions,
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            };
            this.Controls.Add(scoreLabel);
            cells = new Button[labyrintSize, labyrintSize];
            mathPanels = new Panel[labyrintSize, labyrintSize];
            questionLabels = new Label[labyrintSize, labyrintSize];
            answerTextBoxes = new TextBox[labyrintSize, labyrintSize];
            checkButtons = new Button[labyrintSize, labyrintSize];
            for (int i = 0; i < labyrintSize; i++)
            {
                for (int j = 0; j < labyrintSize; j++)
                {
                    cells[i, j] = new Button
                    {
                        Location = new Point(j * cellSize, i * cellSize),
                        Size = new Size(cellSize, cellSize),
                        BackColor = labyrinth[i, j] == 0 ? Color.White : Color.DarkGray,
                        Enabled = false,
                        Tag = new Point(i, j)
                    };
                    int row = i;
                    int col = j;
                    cells[i, j].Click += (sender, e) => TryMove(row, col);
                    labyrinthPanel.Controls.Add(cells[i, j]);
                    mathPanels[i, j] = new Panel
                    {
                        Location = new Point(labyrintSize * cellSize + 40, 60),
                        Size = new Size(240, 200),
                        BorderStyle = BorderStyle.FixedSingle,
                        Visible = false
                    };
                    this.Controls.Add(mathPanels[i, j]);
                    questionLabels[i, j] = new Label
                    {
                        Location = new Point(10, 20),
                        Size = new Size(220, 40),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Microsoft Sans Serif", 12)
                    };
                    mathPanels[i, j].Controls.Add(questionLabels[i, j]);
                    answerTextBoxes[i, j] = new TextBox
                    {
                        Location = new Point(70, 80),
                        Size = new Size(100, 30),
                        Font = new Font("Microsoft Sans Serif", 12)
                    };
                    int cellRow = i;
                    int cellCol = j;
                    answerTextBoxes[i, j].KeyPress += (sender, e) =>
                    {
                        if (e.KeyChar == (char)Keys.Enter)
                        {
                            e.Handled = true;
                            CheckAnswer(cellRow, cellCol);
                        }
                    };
                    mathPanels[i, j].Controls.Add(answerTextBoxes[i, j]);
                    checkButtons[i, j] = new Button
                    {
                        Location = new Point(70, 130),
                        Size = new Size(100, 40),
                        Text = "Tekshirish",
                        Font = new Font("Microsoft Sans Serif", 10)
                    };
                    checkButtons[i, j].Click += (sender, e) => CheckAnswer(row, col);
                    mathPanels[i, j].Controls.Add(checkButtons[i, j]);
                    if (!(i == 0 && j == 0) && !(i == labyrintSize - 1 && j == labyrintSize - 1))
                    {
                        GenerateMathProblem(i, j);
                    }
                }
            }
            cells[0, 0].BackColor = Color.LightGreen;
            cells[labyrintSize - 1, labyrintSize - 1].BackColor = Color.LightBlue;
            Label instructionsLabel = new Label
            {
                Text = "Matematik Labirint: Misollarni hal qilib labirintda harakatlaning.\nFinishga yetib boring!",
                Location = new Point(20, labyrintSize * cellSize + 70),
                Size = new Size(labyrintSize * cellSize + 240, 40),
                Font = new Font("Microsoft Sans Serif", 10)
            };
            this.Controls.Add(instructionsLabel);
        }
        private void GenerateMathProblem(int row, int col)
        {
            int num1, num2, result;
            string operation;
            string question;
            switch (difficulty)
            {
                case "Easy":
                    num1 = random.Next(1, 20);
                    num2 = random.Next(1, 20);
                    if (random.Next(2) == 0)
                    {
                        operation = "+";
                        result = num1 + num2;
                    }
                    else
                    {
                        operation = "-";
                        if (num1 < num2)
                        {
                            int temp = num1;
                            num1 = num2;
                            num2 = temp;
                        }
                        result = num1 - num2;
                    }
                    break;
                case "Medium":
                    num1 = random.Next(5, 30);
                    num2 = random.Next(2, 10);
                    int opChoice = random.Next(3);
                    if (opChoice == 0)
                    {
                        operation = "+";
                        result = num1 + num2;
                    }
                    else if (opChoice == 1)
                    {
                        operation = "-";
                        if (num1 < num2)
                        {
                            int temp = num1;
                            num1 = num2;
                            num2 = temp;
                        }
                        result = num1 - num2;
                    }
                    else
                    {
                        operation = "×";
                        result = num1 * num2;
                    }
                    break;
                default:
                    num1 = random.Next(10, 50);
                    num2 = random.Next(2, 15);
                    opChoice = random.Next(4);
                    if (opChoice == 0)
                    {
                        operation = "+";
                        result = num1 + num2;
                    }
                    else if (opChoice == 1)
                    {
                        operation = "-";
                        if (num1 < num2)
                        {
                            int temp = num1;
                            num1 = num2;
                            num2 = temp;
                        }
                        result = num1 - num2;
                    }
                    else if (opChoice == 2)
                    {
                        operation = "×";
                        result = num1 * num2;
                    }
                    else
                    {
                        operation = "÷";
                        result = num2;
                        num1 = num2 * random.Next(1, 10);
                        result = num1 / num2;
                    }
                    break;
            }
            question = $"{num1} {operation} {num2} = ?";
            questionLabels[row, col].Text = question;
            questionLabels[row, col].Tag = result; 
            totalQuestions++;
            UpdateScore();
        }
        private void UpdateScore()
        {
            scoreLabel.Text = $"Score: {correctAnswers}/{totalQuestions}";
        }
        private void MoveToCell(int row, int col)
        {
            cells[currentRow, currentCol].BackColor = Color.LightYellow;
            currentRow = row;
            currentCol = col;
            cells[currentRow, currentCol].BackColor = Color.Orange;
            for (int i = 0; i < labyrintSize; i++)
            {
                for (int j = 0; j < labyrintSize; j++)
                {
                    mathPanels[i, j].Visible = false;
                }
            }
            if (row == labyrintSize - 1 && col == labyrintSize - 1)
            {
                SaveGameStats();
                MessageBox.Show($"Tabriklayman! Siz labirintni tugatdingiz!\nTo'gri javoblar: {correctAnswers}/{totalQuestions}",
                               "O'yin tugadi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            EnableAdjacentCells();
        }
        private void EnableAdjacentCells()
        {
            for (int i = 0; i < labyrintSize; i++)
            {
                for (int j = 0; j < labyrintSize; j++)
                {
                    cells[i, j].Enabled = false;
                }
            }
            if (currentRow > 0 && labyrinth[currentRow - 1, currentCol] == 0)
            {
                cells[currentRow - 1, currentCol].Enabled = true;
            }
            if (currentRow < labyrintSize - 1 && labyrinth[currentRow + 1, currentCol] == 0)
            {
                cells[currentRow + 1, currentCol].Enabled = true;
            }
            if (currentCol > 0 && labyrinth[currentRow, currentCol - 1] == 0)
            {
                cells[currentRow, currentCol - 1].Enabled = true;
            }
            if (currentCol < labyrintSize - 1 && labyrinth[currentRow, currentCol + 1] == 0)
            {
                cells[currentRow, currentCol + 1].Enabled = true;
            }
        }
        private void TryMove(int row, int col)
        {
            if ((row == 0 && col == 0) || (row == labyrintSize - 1 && col == labyrintSize - 1))
            {
                MoveToCell(row, col);
                return;
            }
            mathPanels[row, col].Visible = true;
            answerTextBoxes[row, col].Clear();
            answerTextBoxes[row, col].Focus();
        }
        private void CheckAnswer(int row, int col)
        {
            int correctAnswer = (int)questionLabels[row, col].Tag;
            int userAnswer;
            if (int.TryParse(answerTextBoxes[row, col].Text, out userAnswer))
            {
                if (userAnswer == correctAnswer)
                {
                    correctAnswers++;
                    UpdateScore();
                    MessageBox.Show("To'g'ri javob!", "Barakalla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mathPanels[row, col].Visible = false;
                    MoveToCell(row, col);
                }
                else
                {
                    MessageBox.Show($"Noto'g'ri javob. Qaytadan urinib ko'ring.", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    answerTextBoxes[row, col].Clear();
                    answerTextBoxes[row, col].Focus();
                }
            }
            else
            {
                MessageBox.Show("Iltimos, raqam kiriting.", "Xato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                answerTextBoxes[row, col].Clear();
                answerTextBoxes[row, col].Focus();
            }
        }
        private void SaveGameStats()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int currentUserId = this.currentUserID;
                    connection.Open();
                    string query = "INSERT INTO GameStats (UserID, GameName, Score, Difficulty, TimeTaken, DatePlayed) " +
                                   "VALUES (@UserID, @GameName, @Score, @Difficulty, @TimeTaken, @DatePlayed)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserId); 
                        command.Parameters.AddWithValue("@GameName", "Raqamli Labirint");
                        command.Parameters.AddWithValue("@Score", correctAnswers);
                        command.Parameters.AddWithValue("@Difficulty", difficulty);
                        command.Parameters.AddWithValue("@TimeTaken", 0); // o'yin davomiyligini hisoblash kiritilishi kerak
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