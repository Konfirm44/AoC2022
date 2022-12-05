namespace aoc2022_2;

internal class ResultMatrix
{
    private readonly Result[,] matrix = new Result[3, 3];
    public static ResultMatrix Instance { get; } = new ResultMatrix();

    public Result this[Move theirs, Move yours]
    {
        get => matrix[(int)theirs - 1, (int)yours - 1];
        private set => matrix[(int)theirs - 1, (int)yours - 1] = value;
    }

    public Move YoursFromTheirsAndResult(Move theirs, Result result)
    {
        var theirsRow = Enumerable.Range(0, 3)
            .Select(e => matrix[(int)theirs - 1, e]).ToArray();
        var column = Array.IndexOf(theirsRow, result);
        return (Move)(column + 1);
    }

    private ResultMatrix()
    {
        foreach (var i in Enumerable.Range(0, 3))
        {
            matrix[i, i] = Result.Draw;
        }

        this[Move.Rock, Move.Paper] = Result.Won;
        this[Move.Rock, Move.Scissors] = Result.Lost;
        this[Move.Paper, Move.Rock] = Result.Lost;
        this[Move.Paper, Move.Scissors] = Result.Won;
        this[Move.Scissors, Move.Rock] = Result.Won;
        this[Move.Scissors, Move.Paper] = Result.Lost;
    }
}
