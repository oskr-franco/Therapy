using Therapy.Domain.DTOs.Media;

namespace Therapy.Domain.DTOs.Exercise {
    public class ExerciseDTO: SlugDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Instructions { get; set; }
        public List<MediaDTO> Media { get; set; }
    }
}