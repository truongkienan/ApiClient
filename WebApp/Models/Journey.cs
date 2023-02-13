using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Journey")]
    public class Journey
    {
        public int JourneyId { get; set; }
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte StatusId { get; set; }
        public string StatusName { get; set; }

//JourneyId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
//DriverId CHAR(32) NOT NULL REFERENCES Member(MemberId),
//CustomerId CHAR(32) NOT NULL REFERENCES Member(MemberId),
//StatusId TINYINT NOT NULL REFERENCES Status(StatusId),
//CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
    }
}
