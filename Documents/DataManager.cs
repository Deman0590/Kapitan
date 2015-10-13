using Documents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documents
{
    public class DataManager
    {
        private IDocumentRepository documentRepository;
        private IDocumentTypeRepository documentTypeRepository;
        private IFileRepository fileRepository;
        private IOrganizationListRepository organizationListRepository;
        private IVehicleTypeRepository vehicleTypeRepository;
        private ICarRepository carRepository;

        public DataManager(IDocumentTypeRepository documentTypeRepository,
            IDocumentRepository documentRepository,
            IFileRepository fileRepository,
            IOrganizationListRepository organizationListRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            ICarRepository carRepository)
        {
            this.documentRepository = documentRepository;
            this.documentTypeRepository = documentTypeRepository;
            this.fileRepository = fileRepository;
            this.organizationListRepository = organizationListRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
            this.carRepository = carRepository;
        }
        public IDocumentRepository Documents { get { return documentRepository; } }
        public IDocumentTypeRepository DocumentTypes { get { return documentTypeRepository; } }
        public IFileRepository Files { get { return fileRepository; } }
        public IOrganizationListRepository Organizations { get { return organizationListRepository; } }
        public IVehicleTypeRepository VehicleTypes { get { return vehicleTypeRepository; } }
        public ICarRepository Cars { get { return carRepository; } }
    }
}