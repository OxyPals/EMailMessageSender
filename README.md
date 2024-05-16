# EMailMessageSender

This repository contains simple extensible EMail message sending client.
Current release supports only Mailtrap EMail sending API.
You can implement your own implementation of IEMailMessageSender interface for different APIs
Progect has "Generate NPM package on build" turned on so you can find ready to deploy package in bin\ folder

EMail class -           EMail sending API. Uses concrete implementation of IEMailMessageSender fo actual sending. Performs message validation
                        using current Mailtrap mail message constraints.

IEMailMessageSender -   Interface for implementation of concrete mail senders. Look at MailtrapEMailMessageSender for sample.

MailtrapEMailMessageSender - Implementation of IEMailMessageSender using Mailtrap Rest API v2.0

EMailAddress, EMailMessage, EMailMessageAttachment - email message and parts.
EMailSendingResult and EMailValidationResult - send and validate message results.

Sample usage:
            const string ApiBaseUrl = "https://send.api.mailtrap.io/api/send";
            const string ApiToken = "YOUR TOKEN HERE";

            IEMailMessageSender sender = new MailtrapEMailMessageSender(ApiBaseUrl, ApiToken);
            EMail.RegisterSender("mailtrap", sender, useAsDeafult:true);

            var result = EMail.SendText(new EMailAddress("from@contoso.org"), "EMail subject", "EMail text", null, new EMailAddress("mailtrap@demomailtrap.com", "Tester"));

For more usage scenarios please look at EMailTests.cs in EMailSenderTests project.

Remarks : Multiple sender support was not implemented in this version. 
