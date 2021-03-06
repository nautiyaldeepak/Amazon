﻿using System;
using System.Threading.Tasks;

using Carbon.Messaging;

namespace Amazon.Sns
{
    public class SnsTopic
    {
        private readonly SnsClient client;
        private readonly string arn;

        public SnsTopic(AwsRegion region, string accountId, string topicName, IAwsCredential credential)
        {
            #region Preconditions

            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));

            if (topicName == null)
                throw new ArgumentNullException(nameof(topicName));

            #endregion

            this.client = new SnsClient(region, credential);
            this.arn = $"arn:aws:sns:{region}:{accountId}:{topicName}";
        }

        public Task<string> PublishAsync(string message)
        {
            #region Preconditions

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            #endregion

            // Max payload = 256KB (262,144 bytes)

            var request = new PublishRequest {
                TopicArn = arn,
                Message = message
            };

            return client.PublishAsync(request);
        }

        public Task PublishAsync(IMessage<string> message) =>
            PublishAsync(message.Body);
    }
}
