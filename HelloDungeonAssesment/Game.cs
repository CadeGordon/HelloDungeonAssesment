using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeonAssesment
{
    public enum Scene
    {
        None
    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }

    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }

    class Game
    {
        private bool _gameOver;
        private Scene _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;
        private string _playerName;
        private Item[] _batmanItems;
        private Item[] _robinItems;

        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        private void Start()
        {
            
        }

        public void End()
        {
           
        }

        private void Update()
        {
            
        }

        void DisplayCurrentScene()
        {

        }


        public void InitializeItems()
        {
            //Batman Gadgets
            Item grapplingHook = new Item { Name = "Grappling Hook", StatBoost = 5, Type = ItemType.DEFENSE };
            Item batterRang = new Item { Name = "BatterRang", StatBoost = 10, Type = ItemType.ATTACK };

            //Robin Gadgets
            Item bowStaff = new Item { Name = "Bow Staff", StatBoost = 10, Type = ItemType.ATTACK };
            Item throwingBird = new Item { Name = "Throwing Bird", StatBoost = 5, Type = ItemType.DEFENSE };

            //Initialize arrays
            _batmanItems = new Item[] { grapplingHook, batterRang };
            _robinItems = new Item[] { bowStaff, throwingBird };
        }


        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {

            Console.WriteLine("Welcome! Please enter your name.");
            _playerName = Console.ReadLine();

            Console.Clear();

            int choice = GetInput("You've entered " + _playerName + ", are you sure you want to keep this name?", "Yes", "No");

            if (choice == 0)
            {
                _currentScene++;
            }



        }




        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.WriteLine("> ");

                //get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputRecieved))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        //set input recieved to be the default value
                        inputRecieved = -1;
                        //display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //if the player didnt type and int
                else
                {
                    //Set input recieved to be the default value
                    inputRecieved = -1;
                    Console.WriteLine("inavlid input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
        }
    }
}
