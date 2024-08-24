
using System.Windows.Input;
using MediatR;

namespace FunctionApp.Discounts.Commands;

public record CreateDiscountCommand(string ProductName, string ProductDescription, int Amount) : IRequest<Guid>;