using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Infrastructure.Commands;

namespace Infrastructure.Handlers.NewEmploeeEventCommandHandler
{
    public class NewEmploeeEventCommandHandler : IRequestHandler<NewEmploeeEventCommand> 
    {
        public async Task<Unit> Handle(NewEmploeeEventCommand request, CancellationToken cancellationToken)
        {
           return Unit.Value;
        }
    }
}