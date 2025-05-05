using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Consulting.Auth.Infrastructure.mongo
{
    public class TraitScore
    {
        public string Trait { get; set; } = null!;
        public double Score { get; set; }
    }

    public class TestResult
    {
        [BsonId] public ObjectId Id { get; set; }

        public string UserId { get; set; } = null!; // Ідентифікатор із Postgres

        public int DreyfusScore { get; set; }

        public List<TraitScore> CareerTraits { get; set; } = new();
    }

    public class Vacancy
    {
        [BsonId] public ObjectId Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int RequiredDreyfusScore { get; set; }

        public List<TraitScore> RequiredTraits { get; set; } = new();
    }
}