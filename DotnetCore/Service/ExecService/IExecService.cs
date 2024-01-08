using DotnetCore.Models;

namespace DotnetCore.Service.ExecService
{
    public interface IExecService
    {
        ResultDTO<loveCodeDTO> loveCodeList(loveCode_Params param);
    }
}
