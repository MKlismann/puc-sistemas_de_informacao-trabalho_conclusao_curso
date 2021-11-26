using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using System.Threading.Tasks;

namespace API_2.Domain.Interfaces.Infraestructure.Repositories.Rest
{
    public interface IAPI_2RestApiRepository
    {
        Task<Result<string>> API_2_Resource_2();
    }
}
