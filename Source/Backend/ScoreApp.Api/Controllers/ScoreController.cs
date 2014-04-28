using ScoreApp.Api.Models;
using ScoreApp.Domain;
using System.Collections.Generic;
using System.Web.Http;

namespace ScoreApp.Api.Controllers
{
    [RoutePrefix("scores")]
    public class ScoreController : ApiController
    {
        private readonly IScoreRepository scoreRepository;

        public ScoreController(IScoreRepository scoreRepository)
        {
            this.scoreRepository = scoreRepository;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<ExpirationScore> GetAll(bool timeUp = false)
        {
            var scores = scoreRepository.GetAll(timeUp);
            foreach (var score in scores)
            {
                yield return new ExpirationScore(score);
            }
        }
    }
}