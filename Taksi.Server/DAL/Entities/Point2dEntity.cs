using System;
using Taksi.Server.DAL.Entities.Helpers;

namespace Taksi.Server.DAL.Entities
{
    public class Point2dEntity : IIdentifiable
    {
        public Guid Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }
}