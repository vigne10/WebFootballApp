using MediatR;
using ErrorOr;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Jobs.UpdateJob;

public class UpdateJobCommand : IRequest<ErrorOr<Job>>
{
    public int Id { get; set; }
    public string Name { get; set; }
}