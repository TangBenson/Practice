using CommonService.Dtos; //Dto就是data return回去的object
using System.Data;

namespace QueryService.Services
{
    public interface IQuerySampleData
    {
         ResultDTO<DataSet> QuerySampleDataFromDB(); //介面定義服務有一個功能，return ResultDTO<DataSet>
    }
}
//CommonService的Models的Dtos是外面傳進來，Params是我傳出去的