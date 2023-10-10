using System.Threading.Tasks;
using MetaTools.Models;

namespace MetaTools.Services.UserAgent;

public interface IUserAgentService
{
    Task<UserAgentModel> Generate();
}