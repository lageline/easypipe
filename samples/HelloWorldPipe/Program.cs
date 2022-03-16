using Lageline.EasyPipe;

var pipeline = new PipelineBuilder<testParmam>()
    .AddStep(new MyStep1())
    .AddStep((parms, context) => Console.WriteLine($"An inline step"))
    .AddStep(async (parms, context) =>
    {
        Console.WriteLine($"An async inline step");
        await Task.Delay(500);
    })
    .AddStep(new MyExitingStep())
    .AddStep(new MyFailingStep())
    .Build();

await pipeline.ExecuteAsync(new testParmam("hello"), CancellationToken.None);


record testParmam(string hello);
class MyStep1 : StepBase<testParmam>
{
    public override Task OnExecuteAsync(testParmam parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello from MyStep1");
        return Task.CompletedTask;
    }
}

class MyStep2: StepBase<testParmam>
{
    public override Task OnExecuteAsync(testParmam parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from MyStep2 getting \"{parameters.hello}\" from parameters");
        return Task.CompletedTask;
    }
}

class MyExitingStep : StepBase<testParmam>
{
    public override Task OnExecuteAsync(testParmam parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello from MyExitingStep. Pipeline will exit after this step even when more steps exists");
        context.SignalExit = true;
        return Task.CompletedTask;
    }
}

class MyFailingStep : StepBase<testParmam>
{
    public override Task OnExecuteAsync(testParmam parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        throw new Exception("Oh no from MyFailingStep");
    }
}