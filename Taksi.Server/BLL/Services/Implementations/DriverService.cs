using System;
using System.Linq;
using System.Threading.Tasks;
using Taksi.DTO.Enums;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Exceptions;
using Taksi.Server.DAL.Repositories.Interfaces;
using Taksi.Server.Logger;

namespace Taksi.Server.BLL.Services.Implementations
{
    public class DriverService : IDriverService
    {
        private readonly IRepository<DriverEntity> _driverRepository;
        private readonly ILogger _logger;

        public DriverService(IRepository<DriverEntity> driverRepo, ILogger logger)
        {
            _driverRepository = driverRepo ?? throw new ArgumentNullException(nameof(driverRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }

        public async Task RegisterDriver(DriverEntity driverEntity)
        {
            _logger.LogInfo($"Driver {driverEntity.Id} registered.");
            
            await _driverRepository.InsertAsync(driverEntity);
        }

        public async Task UnregisterDriver(Guid driverId)
        {
            _logger.LogInfo($"Driver {driverId} unregistered.");
            
            await _driverRepository.RemoveAsync(driverId);
        }

        public async Task<double> GetRating(Guid driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);

            if (driver.CountOfRatings == 0)
                return 0;

            _logger.LogInfo($"Get driver {driverId} rating.");
            
            return driver.RatingSum / driver.CountOfRatings;
        }

        public async Task RateDriver(Guid driverId, double score)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            driver.RatingSum += score;
            driver.CountOfRatings++;
            
            _logger.LogInfo($"Client rate driver {driverId}.");
            
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task SetLocation(Guid driverId, Point2dEntity newLocation)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            driver.Location ??= newLocation;
            driver.Location.X = newLocation.X;
            driver.Location.Y = newLocation.Y;
            
            _logger.LogInfo($"Driver {driverId} update location.");
            
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task<Point2dEntity> GetLocation(Guid driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            
            _logger.LogInfo($"Get driver {driverId} location.");
            
            return driver.Location;
        }

        public async Task SetStatus(Guid driverId, DriverStatus newStatus)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            driver.Status = newStatus;
            
            _logger.LogInfo($"Update driver {driverId} status.");
            
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task<DriverStatus> GetStatus(Guid driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            
            _logger.LogInfo($"Get driver {driverId} status.");
            
            return driver.Status;
        }

        public async Task<TaxiType> GetTaxiType(Guid driverId)
        {
            var driver = await _driverRepository.GetByIdAsync(driverId);
            
            _logger.LogInfo($"Get driver {driverId} taxi type.");
            
            return driver.TaxiType;
        }

        public Guid GetNearestToLocation(Point2dEntity location)
        {
            var drivers = _driverRepository.GetWhereAsync(driver => driver.Status.Equals(DriverStatus.WaitingForClient));
            var driverEntities = drivers.ToList();

            if (!driverEntities.Any())
                return Guid.Empty;

            DriverEntity nearestDriver = driverEntities.First();
            double minDistance = CalculateDistanceBetweenPoints(location, nearestDriver.Location);

            foreach (var driver in driverEntities)
            {
                double distance = CalculateDistanceBetweenPoints(location, driver.Location);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestDriver = driver;
                }
            }
            
            _logger.LogInfo($"Get nearest driver for location ({location.X};{location.Y}).");

            return nearestDriver.Id;
        }

        private double CalculateDistanceBetweenPoints(Point2dEntity firstPoint, Point2dEntity secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) +
                             Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }
    }
}