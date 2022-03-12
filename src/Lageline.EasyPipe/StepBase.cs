using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    public abstract class StepBase<TParameters> where TParameters: class
    {
        public abstract Task OnExecuteAsync(TParameters parameters, PipelineContext context,  CancellationToken cancellationToken);
          
    }
}
