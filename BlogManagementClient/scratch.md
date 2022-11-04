### .NET Framework 4.8 with EF6.4.4

|                 Method |      Mean |    Error |   StdDev |
|----------------------- |----------:|---------:|---------:|
|            GetProducts |  53.02 ms | 1.048 ms | 1.780 ms |
| GetProductsForCategory |  19.50 ms | 0.171 ms | 0.143 ms |
|    DiscontinueProducts | 116.90 ms | 1.169 ms | 1.094 ms |

### .NET 7 with EF6.4.4

|                 Method |     Mean |    Error |   StdDev |
|----------------------- |---------:|---------:|---------:|
|            GetProducts | 30.19 ms | 0.343 ms | 0.321 ms |
| GetProductsForCategory | 11.08 ms | 0.150 ms | 0.126 ms |
|    DiscontinueProducts | 97.09 ms | 1.047 ms | 0.874 ms |


### .NET 7 with EF7

|                 Method |      Mean |     Error |    StdDev |    Median |
|----------------------- |----------:|----------:|----------:|----------:|
|            GetProducts | 16.044 ms | 0.3173 ms | 0.5387 ms | 15.802 ms |
| GetProductsForCategory |  6.111 ms | 0.0680 ms | 0.0636 ms |  6.094 ms |
|    DiscontinueProducts | 21.019 ms | 0.1039 ms | 0.0811 ms | 20.999 ms |

### .NET 7 with EF7 using ExecuteUpdate

|                 Method |      Mean |     Error |    StdDev |
|----------------------- |----------:|----------:|----------:|
|            GetProducts | 15.869 ms | 0.1425 ms | 0.1263 ms |
| GetProductsForCategory |  6.392 ms | 0.1087 ms | 0.1693 ms |
|    DiscontinueProducts |  7.149 ms | 0.0754 ms | 0.0668 ms |
