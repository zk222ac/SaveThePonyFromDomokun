using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientUserInterface
{
    class Program
    {
        // P - player - Pony
        // ^ - percurred- Invaded land
        // M - walkable maze
        // # - not walkable maze
        // E - end
        // D - Domokun- monster
        static List<string> _maze = new List<string>();
        // player (Pony) position in the specific cell of row
        static int X = 0;
        //  Count of total rows 
        static int Y = 0;
        static ConsoleKeyInfo _keyInfo;
        private static  string _playerName;
        private static  int _level;
        //// HTTP Client for consume services 
        //static HttpClient _client = new HttpClient();
        public static  void Main(string[] args)
        {
            Console.WriteLine("P - player");
            Console.WriteLine("^ - percurred- Invaded land");
            Console.WriteLine("M - walkable maze");
            Console.WriteLine("# - not walkable maze");
            Console.WriteLine("E - end");
            Console.WriteLine("D - Domokun- Demons");

            Console.WriteLine("-----------------------------------------------------");

            Console.WriteLine("Save the pony from Domokun");

            Console.WriteLine("-----------------------------------------------------");
            try
            {
                // Take input Name and level of game from Console
                Console.WriteLine("Kindly Enter the Player name:");
                _playerName = Console.ReadLine();
                Console.WriteLine("Kindly Enter the Level of Game: for example 02-10");
                _level = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                // Read data from file
                _maze = File.ReadAllLines("maze.txt").ToList();
                UpdateOutput();
                while (true)
                {
                    Console.WriteLine("Take input from Console!......... Press any arrow  (Left , Right , Top , Bottom) key for Player movemen");
                    _keyInfo = Console.ReadKey();
                    try
                    {
                       WaitForInput();
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("The program is end now!");
                        Console.WriteLine(e);
                        Console.ReadLine();
                        System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        // find out the current position of Pony in the Maze
        private static  void UpdateOutput()
        {
            Console.Clear();
            for (int i = 0; i < _maze.Count; i++)
            {
                if (_maze[i].Contains("P"))
                {
                    // how many rows currently held the program
                    // Height 
                    Y = i;
                    // Simply we identify the Pony position in the particular Row
                    // Width
                    X = _maze[i].IndexOf("P", StringComparison.Ordinal);

                    // Create New resource in the API ...................
                    // Calling API 
                    RunAsync(Y , X).GetAwaiter().GetResult();

                }
                Console.WriteLine(_maze[i]);
            }
        }

        public static void Killed()
        {

            Console.Clear();
            Console.WriteLine("Now you have been killed by Demon! Game is over ");
            Console.WriteLine("Try out Again! Press Enter on the screen");
            Console.ReadLine();
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }

        private static void End()
        {
            Console.Clear();
            Console.WriteLine("Victory!");
            Console.ReadLine();
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }
        private static void NonWalkable()
        {
            Console.WriteLine("Kindly move your player on the walkable position and choose right path for finish the game!");
        }
        private static void  WaitForInput()
        {
            if (_keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (_maze[Y + 1][X] == 'M')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = '^' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y + 1]) { [X] = 'P' };
                    _maze[Y + 1] = sb.ToString();
                    UpdateOutput();
                }
                else if (_maze[Y + 1][X] == 'E')
                {
                    End();
                }
                else if (_maze[Y + 1][X] == '^')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = 'M' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y + 1]) { [X] = 'P' };
                    _maze[Y + 1] = sb.ToString();
                     UpdateOutput();
                }
                else if (_maze[Y + 1][X] == 'D')
                {
                    Killed();
                }
            }

            if (_keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (_maze[Y - 1][X] == 'M')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = '^' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y - 1]) { [X] = 'P' };
                    _maze[Y - 1] = sb.ToString();
                     UpdateOutput();
                }
                else if (_maze[Y - 1][X] == 'E')
                {
                    End();
                }
                else if (_maze[Y - 1][X] == '^')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = 'M' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y - 1]) { [X] = 'P' };
                    _maze[Y - 1] = sb.ToString();
                     UpdateOutput();
                }
                else if (_maze[Y - 1][X] == 'D')
                {
                    Killed();
                }
            }

            if (_keyInfo.Key == ConsoleKey.RightArrow)
            {
                // X means : postion of player in the row cell
                // Y means : How many count of rows the program contained it  
                if (_maze[Y][X + 1] == 'M')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = '^' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y]) { [X + 1] = 'P' };
                    _maze[Y] = sb.ToString();
                     UpdateOutput();
                }
                else if (_maze[Y][X + 1] == 'E')
                {
                    End();
                }
                else if (_maze[Y][X + 1] == '^')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = 'M' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y]) { [X + 1] = 'P' };
                    _maze[Y] = sb.ToString();
                     UpdateOutput();
                }
                else if (_maze[Y][X + 1] == 'D')
                {
                    Killed();
                }
                else
                {
                    NonWalkable();
                }
            }

            if (_keyInfo.Key == ConsoleKey.LeftArrow)
            {
                if (_maze[Y][X - 1] == 'M')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = '^' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y]) { [X - 1] = 'P' };
                    _maze[Y] = sb.ToString();
                    UpdateOutput();
                }
                else if (_maze[Y][X - 1] == 'E')
                {
                    End();
                }
                else if (_maze[Y][X - 1] == '^')
                {
                    StringBuilder sb = new StringBuilder(_maze[Y]) { [X] = 'M' };
                    _maze[Y] = sb.ToString();
                    sb = new StringBuilder(_maze[Y]) { [X - 1] = 'P' };
                    _maze[Y] = sb.ToString();
                    UpdateOutput();
                }
                else if (_maze[Y][X - 1] == 'D')
                {
                    Killed();
                }
                else
                {
                    NonWalkable();
                }
            }
        }

        static async Task<Uri> CreateNewMazeResourceAsync(Maze maze , HttpClient client)
        {
            var  response = await client.PostAsJsonAsync("api/Mazes", maze);
            // check the succes status code 
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }
        static async Task<Maze> GetMazeAsync(string path , HttpClient client)
        {
            Maze maze = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                maze = await response.Content.ReadAsAsync<Maze>();
            }
            return maze;
        }
        static void ShowMaze(Maze maze)
        {
            Console.WriteLine(".............................................................");
            Console.WriteLine("Get Request :api/Mazes/{maze-id}........................ ");
            Console.WriteLine($"Name: {maze.Name}- ID:-{maze.Id}\tHeight:" +
                              $"{maze.Height}\tWidth: {maze.Width}\tLevel: " +
                              $"{maze.Difficulty}");
            Console.WriteLine(".............................................................");
        }
        public static async Task RunAsync(int height, int weight)
        {
            using (var client = new HttpClient()) 
            {
                //client.Timeout = TimeSpan.FromSeconds(1);
                client.BaseAddress = new Uri("http://localhost:60435/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                   
                    // challenge no : 1 (Create a new Maze game resource) 
                    // Post request ( Add some data in a body parameter for sending request)
                    Maze maze = new Maze
                    {
                        Name = _playerName,
                        Height = height,
                        Width = weight,
                        Difficulty = _level
                    };
                    // Create New Maze name 
                    var url = await CreateNewMazeResourceAsync(maze , client);
                    Console.WriteLine("..........................................................................");
                    Console.WriteLine($" Now Resource Has been Created at {url}");
                    Console.WriteLine("..............................................................................");

                    // Get maze current state :GET /pony-challenge/maze/{maze-id}
                    // Get the product
                    maze = await GetMazeAsync(url.PathAndQuery , client);
                    ShowMaze(maze);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            // Update port # in the following line.

            try
            {

                // Get the product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                // Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

