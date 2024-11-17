namespace FormManagement.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
    }
}
