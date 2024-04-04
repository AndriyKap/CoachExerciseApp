using Infrastructure.API.Data.DataAccess;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.API.Data.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<Exercise> dbSet;

        public ExerciseRepository(AppDbContext context)
        {
            _context = context;
            dbSet = context.Set<Exercise>();
        }

        public async Task<Exercise> Add(Exercise exercise)
        {
            try
            {
                _context.Exercises.Add(exercise);
                await _context.SaveChangesAsync();
                return exercise;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while adding exercise: {ex.Message}");
                throw; 
            }
        }

        public async Task<List<Exercise>> GetAll()
        {
            var consumer = await dbSet.OrderBy(x => x.Id).ToListAsync();
            return consumer;
        }

        public async Task<Exercise> GetById(Guid id)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(c => c.Id == id);
            return exercise;
        }

        public async Task<Exercise> Update(Exercise exercise)
        {
            try
            {
                _context.Update(exercise);
                await _context.SaveChangesAsync();

                return exercise;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating exercise: {ex.Message}");
                throw;
            }
        }
    }
}
