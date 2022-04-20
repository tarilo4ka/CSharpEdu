using CustomCollection;
namespace ConsoleApplication;


class ConsoleMenu <T>
{
    Collection<T> _collection;
    public ConsoleMenu()
    {
        _collection = new ();
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
                Console.WriteLine("Invalid value");
                
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
                _collection.FromJson(fromConsole("Enter path to file: "));
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path");
            }
        }
    }

    // public void searchItem()
    // {
    //     string property = fromConsole("Enter property name: ");
    //     string value = fromConsole("Enter value: ");
    //     List<T> result = _collection.Search(property, value);
    //     if (result.Count() != 0)
    //     {
    //         foreach (var item in result)
    //         {
    //             Console.WriteLine(item);
    //         }
    //     }
    //     else
    //     {
    //         Console.WriteLine("No items found");
    //     }
    // }
    
    public void sortItem()
    {
        string property = fromConsole("Enter property name: ");
        _collection.Sort(property);
    }

    public void addItem()
    {
        T? item = (T?)Activator.CreateInstance(typeof(T));
        Console.WriteLine("Enter item properties: ");
        if (item != null)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                while (true)
                {
                    try
                    {
                        prop.SetValue(item, fromConsole(prop.Name + ": "));
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid value");
                    }
                }
            }
            _collection.Add(item);
        }
        
    }

    public void removeItem()
    {
        int id = int.Parse(fromConsole("Enter id: "));
        while (true)
        {
            try
            {
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
        int id = int.Parse(fromConsole("Enter id: "));
        string property = fromConsole("Enter property name: ");
        string value = fromConsole("Enter value: ");
        while (true)
        {
            try
            {
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
                _collection.DumpToJson(fromConsole("Enter path to file: "));
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
        }
    }
    public void DoMenu1()
    {
        // Console.WriteLine("Enter path to file");
        // string? path = Console.ReadLine();

        while (true)
        {
            ShowMenu();
            string? option = Console.ReadLine();
            Console.WriteLine("Enter option: ");
            switch (option)
            {
                case "1":
                    readFromFile();
                    break;
                case "2":
                    sortItem();
                    break;
                case "3":
                    // searchItem();
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
