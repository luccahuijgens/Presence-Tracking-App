using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Service
{
    public static class ServiceProvider
    {
        static NotificationService NotificationService=new NotificationService();
        static QuestionService QuestionService = new QuestionService();
        static ConfigService ConfigService = new ConfigService();

        public static NotificationService GetNotificationService()
        {
            return NotificationService;
        }
        public static QuestionService GetQuestionService()
        {
            return QuestionService;
        }
        public static ConfigService GetConfigService()
        {
            return ConfigService;
        }
    }
}
