namespace Therapy.Domain.DTOs {
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<MediaDTO> Media { get; set; }
    }
}