namespace ETradeAPI.Application.DTOs.ConfigurationDto;

public class Menu
{
    public string Name { get; set; }

    public List<Action> Actions { get; set; } = new();
}