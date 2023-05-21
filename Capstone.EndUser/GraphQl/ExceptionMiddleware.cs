namespace Capstone.EndUser.GraphQl;

using HotChocolate.Resolvers;

public class ExceptionMiddleware
{
    private readonly FieldDelegate _next;

    public ExceptionMiddleware(FieldDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(IMiddlewareContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DException ex) when (SetResult(context, ex.Message))
        {
        }
    }

    private bool SetResult(IMiddlewareContext context, string error)
    {
        Type type = context.Selection.Field.Type.NamedType().ToRuntimeType();
        if (type.IsSubclassOf(typeof(Payload)))
        {
            context.Result = (Payload)Activator.CreateInstance(type, null, error)!;
            return true;
        }

        return false;
    }
}

public class DException : Exception
{
    public DException()
    {
    }

    public DException(string? message) : base(message)
    {
    }

    public DException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}