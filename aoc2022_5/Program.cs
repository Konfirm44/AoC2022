var line = "";

var crateStacks = new List<Stack<char>>();
var instructions = new List<Instruction>();
var parsingCrates = true;
var parsingStack = new Stack<Tuple<int, char>>();

while ((line = Console.ReadLine()) is not null)
{
    if (line.Length is 0)
    {
        parsingCrates = false;
        continue;
    }

    if (parsingCrates)
    {
        var maxSymbols = (line.Length + 1) / 4;
        var symbols = Enumerable.Range(0, maxSymbols)
            .Select(i => new { i, Symbol = line[(i * 4)..(i * 4 + 3)] })
            .Where(iS => iS.Symbol[1] is not ' ');

        if (symbols.First().Symbol.First() is '[')
        {
            symbols.ToList().ForEach(s => parsingStack.Push(new(s.i, s.Symbol[1])));
        }
        else
        {
            symbols.ToList().ForEach(s => crateStacks.Add(new()));
            while (parsingStack.TryPop(out var s))
            {
                var i = s.Item1;
                crateStacks[i].Push(s.Item2);
            }
        }
    }
    else //parsing instructions
    {
        var words = line.Split(' ');
        var count = int.Parse(words[1]);
        var from = int.Parse(words[3]);
        var to = int.Parse(words[5]);

        instructions.Add(new(count, from, to));
    }
}

foreach (var i in instructions)
{
    var movingStack = new Stack<char>();
    for (int j = 0; j < i.Count; j++)
    {
        var crate = crateStacks[i.From - 1].Pop();
        movingStack.Push(crate);
    }

    while (movingStack.TryPop(out var crate))
    {
        crateStacks[i.To - 1].Push(crate);
    }
}

var message = string.Join(null, crateStacks.Select(c => c.Peek()));
Console.WriteLine(message);

Console.WriteLine();


internal class Instruction
{
    public Instruction(int count, int from, int to)
    {
        Count = count;
        From = from;
        To = to;
    }

    public int Count { get; }
    public int From { get; }
    public int To { get; }
}