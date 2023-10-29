using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Jobs.CreateJob;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, ErrorOr<Job>>
{
    private readonly ApplicationDbContext _context;

    public CreateJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Job>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = new Job
        {
            Name = request.Name
        };

        _context.Job.Add(job);
        await _context.SaveChangesAsync(cancellationToken);

        return job;
    }
}