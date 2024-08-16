
namespace OnlineShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OnlineShopDbContext _onlineShopDbContext;

        public CategoryRepository(OnlineShopDbContext onlineShopDbContext)
        {
            _onlineShopDbContext = onlineShopDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get 
            {
                return _onlineShopDbContext.Categories.OrderBy(c => c.CategoryName);
            }
        }
    }
}
