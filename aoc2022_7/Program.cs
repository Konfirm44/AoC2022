var line = "";
var root = Directory.CreateRoot("/");
var currentDir = root;
var directories = new List<Directory>() { root };

while ((line = Console.ReadLine()) is not null)
{
    var words = line.Trim().Split(' ');

    if (words[0] is "$")
    {
        if (words[1] is "cd")
        {
            var targetDir = words[2];
            currentDir = targetDir switch
            {
                "/" => currentDir = root,
                ".." => currentDir = currentDir.Parent,
                _ => (Directory)currentDir.Elements
                    .First(d => d.Name == targetDir)
            };
        }
        else //ls
        {
        }
    }
    else //ls output
    {
        if (words[0] is "dir")
        {
            currentDir.MakeDirectory(words[1], directories);
        }
        else //file
        {
            var size = int.Parse(words[0]);
            var file = new File(words[1], currentDir, size);
            currentDir.AddFile(file);
        }
    }
}

//var totalSizeOfSmallDirs =
//    directories.Where(d => d.Size < 100000).Sum(d => d.Size);
//Console.WriteLine(totalSizeOfSmallDirs);

var totalAvailable = 70000000;
var requiredFree = 30000000;
var maxTakenSize = totalAvailable - requiredFree;

var freeIfDirWasDeleted = directories
    .Select(d => new { d.Name, d.Size, Free = totalAvailable - (root.Size - d.Size) });
var smallestDirToDeleteToFulfillRequiredFree =
    freeIfDirWasDeleted.Where(d => d.Free >= requiredFree)
    .MinBy(d => d.Size).Size;

Console.WriteLine(smallestDirToDeleteToFulfillRequiredFree);

Console.WriteLine();

interface INode
{
    public string Name { get; }
    public int Size { get; }
    public Directory Parent { get; }
}

class Directory : INode
{
    public string Name { get; }
    public int Size { get => Elements.Sum(e => e.Size); }
    public IReadOnlyList<INode> Elements { get; } = new List<INode>();
    public Directory Parent { get; }

    private Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
    }

    private Directory(string name)
    {
        Name = name;
        Parent = this;
    }

    public static Directory CreateRoot(string name = "/")
    {
        return new(name);
    }

    public void MakeDirectory(string name, List<Directory>? list = null)
    {
        var dir = new Directory(name, this);
        ((List<INode>)Elements).Add(dir);
        list?.Add(dir);
    }

    public void AddFile(File file)
    {
        ((List<INode>)Elements).Add(file);
    }
}

class File : INode
{
    public string Name { get; }
    public int Size { get; }
    public Directory Parent { get; }

    public File(string name, Directory parent, int size)
    {
        Name = name;
        Parent = parent;
        Size = size;
    }
}