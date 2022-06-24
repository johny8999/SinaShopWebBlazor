namespace FrameWork.Common.Utility.Paging
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
                take = countAllItem < take ? (int)countAllItem : take;
                page = CountAllPage < page ? CountAllPage : page;

                Skip = (Take * page) - Take;
                Skip = Skip < 0 ? 0 : Skip;

                int StartPage = (page - CountPage) <= 0 ? 1 : page - CountPage;
                int EndPage = (page + CountPage) > (int)countAllItem ? (int)countAllItem : page + CountPage;

                return new OutPagingData()
                {
                    CountAllItem = countAllItem,
                    CountAllPAge = CountAllPage,
                    Page = page,
                    Take = Take,
                    Skip = Skip,
                    StartPage = StartPage,
                    EndtPage = EndPage
                };
            }
            catch
            {
                return new OutPagingData(take);
            }
        }
    }
}
