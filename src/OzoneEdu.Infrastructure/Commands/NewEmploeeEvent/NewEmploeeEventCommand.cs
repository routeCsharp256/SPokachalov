using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public sealed class NewEmploeeEventCommand : IRequest
    {
      
        public List<long> Sku { get; init; }
        
        public int MerchPackType { get; init; }
        
        public int MerchCustomerId { get; init; }
        
        public int IssueTypeId { get; init; }

        public bool IsDone { get; init; }
    }
}