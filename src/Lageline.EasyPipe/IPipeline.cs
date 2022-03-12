using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    public interface IPipeline<TParameters> where TParameters:class
    {
        Task ExecuteAsync(TParameters parameters);
        Task ExecuteAsync(TParameters parameters, CancellationToken cancellationToken);
    }
}
