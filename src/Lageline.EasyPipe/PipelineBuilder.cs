using System.Collections.Generic;

namespace Lageline.EasyPipe
{
    public interface IPipelineBuilder<TParameters> where TParameters: class
    {
        PipelineBuilder<TParameters> AddStep(StepBase<TParameters> step);
        IPipeline<TParameters> Build();
    }

    public class PipelineBuilder<TParameters> : IPipelineBuilder<TParameters> where TParameters: class
    {
        private List<StepBase<TParameters>> steps = new List<StepBase<TParameters>>();
        public PipelineBuilder<TParameters> AddStep(StepBase<TParameters> step)
        {
            steps.Add(step);
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
