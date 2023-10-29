using System.Security.Claims;
using ErrorOr;
using MediatR;
using WebFootballApp.Infrastructure;

namespace WebFootballApp.Features.History.CreateHistory;

public class CreateHistoryCommandHandler : IRequestHandler<CreateHistoryCommand, ErrorOr<Entities.History>>
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CreateHistoryCommandHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ErrorOr<Entities.History>> Handle(CreateHistoryCommand request, CancellationToken cancellationToken)
    {
        // Get the user id from the token
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Error.Failure("User not found");

        var userId = int.Parse(userIdClaim.Value);
        var user = await _context.User.FindAsync(userId);
        if (user == null) return Error.Failure("User not found");

        var history = new Entities.History()
        {
            User = user,
            Content = request.Content,
            Date = DateTime.Now
        };

        _context.History.Add(history);
        await _context.SaveChangesAsync(cancellationToken);

        return history;
    }
}