public class Option
{
    public OptionTypeEnum Type { get; private set; }
    public string Description { get; private set; } // A text description to show players.

    public Option(OptionTypeEnum type, string description)
    {
        Type = type;
        Description = description;
    }
    
}