using NPoco.FluentMappings;
using ScoreApp.Infrastructure.Data.Models;

namespace ScoreApp.Infrastructure.Data.Mappings
{
    internal class ScoreWitnessMapping : Map<ScoreWitness>
    {
        public ScoreWitnessMapping()
        {
            CompositePrimaryKey(p => p.ScoreId, p => p.Witness)
               .TableName("scorewitnesses");
        }
    }
}
