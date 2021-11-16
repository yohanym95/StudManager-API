using MediatR;
using StudManager.Application.Responses.Fee;

namespace StudManager.Application.Commands.Fee
{
    public class CreateFessCommand : IRequest<FeesResponse>
    {
        public int Id { get; set; }
        public string FeesType { get; set; }
        public string AmountofFees { get; set; }
        public string RecieptNo { get; set; }
        public string FeesDescription { get; set; }
        public int StuId { get; set; }
        public int CourseId { get; set; }
    }
}
