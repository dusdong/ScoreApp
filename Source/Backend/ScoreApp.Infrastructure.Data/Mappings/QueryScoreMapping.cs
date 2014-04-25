using NPoco.FluentMappings;
using ScoreApp.Domain;
using ScoreApp.Infrastructure.Data.Models;

namespace ScoreApp.Infrastructure.Data.Mappings
{
    internal class QueryScoreMapping : Map<QueryScore>
    {
        public QueryScoreMapping()
        {
            TableName("Score");
        }
    }
}
