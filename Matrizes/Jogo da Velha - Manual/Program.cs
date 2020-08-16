using System;

namespace tictactoe_manual
{
  class Program
  {
    static void Main(string[] args)
    {
      string entradaUsuario = "";

      Console.WriteLine("Bem-vindo ao Jogo da Velha");
      Game jogo = new Game();

      Console.Write("Digite o nome do Jogador Nº 1: ");
      entradaUsuario = Console.ReadLine();

      Game.Jogador jogador1 = new Game.Jogador(
          Game.Jogador.Id.Jogador1,
          entradaUsuario,
          Game.Jogador.Jogador1Simbolo);

      Console.Write("Digite o nome do Jogador Nº 2: ");
      entradaUsuario = Console.ReadLine();

      Game.Jogador jogador2 = new Game.Jogador(
          Game.Jogador.Id.Jogador2,
          entradaUsuario,
          Game.Jogador.Jogador2Simbolo);

      bool querJogar = true;
      while (querJogar)
      {
        int resultadoJogo = -1;
        while (jogo.HaJogadas())
        {
          Console.Clear();
          ImprimeTabuleiro(jogador1, jogador2, jogo.VerTabuleiro());
          Console.WriteLine();

          Game.Jogador jogadorAtual = new Game.Jogador(
              (jogo.VezJogador1() ? jogador1.id : jogador2.id),
              (jogo.VezJogador1() ? jogador1.nome : jogador2.nome),
              (jogo.VezJogador1() ? jogador1.simbolo : jogador2.simbolo)
          );
          Console.WriteLine(
              "Agora é a vez do Jogador N° " + (jogo.VezJogador1() ? "1" : "2")
              + " que tem o símbolo: {0}", jogadorAtual.simbolo);

          bool jogadaValida = false;
          int coluna;
          int linha;
          do
          {
            coluna = ValidaEntradaUsuario("Em qual coluna você deseja jogar " + jogadorAtual.nome + "? ");
            linha = ValidaEntradaUsuario("Em qual linha você deseja jogar " + jogadorAtual.nome + "? ");

            if (jogo.CasaEstaOcupada(coluna, linha))
              Console.WriteLine("\nA casa já está ocupada.");
            else
              jogadaValida = true;
          } while (!jogadaValida);


          jogo.EfetuarJogada(new int[] { coluna, linha });
          resultadoJogo = jogo.ProcuraGanhador();
        }

        Console.Clear();
        ImprimeTabuleiro(jogador1, jogador2, jogo.VerTabuleiro());
        Console.WriteLine();
        if (resultadoJogo == (int)jogador1.id)
        {
          Console.WriteLine("Parabéns {0}, você ganhou!", jogador1.nome);
          Console.WriteLine("Você era o jogador de Nº {0} e usava o símbolo: {1}", (int)jogador1.id, jogador1.simbolo);
        }
        else if (resultadoJogo == (int)jogador2.id)
        {
          Console.WriteLine("Parabéns {0}, você ganhou!", jogador2.nome);
          Console.WriteLine("Você era o jogador de Nº {0} e usava o símbolo: {1}", (int)jogador2.id, jogador2.simbolo);
        }
        else
          Console.WriteLine("Houve um empate!");

        bool entradaUsuarioCorreta = false;
        do
        {
          Console.WriteLine("Quer jogar novamente? (S/n)");
          string entrada = Console.ReadLine();
          if (entrada.ToLower() == "n")
          {
            entradaUsuarioCorreta = true;
            querJogar = false;
          }
          else if (entrada.ToUpper() == "S")
          {
            entradaUsuarioCorreta = true;
            jogo.ResetaTabuleiro();
          }
        } while (!entradaUsuarioCorreta);
      }
    }

    static int ValidaEntradaUsuario(string fraseExibicao)
    {
      int numeroEntradaUsuario = -1;
      do
      {
        Console.Write(fraseExibicao);
        string entradaUsuario = Console.ReadLine();

        try
        {
          int entradaConvertidaNumero = int.Parse(entradaUsuario);

          if (entradaConvertidaNumero < 1 || entradaConvertidaNumero > 3)
            throw new ArgumentException();
          else
            numeroEntradaUsuario = entradaConvertidaNumero - 1;

        }
        catch (FormatException)
        {
          Console.WriteLine("\nDigite apenas números.");
        }
        catch (ArgumentException)
        {
          Console.WriteLine("\nDigite um número de 1 a 3");
        }
      } while (numeroEntradaUsuario == -1);

      return numeroEntradaUsuario;
    }

