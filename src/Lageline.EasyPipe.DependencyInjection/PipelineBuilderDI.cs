using System;
using Microsoft.Extensions.DependencyInjection;
namespace Lageline.EasyPipe.DependencyInjection
{
    public class PipelineBuilderDI<TParameters>: PipelineBuilder<TParameters> where TParameters: class
    {
        private readonly IServiceProvider services;

        public PipelineBuilderDI(IServiceProvider services)
        {
            this.services = services;
        }
        public PipelineBuilderDI<TParameters> AddStep<TType>() where TType : IStep<TParameters> 
        {
            var instance = services.GetRequiredService<TType>();
            base.AddStep(instance);
            return this;
        }
    }
}

