using System;
using System.Collections.Generic;

namespace ihud.WebApi.Models
{
    public partial class TopicReply
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserDisplayname { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
    }


    /// <summary>
    /// Модель представления для создания комментария
    /// </summary>
    public partial class TopicReplySubmit
    {
        public int TopicId { get; set; }    
        public string UserName { get; set; }
        public bool isAnonym { get; set; }
        public string Description { get; set; }
       
    }

    /// <summary>
    /// Модель представления комментария для фронтенда
    /// </summary>
    public partial class TopicReplyViewModel
    {

        public int Id { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string UserName { get; set; }
        public bool isAnonym { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
