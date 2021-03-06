﻿using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IScoreRepository
    {
        Score Save(SaveScore score);
        Score GetById(int id);
        IPagedResult<Score> GetAll(Pagination pagination, bool timeUp = false);
    }
}
