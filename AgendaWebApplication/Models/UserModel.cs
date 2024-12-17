using System.Text.Json.Serialization;

namespace AgendaWebApplication.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; } = "";
        [JsonIgnore]
        //Relacionamento 1:N - Um usuário pode ter vários contatos
        public ICollection<AgendaModel> Contacts { get; set; } = new List<AgendaModel>();
    }
}
