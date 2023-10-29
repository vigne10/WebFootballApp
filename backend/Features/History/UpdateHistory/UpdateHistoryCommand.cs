using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebFootballApp.Features.History.UpdateHistory;

public class UpdateHistoryCommand : IRequest<ErrorOr<Entities.History>>
{
    [FromRoute] public int Id { get; set; }
    public string? Content { get; set; }
}