using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeonAssesment
{
    public enum Scene
    {
        STARTMENU,
        NAMECREATION,
        CHARACTERSELECTION,
        RIDDLERROOM,
        BATTLE,
        RESTARTMENU
        
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
        private Item[] _nightWingItems;
        private Item[] _redHoodItems;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        private void Start()
        {
            _gameOver = false;
            _currentScene = 0;
            InitializeItems();
            InitializeEnemies();
        }

        public void End()
        {
           
        }

        private void Update()
        {
            DisplayCurrentScene();

        }

        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;
                case Scene.NAMECREATION:
                    GetPlayerName();
                    break;
                case Scene.CHARACTERSELECTION:
                    CharacterSelection();
                    break;
                case Scene.RIDDLERROOM:
                    RiddlerRiddle();
                    break;
                case Scene.BATTLE:
                    Battle();
                    CheckBattleResults();
                    break;

                
                  
                    
            }
        }


        public void InitializeItems()
        {
            //Batman Gadgets
            Item grapplingHook = new Item { Name = "Grappling Hook", StatBoost = 5, Type = ItemType.DEFENSE };
            Item batterRang = new Item { Name = "BatterRang", StatBoost = 10, Type = ItemType.ATTACK };

            //Robin Gadgets
            Item bowStaff = new Item { Name = "Bow Staff", StatBoost = 10, Type = ItemType.ATTACK };
            Item throwingBird = new Item { Name = "Throwing Bird", StatBoost = 5, Type = ItemType.DEFENSE };

            //Nightwing Gadgets
            Item escrimaSticks = new Item { Name = "Escrima Sticks", StatBoost = 2, Type = ItemType.ATTACK };
            Item wingDings = new Item { Name = "Wing Dings", StatBoost = 1, Type = ItemType.DEFENSE };

            //Red Hood Gadgets
            Item dualPistols = new Item { Name = "Dual Pistols", StatBoost = 2, Type = ItemType.ATTACK };
            Item bodyArmor = new Item { Name = "Kevlar Vest", StatBoost = 1, Type = ItemType.DEFENSE };

            //Initialize arrays
            _batmanItems = new Item[] { grapplingHook, batterRang };
            _robinItems = new Item[] { bowStaff, throwingBird };
            _nightWingItems = new Item[] { escrimaSticks, wingDings };
            _redHoodItems = new Item[] { dualPistols, bodyArmor };
        }

        public void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity riddler = new Entity("Riddler", 50, 180, 35);

            Entity mrFreeze = new Entity("Mr. Freeze", 85, 175, 70);

            Entity killerCroc = new Entity("Killer Croc", 20, 20, 20);

            Entity blackMask = new Entity("Black Mask", 20, 20, 20);

            Entity bane = new Entity("Bane", 20, 20, 20);

            Entity deathStroke = new Entity("DeathStroke", 20, 20, 20);

            Entity pyg = new Entity("Proffesor Pyg", 20, 20, 20);

            Entity joker = new Entity("Joker", 20, 20, 20);

            _enemies = new Entity[] { riddler, mrFreeze, killerCroc, blackMask, bane, deathStroke, pyg, joker };

            _currentEnemy = _enemies[_currentEnemyIndex];

        }


        public void DisplayStartMenu()
        {
            int choice = GetInput("Welcome to Gotham Defenders ", "Start New Game", "Load Game");

            if (choice == 0)
            {
                _currentScene = Scene.NAMECREATION;
            }
            else if (choice == 1)
            {

            }   
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


        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayRestartMenu()
        {
            int choice = GetInput("Would like to go back into Gotham?", "Yes", "No");

            if (choice == 0)
            {
                _currentScene = 0;
                InitializeEnemies();
            }
            else if (choice == 1)
            {
                _gameOver = true;
            }

        }


        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {

            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack: " + character.AttackPower);
            Console.WriteLine("Defense: " + character.DefensePower);
            Console.WriteLine();



        }


        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Welcome to Gotham " + _playerName + ", choose your character", "Batman", "Robin", "NightWing", "RedHood");

            if (choice == 0)
            {
                _player = new Player(_playerName, 200, 150, 150, _batmanItems, "Batman");
                _currentScene++;
            }
            else if (choice == 1)
            {
                _player = new Player(_playerName, 150, 100, 85, _robinItems, "Robin");
                _currentScene++;
            }
            else if (choice == 2)
            {
                _player = new Player(_playerName, 1, 1, 1, _nightWingItems, "NightWing");
                _currentScene++;
            }
            else if (choice == 3)
            {
                _player = new Player(_playerName, 1, 1, 1, _redHoodItems, "Red Hood");
                _currentScene++;
            }


        }

        public void DisplayEquipitemMenu()
        {
            //Get item index
            int choice = GetInput("Select and item to equip.", _player.GetItemNames());

            //Equip item at given index
            if (!_player.TryEquipItem(choice))
                Console.WriteLine("You couldnt find that item in your utility belt.");


            //Print feedback
            Console.WriteLine("You equipped " + _player.CurrentItem.Name + "!");
        }


        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealt = 0;

            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            int choice = GetInput(_currentEnemy.Name + " stands in front of you! What will you do?", "Attack", "Equip Item", "Remove Current Item", "Save");

            if (choice == 0)
            {
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage!");
            }
            else if (choice == 1)
            {
                DisplayEquipitemMenu();
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else if (choice == 2)
            {
                if (!_player.TryRemoveCurrentItem())
                    Console.WriteLine("You dont have anything equipped.");
                else
                    Console.WriteLine("You place the item in your utility belt");

                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine(_currentEnemy.Name + " dealt " + damageDealt + " damage!");

            Console.ReadKey(true);
            Console.Clear();

        }

        public void RiddlerRiddle()
        {
            int numberOfAttempts = 3;
            string input = "";

            for (int i = 0; i < numberOfAttempts; i--)
            {
                Console.Clear();

                Console.WriteLine("Welcome to Gotham " + _playerName + " In case you havent heard yet, several of Gothams most notoroius villains have ecscaped Arkham Asylum including myself the riddler the most dangoures of all of The Batmans villains.");
                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine("But the only way you will get to them " + _playerName + " is by getting through me first");
                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine("But it wont be that easy you must first solve my riddle to earn the priviliage of fighting me");
                Console.ReadKey(true);
                Console.Clear();

                Console.WriteLine("be carful because you only get " + numberOfAttempts + " attempts.");

                Console.WriteLine("I am something people celebrate or resist. I change people’s thoughts and lives. I am obvious to some people but, to others, I am a mystery. What am I?");

                //Store the amount of attempts the player has remaining
                int attemptsRemaining = numberOfAttempts + i;

                //Displays the remaining number of attempts
                Console.WriteLine("Attempts Remaining: " + attemptsRemaining);

                //Get input for the players guess
                Console.Write("> ");
                input = Console.ReadLine();

                //If the player answered correctly...
                if (input == "age")
                {
                    //...print text for feedback and break the loop
                    Console.WriteLine("Well i guess you really are the worlds greatest Detecitve. NOW FIGHT US " + _playerName + "!");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue");
                    _currentScene = Scene.BATTLE;
                    Console.ReadKey();
                    Console.Clear();

                    break;
                }

                //If the player doesn't answer correctly deal damage to them
                Console.WriteLine("Really come on this wasn't even the hard one i thought you were the worlds greatest Dectecitve you will never save Gotham at this rate.");
                    
                Console.ReadKey();
                _player.TakeDamage(5);


                //If the player has died after guessing
                if ( _player.Health <= 0)
                {
                    //...update the player state and print player feedback to the screen
                    _gameOver = true;
                    Console.WriteLine("Huh... My riddle was to good i guess i really am the best no i mean... Yes i am the best.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
            }


        
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if (_player.Health <= 0)
            {
                Console.WriteLine("You have failed and now Gotham shall fall!!!");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = Scene.RESTARTMENU;
            }
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You sent " + _currentEnemy.Name + " back to Arkham");
                Console.ReadKey();
                Console.Clear();
                _currentEnemyIndex++;

                if (_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = Scene.RESTARTMENU;
                    Console.WriteLine("You defeated all the escaped villains and sent them back to Arkham");
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
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
