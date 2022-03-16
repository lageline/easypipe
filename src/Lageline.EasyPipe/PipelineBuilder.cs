using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lageline.EasyPipe
{
    public interface IPipelineBuilder<TParameters> where TParameters: class
    {
        PipelineBuilder<TParameters> AddStep(StepBase<TParameters> step);
        IPipeline<TParameters> Build();
    }

    public class PipelineBuilder<TParameters> : IPipelineBuilder<TParameters> where TParameters: class
    {
        private List<Func<TParameters, PipelineContext, CancellationToken, Task>> steps = new List<Func<TParameters, PipelineContext, CancellationToken, Task>>();
        public PipelineBuilder<TParameters> AddStep(StepBase<TParameters> step)
        {
            steps.Add(new Func<TParameters, PipelineContext, CancellationToken, Task>(step.OnExecuteAsync));
            return this;
        }

        public PipelineBuilder<TParameters> AddStep(Func<TParameters, PipelineContext, CancellationToken, Task> step)
        {
            steps.Add(new Func<TParameters, PipelineContext, CancellationToken, Task>(step));
            return this;
        }

        public PipelineBuilder<TParameters> AddStep(Action<TParameters, PipelineContext> step)
        {
            steps.Add(new Func<TParameters, PipelineContext, CancellationToken, Task>((p, c, t) =>
            {
                step(p, c);
                return Task.CompletedTask;
            }));
            return this;
        }

        public IPipeline<TParameters> Build()
        {
            var pipeline = new Pipeline<TParameters>(steps.ToArray());
            steps.Clear();
            return pipeline;
        }
    }
}
