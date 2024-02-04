using System;
namespace Presentation.ViewModels
{
	public record UserSummary(string UserName, List<Balance> Balances);
}

