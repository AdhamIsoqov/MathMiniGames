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
    public partial class Game2Form : Form
    {
        private TextBox[,] cells;
        private int[,] solution;
        private int[,] puzzle;
        private Button checkButton;
        private Button newGameButton;
        private Label timerLabel;
        private Label difficultyDisplayLabel;
        private Timer gameTimer;
        private int elapsedSeconds;
        private Random random = new Random();
        private string currentDifficulty;
        private int currentUserID;

        public Game2Form(string difficulty, int userID)
        {
            this.currentUserID = userID;
            InitializeComponent();
            this.currentDifficulty = difficulty;
            InitializeSudokuBoard();
            InitializeGameControls();
            GenerateNewPuzzle(currentDifficulty);
        }

        private void InitializeSudokuBoard()
        {
            cells = new TextBox[9, 9];
            puzzle = new int[9, 9];
            solution = new int[9, 9];

            int cellSize = 40;
            int margin = 2;
            Panel sudokuPanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size((cellSize + margin) * 9 + margin, (cellSize + margin) * 9 + margin),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(sudokuPanel);
            for (int blockRow = 0; blockRow < 3; blockRow++)
            {
                for (int blockCol = 0; blockCol < 3; blockCol++)
                {
                    Panel block = new Panel
                    {
                        Location = new Point(blockCol * (cellSize + margin) * 3, blockRow * (cellSize + margin) * 3),
                        Size = new Size((cellSize + margin) * 3 - margin, (cellSize + margin) * 3 - margin),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.LightGray
                    };
                    sudokuPanel.Controls.Add(block);

                    // Create 9 cells inside each 3x3 block
                    for (int cellRow = 0; cellRow < 3; cellRow++)
                    {
                        for (int cellCol = 0; cellCol < 3; cellCol++)
                        {
                            int row = blockRow * 3 + cellRow;
                            int col = blockCol * 3 + cellCol;

                            cells[row, col] = new TextBox
                            {
                                Location = new Point(cellCol * (cellSize + margin), cellRow * (cellSize + margin)),
                                Size = new Size(cellSize, cellSize),
                                MaxLength = 1,
                                TextAlign = HorizontalAlignment.Center,
                                Font = new Font("Arial", 16, FontStyle.Bold),
                                Tag = new Point(row, col)
                            };

                            cells[row, col].KeyPress += Cell_KeyPress;
                            cells[row, col].TextChanged += Cell_TextChanged;
                            block.Controls.Add(cells[row, col]);
                        }
                    }
                }
            }
        }

        private void InitializeGameControls()
        {
            // Title
            Label titleLabel = new Label
            {
                Text = "Raqamli Sudoku",
                Font = new Font("Arial", 18, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Timer
            timerLabel = new Label
            {
                Text = "Vaqt: 00:00",
                Font = new Font("Arial", 12),
                Location = new Point(400, 20),
                AutoSize = true
            };
            this.Controls.Add(timerLabel);

            gameTimer = new Timer
            {
                Interval = 1000
            };
            gameTimer.Tick += GameTimer_Tick;

            // Difficulty display label (instead of dropdown)
            Label difficultyStaticLabel = new Label
            {
                Text = "Qiyinlik:",
                Font = new Font("Arial", 12),
                Location = new Point(20, 460),
                AutoSize = true
            };
            this.Controls.Add(difficultyStaticLabel);

            difficultyDisplayLabel = new Label
            {
                Text = currentDifficulty,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(90, 460),
                AutoSize = true
            };
            this.Controls.Add(difficultyDisplayLabel);

            // New Game button
            newGameButton = new Button
            {
                Text = "Yangi o'yin",
                Location = new Point(230, 456),
                Size = new Size(120, 30)
            };
            newGameButton.Click += NewGameButton_Click;
            this.Controls.Add(newGameButton);

            // Check button
            checkButton = new Button
            {
                Text = "Tekshirish",
                Location = new Point(370, 456),
                Size = new Size(120, 30)
            };
            checkButton.Click += CheckButton_Click;
            this.Controls.Add(checkButton);

            // Set form properties
            this.Text = "Sudoku - " + currentDifficulty;
            this.Size = new Size(520, 550);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void SaveGameStats()
        {
            int currentUserId = this.currentUserID;
            string gameName = "Raqamli Sudoku";
            int score = 0;

            // Qiyinlik darajasiga qarab ball berish
            switch (currentDifficulty.ToLower())
            {
                case "oson":
                    score = 100;
                    break;
                case "o'rta":
                    score = 200;
                    break;
                case "qiyin":
                    score = 300;
                    break;
                default:
                    score = 100;
                    break;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectDB"].ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO GameStats (UserID, GameName, Score, Difficulty, TimeTaken) 
                             VALUES (@UserID, @GameName, @Score, @Difficulty, @TimeTaken)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", currentUserId);
                        command.Parameters.AddWithValue("@GameName", gameName);
                        command.Parameters.AddWithValue("@Score", score);
                        command.Parameters.AddWithValue("@Difficulty", currentDifficulty);
                        command.Parameters.AddWithValue("@TimeTaken", elapsedSeconds);

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
        private void GenerateNewPuzzle(string difficulty)
        {
            elapsedSeconds = 0;
            timerLabel.Text = "Vaqt: 00:00";
            gameTimer.Start();

            // 1. First generate a full solved board
            GenerateSolvedBoard();

            // 2. Copy solution to puzzle
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    puzzle[i, j] = solution[i, j];

            // 3. Remove cells based on difficulty
            int cellsToRemove;
            switch (difficulty)
            {
                case "Qiyin":
                    cellsToRemove = 60;
                    break;
                case "O'rta":
                    cellsToRemove = 50;
                    break;
                case "Oson":
                default:
                    cellsToRemove = 40;
                    break;
            }

            RemoveCells(cellsToRemove);

            // 4. Update UI
            UpdateBoardUI();
        }

        private void GenerateSolvedBoard()
        {
            // Clear the solution array
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = 0;

            // Fill the diagonal 3x3 boxes first (these can be filled independently)
            for (int box = 0; box < 9; box += 3)
                FillBox(box, box);

            // Fill the rest of the board using backtracking
            SolveSudoku();
        }

        private void FillBox(int row, int col)
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Shuffle(numbers);

            int index = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    solution[row + i, col + j] = numbers[index++];
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private bool SolveSudoku()
        {
            int row = -1, col = -1;
            bool isEmpty = true;

            // Find an empty cell
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (solution[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty) break;
            }

            // No empty cells left - solution complete
            if (isEmpty) return true;

            // Try digits 1-9 for the empty cell
            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(row, col, num))
                {
                    solution[row, col] = num;

                    if (SolveSudoku()) return true;

                    solution[row, col] = 0; // Backtrack if the solution didn't work
                }
            }
            return false;
        }

        private bool IsSafe(int row, int col, int num)
        {
            // Check if num is already in the row
            for (int j = 0; j < 9; j++)
                if (solution[row, j] == num)
                    return false;

            // Check if num is already in the column
            for (int i = 0; i < 9; i++)
                if (solution[i, col] == num)
                    return false;

            // Check if num is already in the 3x3 box
            int boxRow = row - row % 3;
            int boxCol = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (solution[boxRow + i, boxCol + j] == num)
                        return false;

            return true;
        }

        private void RemoveCells(int count)
        {
            int removed = 0;
            while (removed < count)
            {
                int row = random.Next(9);
                int col = random.Next(9);

                if (puzzle[row, col] != 0)
                {
                    puzzle[row, col] = 0;
                    removed++;
                }
            }
        }

        private void UpdateBoardUI()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j].Text = puzzle[i, j] == 0 ? "" : puzzle[i, j].ToString();

                    // Make initial cells read-only
                    bool isInitialCell = puzzle[i, j] != 0;
                    cells[i, j].ReadOnly = isInitialCell;
                    cells[i, j].BackColor = isInitialCell ? Color.LightBlue : Color.White;
                }
            }
        }

        private void Cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits 1-9 and control characters
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < '1' || e.KeyChar > '9'))
            {
                e.Handled = true;
            }
        }

        private void Cell_TextChanged(object sender, EventArgs e)
        {
            TextBox cell = sender as TextBox;
            if (cell != null && cell.Text.Length > 0)
            {
                // Auto-validate the move (optional)
                Point position = (Point)cell.Tag;
                int row = position.X;
                int col = position.Y;

                int value;
                if (int.TryParse(cell.Text, out value))
                {
                    // Highlight in red if the move violates Sudoku rules
                    if (!IsValidMove(row, col, value))
                    {
                        cell.ForeColor = Color.Red;
                    }
                    else
                    {
                        cell.ForeColor = Color.Black;
                    }
                }
            }
        }

        private bool IsValidMove(int row, int col, int value)
        {
            // Check row
            for (int j = 0; j < 9; j++)
            {
                if (j != col && cells[row, j].Text == value.ToString())
                    return false;
            }

            // Check column
            for (int i = 0; i < 9; i++)
            {
                if (i != row && cells[i, col].Text == value.ToString())
                    return false;
            }

            // Check 3x3 box
            int boxRow = row - row % 3;
            int boxCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int r = boxRow + i;
                    int c = boxCol + j;
                    if ((r != row || c != col) && cells[r, c].Text == value.ToString())
                        return false;
                }
            }

            return true;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            SaveGameStats();
            GenerateNewPuzzle(currentDifficulty);
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            bool isComplete = true;
            bool isCorrect = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Check if any cell is empty
                    if (string.IsNullOrEmpty(cells[i, j].Text))
                    {
                        isComplete = false;
                    }
                    else
                    {
                        int value = int.Parse(cells[i, j].Text);

                        // Check if the value matches the solution
                        if (value != solution[i, j])
                        {
                            isCorrect = false;
                        }
                    }
                }
            }

            if (!isComplete)
            {
                MessageBox.Show("O'yin tugallanmagan! Barcha kataklar to'ldirilishi kerak.",
                    "Sudoku", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!isCorrect)
            {
                MessageBox.Show("Xatoliklar bor! Yechimingizni tekshiring.",
                    "Sudoku", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SaveGameStats(); 
                gameTimer.Stop();
                MessageBox.Show($"Tabriklaymiz! Sudoku to'g'ri yechildi!\nYakunlangan vaqt: {FormatTime(elapsedSeconds)}",
                    "Sudoku", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            timerLabel.Text = "Vaqt: " + FormatTime(elapsedSeconds);
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            seconds %= 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}