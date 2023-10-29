using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.History.ReadHistory;

public class ReadHistoryCommandHandler : IRequestHandler<ReadHistoryCommand, ErrorOr<Entities.History>>
{
    private readonly ApplicationDbContext _context;

    public ReadHistoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Entities.History>> Handle(ReadHistoryCommand request, CancellationToken cancellationToken)
    {
        var history = await _context.History.SingleOrDefaultAsync(h => h.Id == request.Id);

        if (history == null) return Error.NotFound("History not found");

        return history;
    }
}