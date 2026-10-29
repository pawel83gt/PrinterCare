using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PrinterCare.Server.Exceptions;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception ex, CancellationToken ct)
    {
        var (status, title, detail) = ex switch
        {
            // удаление записи, на которую ссылаются другие (наш случай с филиалами)
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation } }
                => (409, "Conflict", "Нельзя удалить: у записи есть связанные данные (например, филиалы)."),

            // сработал уникальный индекс (дубль имени)
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.UniqueViolation } }
                => (409, "Conflict", "Запись с такими данными уже существует."),

            _ => (500, "Server error", "Произошла непредвиденная ошибка.")
        };

        if (status == 500)
            logger.LogError(ex, "Unhandled exception");

        context.Response.StatusCode = status;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = { Status = status, Title = title, Detail = detail }
        });
    }
}