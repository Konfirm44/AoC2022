var elves = new List<Elf>();
var line = "";
var items = new List<int>();

do
{
    line = Console.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        items.Add(int.Parse(line));
    }
    else
    {
        elves.Add(new Elf(items));
        items = new();
    }

} while (line is not null);

var n = 1;

var elvesWithMostCalories = elves.OrderBy(e => e.Sum()).TakeLast(n);
var totalCalories = elvesWithMostCalories.Sum(e => e.Sum());

Console.WriteLine(totalCalories);
Console.WriteLine();

class Elf : List<int>
{
    public Elf(IEnumerable<int> collection) : base(collection)
    {
    }
}