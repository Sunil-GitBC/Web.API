using MediatR;

namespace Web.API.Query
{
    public interface IMediatorQueryHandler<in T, TR> : IRequestHandler<T, TR> where T : IRequest<TR>
    {
    }
}
