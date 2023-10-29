using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Jobs.DeleteJob;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteJobCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _context.Job.FindAsync(request.Id);

        if (job == null) return Error.NotFound("Job not found");

        _context.Job.Remove(job);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}