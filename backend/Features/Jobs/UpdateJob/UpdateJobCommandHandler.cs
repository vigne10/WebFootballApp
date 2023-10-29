using ErrorOr;
using MediatR;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Jobs.UpdateJob;

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, ErrorOr<Job>>
{
    private readonly ApplicationDbContext _context;

    public UpdateJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Job>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _context.Job.FindAsync(request.Id);

        if (job == null) return Error.NotFound("Job not found");

        if (!string.IsNullOrEmpty(request.Name)) job.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return job;
    }
}