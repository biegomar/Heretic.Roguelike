namespace Heretic.Roguelike.Things.Interfaces;

public interface IValuable
{
    /// <summary>
    /// The value of the thing. Can be used e.g. for giving gold or food a certain value. 
    /// </summary>
    public int ActualValue { get; set; }
}