namespace TicTacToe;

class Program
{
    static void Main()
    {
        var game = new Game();
        game.Start();
    }
}

class Game
{
    private readonly Board board = new();
    public void Start()
    {

        while (!board.IsCompeted())
        {
            board.Print();
            Console.WriteLine($"lets Player {board.currentPlayer.GetString()} Play Know");
            var playIndex = int.Parse(Console.ReadLine()!);
            board.PlayAt(playIndex - 1);
        }
        if (board.winner is Player player)
        {
            Console.WriteLine($"the winner is {player.GetString()}");

        }
        else
        {
            Console.WriteLine("The Game End With Draw");

        }

    }
}

class Board
{

    private int playCount = 0;
    public Player currentPlayer;

    public Board(Player currentPlayer)
    {
        this.currentPlayer = currentPlayer;
    }

    public Board()
    {
    }

    readonly public Player?[] items = [null, null, null, null, null, null, null, null, null];

    public void PlayAt(int index)
    {
        if (!CanPlayAt(index))
        {
            Console.WriteLine("Please Try Aging, this already taken");
            return;
        }
        playCount++;
        items[index] = currentPlayer;
        currentPlayer = currentPlayer.Other();
        CheckWinning();
    }

    public bool CanPlayAt(int index)
    {
        return items[index] == null;
    }

    public void Print()
    {
        for (int i = 0; i < 9; i++)
        {
            var stringToPrint = items[i]?.GetString() ?? $"{i + 1}";
            Console.Write($" {stringToPrint} ");
            if ((i + 1) % 3 == 0)
                Console.WriteLine();
        }
    }

    public bool IsCompeted()
    {
        return playCount == 9 || winner != null;
    }
    public Player? winner;
    private void CheckWinning()
    {
        (int, int, int)[] WinningIndexCase = [(1, 2, 3), (4, 5, 6), (7, 8, 9), (1, 4, 7), (2, 5, 8), (3, 6, 9), (1, 5, 9), (3, 5, 7)];
        foreach (var winCase in WinningIndexCase)
        {
            var item1 = items[winCase.Item1 - 1];
            var item2 = items[winCase.Item2 - 1];
            var item3 = items[winCase.Item3 - 1];

            if (item1 != null && item1 == item2 && item1 == item3)
            {
                winner = item1;
                break;
            }
        }
    }
}


enum Player
{
    x, o


}


static class EnumExt
{
    public static string GetString(this Player player) => player switch
    {
        Player.x => "X",
        Player.o => "O",
        _ => throw new NotImplementedException(),
    };

    public static Player Other(this Player player) => player switch { Player.x => Player.o, Player.o => Player.x, _ => throw new NotImplementedException(), };
}