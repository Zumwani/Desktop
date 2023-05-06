namespace Desktop.ViewModels.Helpers;

public abstract class ModelViewModel<T> : ViewModel
{
    public T Model { get; init; } = default!;
}