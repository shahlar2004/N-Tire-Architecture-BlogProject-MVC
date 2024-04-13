namespace Lesson.Web.ResultMessages
{
    public static class Messages
    {
        public static class Article
        {

            public static string Message(string mes)
            {
                return $"{mes}";
            }

            public static string Add(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla əlavə edildi.";
            }

            public static string Update(string articleTitle)
            {
                return $"{articleTitle} başlıqlı məqalə uğurla dəyişdirildi.";
            }
        }

        public static class  Category
        {
            public static string Add(string categoryTitle)
            {
                return $"{categoryTitle} başlıqlı kateqoriya uğurla əlavə edildi.";
            }

            public static string Update(string categoryTitle)
            {
                return $"{categoryTitle} Adlı kateqoriya uğurla dəyişdirildi.";
            }

            public static string Delete(string categoryTitle)
            {
                return $"{categoryTitle} Adlı kateqoriya uğurla dəyişdirildi.";
            }

            public static string Message(string mes)
            {
                return $"{mes}";
            }

        }


        public static class User
        {

            public static string Message(string mes)
            {
                return $"{mes}";
            }
            public static string Add(string email)
            {
                return $"{email} adlı istifadəçi  uğurla əlavə edildi.";
            }

            public static string Update(string email)
            {
                return $"{email} adlı istifadəçi  uğurla dəyişdirildi.";
            }

            public static string Delete(string email)
            {
                return $"{email} adlı istifadəçinin passwordu uğurla silindi.";
            }
        }
    }
}
