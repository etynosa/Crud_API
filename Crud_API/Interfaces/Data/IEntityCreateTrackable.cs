namespace Crud_API.Interfaces.Data
{
    public interface IEntityCreateTrackable
    {
        DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }
    }
}
