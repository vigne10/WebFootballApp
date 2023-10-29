using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Jobs.ReadJobs;

public class ReadJobsCommand : IRequest<ErrorOr<List<Job>>>
{
}