using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.Services
{
    public interface ITrainingWordProvider
    {
        Word Next(Guid userId);
    }
}