using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.Services
{
    public interface ITrainingWordProvider
    {
        Word Next(Guid userId);
    }
}