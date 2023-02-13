using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Member")]
    public class Member
    {
        [Column("MemberId")]
        public string Id { get; set; }
        public string Username { get; set; }        
        public string Email { get; set; }
        public string Role { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        //1 account => 1 Acccess token
        public string AccessToken { get; set; }
        //Error
        [NotMapped]
        public List<Member> Customers { get; set; }

//MemberId CHAR(32) NOT NULL PRIMARY KEY,
//Username VARCHAR(16) NOT NULL UNIQUE,
//Password BINARY(64) NOT NULL,
//Email NVARCHAR(64) NOT NULL,
//Role VARCHAR(16) NOT NULL,
//Longitude FLOAT,
//Latitude FLOAT,
//AccessToken CHAR(32)
    }
}
