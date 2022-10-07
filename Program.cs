using Serialize;
using System.Text.Json;

new Client
{
    Id = 1,
    Nome = "Augusto Moreira",
    Email = "augusto@email.com",
    Telefone = "219696969",
    EnderecoCompleto = "Rua Ali Longe"
}.Save();

new Client
{
    Id = 2,
    Nome = "Enzo Nobre",
    Email = "enzo@email.com",
    Telefone = "2196969696",
    EnderecoCompleto = "Rua Ali Perto"
}.Save(Tipo.CSV);



Console.WriteLine("============[ Lendo do Json ]======================");
foreach (var cliente in Client.GetClients())
{
    Console.WriteLine($"Id: {cliente.Id}");
    Console.WriteLine($"Id: {cliente.Nome}");
}

Console.WriteLine("============[ Lendo do CSV ]======================");
foreach (var cliente in Client.GetClients(Tipo.CSV))
{
    Console.WriteLine($"Id: {cliente.Id}");
    Console.WriteLine($"Id: {cliente.Nome}");
}