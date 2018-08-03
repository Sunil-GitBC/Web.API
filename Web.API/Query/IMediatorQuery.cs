using MediatR;

namespace Web.API.Query
{
    public interface IMediatorQuery<out T> :  IRequest<T>
    {
    }
}
