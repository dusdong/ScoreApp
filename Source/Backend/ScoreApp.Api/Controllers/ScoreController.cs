using ScoreApp.Api.Models;
using ScoreApp.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ScoreApp.Api.Controllers
{
    [UserAppAuthorize]
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
        public PagedResult<ExpirationScore> GetAll([ModelBinder] Pagination pagination, bool timeUp = false)
        {
            var scores = new Collection<ExpirationScore>();
            var paged = scoreRepository.GetAll(pagination, timeUp);

            foreach (var score in paged.Items)
                scores.Add(new ExpirationScore(score));

            return new PagedResult<ExpirationScore>
            {
                CurrentPage = paged.CurrentPage,
                Items = scores,
                ItemsPerPage = paged.ItemsPerPage,
                TotalItems = paged.TotalItems
            };
        }

        [Route(":id")]
        [HttpGet]
        public ExpirationScore GetById(int id)
        {
            var score = scoreRepository.GetById(id);
            return new ExpirationScore(score);
        }
    }
}