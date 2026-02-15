using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<List<Vehicle>> GetAllAsync();
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
    }
}
