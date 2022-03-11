namespace Lageline.EasyPipe;

internal class Pipeline: IPipeline
{   
    private StepBase[] pipelineSteps;

    public Pipeline(StepBase[] pipelineSteps)
    {
        this.pipelineSteps = pipelineSteps; 
    }
    public async Task ExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken)
    {
        for(int i = 0; i<pipelineSteps.Length; i++)
        {
            var step = pipelineSteps[i];
            try
            {
                if(await step.ExecuteAsync(parameters, cancellationToken).ConfigureAwait(false))
                    return;
            }
            catch(Exception e)
            {
                throw CreatePipelineException(i, step, e);
            }
        }
    }

    public Task ExecuteAsync<TParmaters>(TParmaters parameters)
    {
        return ExecuteAsync(parameters,CancellationToken.None);
    }

    private PipelineException CreatePipelineException(int currentIndex, StepBase currentStep, Exception innerException)
    {
         var message = $"Exception during pipeline execution step {1} type {currentStep.GetType().Name}";
         throw new PipelineException(message, innerException);
    }
}
