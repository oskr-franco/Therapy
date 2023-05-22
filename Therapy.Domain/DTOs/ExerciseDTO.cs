namespace Therapy.Domain.DTOs {
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<ExerciseMediaDto> Media { get; set; }
    }
}