using ErrorOr;
using MediatR;

namespace WebFootballApp.Features.Jobs.DeleteJob;

public class DeleteJobCommand : IRequest<ErrorOr<Unit>>
{
    public int Id { get; set; }
}