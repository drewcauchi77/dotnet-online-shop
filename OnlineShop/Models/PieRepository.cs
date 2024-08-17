
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly OnlineShopDbContext _onlineShopDbContext;

        public PieRepository(OnlineShopDbContext onlineShopDbContext) 
        {
            _onlineShopDbContext = onlineShopDbContext;   
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _onlineShopDbContext.Pies.Include(p => p.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _onlineShopDbContext.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int id)
        {
            return _onlineShopDbContext.Pies.FirstOrDefault(p => p.PieId == id);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _onlineShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
