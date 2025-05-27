namespace CompanyAPI.Application.Query
{
    public abstract class PageableQuery
    {
        private int _limit;
        public int Limit 
        { 
            get { return _limit == 0 ? 100 : _limit; }
            set { _limit = value; }
        }

        public int Offset { get; set; }
    }
}
