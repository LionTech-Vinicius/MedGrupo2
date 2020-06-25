using System;

namespace CursoEmTdd.Dominio
{

  public class Curso
  {
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double CargaHoraria { get; private set; }
    public double Valor { get; private set; }

    public Curso(string nome, string descricao, PublicoAlvo publicoAlvo, double cargaHoraria, double valor)
    {
      if (string.IsNullOrEmpty(nome))
        throw new ArgumentException();

      if (cargaHoraria < 50)
        throw new ArgumentException();

      if (cargaHoraria > 250)
        throw new ArgumentException();

      if (valor < 1)
        throw new ArgumentException();

      Nome = nome;
      Descricao = descricao;
      PublicoAlvo = publicoAlvo;
      CargaHoraria = cargaHoraria;
      Valor = valor;
    }
  }

  public enum PublicoAlvo
  {
    Estudante,
    Universitário,
    Empreendedor,
    Empregado,
    Desempregado
  }
}