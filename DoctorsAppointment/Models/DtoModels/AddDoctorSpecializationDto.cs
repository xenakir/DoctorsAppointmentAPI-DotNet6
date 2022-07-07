namespace DoctorsAppointment.Models.DtoModels
{
    public class AddDoctorSpecializationDto
    {
        public Guid DoctorId { get; set; }
        public Guid SpecializationId { get; set; }
    }
}
