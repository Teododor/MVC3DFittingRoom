using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.UserRating.Model;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.UserRating
{
	public class UserRatingService : BaseService
	{
		public UserRatingService(ServiceDependencies serviceDependencies) : base(serviceDependencies) 
		{ 
			
		}



		public void AddRatingFromUser(UserRatingModel model)
		{
			UserReview userReview = new UserReview
			{
				UserId = model.userId,
				ProductId = model.productId,
				StarsGiven = model.numberOfStars
			};

			bool alreadyExists = UnitOfWork.UserReviews
				.Get()
				.Where(p => p.UserId == userReview.UserId)
				.Where(p => p.ProductId == userReview.ProductId)
				.Count() != 0;

			if(!alreadyExists)
			{
				UnitOfWork.UserReviews.Insert(userReview);
				UnitOfWork.SaveChanges();
			}
			else
			{
				UnitOfWork.UserReviews.Update(userReview);
				UnitOfWork.SaveChanges();
			}

		}

		public int? GetUserRatingByUserIdAndProductId(int userId, int productId)
		{
			int? userRating = UnitOfWork.UserReviews
				.Get()
				.Where(p => p.UserId == userId)
				.Where(p => p.ProductId == productId)	
				.Select(p => p.StarsGiven).FirstOrDefault();
			return userRating;
		}

		public List <UserRatingModel> GetUserRatings()
		{
			var listOfRatings = UnitOfWork.UserReviews
				.Get()
				.Where(p => p.UserId == CurrentUser.Id)
				.Select(p =>
			new UserRatingModel
			{
				userId = p.UserId,
				productId = p.ProductId,
				numberOfStars = (int)p.StarsGiven

			}).ToList();
			return listOfRatings;
		}
	}
}
