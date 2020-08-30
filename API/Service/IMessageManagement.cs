using SquishFaceAPI.Model.View;
using System.Collections.Generic;

namespace SquishFaceAPI.Service
{
    public interface IMessageManagement
    {
        IEnumerable<MessageDetail> GetAllMessages();
        IEnumerable<MessageDetail> GetMessagesBy(QueryDetail queryDetail);
        string AddMessage(MessageDetail messageDetail);
        string EditMessage(MessageDetail messageDetail);
        string LikeMessage(MessageDetail messageDetail);
        void ResetGlobals();
    }
}
