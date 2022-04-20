using CustomType;
using CustomCollection;
using ConsoleApplication;

Advertisement c2 = new ("Titok", "CD-456-WS/01", "https://tiktok.com", "https://tiktok.com/photo.jpg", "2020/01/01", "2021/01/01", 100, 2);
Advertisement c0 = new ();
Advertisement c1 = new ("Titok", "CD-456-WS/02", "https://tiktok.com", "https://tiktok.com/photo.jpg", "2020/01/01", "2021/01/01", 100, 1);

Collection<Advertisement> col = new(c2, c1, c0, c1);
//col.DumpToJson("Data/ads.json");
// Console.WriteLine(col);
//Console.WriteLine(col.Count());
col.Edit(1, "Title", "New Title");
foreach (var item in col)
{
    Console.WriteLine(item);
}
Console.WriteLine();
Console.WriteLine(col.Count());
Console.WriteLine();
col.Sort("Id");
foreach (var item in col)
{
    Console.WriteLine(item);
}


// ConsoleMenu<Advertisement> menu = new();
// menu.DoMenu1();