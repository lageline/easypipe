using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    public interface IStep<TParameters> where TParameters: class
    {
        Task OnExecuteAsync(TParameters parameters, PipelineContext context,  CancellationToken cancellationToken);
          
    }
}
