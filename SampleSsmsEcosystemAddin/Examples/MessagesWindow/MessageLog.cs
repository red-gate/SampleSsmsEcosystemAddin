using System;
using System.Collections.ObjectModel;

namespace SampleSsmsEcosystemAddin.Examples
{
    internal class MessageLog
    {
        public ObservableCollection<Message> Messages { get; private set; }

        public MessageLog()
        {
            Messages = new ObservableCollection<Message>();
            AddMessage("Message log started.");
        }

        public void AddMessage(string messageText)
        {
            var message = new Message(messageText);
            Messages.Insert(0, message);
        }
    }

    internal class Message
    {
        private readonly string m_Text;
        private readonly DateTime m_CreatedTime;

        public string Text { get { return m_Text; } }
        public string Time { get { return m_CreatedTime.ToLongTimeString(); } }

        public Message(string text)
        {
            m_Text = text;
            m_CreatedTime = DateTime.Now;
        }
    }
}