using CustomCollection;
using CustomType;
namespace ConsoleApplication;


class ConsoleMenu<T> where T : class
{
    protected Collection<T> _collection;
    public ConsoleMenu()
    {
        _collection = new();
    }

    public static void ShowMenu()
    {
        string helpMessage =
                "\n  Help menu:" +
                "\n  1 - read from file"  +
                "\n  2 - sort item"  +
                "\n  3 - search item"  +
                "\n  4 - add new item to collection" +
                "\n  5 - remove item from collection" +
                "\n  6 - edit item from collection"  +
                "\n  7 - write collection to file " +
                "\n  8 - print collection" +
                "\n  0 - to exit" + "\n";
        Console.WriteLine(helpMessage);
    }

    public static string fromConsole(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            string? st = Console.ReadLine();
            if (st == null || st == "")
            {
                Console.WriteLine("Invalid value\n Enter again: ");
            }
            else
            {
                return st;
            }
        }
    }

    public void readFromFile()
    {
        while (true)
        {
            try
            {
                _collection.FromTxt(fromConsole("Enter path to file: "));
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path");
            }
        }
    }

    public void searchItem()
    {
        string value = fromConsole("Enter value: ");
        List<T> result = _collection.Search(value);
        if (result.Count() != 0)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
        else
        {
            Console.WriteLine("No items found");
        }
    }
    
    public void sortItem()
    {
        string property = fromConsole("Enter property name: ");
        _collection.Sort(property);
    }

    public virtual void addItem() { }

    public void removeItem()
    {
        
        while (true)
        {
            try
            {
                int id = int.Parse(fromConsole("Enter id: "));
                _collection.Remove(id);
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
            }
        }
    }

    public void editItem()
    {

        while (true)
        {
            try
            {
                int id = int.Parse(fromConsole("Enter id: "));
                string property = fromConsole("Enter property name: ");
                string value = fromConsole("Enter value: ");
                _collection.Edit(id, property, value);
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value");
            }
        }
    }

    public void writeToFile()
    {
        while (true)
        {
            try
            {
                _collection.DumpToTxt(fromConsole("Enter path to file: "));
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path");
            }
        }
    }

    public void printCollection()
    {
        foreach (var item in _collection)
        {
            Console.WriteLine(item);
            Console.WriteLine();
        }
    }
    public void Run()
    {
        while (true)
        {
            ShowMenu();
            Console.WriteLine("Enter option: ");
            string? option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    readFromFile();
                    break;
                case "2":
                    sortItem();
                    break;
                case "3":
                    searchItem();
                    break;
                case "4":
                    addItem();
                    break;
                case "5":
                    removeItem();
                    break;
                case "6":
                    editItem();
                    break;
                case "7":
                    writeToFile();
                    break;
                case "8":
                    printCollection();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    continue;
            }
        }
    }
}

// override class Console menu for custom type Transaction
class ConsoleMenuTransaction : ConsoleMenu<Transaction>
{
    public ConsoleMenuTransaction() : base()
    {
    }

    // override method addItem
    public override void addItem()
    {
        Transaction tr = new();
        tr.FromConsole();
        base._collection.Add(tr);
    }
}

// override class Console menu for custom type Advertisement
class ConsoleMenuAdvertisement : ConsoleMenu<Advertisement>
{
    public ConsoleMenuAdvertisement() : base()
    {
    }

    // override method addItem
    public override void addItem()
    {
        Advertisement ad = new();
        ad.FromConsole();
        base._collection.Add(ad);
    }
}
