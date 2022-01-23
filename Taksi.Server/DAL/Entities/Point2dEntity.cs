using System;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class Point2dEntity : IIdentifiable
    {
        public Point2dEntity()
        {
        }

        public Point2dEntity(double x, double y)
        {
            X = x;
            Y = y;
            Id = Guid.NewGuid();
        }


        public virtual Guid Id { get; set; }
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        
        public Point2d GetDto()
        {
            return new Point2d(X, Y);
        }

        public double DistanceTo(Point2dEntity targetPoint)
        {
            return Math.Sqrt((X - targetPoint.X) * (X - targetPoint.X) + (Y - targetPoint.Y) * (Y - targetPoint.Y));
        }
    }
}