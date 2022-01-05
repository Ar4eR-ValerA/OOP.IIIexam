using System;
using System.Collections.Generic;
using Taksi.DTO.DTOs;

namespace Taksi.Server.Interfaces
{
    public interface IClient
    {
        void RegisterClient(ClientDto clientDto);
        void RegisterRide(RideDto rideDto);
        IEnumerable<RideDto> GetHistory(Guid clientId);
        void RateRide(Guid rideId, int rate);
    }
}