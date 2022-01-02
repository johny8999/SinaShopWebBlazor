using SinaShop.Domain.Region.LanguageAgg.Contract;
using SinaShop.Domain.Region.LanguageAgg.Entities;
using SinaShop.Infrastructure.EfCore.Context;

namespace SinaShop.Infrastructure.EfCore.Repository.Languages
{
    public class LanguageRepository : BaseRepository<TblLanguages>, ILanguageRepository
    {
        public LanguageRepository(MainContext context) : base(context)
        {

        }
    }
}
