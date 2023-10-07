using MongoDB.Driver;
using TravelerAppService.Context;
using TravelerAppService.Models;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppService.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookingCollection;

        public BookingService(MongoDBContext dbContext)
        {
            _bookingCollection = dbContext.GetCollection<Booking>("booking");
        }

        //create
        public async Task CreateAsync(Booking booking)
        {
            // Check if the NIC has already made 4 bookings
            var nic = booking.NationalIdentificationCard;
            var existingBookingsCount = await _bookingCollection
                .CountDocumentsAsync(b => b.NationalIdentificationCard == nic);

            if (existingBookingsCount >= 4)
            {
                throw new InvalidOperationException("The maximum number of bookings for this NIC has been reached.");
            }

            // Continue with booking creation if the NIC is within the limit
            await _bookingCollection.InsertOneAsync(booking);
        }

        //get
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            var filter = Builders<Booking>.Filter.Empty;
            var bookings = await _bookingCollection.Find(filter).ToListAsync();
            return bookings;
        }

        //getById
        public async Task<Booking> GetByIdAsync(string id)
        {
            var filter = Builders<Booking>.Filter.Eq(x => x.Id, id);
            return await _bookingCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Booking model)
        {
            // Retrieve the existing booking
            var existingBooking = await _bookingCollection.Find(b => b.Id == id).FirstOrDefaultAsync();

            if (existingBooking == null)
            {
                throw new Exception("Booking not found."); // Handle accordingly
            }

            // Calculate the difference between the reservation date and the current date
            TimeSpan dateDifference = existingBooking.ReservationDate - DateTime.UtcNow;

            // Check if the difference is less than or equal to 5 days
            if (dateDifference.TotalDays <= 5)
            {
                throw new InvalidOperationException("You cannot update the booking as the reservation date is less than or equal to 5 days from the current date.");
            }

            // If the difference is greater than 5 days, allow the update
            var filter = Builders<Booking>.Filter.Eq(x => x.Id, id);
            await _bookingCollection.ReplaceOneAsync(filter, model);
        }

        //delete
        public async Task DeleteAsync(string id)
        {
            // Retrieve the existing booking
            var existingBooking = await _bookingCollection.Find(b => b.Id == id).FirstOrDefaultAsync();

            if (existingBooking == null)
            {
                throw new Exception("Booking not found."); // Handle accordingly
            }

            // Calculate the difference between the reservation date and the current date
            TimeSpan dateDifference = existingBooking.ReservationDate - DateTime.UtcNow;

            // Check if the difference is less than or equal to 5 days
            if (dateDifference.TotalDays <= 5)
            {
                throw new InvalidOperationException("You cannot update the booking as the reservation date is less than or equal to 5 days from the current date.");
            }

            var filter = Builders<Booking>.Filter.Eq(x => x.Id, id);
            await _bookingCollection.DeleteOneAsync(filter);
        }



    }
}
