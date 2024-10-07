namespace eStore.Helpers
{
    public class PagingParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int pageSize;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
