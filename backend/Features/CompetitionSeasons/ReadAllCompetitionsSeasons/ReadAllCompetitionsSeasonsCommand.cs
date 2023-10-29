using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.CompetitionSeasons.ReadAllCompetitionsSeasons;

public class ReadAllCompetitionsSeasonsCommand : IRequest<ErrorOr<List<CompetitionSeason>>>
{
    
}