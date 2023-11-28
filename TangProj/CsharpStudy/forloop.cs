using System;

namespace CsharpStudy;
internal class Forloop
{
    static void Main(string[] args)
    {
        Dictionary<string, DateOnly> excelDict = new()
        {
            {"GF1",new DateOnly(2011,5,5)},
            {"GF2",new DateOnly(2014,5,5)},
            {"GF3",new DateOnly(2021,5,5)},
            {"GF4",new DateOnly(2023,5,5)}
        };
        foreach (var item in excelDict)
        {
            Console.WriteLine($"{item.Key,-4},{item.Value.Day,-4},{item.Value.Month,-4},{item.Value.Year}");
        }
        foreach (var (cellAddress, (day, month, year)) in excelDict)
        {
            Console.WriteLine($"{cellAddress,-4},{day,-4},{month,-4},{year}");
        }
    }
}
public static class CommonDeconstruct
{
    // public static void Deconstruct(this DateOnly date, out int day, out int month, out int year)
    // {
    //     day = date.Day;
    //     month = date.Month;
    //     year = date.Year;
    // }

    //比較美觀的寫法
    public static void Deconstruct(this DateOnly date, out int day, out int month, out int year) =>
        (day, month, year) = (date.Day, date.Month, date.Year);
}