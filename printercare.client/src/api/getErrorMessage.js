export function getErrorMessage(error) {
    // сервер не ответил вообще (упал, нет сети)
    if (!error.response) return 'Сервер недоступен. Попробуйте позже.';

    const data = error.response.data;

    // обычная строка, например Conflict("...") из контроллера
    if (typeof data === 'string' && data) return data;

    // ошибки валидации DTO (400): { errors: { Name: ["..."] } }
    if (data?.errors) return Object.values(data.errors).flat().join(' ');

    // ProblemDetails (появится после глобального обработчика)
    if (data?.detail) return data.detail;
    if (data?.title) return data.title;

    return `Ошибка ${error.response.status}`;
}