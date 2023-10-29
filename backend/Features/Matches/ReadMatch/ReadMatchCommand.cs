using ErrorOr;
using MediatR;
using WebFootballApp.Entities;

namespace WebFootballApp.Features.Matches.ReadMatch;

public class ReadMatchCommand : IRequest<ErrorOr<Match>>
{
    public int Id { get; set; }
}