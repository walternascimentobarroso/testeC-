
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 

public class Pilha 
{ 
    private int[] valores; 
    private int topo; 

    /* 
     * Método construtor
     * Apartir daqui será iniciado a pilha
     */
    public Pilha(int n) 
    { 
        if (n > 0) 
        { 
            valores = new int[n]; 
            topo = -1; 
        } 
    } 

    /* 
     * Método para empilhar 
     * Com este método o valor de entrada (int valor) entra no topo da pilha
     */
    public int Push(int valor)  
    { 
        if (topo < valores.Length - 1) 
        { 
            topo++; 
            valores[topo] = valor; 
            return 0; 
        } 
        return -1; 
    } 

    /* 
     * Método para desempilhar 
     * Remove o elemento do topo
     */ 
    public int Pop()
    { 
        if (topo >= 0) 
        { 
            int valor = valores[topo]; 
            topo--; 
            return valor; 
        } 
        return -1;
    } 

    /* 
     * Método para imprimir a pilha 
     */
    public string ImprimirPilha()    
    { 
        string saida = "\t"; 
        if (topo >= 0) 
        { 
            for (int i = topo; i >= 0; i--) 
            { 
                saida = saida + valores[i] + "\n\t"; 
            } 
            return saida; 
        } 
        return "\tPilha Vazia"; 
    } 
} 

namespace PilhaMain 
{ 
   class Program 
   { 
       static void Main(string[] args) 
       { 
           Pilha pilha = new Pilha(5); 
           int sair = 0; 
           string imprime = ""; 
           while (sair == 0) 
           { 
               imprimeOpcoes(); 
               int opcao = int.Parse(Console.ReadLine()); 
               if (opcao == 0) 
               { 
                   sair = 1; 
               } 
               else 
                   if (opcao == 1) 
                   { 
                       Console.Clear(); 
                       Console.WriteLine("Digite um numero para inserir na pilha\n"); 
                       pilha.Push(int.Parse(Console.ReadLine())); 
                       imprime = pilha.ImprimirPilha(); 
                       Console.WriteLine(imprime); 
                   } 
                   else 
                       if (opcao == 2) 
                       { 
                           Console.Clear(); 
                           pilha.Pop(); 
                           imprime = pilha.ImprimirPilha(); 
                           Console.WriteLine(imprime); 
                       } 
                       else 
                           if (opcao == 3) 
                           { 
                               Console.Clear(); 
                               imprime = pilha.ImprimirPilha(); 
                               Console.WriteLine(imprime); 
                           } 
           } 
       } 
       static public void imprimeOpcoes() 
       { 
           Console.WriteLine("\nEscolha uma opção:\n"); 
           Console.WriteLine("Sair digite 0"); 
           Console.WriteLine("Inserir na pilha digite 1"); 
           Console.WriteLine("Tirar da pilha digite 2"); 
           Console.WriteLine("Imprimir pilha digite 3\n"); 
       } 
   } 
}
