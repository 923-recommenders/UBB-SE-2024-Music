using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_Music.Data;
using UBB_SE_2024_Music.Models;

namespace UBB_SE_2024_Music.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByTwoIdentifiers(int id1, int id2)
        {
            return await context.Set<T>().FindAsync(id1, id2);
        }

        public async Task<T> GetByThreeIdentifiers(int id1, int id2, int id3)
        {
            return await context.Set<T>().FindAsync(id1, id2, id3);
        }

        public async Task<T> GetByThreeIdentifiers(int id1, int id2, DateTime id3)
        {
            return await context.Set<T>().FindAsync(id1, id2, id3);
        }

        public async Task<T> GetByFourIdentifiers(int id1, string id2, string id3, string id4)
        {
            return await context.Set<T>().FindAsync(id1, id2, id3, id4);
        }
    }
}
