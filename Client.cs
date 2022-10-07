using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialize
{
    class Client
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string EnderecoCompleto { get; set; }

        private static readonly string fileName_json = "Client.json";
        private static readonly string fileName_csv = "Client.csv";
        private static readonly string columns_csv = "id;nome;email;telefone;enderecoCompleto\n";

        public void Save(Tipo tipo = Tipo.Json)
        {
            var clients = Client.GetClients(tipo);
            var clientsExist = clients.Find(c => c.Id == this.Id);
            if (clientsExist != null)
            {
                Console.WriteLine($"ARQUIVO ID: {clientsExist.Id}, JÁ EXISTE!");
                clients.Remove(clientsExist);
            }

            clients.Add(this);

            if (tipo == Tipo.Json)
            {
                string clientsString = JsonSerializer.Serialize(clients);
                File.WriteAllText(fileName_json, clientsString);
            }
            else
            {
                var lines = columns_csv;
                foreach (var client in clients)
                {
                    lines = $"{client.Id};{client.Nome};{client.Email};{client.Telefone};{client.EnderecoCompleto}\n";
                }
                File.WriteAllText(fileName_csv, lines);
            }
        }

        public static List<Client> GetClients(Tipo tipo = Tipo.Json)
        {
            if (tipo == Tipo.Json)
            {
                if (!File.Exists(fileName_json)) File.WriteAllText(fileName_json, "[]");
                _ = new List<Client>();
                var clientsJson = File.ReadAllText(fileName_json);
                List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(clientsJson);
                return clients;
            }
            else
            {
                if (!File.Exists(fileName_csv)) File.WriteAllText(fileName_csv, columns_csv);

                List<Client> clients = new List<Client> { };

                string textCsv = File.ReadAllText(fileName_csv);
                string[] linesCsv = textCsv.Split(Environment.NewLine);

                foreach (string line in linesCsv)
                {
                    var columns = line.Split(';');
                    if (columns[0].Trim().ToLower() == "id") continue;

                    clients.Add(new Client()
                    {
                        Id = int.Parse(columns[0]),
                        Nome = columns[1],
                        Email = columns[2],
                        Telefone = columns[3],
                        EnderecoCompleto = columns[4],
                    });
                }
                return clients;
            }
        }
    }

    enum Tipo
    {
        CSV,
        Json,
        SqlServer,
        MySql,
        MongoDb
    }
}