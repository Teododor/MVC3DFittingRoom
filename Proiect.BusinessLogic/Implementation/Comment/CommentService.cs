using Proiect.BusinessLogic.Base;
using Proiect.BusinessLogic.Implementation.Comment.Models;

namespace Proiect.BusinessLogic.Implementation.Comment
{
	public class CommentService : BaseService
	{
		public CommentService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{

		}
		public List<CommentModel> GetComments()
		{
			var commentsList = UnitOfWork.Comments.Get().Select(comment => new CommentModel
			{
				Review = comment.Review,
				Mark = (int)comment.Mark
			}).ToList();
			return commentsList;
		}
	}
}
