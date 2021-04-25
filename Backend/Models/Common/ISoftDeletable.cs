namespace Backend.Models.Common
{
    public interface ISoftDeletable
    {
        public bool IsSoftDeleted { get; set; }
    }
}
