namespace Consulting.Auth.Application.Helpers;

public static class EmailTemplates
{
    public static string GetValidateEmailTemplate(string email, string confirmationLink)
    {
        return $@"
        <html>
        <body style='font-family: Arial, sans-serif; text-align: center; padding: 20px; background-color: #f9f9f9;'>
            <div style='max-width: 500px; margin: auto; padding: 20px; background: #ffffff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Вітаємо, {email}!</h2>
                <p>Ви успішно створили обліковий запис у <strong>Consulting Platform</strong>. Щоб підтвердити свою електронну адресу та завершити реєстрацію, натисніть кнопку нижче:</p>
                <a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; margin: 20px 0; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px; font-size: 16px;'>Підтвердити Email</a>
                <p>Якщо кнопка не працює, скопіюйте це посилання та вставте у свій браузер:</p>
                <p style='word-wrap: break-word; font-size: 14px; color: #555;'>{confirmationLink}</p>
                <hr style='margin: 20px 0; border: none; border-top: 1px solid #ddd;'>
                <p style='font-size: 12px; color: #888;'>Якщо ви не реєструвалися в Consulting Platform, просто ігноруйте цей лист.</p>
                <p style='font-size: 12px; color: #888;'>З найкращими побажаннями, <br><strong>Команда Consulting Platform</strong></p>
            </div>
        </body>
        </html>";
    }

    public static string GetResetPasswordTemplate(string email, string confirmationLink)
    {
        return $@"
        <html>
        <body style='font-family: Arial, sans-serif; text-align: center; padding: 20px; background-color: #f9f9f9;'>
            <div style='max-width: 500px; margin: auto; padding: 20px; background: #ffffff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Вітаємо, {email}!</h2>
                <p>Ви створили запит на зміну пароля у <strong>Consulting Platform</strong>. Для продовження операції зміни пароля натисніть кнопку нижче:</p>
                <a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; margin: 20px 0; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px; font-size: 16px;'>Змінити пароль</a>
                <hr style='margin: 20px 0; border: none; border-top: 1px solid #ddd;'>
                <p style='font-size: 12px; color: #888;'>Якщо ви не створювали заявку на зміну пароля в Consulting Platform, просто ігноруйте цей лист.</p>
                <p style='font-size: 12px; color: #888;'>З найкращими побажаннями, <br><strong>Команда Consulting Platform</strong></p>
            </div>
        </body>
        </html>";
    }
}
