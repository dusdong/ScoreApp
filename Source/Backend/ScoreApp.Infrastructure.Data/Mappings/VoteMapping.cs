using NPoco.FluentMappings;
using ScoreApp.Domain;

namespace ScoreApp.Infrastructure.Data.Mappings
{
    internal class VoteMapping : Map<Vote>
    {
        public VoteMapping()
        {
            CompositePrimaryKey(p => p.ScoreId, p => p.User)
               .TableName("votes");
        }
    }
}
