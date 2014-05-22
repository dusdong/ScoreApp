using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScoreApp.Api.Models
{
    /// <summary>
    /// Exposes only the properties which the client can send to create a score.
    /// </summary>
    public class CreateScoreModel : IValidatableObject
    {
        [Required]
        public string Reason { get; set; }
        [Required]
        public string Candidate { get; set; }
        [Required]
        public IEnumerable<string> Witnesses { get; set; }

        public SaveScore ToSaveScore(string creator)
        {
            return new SaveScore
            {
                Candidate = Candidate,
                Creator = creator,
                Date = DateTime.Now,
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