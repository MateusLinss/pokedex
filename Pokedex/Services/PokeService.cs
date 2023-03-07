namespace Pokedex.Services;
public class PokeService : IPokeService
{
    private readonly IHttpContextAcessor _session;

    public PokeService(IHttpContextAcessor session)
    {
        _session = session;
        PopularSessao();
    }
    public Pokemon GetPokemon(int Numero)
    {
        PopularSessao();
        var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(_session.HttpContext.Session.GetString("Pokemons"));
        return pokemons.Where(pokemons => pokemons.Numero == Numero).FirstOrDefault();
    }



    public List<Pokemon> GetPokemons()
    {
        PopularSessao();
        var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(_session.HttpContext.Session.GetString("Pokemons"));
        return pokemons;
    }
    public List<Tipo> GetPTipos()
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