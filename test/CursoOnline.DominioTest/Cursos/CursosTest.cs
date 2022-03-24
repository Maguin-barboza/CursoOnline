using System;

using ExpectedObjects;
using Xunit;

using CursoOnline.DominioTest._Utils;
using CursoOnline.DominioTest._Builders;
using Bogus;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursosTest: IDisposable
    {
        private readonly string _nome;
        private readonly string _descricao;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursosTest()
        {
            Faker faker = new Faker();

            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(50, 100);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveConterNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome Inválido.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveConterCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horária inválida.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveConterValorMenorQue1(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                    CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido.");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitário,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome Inválido.");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida.");

            if (valor < 1)
                throw new ArgumentException("Valor inválido.");

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição inválida.");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}





