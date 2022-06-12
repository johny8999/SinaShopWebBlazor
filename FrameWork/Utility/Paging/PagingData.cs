using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Utility.Paging
{
    public static class PagingData
    {
        public static OutPagingData Calculate(long countAllItem, int page, int take)
        {
            try
            {
                int Skip = 0;
                int Take = 0;
                int CountPage = 5;
                int CountAllPage = 0;

                page = page == 0 ? 1 : page;

                if (countAllItem is 0)
                    return new OutPagingData(take);

                CountAllPage = (int)Math.Ceiling((decimal)countAllItem / take);
               take = countAllItem <take? (int)countAllItem : take;
                page = CountAllPage < page ? CountAllPage : page;

            }
            catch
            {
                return new OutPagingData(take);
            }
        }
    }
}
