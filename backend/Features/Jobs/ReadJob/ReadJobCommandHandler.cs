using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Jobs.ReadJob;

public class ReadJobCommandHandler : IRequestHandler<ReadJobCommand, ErrorOr<Job>>
{
    private readonly ApplicationDbContext _context;

    public ReadJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Job>> Handle(ReadJobCommand request, CancellationToken cancellationToken)
    {
        var job = _context.Job.SingleOrDefault(p => p.Id == request.Id);

        if (job == null) return Error.NotFound("Job not found");

        return job;
    }
}