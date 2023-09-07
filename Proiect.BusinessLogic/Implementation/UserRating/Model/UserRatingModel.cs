using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.UserRating.Model
{
	public class UserRatingModel
	{
		public int userId { get; set; }
		public int productId { get; set; }
		public int numberOfStars { get; set; }
	}
}
