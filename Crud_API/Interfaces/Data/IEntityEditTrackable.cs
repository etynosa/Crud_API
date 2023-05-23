namespace Crud_API.Interfaces.Data
{
    public interface IEntityEditTrackable
    {
        DateTime? ModifiedOn { get; set; }

        string ModifiedBy { get; set; }
    }
}
