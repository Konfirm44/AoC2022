var line = "";
var priorities = new Dictionary<char, int>();
for (var i = 'a'; i <= 'z'; i++)
{
    var priority = i - 'a' + 1;
    priorities.Add(i, priority);
}

for (var i = 'A'; i <= 'Z'; i++)
{
    var priority = i - 'A' + 27;
    priorities.Add(i, priority);
}

var prioritySum = 0;

while ((line = Console.ReadLine()) is not null)
{
    var firstElf = line;
    var secondElf = Console.ReadLine();
    var thirdElf = Console.ReadLine();

    var badge = firstElf
        .Join(secondElf, f => f, s => s, (f, s) => f).Distinct()
        .Join(thirdElf, fs => fs, t => t, (fs, t) => t).Distinct()
        .Single();
    prioritySum += priorities[badge];
}

Console.WriteLine(prioritySum);
Console.WriteLine();