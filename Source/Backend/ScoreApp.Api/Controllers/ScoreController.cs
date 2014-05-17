using ScoreApp.Domain;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ScoreApp.Api.Controllers
{
    [UserAppAuthorize]
    [RoutePrefix("scores")]
    public class ScoreController : ApiController
    {
        private readonly IScoreRepository scoreRepository;
        private readonly IScoreWitnessRepository scoreWitnessRepository;

        public ScoreController(IScoreRepository scoreRepository, IScoreWitnessRepository scoreWitnessRepository)
        {
            this.scoreRepository = scoreRepository;
            this.scoreWitnessRepository = scoreWitnessRepository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll([ModelBinder] Pagination pagination, bool timeUp = false)
        {
            var paged = scoreRepository.GetAll(pagination, timeUp);
            return Ok(paged);
        }

        [Route("{scoreId:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int scoreId)
        {
            var score = scoreRepository.GetById(scoreId);
            if (score == null)
                return NotFound();

            return Ok(score);
        }

        [Route("{scoreId:int}/witnesses")]
        [HttpGet]
        public IHttpActionResult GetScoreWitnesses(int scoreId)
        {
            var witnesses = scoreWitnessRepository.GetFromScore(scoreId);
            return Ok(witnesses);
        }
    }
}