namespace DoctorsAppointment.Models.DtoModels
{
    public class UpdateDoctorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
    }
}
