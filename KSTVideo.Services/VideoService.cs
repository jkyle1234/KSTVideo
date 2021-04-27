using KSTVideo.Data;
using KSTVideo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Services
{
    public class VideoService
    {
        private readonly Guid _userid;

        public VideoService(Guid userid)
        {
            _userid = userid;
        }


        public bool CreateVideo(VideoCreate vid)
        {
            var entity =
                 new Video()
                 {
                     Name = vid.Name,
                     Description = vid.Description,
                     UPCcode = vid.UPCcode,
                     RentalPrice = vid.RentalPrice,
                     GenreID = vid.GenreID
                 };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Videos.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<VideoListItem> GetVideos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Videos
                        .Select(
                            e =>
                                new VideoListItem
                                {
                                    Name = e.Name,
                                    Description = e.Description,
                                    RentalPrice = e.RentalPrice,
                                    UPCcode = e.UPCcode,
                                    ID = e.ID,
                                    Genre = e.Genre
                                    
                                }
                        );
                        
                        

                return query.ToList<VideoListItem>();
            }
        }


        public VideoDetail GetVideoById(int? id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Videos
                        .Single(e => e.ID == id);
                return
                    new VideoDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Description = entity.Description,
                        RentalPrice = entity.RentalPrice,
                        UPCcode = entity.UPCcode,
                        Genre = entity.Genre,
                        GenreID = entity.GenreID
                    };
            }
        }


        public bool UpdateVideo(VideoUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Videos
                        .Single(e => e.ID == model.ID);

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.RentalPrice = model.RentalPrice;
                entity.UPCcode = model.UPCcode;
                entity.GenreID = model.GenreID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVideo(int? videoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Videos
                        .Single(e => e.ID == videoId);

                ctx.Videos.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
