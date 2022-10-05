using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialize
{
    public class Client
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string EnderecoCompleto { get; set; }

        private static readonly string fileName = "Client.json";

        public void Save()
        {
            var clients = Client.GetClients();
            var clientsExist = clients.Find(c => c.Id == this.Id);
            if (clientsExist != null)
            {
                Console.WriteLine("ARQUIVO EXISTE!");
                clients.Remove(clientsExist);
            }

            clients.Add(this);

            string clientsString = JsonSerializer.Serialize(clients);

            File.WriteAllText(fileName, clientsString);
        }

        public static List<Client> GetClients()
        {
            _ = new List<Client>();
            var clientsJson = File.ReadAllText(fileName);
            List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(clientsJson);
            return clients;
        }

    }
}