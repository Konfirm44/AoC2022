var line = "";
//var oneContainsTheOtherCount = 0;
var overlapCount = 0;

while ((line = Console.ReadLine()) is not null)
{
    var first = line.Split(',').First();
    var last = line.Split(',').Last();

    var firstRange = AsRange(first);
    var lastRange = AsRange(last);

    var overlap = firstRange.Join(lastRange, f => f, l => l, (f, l) => f);
    if (overlap.Any())
    {
        overlapCount++;
        //var firstEqualsOverlap = !firstRange.Except(overlap).Any();
        //var lastEqualsOverlap = !lastRange.Except(overlap).Any();
        //if (firstEqualsOverlap || lastEqualsOverlap)
        //{
        //    oneContainsTheOtherCount++;
        //}
    }
}

Console.WriteLine(overlapCount);
//Console.WriteLine(oneContainsTheOtherCount);
Console.WriteLine();

static IEnumerable<int> AsRange(string str)
{
    var start = int.Parse(str.Split('-').First());
    var end = int.Parse(str.Split('-').Last());
    var count = end - start + 1;
    return Enumerable.Range(start, count);
}