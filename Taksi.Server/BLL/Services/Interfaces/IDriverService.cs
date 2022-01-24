using System;
using System.Threading.Tasks;
using Taksi.DTO.Enums;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.BLL.Services.Interfaces
{
    public interface IDriverService
    {
        Task RegisterDriver(DriverEntity driverEntity);
        Task UnregisterDriver(Guid driverId);
        
        Task<double> GetRating(Guid driverId);
        
        Task RateDriver(Guid driverId, double score);

        // Driver should send it's location frequently
        Task SetLocation(Guid driverId, Point2dEntity newLocation);
        
        Task<Point2dEntity> GetLocation(Guid driverId);

        Task SetStatus(Guid driverId, DriverStatus newStatus);
        Task<DriverStatus> GetStatus(Guid driverId);
        Task<TaxiType> GetTaxiType(Guid driverId);
        
        Guid GetNearestToLocation(Point2dEntity location);
    }
}