using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace simpleSendMessage
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        [NonSerialized]
        Timer t;

        

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //We need to keep this data so we know who to send the message to. Assume this would be stored somewhere, e.g. an Azure Table
            ConversationStarter.toId = message.From.Id;
            ConversationStarter.toName = message.From.Name;
            ConversationStarter.fromId = message.Recipient.Id;
            ConversationStarter.fromName = message.Recipient.Name;
            ConversationStarter.serviceUrl = message.ServiceUrl;
            ConversationStarter.channelId = message.ChannelId;
            ConversationStarter.conversationId = message.Conversation.Id;

            
            var url = HttpContext.Current.Request.Url;
            //We now tell the user that we will talk to them in a few seconds
            await context.PostAsync("Hello! If you have some free time, look for a link to click...");
            context.Wait(MessageReceivedAsync);
        }
       

    }
}