using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private const int boardSize = 8;
        private const int cellSize = 50;
        private Button[,] chessBoard = new Button[boardSize, boardSize];
        private Point? firstSelection = null;
        private Point? secondSelection = null;

        public Form1()
        {
            InitializeComponent();
            CreateChessBoard();
        }

        private void CreateChessBoard()
        {
            Color darkColor = Color.Gray;
            Color lightColor = Color.White;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button cell = new Button();
                    cell.Size = new Size(cellSize, cellSize);
                    cell.Location = new Point((col + 1) * cellSize + 10, (row + 1) * cellSize + 10);
                    cell.BackColor = (row + col) % 2 == 0 ? lightColor : darkColor;
                    cell.Tag = new Point(col, row);
                    cell.Click += Cell_Click;
                    this.Controls.Add(cell);
                    chessBoard[row, col] = cell;
                }
            }
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button clickedCell = (Button)sender;
            Point position = (Point)clickedCell.Tag;

            if (firstSelection == null)
            {
                firstSelection = position;
                clickedCell.BackColor = Color.LightGreen;
            }
            else if (secondSelection == null && position != firstSelection)
            {
                secondSelection = position;
                clickedCell.BackColor = Color.LightBlue;
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            if (firstSelection == null || secondSelection == null)
            {
                MessageBox.Show("Пожалуйста, выберите две клетки!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool isValidMove = IsValidQueenMove(firstSelection.Value, secondSelection.Value);

            if (isValidMove)
            {
                MessageBox.Show("Ферзь может так ходить",
                    "Результат проверки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ферзь НЕ может так ходить",
                    "Результат проверки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            ResetSelections();
        }

        private bool IsValidQueenMove(Point from, Point to)
        {
            return from.X == to.X || from.Y == to.Y ||
                   Math.Abs(from.X - to.X) == Math.Abs(from.Y - to.Y);
        }

        private void ResetSelections()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    chessBoard[row, col].BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
                }
            }

            firstSelection = null;
            secondSelection = null;
        }
    }
}