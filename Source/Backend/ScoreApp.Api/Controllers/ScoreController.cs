using ScoreApp.Api.Models;
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
        [ValidateModel]
        public IHttpActionResult CreateScore([FromBody] CreateScoreModel model)
        {
            //TODO: validate if the users really exists.
            var user = Request.GetCurrentUser();
            using (var unit = unitOfWorkFactory.Create(transactional: true))
            {
                var score = scoreRepository.Save(model.ToSaveScore(user.Id));

                unit.Done();
                return Created(Url.Link("GetById", new { scoreId = score.Id }), score);
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

        [Route("{scoreId:int}")]
        [HttpPut]
        [ValidateModel]
        public IHttpActionResult UpdateScore(int scoreId, [FromBody] UpdateScoreModel model)
        {
            //TODO: make sure the creator is the only one who can edit.
            //TODO: if a vote already exists, only the reason can be edited.
            //TODO: if the score is expired (timeUp=true), then the update cannot happen.
            var current = scoreRepository.GetById(scoreId);
            if (current == null)
                return NotFound();

            var user = Request.GetCurrentUser();
            using (var unit = unitOfWorkFactory.Create(transactional: true))
            {
                scoreRepository.Save(model.ToSaveScore(current));
                unit.Done();

                return Ok();
            }
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