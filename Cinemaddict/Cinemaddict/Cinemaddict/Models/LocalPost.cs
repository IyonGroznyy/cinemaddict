using System;
using System.Collections.Generic;
using System.Text;

namespace Cinemaddict.Models
{
    public class LocalPost : Item
    {
        public string AuthorDisplayName { get; set; }
        public string AuthorPhotoUri { get; set; }
        public int AuthorId { get; set; }

        public LocalPost()
        {

        }

        public LocalPost(User author)
        {
            AuthorDisplayName = author.DisplayName;
            AuthorPhotoUri = author.PhotoUri;
            AuthorId = (int)author.Id;
        }

    }
}
