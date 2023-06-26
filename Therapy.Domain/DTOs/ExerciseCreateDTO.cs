namespace Therapy.Domain.DTOs {
    public class ExerciseCreateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<MediaCreateDTO> Media { get; set; }
    }
}