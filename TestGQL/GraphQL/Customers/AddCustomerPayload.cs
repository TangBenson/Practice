using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGQL.Models;

namespace TestGQL.GraphQL.Customers;
public record AddCustomerPayload(Customer customer);