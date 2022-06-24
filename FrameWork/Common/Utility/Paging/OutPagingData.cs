namespace FrameWork.Common.Utility.Paging
{
    public class OutPagingData
    {
        public OutPagingData()
        {

        }
        public OutPagingData(int Take)
        {
            CountAllItem = 0;
            CountAllPAge = 1;
            Page = 1;
            this.Take = Take;
            Skip = 0;
            StartPage = 1;
            EndtPage = 1;
        }
        public long CountAllItem { get; set; }
        public int CountAllPAge { get; set; }
        public int Page { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int StartPage { get; set; }
        public int EndtPage { get; set; }

    }
}
