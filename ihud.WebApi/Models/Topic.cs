using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ihud.WebApi.Models
{
    public partial class Topic
    {
        public Topic()
        {
            TopicReply = new HashSet<TopicReply>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserDisplayname { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TopicReply> TopicReply { get; set; }
    }
    /// <summary>
    /// Модель представления для фронтенда
    /// </summary>
    public partial class TopicViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isAnonym { get; set; }
        public string UserName { get; set; }
        public int TopicReplyCnt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastCommentAt { get; set; }

    }


    [JsonObject]
    public partial class TopicSubmit
    {
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }


        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }
    }
}
