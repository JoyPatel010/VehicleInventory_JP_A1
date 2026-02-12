using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string VehicleCode { get; private set; }
        public string LocationId { get; private set; }
        public string VehicleType { get; private set; }
        public VehicleStatus Status { get; private set; }

        // Required for EF Core
        private Vehicle() { }

        public Vehicle(string vehicleCode, string locationId, string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new ArgumentException("Vehicle code is required.");

            if (string.IsNullOrWhiteSpace(locationId))
                throw new ArgumentException("Location is required.");

            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new ArgumentException("Vehicle type is required.");

            Id = Guid.NewGuid();
            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = VehicleStatus.Available;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is already rented.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Reserved vehicle cannot be rented.");

            if (Status == VehicleStatus.UnderService)
                throw new DomainException("Vehicle under service cannot be rented.");

            Status = VehicleStatus.Rented;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Rented vehicle cannot be reserved.");

            if (Status == VehicleStatus.UnderService)
                throw new DomainException("Vehicle under service cannot be reserved.");

            Status = VehicleStatus.Reserved;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Rented vehicle cannot go to service.");

            Status = VehicleStatus.UnderService;
        }

        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Reserved vehicle cannot be made available without release.");

            Status = VehicleStatus.Available;
        }
    }
}
