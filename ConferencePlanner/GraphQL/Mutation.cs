using System.Threading.Tasks;
using GraphQL.Data;
using GraphQL.Extensions;
using HotChocolate;

namespace GraphQL
{
    public class Mutation
    {
        [UseApplicationDbContext] //加這行配合一次查多筆
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerInput input,
            [Service] ApplicationDbContext context) //因為有加入[Service]，因此會自動注入ApplicationDbContext
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker); //這是EF的用法，只要透過.Add(資料物件)，即可將資料準備寫入資料庫
            await context.SaveChangesAsync(); //實際將資料寫入資料庫

            return new AddSpeakerPayload(speaker); //一般都會將異動後的資料回傳
        }
    }
}