using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class GiveOutCreateMerchItemCommand : IRequest
    {
        public long Id { get; set; }
        public List<string> MerchPaks { get; set; }
    }
}