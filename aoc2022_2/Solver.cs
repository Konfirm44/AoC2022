namespace aoc2022_2;

public class Solver
{
    public enum SolvingStrategy
    {
        RoundFromMoveAndMove,
        RoundFromMoveAndResult
    }

    public static void Solve(Func<string?> readInput, Action<int> writeOutput, SolvingStrategy strategy)
    {
        Func<char, char, Round> parseRound = strategy is SolvingStrategy.RoundFromMoveAndMove ? RoundFromMoveAndMove : RoundFromMoveAndResult;

        var rounds = new List<Round>();
        var line = "";

        while ((line = readInput()) is not null)
        {
            var abc = line[0];
            var xyz = line[2];

            var round = parseRound(abc, xyz);
            rounds.Add(round);
        }

        var totalScore = rounds.Sum(r => r.Score());
        writeOutput(totalScore);
    }

    static Round RoundFromMoveAndMove(char abc, char xyz)
    {
        var theirMove = ParseMove(abc);
        var yourMove = ParseMove(xyz);

        return new Round { Theirs = theirMove, Yours = yourMove };
    }

    static Round RoundFromMoveAndResult(char abc, char xyz)
    {
        var theirMove = ParseMove(abc);
        var result = ParseResult(xyz);
        var yourMove = ResultMatrix.Instance.YoursFromTheirsAndResult(theirMove, result);

        return new Round { Theirs = theirMove, Yours = yourMove };
    }

    static Move ParseMove(char abcxyz) => abcxyz switch
    {
        'A' or 'X' => Move.Rock,
        'B' or 'Y' => Move.Paper,
        'C' or 'Z' => Move.Scissors,
        _ => throw new NotImplementedException(),
    };

    static Result ParseResult(char xyz) => xyz switch
    {
        'X' => Result.Lost,
        'Y' => Result.Draw,
        'Z' => Result.Won,
        _ => throw new NotImplementedException(),
    };
}
