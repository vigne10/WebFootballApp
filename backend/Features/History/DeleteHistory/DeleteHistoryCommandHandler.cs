using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.History.DeleteHistory;

public class DeleteHistoryCommandHandler : IRequestHandler<DeleteHistoryCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteHistoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteHistoryCommand request, CancellationToken cancellationToken)
    {
        var history = await _context.History.FindAsync(request.Id);

        if (history == null) return Error.NotFound("History not found");

        _context.History.Remove(history);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}