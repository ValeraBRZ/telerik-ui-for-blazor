using System;
using System.ComponentModel.DataAnnotations;
using TelerikBlazorDemos.DataAccess.Models;

namespace TelerikBlazorDemos.DataAccess.Dto
{
    public class PhotoContestDto
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }
        public int Participants { get; set; }

        [StringLength(50)]
        public string PhotoId { get; set; }

        public static Func<PhotoContest, PhotoContestDto> PhotoContestFunc = (photoContest) =>
            new PhotoContestDto
            {
                Id = photoContest.Id,
                Title = photoContest.Title,
                Participants = photoContest.Participants,
                PhotoId = photoContest.PhotoId,
            };
    }
}
