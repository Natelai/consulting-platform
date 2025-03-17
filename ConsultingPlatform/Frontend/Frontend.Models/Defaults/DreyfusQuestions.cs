using Frontend.Models.Dtos;

namespace Frontend.Models.Defaults;

public static class DreyfusQuestions
{
    public static DreyfusQuestion Question1 = new()
    {
        Question = "Переживаєте за успіх в роботі?",
        QuestionRates = new()
        {
            { "Сильно", 5 },
            { "Не дуже", 3 },
            { "Спокійний", 2 },
        }
    };

    public static DreyfusQuestion Question2 = new()
    {
        Question = "Прагнете досягти швидко результату?",
        QuestionRates = new()
        {
            { "Поступово", 2 },
            { "Якомога швидше", 3 },
            { "Дуже", 5 }
        }
    };

    public static DreyfusQuestion Question3 = new()
    {
        Question = "Легко попадаєте в тупик при проблемах в роботі?",
        QuestionRates = new()
        {
            { "Неодмінно", 5 },
            { "Поступово", 3 },
            { "Зрідка", 2 }
        }
    };

    public static DreyfusQuestion Question4 = new()
    {
        Question = "Чи потрібен чіткий алгоритм для вирішення задач?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "В окремих випадках", 3 },
            { "Не потрібен", 2 }
        }
    };

    public static DreyfusQuestion Question5 = new()
    {
        Question = "Чи використовуєте власний досвід при вирішенні задач?",
        QuestionRates = new()
        {
            { "Зрідка", 5 },
            { "Частково", 3 },
            { "Ні", 2 }
        }
    };

    public static DreyfusQuestion Question6 = new()
    {
        Question = "Чи користуєтесь фіксованими правилами для вирішення задач?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "В окремих випадках", 3 },
            { "Ні", 2 }
        }
    };

    public static DreyfusQuestion Question7 = new()
    {
        Question = "Чи відчуваєте ви загальний контекст вирішення задачі?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Частково", 3 },
            { "В окремих випадках", 2 }
        }
    };

    public static DreyfusQuestion Question8 = new()
    {
        Question = "Чи можете ви побудувати модель вирішуваної задачі?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Не повністю", 3 },
            { "В окремих випадках", 2 }
        }
    };

    public static DreyfusQuestion Question9 = new()
    {
        Question = "Чи вистачає вам ініціативи при вирішенні задач?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Зрідка", 3 },
            { "Потрібне натхнення", 2 }
        }
    };

    public static DreyfusQuestion Question10 = new()
    {
        Question = "Чи можете вирішувати проблеми, з якими ще не стикались?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "В окремих випадках", 3 },
            { "Ні", 2 }
        }
    };

    public static DreyfusQuestion Question11 = new()
    {
        Question = "Чи необхідний вам весь контекст задачі?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "В окремих деталях", 3 },
            { "В загальному", 2 }
        }
    };

    public static DreyfusQuestion Question12 = new()
    {
        Question = "Чи переглядаєте ви свої наміри до вирішення задачі?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Зрідка", 3 },
            { "Коли є потреба", 2 }
        }
    };

    public static DreyfusQuestion Question13 = new()
    {
        Question = "Чи здатні ви навчатись у інших?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Зрідка", 3 },
            { "Коли є потреба", 2 }
        }
    };

    public static DreyfusQuestion Question14 = new()
    {
        Question = "Чи обираєте ви нові методи своєї роботи?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Вибірково", 3 },
            { "Вистачає досвіду", 2 }
        }
    };

    public static DreyfusQuestion Question15 = new()
    {
        Question = "Чи допомагає власна інтуїція при вирішенні задач?",
        QuestionRates = new()
        {
            { "Так", 5 },
            { "Частково", 3 },
            { "При емоційному напруженні", 2 }
        }
    };

    public static DreyfusQuestion Question16 = new()
    {
        Question = "Чи застосовуєте рішення задач за аналогією?",
        QuestionRates = new()
        {
            { "Часто", 5 },
            { "Зрідка", 3 },
            { "Тільки власний варіант", 2 }
        }
    };
}