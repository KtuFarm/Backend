namespace Backend.Models.Database
{
    public interface ISoftDeletable
    {
        public bool IsSoftDeleted { get; set; }
    }
}
