



using System;
using System.Collections.Generic;
using System.Linq;

namespace ryggsäck
{
    public class Inventory
    {
        public Inventory(List<string> items){}
       
        public List<string> Content {get; set;}


    }
    public class Menu
    {
        public Menu(IEnumerable<string> items)      // Itererar över elementet
        {
            Items = items.ToArray();                // Gör om till en array
        }

        public IReadOnlyList<string> Items {get;}

        public int SelectedIndex { get; private set;} = -1; // Utgångspunkt med inget markerat

        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;

        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);      // Rör dig ett steg uppåt om du inte är på högsta nivån

        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1); // Rör dig ett steg neråt om du inte är på lägsta nivån
    }

    public class ConsoleMenuPainter
    {

        readonly Menu menu;

        public ConsoleMenuPainter(Menu menu)
        {
            this.menu = menu;
        }

        public void Paint()
        {


            for (int i = 0; i < menu.Items.Count; i++){
                Console.SetCursorPosition(0, 3 + i);

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Gray;
                
                Console.ForegroundColor = color;
                Console.WriteLine (menu.Items[i]);
            }
        }
    }

    class Program
    {
        static Menu GetMenu(Menu menu)
        {

            var menuPainter = new ConsoleMenuPainter (menu);        // Skapar meny med hjälp av klassen

            Console.Clear();                                        // Så att consolen alltid ser likadan ut
            Console.CursorVisible = false;

            Console.WriteLine("Välkommen till Lukas Ryggsäck!");    // Skriv ut header
            Console.WriteLine("------------------------------");

            bool done = false;

            do
            {
                menuPainter.Paint();                                // Skriver ut menyn
                var keyInfo = Console.ReadKey();                    // Läser av tangentnedtryckning

                switch (keyInfo.Key)                                
                {                                                    // Switch argument för de olika tangenterna
                    case ConsoleKey.UpArrow : 
                        menu.MoveUp();
                        break;
                    case ConsoleKey.DownArrow :
                        menu.MoveDown();
                        break;
                    case ConsoleKey.Enter :
                        done = true;
                        break;
                }
          

            }
              while (!done);                                          // Om enter har tryckts gå ur meny loopen
              Console.CursorVisible = true;
             
              return menu;
        }
        static void Main(string[] args)
        {
            string[] menuContent = {"Lägg till ett föremål", "Skriv ut innehållet", "Rensa innehållet", "Avsluta"}; // Vad som ska med i menyn

            var menu = new Menu (menuContent);  
            var inventory = new Inventory(new List<string>{});
            
           
            menu = GetMenu(menu);

            Console.ForegroundColor = ConsoleColor.DarkRed;           // Byter färg på texten inför nästa utskrift
            
            Console.WriteLine("Selected option: " + (menu.SelectedOption ?? "nothing"));    // Skriver ut SelectedOption om det är inget så skriv nothing
            string option = menu.SelectedOption;

            switch (menu.SelectedOption)
            {

                case "Lägg till ett föremål" :
                    Console.Clear();
                    Console.Write("Vilket föremål vill du lägga till?: ");
                    string foobar = Console.ReadLine();
                    inventory.Content.Add(foobar);
                break;

            }
          





            Console.ReadKey();                                      // Paus i programmet så att användaren hinner läsa
            Console.ForegroundColor = ConsoleColor.Gray;            // Byter tillbaka färg till gray
            Console.Clear();                                        // Rensar fönstret så att man inte kan se rester från programmet
        }
    }
}







