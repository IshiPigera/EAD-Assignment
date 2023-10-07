using MongoDB.Bson.Serialization.Attributes;

namespace TravelerAppService.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string NationalIdentificationCard { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NoOfReservations { get; set; }
        public string Destination { get; set; }
    }
}
