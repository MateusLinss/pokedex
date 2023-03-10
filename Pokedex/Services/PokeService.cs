using Pokedex.Models;
using System.Text.Json;

namespace Pokedex.Services;
public class PokeService : IPokeService
{
    private readonly IHttpContextAccessor _session;

    public PokeService(IHttpContextAccessor session)
    {
        _session = session;
        PopularSessao();
    }
    public Pokemon GetPokemon(int Numero)
    {
        var pokemons= GetPokemons();
        return pokemons.Where(pokemons => pokemons.Numero == Numero).FirstOrDefault();
    }



    public List<Pokemon> GetPokemons()
    {
        PopularSessao();
        var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(_session.HttpContext.Session.GetString("Pokemons"));
        return pokemons;
    }
    public List<Tipo> GetTipos()
    {
        PopularSessao();
        var tipos = JsonSerializer.Deserialize<List<Tipo>>(_session.HttpContext.Session.GetString("Tipos"));
        return tipos;
    }

    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(
            _session.HttpContext.Session.GetString("Pokemons")))
        {
            var dados = LerArquivo(@"Data\pokemons.json");
            _session.HttpContext.Session.SetString("Pokemons", dados);
            dados = LerArquivo(@"Data\tipos.json");
            _session.HttpContext.Session.SetString("Tipos", dados);
        }
    }

    private string LerArquivo(string nomeArquivo)
    {
        using (StreamReader leitor = new StreamReader(nomeArquivo))
        {
            string dados = leitor.ReadToEnd();
            return dados;
        }
    }



}