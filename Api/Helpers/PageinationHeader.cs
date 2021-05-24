namespace Api.Helpers
{
    public class PageinationHeader
    {
        public PageinationHeader(int currentPge, int itemPerPage, int totalItem, int totalPage)
        {
            CurrentPge = currentPge;
            ItemPerPage = itemPerPage;
            TotalItem = totalItem;
            TotalPage = totalPage;
        }

        public int CurrentPge { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
    }
}