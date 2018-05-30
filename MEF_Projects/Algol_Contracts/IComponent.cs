namespace Algol_Contracts
{
    public interface IComponent
    {
        string Description { get; }
        string ManipulateOperation(params double[] args);
    }

    public interface IMetadata
    {
        char Symbol { get; }
    }

}
