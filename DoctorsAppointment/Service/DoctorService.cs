﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorsAppointment.Service
{
    public class DoctorService : ControllerBase, IDoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<GetDoctorModel>>> GetDoctors()
        {
            var doctors = new List<GetDoctorModel>();
            var dbDoctors = await _context.Doctors.ToListAsync();
            foreach (var item in dbDoctors)
            {
                doctors.Add(new GetDoctorModel
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Photo = item.Photo
                });
            }
            return Ok(doctors);
        }

        public async Task<ActionResult<GetDoctorModel>> GetDoctor(Guid id)
        {
            var dbDoctors = await _context.Doctors.ToListAsync();
            var doctor = new GetDoctorModel();
            foreach (var item in dbDoctors)
            {
                if (item.Id == id)
                    doctor = new GetDoctorModel
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                        Photo = item.Photo
                    };
            }
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            return Ok(doctor);
        }

        public async Task<ActionResult<List<GetPolyclinicModel>>> GetDoctorPolyclinics(Guid id)
        {
            var doctor = await _context.Doctors
                .Where(d => d.Id == id)
                .Include(p => p.Polyclinics)
                .ThenInclude(c => c.City)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var polyclinics = new List<GetPolyclinicModel>();
            foreach (var item in doctor.Polyclinics)
            {
                polyclinics.Add(new GetPolyclinicModel
                {
                    Id = item.Id,
                    Address = item.Address,
                    Photo = item.Photo,
                    Location = item.Location,
                    CityId = item.City.Id,
                    CityName = item.City.Name
                });
            }
            return Ok(polyclinics);
        }

        public async Task<ActionResult<List<GetSpecializationModel>>> GetDoctorSpecializations(Guid id)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == id)
                .Include(c => c.Specializations)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var specializations = new List<GetSpecializationModel>();
            foreach (var item in doctor.Specializations)
            {
                specializations.Add(new GetSpecializationModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(specializations);
        }

        public async Task<ActionResult> AddDoctor(AddDoctorDto request)
        {
            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Photo = request.Photo
            };

            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult> AddDoctorSpecialization(AddDoctorSpecializationDto request)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == request.DoctorId)
                .Include(c => c.Specializations)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var specialization = await _context.Specializations.FindAsync(request.SpecializationId);
            if (specialization == null)
                return BadRequest("Специализация не найдена.");

            doctor.Specializations.Add(specialization);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult> AddDoctorPolyclinic(AddDoctorPolyclinicDto request)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == request.DoctorId)
                .Include(c => c.Polyclinics)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var polyclinic = await _context.Polyclinics.FindAsync(request.PolyclinicId);
            if (polyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            doctor.Polyclinics.Add(polyclinic);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult> UpdateDoctor(UpdateDoctorDto request)
        {
            var dbDoctor = await _context.Doctors.FindAsync(request.Id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            dbDoctor.FullName = request.FullName;
            dbDoctor.Photo = request.Photo;

            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<ActionResult> DeleteDoctor(Guid id)
        {
            var dbDoctor = await _context.Doctors.FindAsync(id);
            if (dbDoctor == null)
                return BadRequest("Доктор не найден.");

            _context.Doctors.Remove(dbDoctor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult> DeleteDoctorSpecialization(AddDoctorSpecializationDto request)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == request.DoctorId)
                .Include(c => c.Specializations)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var specialization = await _context.Specializations.FindAsync(request.SpecializationId);
            if (specialization == null)
                return BadRequest("Специализация не найдена.");

            doctor.Specializations.Remove(specialization);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<ActionResult> DeleteDoctorPolyclinic(AddDoctorPolyclinicDto request)
        {
            var doctor = await _context.Doctors
                .Where(c => c.Id == request.DoctorId)
                .Include(c => c.Polyclinics)
                .FirstOrDefaultAsync();
            if (doctor == null)
                return BadRequest("Доктор не найден.");

            var polyclinic = await _context.Polyclinics.FindAsync(request.PolyclinicId);
            if (polyclinic == null)
                return BadRequest("Поликлиника не найдена.");

            doctor.Polyclinics.Remove(polyclinic);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}