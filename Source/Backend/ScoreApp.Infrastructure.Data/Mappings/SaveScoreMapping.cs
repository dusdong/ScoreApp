using NPoco.FluentMappings;
using ScoreApp.Domain;

namespace ScoreApp.Infrastructure.Data.Mappings
{
    internal class SaveScoreMapping : Map<SaveScore>
    {
        public SaveScoreMapping()
        {
            PrimaryKey(p => p.Id, autoIncrement: true)
                .TableName("Scores")
                .Columns(x =>
                {
                    x.Column(c => c.Witnesses).Ignore();
                });
        }
    }
}
