using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
	[Table("users")]
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string UserName { get; set; }
	}
}

