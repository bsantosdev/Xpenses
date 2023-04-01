using Xpenses.API.Models;

namespace Xpenses.API.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> ReadAll();

        Category ReadById(Guid id);

        void Create(Category entity);

        void Update(Category entity);

        void Delete(Guid id);
    }
}