using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        // CREATE VEHICLE
        public async Task<Guid> CreateVehicleAsync(CreateVehicleDto dto)
        {
            var vehicle = new Vehicle(dto.VehicleCode, dto.LocationId, dto.VehicleType);

            await _repository.AddAsync(vehicle);

            return vehicle.Id;
        }

        // GET VEHICLE BY ID
        public async Task<VehicleDto> GetVehicleByIdAsync(Guid id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                return null;

            return MapToDto(vehicle);
        }

        // GET ALL VEHICLE
        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _repository.GetAllAsync();

            return vehicles.Select(v => MapToDto(v)).ToList();
        }

        // UPDATE STATUS OF VEHICLE
        public async Task UpdateVehicleStatusAsync(Guid id, UpdateVehicleStatusDto dto)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new Exception("Vehicle not found.");

            switch (dto.Status)
            {
                case VehicleStatus.Available:
                    vehicle.MarkAvailable();
                    break;

                case VehicleStatus.Rented:
                    vehicle.MarkRented();
                    break;

                case VehicleStatus.Reserved:
                    vehicle.MarkReserved();
                    break;

                case VehicleStatus.UnderService:
                    vehicle.MarkServiced();
                    break;

                default:
                    throw new Exception("Invalid vehicle status.");
            }

            await _repository.UpdateAsync(vehicle);
        }

        // DELETE VEHICLE BT ID
        public async Task DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new Exception("Vehicle not found.");

            await _repository.DeleteAsync(vehicle);
        }

        // METHOD OF MAPPING
        private VehicleDto MapToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                VehicleCode = vehicle.VehicleCode,
                LocationId = vehicle.LocationId,
                VehicleType = vehicle.VehicleType,
                Status = vehicle.Status
            };
        }
    }
}
