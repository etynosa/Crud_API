namespace Crud_API.DomainModels
{
    public class StudentDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
