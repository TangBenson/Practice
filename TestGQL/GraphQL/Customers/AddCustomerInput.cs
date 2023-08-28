using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGQL.GraphQL.Customers;

public record AddCustomerInput(string name,string phoneno);