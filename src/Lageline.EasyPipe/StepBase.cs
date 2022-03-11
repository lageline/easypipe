namespace Lageline.EasyPipe;

public abstract class StepBase 
{
    private bool signalExit = false;
    internal async Task<bool> ExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken){

        await OnExecuteAsync(parameters, cancellationToken);
        return signalExit;
    }

    public abstract Task OnExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken);
    protected void SetExitPipeline()
    {
        signalExit = true;
    }
}
