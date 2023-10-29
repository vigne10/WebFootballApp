using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Jobs.ReadJobs;

public class ReadJobsCommandHandler : IRequestHandler<ReadJobsCommand, ErrorOr<List<Job>>>
{
    private readonly ApplicationDbContext _context;

    public ReadJobsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Job>>> Handle(ReadJobsCommand request, CancellationToken cancellationToken)
    {
        var jobs = _context.Job.OrderBy(p => p.Name).ToListAsync(cancellationToken);

        if (jobs == null) return Error.NotFound("Jobs not found");

        return await jobs;
    }
}