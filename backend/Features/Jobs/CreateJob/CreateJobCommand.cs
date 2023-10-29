using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Jobs.CreateJob;

public class CreateJobCommand : IRequest<ErrorOr<Job>>
{
    public string Name { get; set; }
}