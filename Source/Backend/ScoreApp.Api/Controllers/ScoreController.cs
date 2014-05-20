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
        private readonly IWitnessRepository witnessRepository;
        private readonly IVoterRepository voterRepository;

        public ScoreController(IScoreRepository scoreRepository, IWitnessRepository witnessRepository, IVoterRepository voterRepository)
        {
            this.scoreRepository = scoreRepository;
            this.witnessRepository = witnessRepository;
            this.voterRepository = voterRepository;
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
            try
            {
                var witnesses = witnessRepository.GetFromScore(scoreId);
                return Ok(witnesses);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("{scoreId:int}/voters")]
        [HttpGet]
        public IHttpActionResult GetScoreVoters(int scoreId)
        {
            try
            {
                var voters = voterRepository.GetFromScore(scoreId);
                return Ok(voters);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("{scoreId:int}/voters")]
        [HttpPost]
        [UserFilter]
        public IHttpActionResult ApplyVote(User user, int scoreId, [FromBody] bool isInFavor)
        {
            var voters = voterRepository.GetFromScore(scoreId);
            return Ok(voters);
        }
    }
}