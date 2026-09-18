namespace MenuSystem;

public class MenuItem
{
    public string Label { get; init; }
    private string Name { get; set; }
    public Func<string> MethodToRun { get; set; }

    public MenuItem(string  label, string name, Func<string> methodToRun)
    {
        Label = label.ToLower();
        Name = name;
        MethodToRun = methodToRun;
    }

    public override string ToString()
    {
        return $"{Label}) {Name}";
    }
}