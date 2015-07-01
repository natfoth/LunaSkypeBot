using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunaSkypeBot.Configuration;
using LunaSkypeBot.Database.Entity;

namespace LunaSkypeBot.Database
{
    public static class DatabaseManager
    { 
        public static string GetRandomImageSearch(string terms)
        {
            var derpyImages = new List<DerpyImage>();
            DerpyImage derpyImage;

            var searchTags = terms.ToLowerInvariant().Split(',').ToList();

           /* for (int i = searchTags.Count - 1; i >= 0; i--)
            {
                searchTags[i] = searchTags[i] + ",";
            }*/

            if (ConfigManager.Config.SFWOnly)
            {
                if (!searchTags.Contains("safe"))
                    searchTags.Add(", safe,");
            }

            using (var db = new DerpyDbContext())
            {
                derpyImages = db.DerpyImages.Where(c => searchTags.All(tag => c.Tags.Contains(tag)) && c.Score >= ConfigManager.Config.MinSearchScore).ToList();

               /* var sql = "SELECT * FROM DerpyImages WHERE Id = 1";

                var queryManual = db.DerpyImages.SqlQuery(sql).FirstOrDefault();

                var query =
                    from x in db.DerpyImages
                    orderby Guid.NewGuid()
                    select new
                    {
                        bool1 = searchTags.All(tag => x.Tags.Contains(tag)),
                        bool2 = x.Score >= ConfigManager.Config.MinSearchScore
                    };

                Debug.WriteLine(query);

                
                 
                derpyImage =
                    db.DerpyImages.Where(
                        c =>
                            searchTags.All(tag => c.Tags.Contains(tag)) &&
                            c.Score >= ConfigManager.Config.MinSearchScore)
                        .OrderBy(x => Guid.NewGuid())
                        .FirstOrDefault();

                int bob = 1;*/
            }

            if (ConfigManager.Config.SFWOnly)
            {
                derpyImages = derpyImages.Where(c => !c.Tags.Contains("explicit") && !c.Tags.Contains("suggestive") && !c.Tags.Contains("grimdark") && !c.Tags.Contains("bdsm")).ToList();
            }

            // derpyImages = derpyImages.Where(search => !AlreadyContainsHash.Contains(search.DerpyImageId)).ToList();


                if (derpyImages.Count == 0)
            {
                return "";
            }


            /*if (Configuration.Instance.ImageWidth > 0)
                derpyImages = derpyImages.Where(search => search.Width >= Configuration.Instance.ImageWidth).ToList();

            if (Configuration.Instance.ImageHeight > 0)
                derpyImages = derpyImages.Where(search => search.Height >= Configuration.Instance.ImageHeight).ToList();


            if (Configuration.Instance.MinRating > 0)
                derpyImages = derpyImages.Where(search => search.Score >= Configuration.Instance.MinRating).ToList();


            switch (Configuration.Instance.OrderingSelectedIndex)
            {
                case 0: // ratings
                    derpyImages = derpyImages.OrderByDescending(search => search.Score).ToList();
                    break;
                case 1: // newest
                    derpyImages = derpyImages.OrderByDescending(search => search.DerpyImageId).ToList();
                    break;
                case 2: // oldest
                    derpyImages = derpyImages.OrderBy(search => search.DerpyImageId).ToList();
                    break;
                case 3: // Favorites
                    derpyImages = derpyImages.OrderByDescending(search => search.Favourites).ToList();
                    break;
                case 4: // Random
                    derpyImages.Shuffle();
                    break;

            }

            //restrict the images last
            if (Configuration.Instance.NumOfPicsTotal != 0)
            {
                while (derpyImages.Count > Configuration.Instance.NumOfPicsTotal)
                {
                    derpyImages.RemoveAt(derpyImages.Count - 1);
                }
            }*/


            var random = new LunaRandom();

            var randomImage = derpyImages[random.Next(derpyImages.Count)];


            return randomImage.GetImageUri().ToString();
        }
    }
}
