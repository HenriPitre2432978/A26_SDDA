using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyLittleRPG
{
    class Program
    {
        //"async" permet d'attendre les appels réseau)
        static async Task Main(string[] args)
        {
            // Création du helper PokeApi

            int idx = 0;
            Pokemon[] pokemons = new Pokemon[809];
            ConfigHelper configHelper = new();

            // Appel asynchrone à PokeAPi
            while (idx < pokemons.Length)
            {
                pokemons[idx] = await configHelper.GetPokemon(idx + 1);
                idx++;
            }

            // Convert Pokemon objects to PokemonResponse objects
            PokemonResponse[] responses = new PokemonResponse[pokemons.Length];

            for (int i = 0; i < pokemons.Length; i++)
            {
                responses[i] = new PokemonResponse(pokemons[i]);
            }

            //Save in json file
            string json = JsonSerializer.Serialize(
                responses,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            string filePath = "monstersdata.json";
            File.WriteAllText(filePath, json);
        }
    }
}
