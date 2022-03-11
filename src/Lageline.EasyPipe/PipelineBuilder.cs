using System.Collections.Generic;

namespace Lageline.EasyPipe
{
    public class PipelineBuilder
    {
        private List<StepBase> steps= new List<StepBase>();
        public PipelineBuilder AddStep(StepBase step){
            steps.Add(step);
            return this;
        }
        public IPipeline Build(){
            return new Pipeline(steps.ToArray());
        }
    }
}
