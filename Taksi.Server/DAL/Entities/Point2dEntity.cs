using System;
using Taksi.DTO.Models;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class Point2dEntity : IIdentifiable
    {

        public Point2dEntity(double x, double y)
        {
            X = x;
            Y = y;
            Id = Guid.NewGuid();
        }


        public Guid Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }


        public Point2d GetDto()
        {
            return new Point2d(X, Y);
        }
    }
}