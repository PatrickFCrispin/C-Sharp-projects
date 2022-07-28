using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiAluno.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAluno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        static List<Aluno> alunos = new List<Aluno>();

        // GET: api/<AlunosController>
        [HttpGet]
        public IEnumerable<Aluno> Get()
        {
            return alunos;
        }

        // GET api/<AlunosController>/5
        [HttpGet("{matricula}")]
        public string Get(int matricula)
        {
            var aluno = alunos.FirstOrDefault(a => a.Matricula == matricula);

            if (aluno == null) return Mensagens.AlunoNaoEncontrado;
            
            return $"Nome: {aluno.Nome}\n" +
                $"Matrícula: {aluno.Matricula}\n" +
                $"Ativo: {aluno.Ativo}";
        }

        // POST api/<AlunosController>
        /*
         * {
            "Nome": "Teste 1",
            "Matricula": 100001,
            "Ativo": true
         * }
         */
        [HttpPost]
        public string Post([FromBody] Aluno novoAluno)
        {
            var aluno = alunos.FirstOrDefault(a => a.Matricula == novoAluno.Matricula);

            if (aluno == null)
            {
                if (alunos.Count == 0)
                {
                    novoAluno.ID = 1;
                }
                else
                {
                    int? id = alunos.Last().ID;
                    id++;
                    novoAluno.ID = id;
                }

                alunos.Add(novoAluno);
                return Mensagens.NovoAluno;
            }

            return Mensagens.MatriculaDuplicada;
        }

        // PUT api/<AlunosController>/5
        /*
         * {
            "Nome": "Teste 1",
            "Ativo": true
         * }
         */
        [HttpPut("{matricula}")]
        public string Put(int matricula, [FromBody] Aluno value)
        {
            var aluno = alunos.FirstOrDefault(a => a.Matricula == matricula);

            if (aluno == null) return Mensagens.AlunoNaoEncontrado;

            aluno.Nome = value.Nome;
            aluno.Ativo = value.Ativo;

            return Mensagens.AlunoAtualizado;
        }

        // DELETE api/<AlunosController>/5
        [HttpDelete("{matricula}")]
        public string Delete(int matricula)
        {
            var aluno = alunos.FirstOrDefault(a => a.Matricula == matricula);

            if (aluno == null) return Mensagens.AlunoNaoEncontrado;

            alunos.Remove(aluno);

            return Mensagens.AlunoRemovido;
        }
    }
}