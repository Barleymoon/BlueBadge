using _12_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _12_APIs
{
    public class SWAPIUI
    {
        private SWAPIService _service;

        // Dependency injection example;
        public SWAPIUI(SWAPIService service)
        {
            _service = service;
        }
        public void Run()
        {
            // _service = new SWAPIService();

            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("What would you like to look up?\n" +
                    "1. Person\n" +
                    "2. Planet\n" +
                    "3. StarShip\n" +
                    "4. Search People\n" +
                    "5. Search Planets\n"+
                    "6. Exit");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GetPerson();
                        break;
                    case "2":
                        GetPlanet();
                        break;
                    case "3":
                        GetStarShip();
                        break;
                    case "4":
                        SearchPeople();
                        break;
                    case "5":
                        SearchPlanets();
                        break;
                    case "6":
                        continueToRun = false;
                        break;
                }
            }
        }

        public void GetPerson()
        {
            Console.Clear();
            Console.WriteLine("What is the ID of the person you want to see?");
            string id = Console.ReadLine();

            Console.WriteLine("Loading...");
            Thread.Sleep(50);
            Person person = _service.GetAsync<Person>($"https://swapi.dev/api/people/{id}/").Result;
            Console.Clear();

            Console.WriteLine($"\n\n\n{person.Name} is {person.Height}cm tall and has {person.Eye_Color} eyes.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void GetPlanet()
        {
            Console.Clear();
            Console.WriteLine("What is the ID of the Planet you want to see?");
            string id = Console.ReadLine();

            Console.WriteLine("Loading...");
            Thread.Sleep(50);
            Planet planet = _service.GetAsync<Planet>($"https://swapi.dev/api/planets/{id}/").Result;
            Console.Clear();

            Console.WriteLine($"\n\n{planet.Name} is {planet.Climate} and has {planet.Population} people on it!");
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();

        }
        public void GetStarShip()
        {
            Console.Clear();
            Console.WriteLine("What is the ID of the vehicle you want to see?");
            string id = Console.ReadLine();

            Console.WriteLine("Loading...");
            Thread.Sleep(30);
            StarShip starship = _service.GetAsync<StarShip>($"http://swapi.dev/api/starships/{id}/").Result;
            Console.Clear();

            if (starship == default)
            {
                Console.WriteLine("Starship does not exist...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n\n{starship.Name} is a {starship.Make} and is a {starship.StarShip_Class} It can hold up to {starship.Passengers} passengers. Cost: {starship.Cost_In_Credits}.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public void SearchPeople()
        {
            Console.Clear();
            Console.WriteLine("What name are you searching for?");
            string query = Console.ReadLine();

            SearchResult<Person> results = _service.SearchPeopleAsync(query).Result;
            Console.Clear();
            Console.WriteLine($"Found {results.Count} results\n");

            foreach (Person person in results.Results)
            {
                Console.WriteLine($"{person.Name} - {person.Height}cm, {person.Eye_Color} eyes");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void SearchPlanets()
        {
            Console.Clear();
            Console.WriteLine("What planet are you searching for?");
            string query = Console.ReadLine();
            
            SearchResult<Planet> results = _service.SearchPlanetAsync(query).Result;
            Console.Clear();
            Console.WriteLine($"Found {results.Count} results\n");
            // Console.WriteLine($"Next page: {results.Next}");
            if (results.Count > 10)
            {
                // Paginate
                for (int page = 1; page <= Math.Ceiling(results.Count/10.0); page++)
                {
                    Console.WriteLine($"Page {page}:");
                    foreach (Planet planet in results.Results)
                    {
                        Console.WriteLine($"{planet.Name} - {planet.Climate} climate, {planet.Population} inhabitants");
                    }  
                    
                    if (page != Math.Ceiling(results.Count/10.0))
                    {
                        Console.WriteLine("\nPress any key to see the next page . . .");
                        Console.ReadKey();
                        // results = _service.SearchAsync<Planet>(results.Next).Result;
                    }
                }
            }
            else
            {
                foreach (Planet planet in results.Results)
                {
                    Console.WriteLine($"{planet.Name} - {planet.Climate} climate, {planet.Population} inhabitants");
                }
            }
            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
        }
    }
}
