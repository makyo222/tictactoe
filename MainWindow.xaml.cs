using System.Windows;
using System.Windows.Controls;
namespace tictactoe
{
    public partial class MainWindow : Window
    {
        public string CurrentPlayer="X";
        public int NumberOfMoves = 0;
        public bool GameOver = false;
        public bool Thinking = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameOver || NumberOfMoves == 9 || Thinking) return;
            var button=sender as Button;
            button.Content = CurrentPlayer;
            CurrentPlayer = "O";
            button.IsEnabled = false;
            NumberOfMoves++;
            ShowWinner();
            if (NumberOfMoves == 9) return;
            if (GameOver) return;
            Thinking=true;
            _=CPUMove();
        }
        private async Task CPUMove()
        {
            await Task.Delay(2000);
            int row = 0;
            int column = 0;
            do
            {
                row = Random.Shared.Next(0, 3);
                column = Random.Shared.Next(0, 3);
            }
            while ((string)(asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).Content) != "");
            var selectedbutton = asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
            selectedbutton.Content = CurrentPlayer;
            selectedbutton.IsEnabled = false;
            NumberOfMoves++;
            CurrentPlayer = "X";
            Thinking =false;
            ShowWinner();
        }
        private string GetPlayer(int row, int column)
        {
            return (string)asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).Content;
        }
        private string GetWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (GetPlayer(i, 0) != "" && GetPlayer(i, 0) == GetPlayer(i, 1) && GetPlayer(i, 0) == GetPlayer(i, 2))
                {
                    return GetPlayer(i, 0);
                }
                if (GetPlayer(0, i) != "" && GetPlayer(0, i) == GetPlayer(1, i) && GetPlayer(0, i) == GetPlayer(2, i))
                {
                    return GetPlayer(0, i);
                }
            }
            if (GetPlayer(0, 0) != "" && GetPlayer(0, 0) == GetPlayer(1, 1) && GetPlayer(0, 0) == GetPlayer(2, 2))
            {
                return GetPlayer(0, 0);
            }
            if (GetPlayer(0, 2) != "" && GetPlayer(0, 2) == GetPlayer(1, 1) && GetPlayer(0, 2) == GetPlayer(2, 0))
            {
                return GetPlayer(0, 2);
            }
            return "";
        }
        private void ShowWinner()
        {
            var winner = GetWinner();
            if (NumberOfMoves==9 && winner=="")
            {
                MessageBox.Show("Draw");
                GameOver=true;
                return;
            }
            if (winner=="") return;
            GameOver=true;
            if (winner=="X")
            {
                MessageBox.Show("Player Won");
                return;
            }
            MessageBox.Show("CPU Won");
        }
    }
}