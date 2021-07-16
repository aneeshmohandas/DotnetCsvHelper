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
  var result=new CsvHelper().ReadDataFromCsv<Fruit>(path);
}
public class Fruit
{
   public int Id { get; set; }
   public string Name { get; set; }
}
```
