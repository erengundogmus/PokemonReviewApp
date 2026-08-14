namespace PokemonConsoleApp
{

    class Program
    {
        static readonly string baseUrl = "https://localhost:7013/api/";

        static async Task Main(string[] args)
        {
            bool exit = false;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("//Ana Menü");
                Console.WriteLine();
                Console.WriteLine("1-Pokemon");
                Console.WriteLine("2-Category");
                Console.WriteLine("3-Country");
                Console.WriteLine("4-Food");
                Console.WriteLine("5-Owner");
                Console.WriteLine("6-Review");
                Console.WriteLine("7-Reviewer");
                Console.WriteLine("0-Çıkış");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Gitmek istediğiniz menüyü seçin: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    //case "1":
                    //    await CategoryMenu();
                    //    break;

                    //case "2":
                    //    await CountryMenu();
                    //    break;

                    //case "3":
                    //    await FoodMenu();
                    //    break;

                    //case "4":
                    //    await OwnerMenu();
                    //    break;

                    case "5":
                        await PokemonMenu();
                        break;

                    //case "6":
                    //    await ReviewMenu();
                    //    break;

                    //case "7":
                    //    await ReviewerMenu();
                    //    break;

                    case "0":
                        exit = true;
                        Console.WriteLine("Çıkış yapılıyor");
                        break;

                    default: Console.WriteLine("Seçim geçerli değil, devam etmek için bir tuşa basın");
                        Console.Read();
                        break;
                }
            }
        }

        static async Task PokemonMenu()
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("//Pokemon");
                Console.WriteLine("1-GetAll");
                Console.WriteLine("2-GetById");
                Console.WriteLine("3-Create");
                Console.WriteLine("4-Update");
                Console.WriteLine("5-Delete");
                Console.WriteLine("0-Ana Menüye Dön");
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        await GetAllPokemons();
                        break;

                    //case "2":
                    //    await GetPokemonById();
                    //    break;
                    
                    //case "3":
                    //    await CreatePokemon();
                    //    break;
                    
                    //case "4":
                    //    await UpdatePokemon();
                    //    break;
                    
                    //case "5":
                    //    await DeletePokemon();
                    //    break;

                    case "0":
                        backToMain = true;
                        break;
                    default :
                        Console.WriteLine("Geçersiz seçim, devam edebilmek için herhangi bir tuşa basın");
                        Console.ReadKey();
                        break;
                }
            }

        }


        static async Task GetAllPokemons()
        {

        }

























    }
}