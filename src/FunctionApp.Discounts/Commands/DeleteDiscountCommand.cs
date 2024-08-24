

using MediatR;

namespace FunctionApp.Discounts.Commands;

public record DeleteDiscountCommand(Guid Id) : IRequest<bool>;