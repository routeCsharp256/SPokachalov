using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public sealed class CheckMerchItemCommand : IRequest<bool>
    {
        public int MerchPackTypeId { get; init; }
        public int MerchCustomerId { get; init; }
    }
}