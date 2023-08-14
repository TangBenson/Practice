``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19045
Intel Core i5-10210U CPU 1.60GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT
  DefaultJob : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT


```
|            Method |    Mean |    Error |   StdDev |     Gen 0 | Allocated |
|------------------ |--------:|---------:|---------:|----------:|----------:|
|      LoadDataTask | 1.832 s | 0.0667 s | 0.1924 s | 8000.0000 |     24 MB |
| LoadDataValueTask | 1.811 s | 0.0748 s | 0.2111 s | 1000.0000 |      4 MB |
