using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Seasons.ReadSeasons;

public class ReadSeasonsCommand : IRequest<ErrorOr<List<Season>>>
{
    
}