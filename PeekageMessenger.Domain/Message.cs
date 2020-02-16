using System;
using System.Collections.Generic;
using System.Text;

namespace PeekageMessenger.Domain
{
    public class Message
    {
        public Message(Guid messageId, string text)
        {
            MessageId = messageId;
            Text = text;
        }

        public Guid MessageId { get; private set; }
        public string Text { get; private set; }
        public override string ToString()
        {
            return $"{MessageId}~{Text}";
        }
    }
}
