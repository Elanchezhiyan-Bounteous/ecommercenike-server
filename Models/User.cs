using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;


namespace ecommercenike_server.Models
{
    [Table("user")]

    public class User : BaseModel
    {


        [PrimaryKey("id", false)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password_hash")]
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;


    }
}