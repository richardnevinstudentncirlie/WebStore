using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> Categories { get; }

        void SaveCategory(Category category);

        Category DeleteCategory(int categoryID);
    }
}