using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;

namespace MyWorkbench.BusinessObjects.Helpers
{
    public enum MessageTypes
    {
        Success = 0,
        Warning = 1,
        Error = 2,
        RecordsProcessed = 3
    }

    public interface IMessageInformation
    {
        Guid Oid { get; set; }
        Guid UserID { get; set; }
        MessageTypes MessageType { get; set; }
        string MessageText { get; set; }
        InformationPosition Position { get; set; }
        Exception Exception { get; set; }
    }

    public class MessageInformation : IMessageInformation
    {
        public Guid Oid { get; set; }
        public Guid UserID { get; set; }
        public MessageTypes MessageType { get; set; }
        public string MessageText { get; set; }
        public InformationPosition Position { get; set; }
        public Exception Exception { get; set; }

        public MessageInformation(MessageTypes MessageType, Exception Exception)
        {
            this.Oid = Guid.NewGuid();
            this.MessageType = MessageType;
            this.Exception = Exception;
            this.Position = InformationPosition.Bottom;
        }

        public MessageInformation(MessageTypes MessageType, string MessageText)
        {
            this.Oid = Guid.NewGuid();
            this.MessageType = MessageType;
            this.MessageText = MessageText;
            this.Position = InformationPosition.Bottom;
        }

        public MessageInformation(Guid UserID, MessageTypes MessageType, string MessageText)
        {
            this.Oid = Guid.NewGuid();
            this.UserID = UserID;
            this.MessageType = MessageType;
            this.MessageText = MessageText;
            this.Position = InformationPosition.Bottom;
        }

        public MessageInformation(Guid UserID, MessageTypes MessageType, string MessageText, InformationPosition Position)
        {
            this.Oid = Guid.NewGuid();
            this.MessageType = MessageType;
            this.MessageText = MessageText;
            this.Position = Position;
        }
    }

    public static class MessageProvider
    {
        public static List<IMessageInformation> _messages;
        public static List<IMessageInformation> Messages {
            get {
                if (_messages == null)
                    _messages = new List<IMessageInformation>();
                return _messages;
            }
        }

        public static void RegisterMessage(IMessageInformation MessageInformation)
        {
            Messages.Add(MessageInformation);
        }

        public static void RegisterMessage(IEnumerable<IMessageInformation> MessageInformations)
        {
            Messages.AddRange(MessageInformations);
        }

        public static void DeRegisterMessage(IMessageInformation MessageInformation)
        {
            Messages.Remove(MessageInformation);
        }
    }
}
