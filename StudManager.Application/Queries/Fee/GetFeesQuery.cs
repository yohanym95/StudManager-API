using MediatR;
using StudManager.Application.Responses.Fee;

namespace StudManager.Application.Queries.Fee
{
    public class GetFeesQuery : IRequest<GetFeesReponse>
    {
        public int Id { get; set; }
    }
}
