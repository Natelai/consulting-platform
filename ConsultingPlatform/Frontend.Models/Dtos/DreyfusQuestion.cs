namespace Frontend.Models.Dtos;

public class DreyfusQuestion
{
    public string Question { get; set; }
    public Dictionary<string, int> QuestionRates { get; set; }
}