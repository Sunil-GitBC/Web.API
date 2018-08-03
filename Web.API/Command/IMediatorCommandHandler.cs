using MediatR;

namespace Web.API.Command
{
    public interface IMediatorCommandHandler<in T> : IRequestHandler<T> where T : IRequest
    {
    }
}
