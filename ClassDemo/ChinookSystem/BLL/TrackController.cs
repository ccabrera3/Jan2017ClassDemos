using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Enitities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, int argid)
        {
            using (var context = new ChinookContext())
            {
                List<TrackList> results = null;
                switch (tracksby)
                {
                    case "Artist":
                        {
                            results = (from x in context.Tracks
                                      orderby x.Name
                                      where x.Album.ArtistId == argid
                                      select new TrackList
                                      {
                                          TrackID = x.TrackId,
                                          Name = x.Name,
                                          Title = x.Album.Title,
                                          MediaName = x.MediaType.Name,
                                          GenreName = x.Genre.Name,
                                          Composer = x.Composer,
                                          Milliseconds = x.Milliseconds,
                                          Bytes = x.Bytes,
                                          UnitPrice = x.UnitPrice
                                      }).ToList();
                        break;
                        }
                    case "MediaType":
                        {
                            results = (from x in context.Tracks
                                       orderby x.Name
                                       where x.MediaType.MediaTypeId == argid
                                       select new TrackList
                                       {
                                           TrackID = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                    case "Genre":
                        {
                            results = (from x in context.Tracks
                                       orderby x.Name
                                       where x.Genre.GenreId == argid
                                       select new TrackList
                                       {
                                           TrackID = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                    default:
                        {
                            results = (from x in context.Tracks
                                       orderby x.Name
                                       where x.AlbumId == argid
                                       select new TrackList
                                       {
                                           TrackID = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                }//eos
                return results;
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<GenreAlbumReport> GenreAlbumReport_Get()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Tracks
                              select new GenreAlbumReport()
                              {
                                  GenreName = x.Genre.Name,
                                  AlbumTitle = x.Album.Title,
                                  TrackName = x.Name,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                return results.ToList();
            }
        }//eom
    }//eoc
}
