using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Service
{
    public static class ServiceProvider
    {
        static INotificationService NotificationService=new NotificationService();
        static IQuestionService QuestionService = new QuestionService();

        public static INotificationService GetNotificationService()
        {
            return NotificationService;
        }
        public static IQuestionService GetQuestionService()
        {
            return QuestionService;
        }
    }
}
