using ScoreApp.Api.Models;
using ScoreApp.Domain;
using ScoreApp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScoreApp.Api.Controllers
{
    [RoutePrefix("scores")]
    public class ScoreController : ApiController
    {
        private readonly IScoreRepository scoreRepository;
        private readonly IExpirationDateCalculator expirationCalculator;

        public ScoreController(IScoreRepository scoreRepository, IExpirationDateCalculator expirationCalculator)
        {
            this.scoreRepository = scoreRepository;
            this.expirationCalculator = expirationCalculator;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<ExpirationScore> GetAll(bool timeUp = false)
        {
            var scores = scoreRepository.GetAll(timeUp);
            foreach (var score in scores)
            {
                yield return new ExpirationScore(score)
                {
                    ExpirationDate = expirationCalculator.Calculate(score.Date)
                };
            }
        }
    }
}