using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGQL.GraphQL.Orders;
public record AddOrderInput(string orderno, string orderdate, int custid);