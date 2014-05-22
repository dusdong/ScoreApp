using ScoreApp.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScoreApp.Api.Models
{
    /// <summary>
    /// Exposes only the properties which the client can send to update a score.
    /// </summary>
    public class UpdateScoreModel : IValidatableObject
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Candidate { get; set; }
        [Required]
        public IEnumerable<string> Witnesses { get; set; }

        public SaveScore ToSaveScore(Score current)
        {
            return new SaveScore
            {
                Candidate = Candidate,
                Creator = current.Creator.Id,
                Date = current.Date,
                Id = current.Id,
                Reason = Reason,
                TimeUp = false,
                Witnesses = Witnesses
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Witnesses.Any())
                yield return new ValidationResult("At least one Witness is required.", new[] { "Witnesses" });
        }
    }
}