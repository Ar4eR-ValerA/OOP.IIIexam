using System;
using System.Threading.Tasks;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class DriverService : IDriverService
    {
        private IRepository<DriverEntity> _driverRepository;
        
        public DriverService(IRepository<DriverEntity> driverRepo)
        {
            _driverRepository = driverRepo ?? throw new ArgumentNullException(nameof(driverRepo));
        }
        
        public Task RegisterDriver(DriverEntity driverEntity)
        {
            throw new NotImplementedException();
        }

        public Task UnregisterDriver(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetRating(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task<double> RateDriver(Guid driverId, double score)
        {
            throw new NotImplementedException();
        }

        public async Task SetLocation(Guid driverId, Point2d newLocation)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            driver.Location = new Point2dEntity
            {
                Id = Guid.NewGuid(),
                X = newLocation.X,
                Y = newLocation.Y,
            };
            await _driverRepository.UpdateAsync(driver);
        }

        public Task<Point2d> GetLocation(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task SetStatus(Guid driverId, DriverStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<DriverStatus> GetStatus(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task<TaxiType> GetTaxiType(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetNearestToLocation(Point2d location)
        {
            throw new NotImplementedException();
        }
    }
}