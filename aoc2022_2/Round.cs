namespace aoc2022_2;

internal class Round
{
    public required Move Theirs { get; init; }
    public required Move Yours { get; init; }
    public int Score() => (int)ResultMatrix.Instance[Theirs, Yours] + (int)Yours;
}
