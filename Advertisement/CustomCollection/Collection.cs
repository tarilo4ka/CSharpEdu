using System.Collections;
using System.Reflection;
using Newtonsoft.Json;

namespace CustomCollection;

// class Container<T> where T is any object
class Collection<T> : IEnumerable<T>
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
            _items.Add(item);
        }
    }

    // method DumpToJson that dumps the collection to JSON file
    public void DumpToJson(string path)
    {
        string json = JsonConvert.SerializeObject(_items);
        File.WriteAllText(path, json);
    }

    // method FromJson that loads the collection from JSON file
    public void FromJson(string path)
    {
        string json = File.ReadAllText(path);
        List<T>? items = JsonConvert.DeserializeObject<List<T>>(json);
        if (items != null)
        {
            _items = items;
        }
    }

    // Adds item to collection
    public void Add(T item)
    {  
        object? idOfItem = item?.GetType().GetProperty("Id")?.GetValue(item);
        foreach (var i in _items)
        {
            if ((int?)idOfItem == System.Convert.ToInt32(i?.GetType().GetProperty("Id")?.GetValue(i)))
            {
                throw new InvalidOperationException("Item already exists");
            }
        }
        this._items.Add(item);
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
    public List<T> Search(string propertyName, object? value)
    {
        List<T> result = new ();
        foreach (var item in _items)
        {
            if (item != null)
            {
                object? valueOfItem = item?.GetType().GetProperty(propertyName)?.GetValue(item);
                if (valueOfItem == value)
                {
                    result.Add(item);
                }
            }
        }
        return result;
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

    // method that allows you to edit the item by Id
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

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
