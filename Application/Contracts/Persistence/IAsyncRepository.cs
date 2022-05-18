using Ardalis.Specification;

namespace Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> : IRepositoryBase<T> where T : class
    {

    }
}
