using Proiect.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;
using Proiect.BusinessLogic.Implementation;
using Proiect.BusinessLogic.Implementation.Implementation;
using Proiect.BusinessLogic.Implementation.User.Models;
using Proiect.BusinessLogic.Implementation.UserRating;
using Proiect.BusinessLogic.Implementation.UserRating.Model;
using Proiect.DataAccess;

namespace ProiectAcademie.Controllers
{
	public class UserRatingController : BaseController
	{

		private readonly UserRatingService UserRatingService;
		public UserRatingController(ControllerDependencies dependencies, UserRatingService userRatingService) : base(dependencies)
		{
			this.UserRatingService = userRatingService;
		}


		public void AddRatingFromUser([FromBody] UserRatingModel model )
		{
			UserRatingService.AddRatingFromUser(model);
		}


		[HttpGet("GetUserRating")]
		public int? GetUserRatingByUserIdAndProductIdController(int userId, int productId)
		{
			int? rating = UserRatingService.GetUserRatingByUserIdAndProductId(userId, productId);
			return rating;

		}

	}
}