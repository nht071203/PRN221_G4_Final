using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Models.DTO
{
    public class PostDTO
    {
        public Post post { get; set; }
        public IEnumerable<PostImage> postImages { get; set; }
        public Account? account { get; set; }

        public PostDTO() { }

        public PostDTO(Post post, IEnumerable<PostImage> postImages, Account? account)
        {
            this.post = post;
            this.postImages = postImages;
            this.account = account;
        }
    }
}
