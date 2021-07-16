# Dotnet CSV Helper
A library for reading and writing CSV files.

## Install

### Package Manager Console

```
PM> Install-Package DotnetCsvHelper
```

### .NET CLI Console

```
> dotnet add package DotnetCsvHelper
```
# Documentation

## Reading Data

### Data

```
Id,Name
1,Apple
```
### Example

```
void Main()
{
  var result=new CsvHelper().ReadDataFromCsv<Fruit>(FilePath);
}
public class Fruit
{
   public int Id { get; set; }
   public string Name { get; set; }
}
```
## Writing Data

### Example
```
void Main()
{
    var data = new List<Fruit>();
    data.Add(new Fruit { Id = 100,Name="Apple"});
    var result = new CsvHelper().WriteDataToCsv<Fruit>(data);
    System.IO.File.WriteAllBytes(FilePath, result);
}
```

