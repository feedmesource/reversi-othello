namespace MenuSystem;

public class MenuItem
{
    public string Label { get; init; }
    private string Name { get; set; }
    private Func<string>? NameProvider { get; set; }
    public Func<string> MethodToRun { get; set; }

    public MenuItem(string  label, string name, Func<string> methodToRun, Func<string>? nameProvider = default)
    {
        Label = label.ToLowerInvariant();
        Name = name;
        MethodToRun = methodToRun;
        NameProvider = nameProvider;
    }

    private string GetName() => NameProvider?.Invoke() ?? Name; 

    public override string ToString()
    {
        return $"{Label}) {GetName()}";
    }
}