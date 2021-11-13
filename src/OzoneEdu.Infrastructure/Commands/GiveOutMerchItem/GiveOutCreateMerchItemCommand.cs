using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class GiveOutIssuedMerchItemCommand : IRequest
    {
        public long RequestId { get; set; }
    }
}