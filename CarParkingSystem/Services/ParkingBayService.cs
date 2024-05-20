using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParkingSystem.Services
{
    public class ParkingBayService
    {
        private readonly ApplicationDbContext _context;

        public ParkingBayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateOccupancyAsync(string baySection, string newStatus, List<string> specificBayNumbers = null)
        {
            var query = _context.ParkingBays.AsQueryable();

            // Filter by BaySection
            query = query.Where(pb => pb.BaySection == baySection);

            // If specific BayNumbers are provided, filter by them as well
            if (specificBayNumbers != null && specificBayNumbers.Count > 0)
            {
                query = query.Where(pb => specificBayNumbers.Contains(pb.BayNumber));
            }

            // Get the filtered parking bays
            var parkingBays = await query.ToListAsync();

            // Update occupancy status
            foreach (var parkingBay in parkingBays)
            {
                parkingBay.Occupacy = newStatus;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}

