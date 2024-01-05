using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestASPNET.API.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("refresh_token")]
        public string RefresToken { get; set; }
        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
