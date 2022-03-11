using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    public abstract class StepBase 
    {
        public abstract Task OnExecuteAsync<TParmaters>(TParmaters parameters, PipelineContext context,  CancellationToken cancellationToken);
    }
}
