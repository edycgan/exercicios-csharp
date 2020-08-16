using System;

namespace teatro_alg
{
  class Program
  {
    static void Main(string[] args)
    {
      int qtdeSecoes = 0, qtdeFileiras = 0, qtdeCadeiras = 0;
      int numTotalReservas, numMaxReservas;

      while (qtdeSecoes <= 0)
      {
        Console.Write("Digite a quantidade de seções: ");

        try
        {
          qtdeSecoes = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
          Console.WriteLine("Digite apenas números.");
          continue;
        }
      }
      while (qtdeFileiras <= 0)
      {
        Console.Write("Digite a quantidade de fileiras em uma seção: ");

        try
        {
          qtdeFileiras = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
          Console.WriteLine("Digite apenas números.");
          continue;
        }
      }
      while (qtdeCadeiras <= 0)
      {
        Console.Write("Digite a quantidade de cadeiras em uma fileira: ");

        try
        {
          qtdeCadeiras = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
          Console.WriteLine("Digite apenas números.");
          continue;
        }
      }

      numTotalReservas = qtdeSecoes * qtdeFileiras * qtdeCadeiras;
      numMaxReservas = (numTotalReservas / 5) * 4; // 4/5 da capacidade total do teatro

      string[,,] reservas = new string[qtdeSecoes, qtdeFileiras, qtdeCadeiras];

      bool fezReservas = false;
      int numReservasFeitas = 0;
      while (!fezReservas)
      {
        if (numReservasFeitas == numMaxReservas)
        {
          Console.WriteLine("Não é possível fazer mais reservas. Teatro cheio.");
          break;
        }

        int secao = -1;
        int fileira = -1;
        int cadeira = -1;

        do
        {
          Console.Write("Digite a seção: ");

          int entradaUsuario;
          try
          {
            entradaUsuario = int.Parse(Console.ReadLine());
          }
          catch (Exception)
          {
            Console.WriteLine("Digite apenas números.");
            continue;
          }

          if (entradaUsuario >= reservas.GetLength(0))
          {
            Console.WriteLine("A seção não existe.");
          }
          else
          {
            secao = entradaUsuario;
          }
        }
        while (secao == -1);

        do
        {
          Console.Write("Digite a fileira: ");

          int entradaUsuario;
          try
          {
            entradaUsuario = int.Parse(Console.ReadLine());
          }
          catch (Exception)
          {
            Console.WriteLine("Digite apenas números.");
            continue;
          }

          if (entradaUsuario >= reservas.GetLength(1))
          {
            Console.WriteLine("A fileira não existe.");
          }
          else
          {
            fileira = entradaUsuario;
          }
        }
        while (fileira == -1);

        do
        {
          Console.Write("Digite a cadeira: ");

          int entradaUsuario;
          try
          {
            entradaUsuario = int.Parse(Console.ReadLine());
          }
          catch (Exception)
          {
            Console.WriteLine("Digite apenas números.");
            continue;
          }

          if (entradaUsuario >= reservas.GetLength(2))
          {
            Console.WriteLine("A cadeira não existe.");
          }
          else
          {
            cadeira = entradaUsuario;
          }
        }
        while (cadeira == -1);

        if (reservas[secao, fileira, cadeira] != null)
        {
          Console.WriteLine("A cadeira escolhida está ocupada. Por favor, selecione outra.");
          secao = -1;
          fileira = -1;
          cadeira = -1;
          continue;
        }
        else//୧ʕ•̀ᴥ•́ʔ୨ 
        {
          Console.Write("Digite seu nome: ");
          reservas[secao, fileira, cadeira] = Console.ReadLine();

          numReservasFeitas++;

          bool opcaoValida = false;
          while (!opcaoValida)
          {
            Console.Write("Mais alguém para adicionar? (Digite 0 para NÃO e 1 para SIM): ");

            string entradaUsuario = Console.ReadLine();
            if (entradaUsuario == "0")
            {
              fezReservas = true;
              opcaoValida = true;
            }
            else if (entradaUsuario == "1")
            {
              opcaoValida = true;
            }
            else
            {
              Console.WriteLine("Opção inválida.");
            }
          }
        }
      }

      Console.WriteLine("\nLugares Ocupados");
      for (int secoes = 0; secoes < reservas.GetLength(0); secoes++)
      {
        for (int fileiras = 0; fileiras < reservas.GetLength(1); fileiras++)
        {
          for (int cadeiras = 0; cadeiras < reservas.GetLength(2); cadeiras++)
          {
            if (reservas[secoes, fileiras, cadeiras] == null)
            {
              Console.Write("LIVRE    "); // 9 caracteres
            }
            else
            {
              string nome = reservas[secoes, fileiras, cadeiras];
              if (nome.Length > 8)
              {
                Console.Write(nome.Substring(0, 8).PadRight(9));
              }
              else
              {
                Console.Write(nome.PadRight(9));
              }
            }
          }
          Console.WriteLine("");
        }
        Console.WriteLine("");
      }
    }
  }
}