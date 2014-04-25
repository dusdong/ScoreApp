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
    public class ScoreController : ApiController
    {
        private readonly IScoreRepository scoreRepository;
        private readonly IExpirationDateCalculator expirationCalculator;

        public ScoreController(IScoreRepository scoreRepository, IExpirationDateCalculator expirationCalculator)
        {
            this.scoreRepository = scoreRepository;
            this.expirationCalculator = expirationCalculator;
        }

        // GET api/<controller>
        public IEnumerable<ExpirationScore> Get(bool timeUp)
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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}