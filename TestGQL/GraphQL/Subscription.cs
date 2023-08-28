using HotChocolate;
using HotChocolate.Types;
using TestGQL.Models;

namespace TestGQL.GraphQL;
public class Subscription
{
    [Subscribe]
    [Topic]
    public Customer OnCustomerAdded([EventMessage] Customer customer)
    {
        return customer;
    }
}