using Microsoft.Extensions.Options;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.Data;
using SquishFaceAPI.Model.View;
using SquishFaceAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquishFaceAPI.Service
{
    public class MessageManagement : IMessageManagement
    {
        private readonly IRepository<Message> _messageRepository = null;
        private IEnumerable<MessageDetail> _messageDetails = null;

        public MessageManagement(IOptions<AppConfig> appConfigs)
        {
            _messageRepository = new MessageRepository(appConfigs);
        }

        public string AddMessage(MessageDetail messageDetail)
        {
            Guid id = Guid.NewGuid();

            _messageRepository.CreateItem(new Message
            {
                Id = id,
                By = messageDetail.By,
                Text = messageDetail.Text
            });

            ResetGlobals();

            return id.ToString();
        }

        public string EditMessage(MessageDetail messageDetail)
        {
            var message = _messageRepository.GetItem(i => i.Id == messageDetail.Id);
            message.Likes = messageDetail.Likes;

            _messageRepository.UpdateItem(j => j.Id == messageDetail.Id, message);
            
            ResetGlobals();

            return messageDetail.Id.ToString();
        }

        public IEnumerable<MessageDetail> GetAllMessages()
        {
            return _messageRepository.GetAllItems()
                .Select(i => new MessageDetail
                {
                    By = i.By,
                    Id = i.Id,
                    Likes = i.Likes,
                    On = i.On,
                    Text = i.Text
                })
                .OrderByDescending(i => i.On);
        }

        public IEnumerable<MessageDetail> GetMessagesBy(QueryDetail queryDetail)
        {
            RemoveOldMessages().Wait();

            return GetMessages()?
                .OrderByDescending(i => i.On)
                .Take(queryDetail.Count);
        }

        public string LikeMessage(MessageDetail messageDetail)
        {
            List<Like> newLikes = new List<Like>();
            var message = _messageRepository.GetItem(i => i.Id == messageDetail.Id);
            var likes = message?.Likes;

            if (likes?.Count > 0)
            {
                var filterLike = likes.FirstOrDefault(j => j.By.ToLower() == messageDetail.LikedBy.ToLower());                

                if(filterLike != null)
                {
                    likes.Remove(filterLike);
                } 
                else
                {
                    likes.Add(new Like { By = messageDetail.LikedBy, On = DateTime.Now });
                }
            }
            else
            {
                newLikes.Add(new Like { By = messageDetail.LikedBy, On = DateTime.Now });
                likes = newLikes;
            }
                        

            return EditMessage(new MessageDetail
                {
                    By = message.By,
                    Id = message.Id,
                    Likes = likes,
                    On = message.On,
                    Text = message.Text
                }
            );
        }

        public void ResetGlobals()
        {
            _messageDetails = null;
        }

        private IEnumerable<MessageDetail> GetMessages()
        {
            if (_messageDetails == null) _messageDetails = GetAllMessages();

            return _messageDetails;
        }

        private async Task RemoveOldMessages()
        {
            DateTime now = DateTime.Now;
            var messages = GetMessages();

            var ids = messages
                .Where(i => (i.On.AddHours(24) > now && i.On <= now))
                .Select(j => j.Id);

            foreach(Guid guid in ids)
            {
                var result = await _messageRepository.RemoveItemAsync(k => k.Id == guid);
            }
        }
    }
}
