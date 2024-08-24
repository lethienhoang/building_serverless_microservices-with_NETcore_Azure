

using MediatR;

namespace FunctionApp.Discounts.Commands;

public record DeleteDiscountCommand(string ProductName) : IRequest<bool>;