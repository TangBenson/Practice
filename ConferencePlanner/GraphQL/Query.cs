using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GraphQL.Data;
using HotChocolate;
using GraphQL.Extensions;

namespace GraphQL
{
    public class Query
    {
        //GetSpeakers方法，是一個解析器
        //這裡會需要一個參數ApplicationDbContext，但它在被呼叫時需要由Hot Chocolate來協助注入到方法中，因此需要在參數前加上[Service]的註記。(Dotnet原生DI是不支援直接注入到方法中)
        // public IQueryable<Speaker> GetSpeakers([Service] ApplicationDbContext context) =>
        //     context.Speakers;

        //此寫法配合一次查多筆
        [UseApplicationDbContext]
        public Task<List<Speaker>> GetSpeakers([ScopedService] ApplicationDbContext context) =>
            context.Speakers.ToListAsync();
    }
}