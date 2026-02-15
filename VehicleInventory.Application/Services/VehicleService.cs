using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateVehicleAsync(CreateVehicleDto dto)
        {
            var vehicle = new Vehicle(dto.VehicleCode, dto.LocationId, dto.VehicleType);

            await _repository.AddAsync(vehicle);

            return vehicle.Id;
        }
    }
}
