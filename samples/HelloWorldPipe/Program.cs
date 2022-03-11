using Lageline.EasyPipe;

var pipeline = new PipelineBuilder()
    .AddStep(new MyStep1())
    .AddStep(new MyStep2())
    .AddStep(new MyFailingStep())
    .Build();


await pipeline.ExecuteAsync("You", CancellationToken.None);


class MyStep1 : StepBase
{
    public override Task OnExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello from MyStep1");
        return Task.CompletedTask;
    }
}

class MyStep2 : StepBase
{
    public override Task OnExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello from MyStep2");
        return Task.CompletedTask;
    }
}
 
class MyExitingStep : StepBase
{
    public override Task OnExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken)
    {
        SetExitPipeline();
        return Task.CompletedTask;
    }
}

class MyFailingStep : StepBase
{
    public override Task OnExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken)
    {
        throw new Exception("Oh no from MyFailingStep");
    }
}