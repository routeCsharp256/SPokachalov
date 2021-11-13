using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public sealed class SetStatusMerchItemCommand : IRequest<bool>
    {
        public long  MerchId { get; init; }
        public bool Status { get; init; }
    }
}