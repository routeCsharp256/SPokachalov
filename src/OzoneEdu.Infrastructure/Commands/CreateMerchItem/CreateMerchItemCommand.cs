using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public sealed class CreateMerchItemCommand : IRequest<bool>
    {
      
        public List<long> Sku { get; init; }
        
        public long MerchPackTypeId { get; init; }
        
        public long MerchCustomerId { get; init; }
        
        public long IssueTypeId { get; init; }

        public bool IsDone { get; init; }
    }
}