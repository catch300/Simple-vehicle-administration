namespace Project_service.PagingFIlteringSorting
{
    public interface IFiltering
    {
        string CurrentFilter { get; set; }
        string SearchString { get; set; }
    }
}