var line = "";
var markerLength = 14; //4;

while ((line = Console.ReadLine()) is not null)
{
    Console.WriteLine(line);
    var startMarker = Enumerable.Range(markerLength, line.Length - markerLength + 1)
        .Select(i => line[(i - markerLength)..i])
        .Select(s => s.Distinct().Count())
        .ToList()
        .FindIndex(c => c == markerLength)
        + markerLength;
    Console.WriteLine(startMarker);
}

Console.WriteLine();