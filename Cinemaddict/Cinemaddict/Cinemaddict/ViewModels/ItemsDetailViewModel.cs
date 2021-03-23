using Cinemaddict.Models;
using System.Threading.Tasks;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    public class ItemsDetailViewModel : BaseViewModel
    {
        private string title;
        private string description;
        private string uri;
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

        public string Uri
        {
            get => uri;
            set => SetProperty(ref uri, value);
        }

       public async Task SavePost(string pTitleEditor, string pDescriptionEditor)
       {
            await new FirebaseHelper().UpdatePost(new Post()
            {
                Id = Id,
                TitleText = pTitleEditor,
                Description = pDescriptionEditor
            });
       }
    }
}
