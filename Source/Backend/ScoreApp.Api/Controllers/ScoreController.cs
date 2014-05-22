using ScoreApp.Domain;
using ScoreApp.Domain.Factories;
using System;
using System.Net.Http;
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
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public ScoreController(IUnitOfWorkFactory unitOfWorkFactory, IScoreRepository scoreRepository,
            IWitnessRepository witnessRepository, IVoterRepository voterRepository)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.scoreRepository = scoreRepository;
            this.witnessRepository = witnessRepository;
            this.voterRepository = voterRepository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll([ModelBinder(BinderType = typeof(PaginationBinder))] Pagination pagination, bool timeUp = false)
        {
            var paged = scoreRepository.GetAll(pagination, timeUp);
            return Ok(paged);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult AddScore([FromBody] SaveScore saveScore)
        {
            try
            {
                var user = Request.GetCurrentUser();
                using (var unit = unitOfWorkFactory.Create(transactional: true))
                {
                    saveScore.Date = DateTime.Now;
                    saveScore.Creator = user.Id;
                    var score = scoreRepository.Save(saveScore);

                    unit.Done();
                    return Created(Url.Link("GetById", new { scoreId = score.Id }), score);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{scoreId:int}", Name = "GetById")]
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
        public IHttpActionResult GetScoreVoters(int scoreId, bool? isInFavor = null)
        {
            try
            {
                var voters = voterRepository.GetFromScore(scoreId, isInFavor);
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
        public IHttpActionResult SaveVote(User user, int scoreId, bool isInFavor)
        {
            try
            {
                using (var unit = unitOfWorkFactory.Create())
                {
                    var vote = new Vote
                    {
                        Date = DateTime.Now,
                        IsInFavor = isInFavor,
                        ScoreId = scoreId,
                        User = user.Id
                    };
                    voterRepository.SaveVote(vote);
                    unit.Done();

                    return Ok();
                }
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}