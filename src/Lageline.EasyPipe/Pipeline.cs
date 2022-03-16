using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    internal class Pipeline<TParameters>: IPipeline<TParameters> where TParameters: class
    {   
        private Func<TParameters, PipelineContext, CancellationToken, Task>[] pipelineSteps;

        public Pipeline(Func<TParameters, PipelineContext, CancellationToken, Task>[] pipelineSteps)
        {
            this.pipelineSteps = pipelineSteps; 
        }
        public async Task ExecuteAsync(TParameters parameters, CancellationToken cancellationToken)
        {
            for(int i = 0; i<pipelineSteps.Length; i++)
            {
                var step = pipelineSteps[i];
                try
                {
                    var context = new PipelineContext();
                    await step(parameters, context, cancellationToken).ConfigureAwait(false);
                    if(context.SignalExit)
                        return;
                }
                catch(Exception e)
                {
                    throw CreatePipelineException(i, step, e);
                }
            }
        }

        public Task ExecuteAsync(TParameters parameters)
        {
            return ExecuteAsync(parameters,CancellationToken.None);
        }

        private PipelineException CreatePipelineException(int currentIndex, Func<TParameters, PipelineContext, CancellationToken, Task> currentStep, Exception innerException)
        {
            var message = $"Exception during pipeline execution step {currentIndex} type {currentStep.GetType().Name}";
            throw new PipelineException(message, innerException);
        }
    }

    public class PipelineContext
    {
        public bool SignalExit { get; set; }

        public string Name { get; set; }
    }
}
