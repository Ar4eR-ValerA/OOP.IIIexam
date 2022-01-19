using System;
using Taksi.DTO.Enums;
using Taksi.DTO.Models;

namespace Taksi.DTO.DTOs
{
    public class DriverDto
    {
        public DriverDto(Guid id, string fullName, DriverStatus status, TaxiType taxiType, double ratingSum,
            int countOfRatings, Point2d location)
        {
            Id = id;
            FullName = fullName;
            Status = status;
            TaxiType = taxiType;
            RatingSum = ratingSum;
            CountOfRatings = countOfRatings;
            Location = location;
        }

        public Guid Id { get; }
        public string FullName { get; }
        public DriverStatus Status { get; }
        public TaxiType TaxiType { get; }
        public double RatingSum { get; }
        public int CountOfRatings { get; }
        public Point2d Location { get; }
    }
}