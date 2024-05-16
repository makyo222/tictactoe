using System.Windows;
using System.Windows.Controls;
namespace tictactoe;
public partial class MainWindow : Window {
    private int NumberOfMoves;
    private bool GameOver, Thinking;
    public MainWindow() {
        InitializeComponent();
    }
    private void Button_Click(object sender, RoutedEventArgs e) {
        if (GameOver || NumberOfMoves == 9 || Thinking) return;
        (sender as Button)!.Content = NumberOfMoves % 2 == 0 ? "X" : "O";
        (sender as Button)!.IsEnabled = false;
        NumberOfMoves++;
        ShowWinner();
        if (NumberOfMoves <= 9 && !GameOver) _ = CPUMove();
    }
    private async Task CPUMove() {
        Thinking = true;
        await Task.Delay(2000);
        int row, column;
        do {
            (row, column) = (Random.Shared.Next(0, 3), Random.Shared.Next(0, 3));
        } while (GetPlayer(row, column) != "");
        asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).Content = NumberOfMoves % 2 == 0 ? "X" : "O";
        asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).IsEnabled = false;
        NumberOfMoves++;
        Thinking = false;
        ShowWinner();
    }
    private string GetPlayer(int row, int column) => (string)asd.Children.Cast<Button>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).Content;
    private string GetWinner() {
        for (var i = 0; i < 3; i++) {
            if (GetPlayer(i, 0) != "" && GetPlayer(i, 0) == GetPlayer(i, 1) && GetPlayer(i, 0) == GetPlayer(i, 2)) return GetPlayer(i, 0);
            if (GetPlayer(0, i) != "" && GetPlayer(0, i) == GetPlayer(1, i) && GetPlayer(0, i) == GetPlayer(2, i)) return GetPlayer(0, i);
        }
        if (GetPlayer(0, 0) != "" && GetPlayer(0, 0) == GetPlayer(1, 1) && GetPlayer(0, 0) == GetPlayer(2, 2)) return GetPlayer(0, 0);
        if (GetPlayer(0, 2) != "" && GetPlayer(0, 2) == GetPlayer(1, 1) && GetPlayer(0, 2) == GetPlayer(2, 0)) return GetPlayer(0, 2);
        return "";
    }
    private void ShowWinner() {
        if (NumberOfMoves < 9 && GetWinner() == "") return;
        GameOver = true;
        MessageBox.Show(GetWinner() switch {
            "" => "Draw",
            "X" => "Player Won",
            "O" => "CPU Won", _ => ""
        });
    }
}