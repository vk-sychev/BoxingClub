using AutoMapper;
using BoxingClub.BLL.DomainEntities;
using BoxingClub.BLL.Interfaces;
using BoxingClub.DAL.Entities;
using BoxingClub.DAL.Interfaces;
using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.BLL.Implementation.Services
{
    public class MedicalCertificateService : IMedicalCertificateService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public MedicalCertificateService(IUnitOfWork uow,
                                         IMapper mapper)
        {
            _database = uow ?? throw new ArgumentNullException(nameof(uow), "uow is null"); ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper is null");
        }

        public async Task<MedicalCertificateDTO> GetMedicalCertificateByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Medical Certificate's id is null");
            }
            var medicalCertificate = await _database.MedicalCertificates.GetByIdAsync(id.Value);
            if (medicalCertificate == null)
            {
                throw new NotFoundException($"Medical Certificate with id = {id.Value} isn't found", "");
            }
            var mappedCertificate = _mapper.Map<MedicalCertificateDTO>(medicalCertificate);
            return mappedCertificate;
        }

        public async Task<List<MedicalCertificateDTO>> GetMedicalCertificatesAsync()
        {
            var medicalCertificates = await _database.MedicalCertificates.GetAllAsync();
            var mappedCertificates = _mapper.Map<List<MedicalCertificateDTO>>(medicalCertificates);
            return mappedCertificates;
        }

        public async Task CreateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO)
        {
            if (certificateDTO == null)
            {
                throw new ArgumentNullException(nameof(certificateDTO), "Medical Certificate is null");
            }

            var medicalCertificate = _mapper.Map<MedicalCertificate>(certificateDTO);

            await _database.MedicalCertificates.CreateAsync(medicalCertificate);
            await _database.SaveAsync();
        }

        public async Task DeleteMedicalCertificateAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Medical Certificate's id is null");
            }

            var medicalCertificate = await _database.MedicalCertificates.GetByIdAsync(id.Value);

            if (medicalCertificate == null)
            {
                throw new NotFoundException($"Medical Certificate with id = {id.Value} isn't found", "");
            }
            _database.MedicalCertificates.Delete(medicalCertificate);
            await _database.SaveAsync();
        }

        public async Task UpdateMedicalCertificateAsync(MedicalCertificateDTO certificateDTO)
        {
            if (certificateDTO == null)
            {
                throw new ArgumentNullException(nameof(certificateDTO), "Medical Certificate is null");
            }

            var medicalCertificate = _mapper.Map<MedicalCertificate>(certificateDTO);
            _database.MedicalCertificates.Update(medicalCertificate);
            await _database.SaveAsync();
        }
    }
}
