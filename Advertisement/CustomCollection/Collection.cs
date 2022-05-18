using System.Collections;
using System.Reflection;
using System.Text.Json;
#nullable disable
namespace CustomCollection;




public class Collection<T> : IEnumerable<T>
{
    private List<T> _items;

    // default constructor
    public Collection()
    {
        _items = new ();
    }

    // constructor with parameters
    public Collection(params T[] items)
    {
        _items = new ();
        foreach (var item in items)
        {
            int id = System.Convert.ToInt32(item?.GetType()?.GetProperty("Id")?.GetValue(item));
            if (!IsUniqueId(id))
            {
                throw new Exception($"Id({id}) is not unique");
            }
            else
            {
                _items.Add(item);
            }
        }
    }


    public void FromJson(string path)
    {
        string json = File.ReadAllText(path);
        _items = JsonSerializer.Deserialize<List<T>>(json);
    }

    public void DumpToJson(string path)
    {
        string json = JsonSerializer.Serialize(_items);
        File.WriteAllText(path, json);
    }


    public bool IsUniqueId(int id)
    {
        foreach (var i in _items)
        {
            if (id == System.Convert.ToInt32(i?.GetType().GetProperty("Id")?.GetValue(i)))
            {

                Console.WriteLine($"Id({id}) isn't unique");
                return false;
            }
        }
        return true;
    }
    
    private static bool IsUniqueId(int id, List<T> items)
    {
        foreach (var i in items)
        {
            if (id == System.Convert.ToInt32(i?.GetType().GetProperty("Id")?.GetValue(i)))
            {

                Console.WriteLine($"Id({id}) isn't unique");
                return false;
            }
        }
        return true;
    }

    public void FromTxt (string path)
    {
        string[] lines = File.ReadAllLines(path);
        string[] values;
        foreach (var line in lines)
        {
            values = line.Split("; ");
            T item = Activator.CreateInstance<T>();
            int i = 0;
            bool invalid = false;
            foreach (var prop in item.GetType().GetProperties())
            {
                try
                {
                    i++;
                    if (prop.Name == "Price" || prop.Name == "Id" || prop.Name == "Month" || prop.Name == "Year")
                    {
                        prop.SetValue(item, int.Parse(values[i - 1]));
                    }
                    else if (prop.Name == "StartDate" || prop.Name == "EndDate" || prop.Name == "TDate")
                    {
                        prop.SetValue(item, DateTime.Parse(values[i - 1]));
                    }
                    else
                    {
                        prop.SetValue(item, values[i - 1]);
                    }
                }
                catch (System.Exception)
                {
                    //Console.WriteLine(e.Message);
                    invalid = true;
                }
            }
            if (!invalid && IsUniqueId(System.Convert.ToInt32(item?.GetType()?.GetProperty("Id")?.GetValue(item))))
            {
                _items.Add(item);
            }
        }
    }

    public void DumpToTxt(string path)
    {
        List<string> lines = new();
        foreach (var item in _items)
        {
            lines.Add(item.ToString());
        }
        File.WriteAllLines(path, lines.ToArray());
    }


    // Adds item to collection
    public void Add(T item)
    {  
        object idOfItem = item.GetType().GetProperty("Id").GetValue(item);
        if (IsUniqueId(System.Convert.ToInt32(idOfItem)))
        {
            this._items.Add(item);
        }
        else
        {
            Console.WriteLine("Item already exists");
        }
    }

    // Remove item from collection by Id
    public void Remove(int id)
    {
        foreach (var i in _items)
        {
            if (id == (int?)i?.GetType().GetProperty("Id")?.GetValue(i))
            {
                _items.Remove(i);
                return;
            }
        }
        throw new InvalidOperationException("Item does not exist");
    }

    public int Count()
    {
        return _items.Count;
    }

    // method Clear that removes all items from the collection
    public void Clear()
    {
        _items.Clear();
    }

    public override string ToString()
    {
        string result = String.Empty;
        foreach (T i in _items)
        {
            if (i != null)
            {
                result += i.ToString() + "\n";
            }
        }
        return result;
    }

    // method Contains that returns true if the collection contains an item with the given Id
    public bool Contains(int id)
    {
        foreach (var i in _items)
        {
            if (id == (int?)i?.GetType().GetProperty("Id")?.GetValue(i))
            {
                return true;
            }
        }
        return false;
    }

    // method Find by any property, returns all items in which there is a coincidence
    public List<T> Search(string value)
    {
        HashSet<T> result = new HashSet<T>();
        foreach (var item in _items)
        {
            if (item != null)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    #pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (prop.GetValue(item).ToString().Contains(value))
                    {
                        result.Add(item);
                    }
                }
            }
        }
        return result.ToList();
    }

    public void Edit(int id, string propertyToEdit, string newValue)
    {
        foreach (var i in _items)
        {
            if (id == (int?)i?.GetType().GetProperty("Id")?.GetValue(i))
            {
                PropertyInfo[] properties = i.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property != null)
                    {
                        if (property.Name == propertyToEdit)
                        {
                            property.SetValue(i, newValue);
                        }
                    }
                }
            }
        }
    }   
    public void Sort(string property)
    {
        try
        {
            this._items = _items.OrderBy(e => e?.GetType().GetProperty(property)?.GetValue(e, null)).ToList();
        }
        catch (Exception)
        {
            throw new ArgumentException("Invalid property name!");
        }
    }     

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
