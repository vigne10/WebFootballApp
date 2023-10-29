using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Rewards.DeleteTeamReward;

public class DeleteTeamRewardCommandHandler : IRequestHandler<DeleteTeamRewardCommand, ErrorOr<Unit>>
{
    private readonly ApplicationDbContext _context;

    public DeleteTeamRewardCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteTeamRewardCommand request, CancellationToken cancellationToken)
    {
        var reward = await _context.Reward.FindAsync(request.Id);
        if (reward == null) return Error.NotFound("Reward not found");

        _context.Reward.Remove(reward);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}