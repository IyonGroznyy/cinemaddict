namespace Cinemaddict.ViewModels
{
    public class NewDetailViewModel : BaseViewModel
    {
        private string title;
        private string description;
        public int Id { get; set; }

        public string TitleText
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
    }
}
