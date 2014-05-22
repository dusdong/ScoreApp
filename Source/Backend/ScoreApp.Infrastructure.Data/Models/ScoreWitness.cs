
namespace ScoreApp.Infrastructure.Data.Models
{
    internal class ScoreWitness
    {
        public int ScoreId { get; set; }
        public string Witness { get; set; }

        public static ScoreWitness Create(int scoreId, string witness)
        {
            return new ScoreWitness
            {
                ScoreId = scoreId,
                Witness = witness
            };
        }
    }
}
