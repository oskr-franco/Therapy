using Therapy.Domain.DTOs.Media;

namespace Therapy.Domain.DTOs.Exercise {
    public class ExerciseCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Instructions { get; set; }
        public List<MediaCreateDTO> Media { get; set; }
    }
}