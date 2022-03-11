namespace Lageline.EasyPipe;

[System.Serializable]
public class PipelineException : System.Exception
{
    public PipelineException(string message, System.Exception inner) : base(message, inner) { }
    protected PipelineException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}