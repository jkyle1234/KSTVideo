using KSTVideo.Data;
using KSTVideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTVideo.Services
{
    public class GenreService
    {
        private readonly Guid _userid;

        public GenreService(Guid userid)
        {
            _userid = userid;
        }


        public bool CreateGenre(GenreCreate gen)
        {
            var entity =
                 new Genre()
                 {
                     Name = gen.Name
                     
                 };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Genres.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GenreListItem> GetGenres()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Genres
                        .Select(
                            e =>
                                new GenreListItem
                                {
                                    ID = e.ID,
                                    Name = e.Name
                                }
                        );
                return query.ToList<GenreListItem>();
            }
        }

        public GenreDetail GetGenreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Genres
                        .Single(e => e.ID == id);
                return
                    new GenreDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name
                      
                    };
            }
        }

        public bool UpdateGenre(GenreUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Genres
                        .Single(e => e.ID == model.ID);

                entity.Name = model.Name;
                

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGenre(int genreid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Genres
                        .Single(e => e.ID == genreid);

                ctx.Genres.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
