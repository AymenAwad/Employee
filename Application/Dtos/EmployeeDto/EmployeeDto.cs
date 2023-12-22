
namespace Application.Dtos.EmployeeDto
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string Agency { get; set; }
        public string Department { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string NameEn { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string CuisineType { get; set; }
        public string ReservationPolicy { get; set; }
        public string SpecialFeatures { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
