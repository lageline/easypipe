namespace Lageline.EasyPipe;


public interface IPipeline
{
    Task ExecuteAsync<TParmaters>(TParmaters parameters);
    Task ExecuteAsync<TParmaters>(TParmaters parameters, CancellationToken cancellationToken);
}
