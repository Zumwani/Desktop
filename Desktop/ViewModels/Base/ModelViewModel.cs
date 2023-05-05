namespace Desktop.ViewModels.Helpers;

public abstract class ModelViewModel<T> : ViewModel
{

    public ModelViewModel(T model) => Model = model;
    public T Model { get; set; }

}