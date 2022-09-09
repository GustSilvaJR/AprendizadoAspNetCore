namespace FirstAppCrud.Models
{
    public class HomeModel
    {
        private string? Nome { get; set; }
        private string? Email { get; set; }

        public string getNome()
        {
            return this.Nome;
        }
        public void setNome(string nome)
        {
            this.Nome = nome;
        }

        public string getEmail()
        {
            return this.Email;
        }
        public void setEmail(string email)
        {
            this.Email = email;
        }
    }
}
