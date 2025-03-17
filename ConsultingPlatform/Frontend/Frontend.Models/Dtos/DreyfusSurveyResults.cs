using Frontend.Models.Dtos.Enums;

namespace Frontend.Models.Dtos;

public class DreyfusSurveyResults
{
    private readonly Dictionary<DreyfusBlock, List<int>> _results = new();

    public DreyfusSurveyResults()
    {
        foreach (DreyfusBlock block in Enum.GetValues(typeof(DreyfusBlock)))
        {
            _results[block] = [];
        }
    }

    public void AddResponse(DreyfusBlock block, int score)
    {
        if (!_results.ContainsKey(block))
        {
            _results[block] = [];
        }
        _results[block].Add(score);
    }

    public List<int> GetScores(DreyfusBlock block)
    {
        return _results.TryGetValue(block, out var result) 
            ? [..result]
            : [];
    }

    public double GetAverageScoreForBlock(DreyfusBlock block)
    {
        return _results.ContainsKey(block) && _results[block].Count > 0
            ? _results[block].Average()
            : 0;
    }

    public Dictionary<DreyfusBlock, List<int>> GetAllResults()
    {
        return new Dictionary<DreyfusBlock, List<int>>(_results);
    }

    public double GetOverallAverageScore()
    {
        var allScores = _results.Values.SelectMany(scores => scores).ToList();
        return allScores.Count > 0 ? allScores.Average() : 0;
    }
}