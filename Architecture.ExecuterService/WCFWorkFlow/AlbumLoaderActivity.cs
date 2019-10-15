using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WCFWorkFlow
{

    public sealed class AlbumLoaderActivity : CodeActivity<Album>
    {
        // Define an activity input argument of type string
        public InArgument<Int32> ActivityAlbumId { get; set; }
         
        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override Album Execute(CodeActivityContext context)
        {
            return new Album
            {
                Id = ActivityAlbumId.Get(context),
                Name="Benim Türkülerim",
                Tracks =new List<Track>
                {
                    new Track {Id=10,Title="Part 1" },
                    new Track {Id=12,Title="Part 2" }
                }
            };
        }
    }
}
