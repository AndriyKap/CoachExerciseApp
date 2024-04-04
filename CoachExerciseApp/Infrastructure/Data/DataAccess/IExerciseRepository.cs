using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.API.Data.DataAccess
{
    public interface IExerciseRepository
    {
        Task<Exercise> Add(Exercise exercise);
        Task<List<Exercise>> GetAll();
        Task<Exercise> GetById(Guid id);
        Task<Exercise> Update(Exercise exercise);
    }
}
