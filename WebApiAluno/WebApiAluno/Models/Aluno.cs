namespace WebApiAluno.Models
{
    public class Aluno
    {
        public int? ID { get; set; }    // Usado apenas para simular com o BD
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public bool Ativo { get; set; }
    }
}