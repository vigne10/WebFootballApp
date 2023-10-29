using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebFootballApp.Entities;
using WebFootballApp.Infrastructure;

// ReSharper disable once UnusedType.Global

namespace WebFootballApp.Features.Competitions.ReadCompetitions;

public class ReadCompetitionsCommandHandler : IRequestHandler<ReadCompetitionsCommand, ErrorOr<List<Competition>>>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public ReadCompetitionsCommandHandler(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ErrorOr<List<Competition>>> Handle(ReadCompetitionsCommand request,
        CancellationToken cancellationToken)
    {
        var competitions = await _context.Competition.OrderBy(c => c.Name).ToListAsync(cancellationToken);

        if (competitions == null) return Error.NotFound("Competitions not found");

        foreach (var competition in competitions)
            competition.Logo = competition.Logo != null
                ? $"{_configuration.GetValue<string>("ApiBaseUrl")}/Images/Competitions/{competition.Logo}"
                : null;

        return competitions;
    }
}