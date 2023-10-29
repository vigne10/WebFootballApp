using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.History.UpdateHistory;

public class UpdateHistoryCommandHandler : IRequestHandler<UpdateHistoryCommand, ErrorOr<Entities.History>>
{
    private readonly ApplicationDbContext _context;

    public UpdateHistoryCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Entities.History>> Handle(UpdateHistoryCommand request,
        CancellationToken cancellationToken)
    {
        var history = await _context.History.SingleOrDefaultAsync(a => a.Id == request.Id);

        if (history == null) return Error.NotFound("History not found");

        history.Content = string.IsNullOrEmpty(request.Content) ? null : request.Content;

        await _context.SaveChangesAsync(cancellationToken);

        return history;
    }
}