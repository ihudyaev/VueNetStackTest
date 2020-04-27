using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ihud.WebApi.Models
{
    /// <summary>
    /// Преобразование данных из базы в модели для приложения
    /// </summary>

    public static class ConversionModels
    {
        /// <summary>
        /// From Category to Category View Model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<CategoryViewModel> CategoryToCategoryViewModel( this IQueryable<Category> query )
        {
            return query.Select(u => new CategoryViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                TopicCount = u.Topic.Count,
                LastNewTopic = u.Topic.Select(x => x.CreatedAt).Max()
            }) ;
        }
        /// <summary>
        /// From Topic to Topic View Model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<TopicViewModel> TopicToTopicViewModel( this IQueryable<Topic> query )
        {
            return query.Select(u => new TopicViewModel
            {
                Id = u.Id,
                CategoryId = u.CategoryId,
                UserId = u.UserId,       
                //анонимный пользователь? выводим имя из таблицы статьи. Если авторизованный то из профиля
                UserName = (u.UserId == 10 ? u.UserDisplayname : u.User.DisplayName),
                Title = u.Title,
                Description = u.Description,
                isAnonym = ( u.UserId == 10 ? true : false ),
                TopicReplyCnt = u.TopicReply.Count,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                LastCommentAt = u.TopicReply.Select(x => x.UpdatedAt).Max()
            });
        }

        /// <summary>
        /// From Topic Reply to Topic Reply View Model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<TopicReplyViewModel> TopicReplyToTopicReplyViewModel( this IQueryable<TopicReply> query )
        {
            return query.Select(u => new TopicReplyViewModel
            {
                Id = u.Id,
                TopicId = u.TopicId,
                UserId = u.UserId,
                //Проверка на анонимного пользователя
                isAnonym = ( u.UserId == 10 ? true : false ),
                CategoryId = u.Topic.CategoryId,
                UserName = u.UserDisplayname,
                Description = u.Description,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
        }

        /// <summary>
        /// From User to Profile View Model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<Profile> UserToUserProfileModel( this IQueryable<User> query )
        {
            return query.Select(u => new Profile(u));
        }

        /// <summary>
        /// From User to Profile Full View Model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<Profile> UserToUserProfileFullModel( this IQueryable<User> query )
        {
            return query.Select(u => new Profile(u));
        }

    }

}
