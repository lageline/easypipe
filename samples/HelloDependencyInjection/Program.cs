using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lageline.EasyPipe;
using Lageline.EasyPipe.DependencyInjection;


var services = new ServiceCollection();
services.AddLogging(configure => configure.AddConsole())
    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
    .AddTransient<MyStep1>()
    .AddTransient<MyStep2>();

var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Main");

var pipeline = new PipelineBuilderDI<MyParameters>(serviceProvider)
    .AddStep<MyStep1>()
    .AddStep<MyStep2>()
    .Build();

var parameters = new MyParameters{Greeting ="hello"};
await pipeline.ExecuteAsync(parameters, CancellationToken.None);
logger.LogInformation($"The answer is {parameters.Answer}");

Console.ReadLine();

class MyParameters
{
    public string? Greeting { get; init; }
    public string? Answer {get; set;}
}


class MyStep1 : IStep<MyParameters>
{
    private ILogger<MyStep1> logger;

    public MyStep1(ILogger<MyStep1> logger)
    {
        this.logger = logger;
    }
    public Task OnExecuteAsync(MyParameters parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        logger.LogInformation("Hello from MyStep1, no answer from me");
        return Task.CompletedTask;
    }
}

class MyStep2: IStep<MyParameters>
{
    private readonly ILogger<MyStep2> logger;

    public MyStep2(ILogger<MyStep2> logger)
    {
       this.logger = logger;
    }
    public Task OnExecuteAsync(MyParameters parameters, PipelineContext context, CancellationToken cancellationToken)
    {
        logger.LogInformation("Hello from MyStep2, I've got an answer");
        parameters.Answer = "42";
        return Task.CompletedTask;
    }
}
