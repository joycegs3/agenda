namespace AgendaWebApplication.Models
{
    public class AgendaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        // Ref para o usuário associado
        public UserModel User { get; set; }
    }
}
