using NPoco.FluentMappings;
using ScoreApp.Infrastructure.Data.Models;

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
