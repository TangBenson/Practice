using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGQL.Models;

namespace TestGQL.GraphQL.Orders;

public record AddOrderPayload(Order order);