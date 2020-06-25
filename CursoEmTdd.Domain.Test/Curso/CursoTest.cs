using System;
using Bogus;
using CursoEmTdd.Dominio;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoEmTdd.Domain.Test
{
  // curso não pode ser vazio ok
  // nome não pode ser nulo nem vazio ok
  // valor não pode ser menor que um ok
  // carga horária não pode ser menor que 50 nem superior a 250 ok


  public class CursoTest
  {
    private readonly PublicoAlvo _publicoAlvo;
    private readonly string _nome;
    private readonly string _descricao;
    private readonly double _cargaHoraria;
    private readonly double _valor;
    private ITestOutputHelper _outputHelper;

    public CursoTest(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;

      var faker = new Faker();

      _nome = faker.Random.Word();
      _cargaHoraria = faker.Random.Double(50, 250);
      _publicoAlvo = PublicoAlvo.Estudante;
      _valor = faker.Random.Double(100, 1000); ;
      _descricao = faker.Lorem.Paragraph();

    }

    public void Dispose()
    {
      _outputHelper.WriteLine("Dispose sendo executado!");
    }

    [Fact]
    public void CursoCriarCurso()
    {
      var cursoEsperado = new
      {
        Nome = _nome,
        Descricao = _descricao,
        PublicoAlvo = _publicoAlvo,
        CargaHoraria = _cargaHoraria,
        Valor = _valor
      };

      var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.PublicoAlvo,
        cursoEsperado.CargaHoraria, cursoEsperado.Valor);

      cursoEsperado.ToExpectedObject().ShouldMatch(curso);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
    {
      Assert.Throws<ArgumentException>(() =>
        new Curso(nomeInvalido, _descricao, _publicoAlvo, _cargaHoraria, _valor));
    }

    [Theory]
    [InlineData(45)]
    [InlineData(35)]
    [InlineData(25)]
    [InlineData(15)]
    [InlineData(5)]
    [InlineData(0)]
    [InlineData(-1)]
    public void NaoDeveCargaHorariaSerMenor50(double cargaHorariaInvalida)
    {
      Assert.Throws<ArgumentException>(() =>
        new Curso(_nome, _descricao, _publicoAlvo, cargaHorariaInvalida, _valor));
    }

    [Theory]
    [InlineData(450)]
    [InlineData(350)]
    [InlineData(251)]
    public void NaoDeveCargaHorariaSerMair250(double cargaHorariaInvalida)
    {
      Assert.Throws<ArgumentException>(() =>
        new Curso(_nome, _descricao, _publicoAlvo, cargaHorariaInvalida, _valor));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-1001)]
    [InlineData(0.15)]
    public void NaoDeveCursoTerValorInvalido(double valorInvalido)
    {
      Assert.Throws<ArgumentException>(() =>
        new Curso(_nome, _descricao, _publicoAlvo, _cargaHoraria, valorInvalido));
    }
  }

}