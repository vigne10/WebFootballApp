using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Jobs.ReadJob;

public class ReadJobCommand : IRequest<ErrorOr<Job>>
{
    public int Id { get; set; }
}