    static void ImprimeTabuleiro(
        Game.Jogador jogador1, Game.Jogador jogador2, byte[,] tabuleiro)
    {
      Console.WriteLine("    1   2   3");
      for (int i = 0; i < tabuleiro.GetLength(0); i++)
      {
        Console.Write("{0} ", i + 1);
        for (int j = 0; j < tabuleiro.GetLength(1); j++)
        {
          byte idJogador = tabuleiro[j, i];
          char simboloParaImprimir = ' ';
          if (idJogador == (byte)jogador1.id)
            simboloParaImprimir = jogador1.simbolo;
          else if (idJogador == (byte)jogador2.id)
            simboloParaImprimir = jogador2.simbolo;

          Console.Write(
              j == 0 ? "| {0} |" : " {0} |",
              simboloParaImprimir);
        }
        Console.WriteLine();
      }
    }
  }

  class Game
  {
    private byte[,] tabuleiro = new byte[3, 3];
    private bool haJogadas = true;
    private bool vezJogador1 = true;

    public void EfetuarJogada(int[] posicao)
    {
      if (posicao[0] >= tabuleiro.GetLength(0) || posicao[1] >= tabuleiro.GetLength(1))
      {
        throw new ArgumentException("Valores não corresponde a nenhuma casa do tabuleiro");
      }

      tabuleiro[posicao[0], posicao[1]] = (byte)(vezJogador1 ? Jogador.Id.Jogador1 : Jogador.Id.Jogador2);
      vezJogador1 = !vezJogador1;
    }

    public void ResetaTabuleiro()
    {
      for (int i = 0; i < tabuleiro.GetLength(0); i++)
      {
        for (int j = 0; j < tabuleiro.GetLength(0); j++)
        {
          tabuleiro[i, j] = 0;
        }
      }
      haJogadas = true;
      vezJogador1 = true;
    }

    public int ProcuraGanhador()
    {
      // colunas
      if (tabuleiro[0, 0] == tabuleiro[0, 1] && tabuleiro[0, 0] == tabuleiro[0, 2]
          && tabuleiro[0, 0] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 0];
      }
      if (tabuleiro[1, 0] == tabuleiro[1, 1] && tabuleiro[1, 0] == tabuleiro[1, 2]
          && tabuleiro[1, 0] != 0)
      {
        haJogadas = false;
        return tabuleiro[1, 0];
      }
      if (tabuleiro[2, 0] == tabuleiro[2, 1] && tabuleiro[2, 0] == tabuleiro[2, 2]
          && tabuleiro[2, 0] != 0)
      {
        haJogadas = false;
        return tabuleiro[2, 0];
      }

      //linhas
      if (tabuleiro[0, 0] == tabuleiro[1, 0] && tabuleiro[0, 0] == tabuleiro[2, 0]
          && tabuleiro[0, 0] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 0];
      }
      if (tabuleiro[0, 1] == tabuleiro[1, 1] && tabuleiro[0, 1] == tabuleiro[2, 1]
          && tabuleiro[0, 1] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 1];
      }
      if (tabuleiro[0, 2] == tabuleiro[1, 2] && tabuleiro[0, 2] == tabuleiro[2, 2]
          && tabuleiro[0, 2] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 2];
      }

      // diagonais
      if (tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[0, 0] == tabuleiro[2, 2]
          && tabuleiro[0, 0] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 0];
      }
      if (tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[0, 2] == tabuleiro[2, 0]
          && tabuleiro[0, 2] != 0)
      {
        haJogadas = false;
        return tabuleiro[0, 2];
      }

      bool haCasasVazias = false;
      for (int i = 0; i < tabuleiro.GetLength(0); i++)
      {
        for (int j = 0; j < tabuleiro.GetLength(0); j++)
        {
          if (tabuleiro[i, j] == 0)
            haCasasVazias = true;
        }
      }

      if (!haCasasVazias)
      {
        haJogadas = false;
        return -1;
      }
      else
        return -1;
    }

    public bool CasaEstaOcupada(int coluna, int linha)
    {
      if (tabuleiro[coluna, linha] == 0)
        return false;
      else
        return true;
    }

    public byte[,] VerTabuleiro()
    {
      return tabuleiro;
    }

    public bool HaJogadas()
    {
      return haJogadas;
    }

    public bool VezJogador1()
    {
      return vezJogador1;
    }

    public class Jogador
    {
      public enum Id : byte
      {
        Jogador1 = 0b1,
        Jogador2 = 0b10
      }

      public const char Jogador1Simbolo = 'X';
      public const char Jogador2Simbolo = 'O';

      public Id id { get; }
      public string nome { get; }
      public char simbolo { get; }

      public Jogador(Id idJogador, string nome, char simbolo)
      {
        this.id = idJogador;

        if (nome.Trim() == String.Empty)
          throw new ArgumentException("O nome do jogador é obrigatório.");
        this.nome = nome;
        if (!(simbolo == Jogador1Simbolo || simbolo == Jogador2Simbolo))
          throw new ArgumentException("Símbolo inválido.");
        this.simbolo = simbolo;
      }
    }
  }
}
