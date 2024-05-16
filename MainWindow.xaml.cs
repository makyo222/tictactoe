using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameBoard TicTacToe = new GameBoard();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = TicTacToe;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;
            clickedButton.Background = Brushes.White;
            clickedButton.Content = TicTacToe.currentPlayer;
            clickedButton.IsHitTestVisible = false;
            TicTacToe.UpdateBoard(clickedButton.Name);
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(MyGrid); i++)
            {
                var child = VisualTreeHelper.GetChild(MyGrid, i) as Button;
                if (child == null) continue;
                child.Content = null;
                child.IsHitTestVisible = true;
            }
            TicTacToe = new GameBoard();
            this.DataContext = TicTacToe;
        }
    }

    public class GameBoard : INotifyPropertyChanged
    {
        
        public enum CurrentPlayer
        {
            X = 1,
            O
        }

        private int turn = 1;
        public CurrentPlayer currentPlayer = CurrentPlayer.X;
        private bool hasWon = false;
        public bool HasWon
        {
            get { return hasWon; }
            set { hasWon = value; NotifyPropertyChanged("HasWon"); }
        }

        private Dictionary<string, int> board = new Dictionary<string, int>()
            {
                {"TopXLeft",0 },
                {"TopXMiddle",0 },
                {"TopXRight",0 },
                {"CenterXLeft",0 },
                {"CenterXMiddle",0 },
                {"CenterXRight",0 },
                {"BottomXLeft",0 },
                {"BottomXMiddle",0 },
                {"BottomXRight",0 }
            };

        public void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private bool CheckIfWon(string buttonName)
        {
            if (WonInRow(buttonName))
            {
                return true;
            }
            else if (WonInColumn(buttonName))
            {
                return true;
            }
            else if (WonInDiagonal(buttonName))
            {
                return true;
            }
            else
                return false;
        }

        private bool WonInRow(string name)
        {
            string row = name.Substring(0, name.IndexOf('X') - 1);

            foreach (var element in board)
            {
                string keyName = element.Key;
                string rowOfElement = keyName.Substring(0, keyName.IndexOf('X') - 1);

                if (rowOfElement == row)
                {
                    if (element.Value != (int)currentPlayer)
                        return false;
                }
            }

            return true;
        }

        private bool WonInColumn(string name)
        {
            string column = name.Substring(name.IndexOf('X') + 1);

            foreach (var element in board)
            {
                string keyName = element.Key;
                string columnOfElement = keyName.Substring(keyName.IndexOf('X') + 1);

                if (columnOfElement == column)
                {
                    if (element.Value != (int)currentPlayer)
                        return false;
                }
            }

            return true;
        }

        private bool WonInDiagonal(string name)
        {
            if (name == "TopXLeft" || name == "CenterXMiddle" || name == "BottomXRight")
            {
                if (board["CenterXMiddle"] == (int)currentPlayer && board["BottomXRight"] == (int)currentPlayer && board["TopXLeft"] == (int)currentPlayer)
                {
                    return true;
                }
                else
                    return false;
            }
            if (name == "TopXRight" || name == "CenterXMiddle" || name == "BottomXLeft")
            {
                if (board["CenterXMiddle"] == (int)currentPlayer && board["BottomXLeft"] == (int)currentPlayer && board["TopXRight"] == (int)currentPlayer)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void UpdateDictionary(string buttonName)
        {
            string tileName = buttonName;

            board[tileName] = (int)currentPlayer;
        }

        public void UpdateBoard(string buttonName)
        {
            UpdateDictionary(buttonName);

            if (turn >= 5)
            {
                if (CheckIfWon(buttonName))
                {
                    HasWon = true;
                }
            }

            turn++;

            if (currentPlayer == CurrentPlayer.X)
                currentPlayer = CurrentPlayer.O;

            else if (currentPlayer == CurrentPlayer.O)
                currentPlayer = CurrentPlayer.X;
        }
    }
}